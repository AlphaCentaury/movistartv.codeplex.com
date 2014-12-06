// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006;
using Project.DvbIpTv.DvbStp.Client;
using Project.DvbIpTv.RecorderLauncher.Serialization;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Controls;
using Project.DvbIpTv.UiServices.Discovery;
using Project.DvbIpTv.UiServices.Forms;
using Project.DvbIpTv.UiServices.Forms.Startup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Property = System.Collections.Generic.KeyValuePair<string, string>;

namespace Project.DvbIpTv.ChannelList
{
    public partial class ChannelListForm : Form, ISplashScreenAwareForm
    {
        UiServiceProvider SelectedServiceProvider;
        UiBroadcastDiscovery BroadcastDiscovery;
        UiBroadcastService SelectedBroadcastService;
        MulticastScannerDialog MulticastScanner;
        Font ChannelListTileFont;
        Font ChannelListTileDisabledFont;
        Font ChannelListDetailsFont;
        Font ChannelListDetailsNameItemFont;
        int ManualUpdateLock;
        View CurrentChannelListView;

        public ChannelListForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.IPTV;
        } // constructor

        #region ISplashScreenAwareForm implementation

        public event EventHandler FormLoadCompleted;

        bool ISplashScreenAwareForm.DisposeOnFormClose
        {
            get { return true; }
        } // ISplashScreenAwareForm.DisposeOnFormClose

        #endregion

        #region Form events

        private void ChannelListForm_Load(object sender, EventArgs e)
        {
            this.Text = Properties.Texts.ChannelListFormCaption;

            listViewChannels.TileSize = new Size(225, imageListChannelsLarge.ImageSize.Height + 6);
            ChannelListTileFont = new Font("Tahoma", 10.5f, FontStyle.Bold);
            ChannelListTileDisabledFont = listViewChannels.Font;
            ChannelListDetailsFont = listViewChannels.Font;
            ChannelListDetailsNameItemFont = new Font("Tahoma", 11.0f, FontStyle.Bold);
            ChannelListViewChanged(View.Tile);
            listViewChannels.Sort(0, true);

            // load from cache, if available
            SelectedServiceProvider = SelectProviderDialog.GetLastUserSelectedProvider();
            ServiceProviderChanged();

            // notify Splash Screeen the form has finished loading and is about to be shown
            if (FormLoadCompleted != null)
            {
                FormLoadCompleted(this, e);
            } // if
        }  // ChannelListForm_Load

        private void menuItemDvbAbout_Click(object sender, EventArgs e)
        {
            using (var box = new AboutBox())
            {
                box.ShowDialog(this);
            } // using box
        } // menuItemDvbAbout_Click

        private void menuItemDvbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        } // menuItemDvbExit_Click

        #endregion

        #region Provider-related events

        private void menuItemProviderSelect_Click(object sender, EventArgs e)
        {
            using (var dialog = new SelectProviderDialog())
            {
                dialog.SelectedServiceProvider = SelectedServiceProvider;
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                SelectedServiceProvider = dialog.SelectedServiceProvider;
                ServiceProviderChanged();
            } // dialog
        } // menuItemProviderSelect_Click

        private void menuItemProviderDetails_Click(object sender, EventArgs e)
        {
            if (SelectedServiceProvider == null) return;

            using (var dlg = new PropertiesDialog()
                {
                    Caption = Properties.Texts.SPProperties,
                    Properties = SelectedServiceProvider.DumpProperties(),
                    Description = SelectedServiceProvider.DisplayName,
                    Logo = SelectedServiceProvider.Logo.GetImage(LogoSize.Size64, true),
                })
            {
                dlg.ShowDialog(this);
            } // using
        } // buttonProviderDetails_Click

        #endregion

        #region Service-related events

        private void radioListViewTile_Click(object sender, EventArgs e)
        {
            ChannelListViewChanged(View.Tile);
        } // radioListViewTile_Click

        private void radioListViewDetails_Click(object sender, EventArgs e)
        {
            ChannelListViewChanged(View.Details);
        } // radioListViewDetails_Click

        private void menuItemChannelListViewTile_Click(object sender, EventArgs e)
        {
            ChannelListViewChanged(View.Tile);
        } // menuItemChannelListViewTile_Click

        private void menuItemChannelListViewDetails_Click(object sender, EventArgs e)
        {
            ChannelListViewChanged(View.Details);
        } // menuItemChannelListViewDetails_Click

        private void menuItemChannelListSortName_Click(object sender, EventArgs e)
        {
            listViewChannels.Sort(0, null);
        } // menuItemChannelListSortName_Click

        private void menuItemChannelListSortDescription_Click(object sender, EventArgs e)
        {
            listViewChannels.Sort(1, null);
        } // menuItemChannelListSortDescription_Click

        private void menuItemChannelListSortType_Click(object sender, EventArgs e)
        {
            if (CurrentChannelListView == View.Details)
            {
                listViewChannels.Sort(2, null);
            }
            else
            {
                listViewChannels.Sort(1, null);
            } // if-else
        } // menuItemChannelListSortType_Click

        private void menuItemChannelListSortLocation_Click(object sender, EventArgs e)
        {
            listViewChannels.Sort(3, null);
        } // menuItemChannelListSortLocation_Click

        private void menuItemChannelListSortNone_Click(object sender, EventArgs e)
        {
            listViewChannels.Sort(-1, true);
            FillListViewChannels();
        } // menuItemChannelListSortNone_Click

        private void UpdateSortMenuStatus()
        {
            menuItemChannelListSortName.Checked = (listViewChannels.CurrentSortColumn == 0);
            menuItemChannelListSortLocation.Checked = (listViewChannels.CurrentSortColumn == 3);
            menuItemChannelListSortNone.Checked = (listViewChannels.CurrentSortColumn < 0);

            if (CurrentChannelListView == View.Details)
            {
                menuItemChannelListSortDescription.Checked = (listViewChannels.CurrentSortColumn == 1);
                menuItemChannelListSortType.Checked = (listViewChannels.CurrentSortColumn == 2);
            }
            else
            {
                menuItemChannelListSortDescription.Checked = false;
                menuItemChannelListSortType.Checked = (listViewChannels.CurrentSortColumn == 1);
            } // if-else

            menuItemChannelListSortName.Image = menuItemChannelListSortName.Checked ? (!listViewChannels.CurrentSortIsDescending ? Properties.Resources.SortAscending_16x16 : Properties.Resources.SortDescending_16x16) : null;
            menuItemChannelListSortDescription.Image = menuItemChannelListSortDescription.Checked ? (!listViewChannels.CurrentSortIsDescending ? Properties.Resources.SortAscending_16x16 : Properties.Resources.SortDescending_16x16) : null;
            menuItemChannelListSortType.Image = menuItemChannelListSortType.Checked ? (!listViewChannels.CurrentSortIsDescending ? Properties.Resources.SortAscending_16x16 : Properties.Resources.SortDescending_16x16) : null;
            menuItemChannelListSortLocation.Image = menuItemChannelListSortLocation.Checked ? (!listViewChannels.CurrentSortIsDescending ? Properties.Resources.SortAscending_16x16 : Properties.Resources.SortDescending_16x16) : null;
        } // UpdateSortMenuStatus

        private void ChannelListViewChanged(View newView)
        {
            // avoid events re-entrancy
            if (ManualUpdateLock > 0) return;
            if (CurrentChannelListView == newView) return;

            ManualUpdateLock++;

            // update menu items
            menuItemChannelListSortDescription.Enabled = (newView == View.Details);
            menuItemChannelListSortLocation.Enabled = (newView == View.Details);

            if (((menuItemChannelListSortDescription.Enabled) && (!menuItemChannelListSortLocation.Enabled)) ||
                (((menuItemChannelListSortLocation.Checked) && (!menuItemChannelListSortLocation.Enabled))))
            {
                menuItemChannelListSortName.Checked = true;
            } // if

            // update menu items & radio buttons view selection
            radioListViewTile.Checked = (newView == View.Tile);
            radioListViewDetails.Checked = (newView == View.Details);
            menuItemChannelListViewTile.Checked = (newView == View.Tile);
            menuItemChannelListViewDetails.Checked = (newView == View.Details);

            // preserve sorted column ans sorting direction, if compatible with new view
            // only by name or by type is compatible; in any other case, revert to name and ascending
            var newSortColumn = listViewChannels.CurrentSortColumn;
            var sortIsDescending = listViewChannels.CurrentSortIsDescending;
            if (newSortColumn >= 0)
            {
                if (newView == View.Tile)
                {
                    // changing from Details to Tile view
                    if (newSortColumn == 2) // Column 2 in Details view is Type
                    {
                        // preserve sorted column and sorting direction
                        newSortColumn = 1; // Column 1 in Tile view is Type
                    }
                    else if (newSortColumn != 0) // Column 0 is Name in both views
                    {
                        // in any other case, revert to name and ascending
                        newSortColumn = 0;
                        sortIsDescending = false;
                    } // if-else
                }
                else
                {
                    // changing from Tile to Detials view
                    if (newSortColumn == 1)  // Column 1 in Tile view is Type
                    {
                        newSortColumn = 2; // Column 2 in Details view is Type
                    }
                    else if (newSortColumn > 1)  // Column 0 is Name in both views
                    {
                        // in any other case, revert to name and ascending
                        newSortColumn = 0;
                        sortIsDescending = false;
                    } // if-else
                } // if-else
            } // if

            // save user selected item
            var selectedItemKey = (listViewChannels.SelectedItems.Count == 0) ? null : listViewChannels.SelectedItems[0].Name;

            // set new view and fill new list
            listViewChannels.Sort(-1, true);
            CurrentChannelListView = newView;
            FillListViewChannels();

            // sort new list
            if (newSortColumn >= 0)
            {
                listViewChannels.Sort(newSortColumn, !sortIsDescending);
            } // if

            ManualUpdateLock--;

            // maintain user selected item;
            if (selectedItemKey != null)
            {
                var item = listViewChannels.Items[selectedItemKey];
                item.EnsureVisible();
                item.Selected = true;
            } // if
        } // ChannelListViewChanged

        private void menuItemChannelRefreshList_Click(object sender, EventArgs e)
        {
            LoadBroadcastDiscovery(false);
        } // menuItemChannelRefreshList_Click

        private void menuItemChannelVerify_Click(object sender, EventArgs e)
        {
            int timeout;

            if ((MulticastScanner != null) && (!MulticastScanner.IsDisposed))
            {
                MulticastScanner.Visible = false;
                MulticastScanner.Show(this);
                return;
            } // if

            using (var dialog = new MulticastScannerOptionsDialog())
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                // TODO: get "what" & "action" and proceed accordingly
                timeout = dialog.Timeout;
            } // using

            MulticastScanner = new MulticastScannerDialog();
            MulticastScanner.ChannelScanResult += MulticastScanner_ChannelScanResult;
            MulticastScanner.Timeout = timeout;
            // TODO: filter as indicated in "what"
            MulticastScanner.BroadcastDiscovery = BroadcastDiscovery;
            MulticastScanner.Show(this);
        } // menuItemChannelVerify_Click

        private void listViewChannels_SelectedIndexChanged(object sender, EventArgs e)
        {
            BroadcastServiceChanged();
        } // listViewChannels_SelectedIndexChanged

        private void listViewChannels_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ShowTvChannel();
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, ex);
            } // try-catch
        } // listViewChannels_DoubleClick

        private void listViewChannels_AfterSorting(object sender, EventArgs e)
        {
            UpdateSortMenuStatus();
        } // listViewChannels_AfterSorting

        private void menuItemChannelDetails_Click(object sender, EventArgs e)
        {
            if (SelectedBroadcastService == null) return;

            using (var dlg = new PropertiesDialog()
            {
                Caption = Properties.Texts.BroadcastServiceProperties,
                Properties = SelectedBroadcastService.DumpProperties(),
                Description = SelectedBroadcastService.DisplayName,
                Logo = SelectedBroadcastService.Logo.GetImage(LogoSize.Size64, true),
            })
            {
                dlg.ShowDialog(this);
            } // using
        } // menuItemChannelDetails_Click

        void MulticastScanner_ChannelScanResult(object sender, MulticastScannerDialog.ChannelScanResultEventArgs e)
        {
            // TODO: implement "disable" or delete as indicated in "action"

            var item = listViewChannels.Items[e.ServiceKey];
            if (e.IsDead)
            {
                item.Font = (CurrentChannelListView == View.Tile) ? ChannelListTileDisabledFont : ChannelListDetailsFont;
                item.UseItemStyleForSubItems = true;
                item.ForeColor = SystemColors.GrayText;
            }
            else
            {
                item.ForeColor = item.ListView.ForeColor;
                item.UseItemStyleForSubItems = false;
                item.Font = (CurrentChannelListView == View.Tile) ? ChannelListTileFont : ChannelListDetailsNameItemFont;
            } // if-else
        }  // MulticastScanner_ChannelScanResult

        private void buttonRecordChannel_Click(object sender, EventArgs e)
        {
            RecordTask task;

            using (var dlg = new RecordChannelDialog())
            {
                dlg.Task = RecordTask.CreateWithDefaultValues(new RecordChannel()
                    {
                        LogicalNumber = "---",
                        Name = SelectedBroadcastService.DisplayName,
                        Description = SelectedBroadcastService.DisplayDescription,
                        LogoKey = SelectedBroadcastService.Logo.Key,
                        ServiceName = SelectedBroadcastService.ServiceName,
                        ChannelUrl = SelectedBroadcastService.LocationUrl,
                    });
                dlg.IsNewTask = true;
                dlg.ShowDialog(this);
                task = dlg.Task;
                if (dlg.DialogResult != DialogResult.OK) return;
            } // using dlg

            RecordHelper helper = new RecordHelper();
            helper.Record(task);
        } // buttonRecordChannel_Click

        private void buttonDisplayChannel_Click(object sender, EventArgs e)
        {
            try
            {
                ShowTvChannel();
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, ex);
            } // try-catch
        } // buttonDisplayChannel_Click

        #endregion

        #region Auxiliary methods: providers

        private void ServiceProviderChanged()
        {
            BroadcastDiscovery = null;

            Properties.Settings.Default.LastSelectedServiceProvider = (SelectedServiceProvider != null) ? SelectedServiceProvider.Key : null;
            Properties.Settings.Default.Save();

            if (SelectedServiceProvider == null)
            {
                labelProviderName.Text = Properties.Texts.NotSelectedServiceProvider;
                labelProviderDescription.Text = null;
                pictureProviderLogo.Image = null;
                menuItemProviderDetails.Enabled = false;
                menuItemChannelRefreshList.Enabled = false;
                BroadcastServiceChanged();

                return;
            } // if

            labelProviderName.Text = SelectedServiceProvider.DisplayName;
            labelProviderDescription.Text = SelectedServiceProvider.DisplayDescription;
            pictureProviderLogo.Image = SelectedServiceProvider.Logo.GetImage(LogoSize.Size32, true);

            menuItemProviderDetails.Enabled = true;
            menuItemChannelRefreshList.Enabled = true;

            LoadBroadcastDiscovery(true);
            BroadcastServiceChanged();
        } // ServiceProviderChanged

        #endregion

        #region Auxiliary methods: services

        private bool LoadBroadcastDiscovery(bool fromCache)
        {
            BroadcastDiscoveryXml discovery;

            try
            {
                discovery = null;
                if (fromCache)
                {
                    discovery = AppUiConfiguration.Current.Cache.LoadXml<BroadcastDiscoveryXml>("BroadcastDiscovery", SelectedServiceProvider.Key);
                    if (discovery == null)
                    {
                        return false;
                    } // if
                } // if

                if (discovery == null)
                {
                    var download = new DvbStpDownloadHelper()
                    {
                        Request = new DvbStpDownloadRequest()
                        {
                            PayloadId = 0x02,
                            SegmentId = 0x00,
                            MulticastAddress = IPAddress.Parse(SelectedServiceProvider.Offering.Push[0].Address),
                            MulticastPort = SelectedServiceProvider.Offering.Push[0].Port,
                            Description = Properties.Texts.BroadcastObtainingList,
                            DescriptionParsing = Properties.Texts.BroadcastParsingList,
                            PayloadDataType = typeof(BroadcastDiscoveryXml)
                        },
                        TextUserCancelled = Properties.Texts.UserCancelListRefresh,
                        TextDownloadException = Properties.Texts.BroadcastListUnableRefresh,
                    };
                    download.ShowDialog(this);
                    if (!download.IsOk) return false;

                    discovery = download.Response.DeserializedPayloadData as BroadcastDiscoveryXml;
                    AppUiConfiguration.Current.Cache.SaveXml("BroadcastDiscovery", SelectedServiceProvider.Key, download.Response.Version, discovery);
                } // if

                BroadcastDiscovery = new UiBroadcastDiscovery(discovery, SelectedServiceProvider.DomainName);
                FillListViewChannels();
                BroadcastServiceChanged();

                return true;
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, null, Properties.Texts.BroadcastListUnableRefresh, ex);
                return false;
            } // try-catch
        } // LoadBroadcastDiscovery

        private void FillListViewChannels()
        {
            ListViewItem[] listItems;
            int index;

            if (BroadcastDiscovery == null) return;

            listItems = new ListViewItem[BroadcastDiscovery.Services.Count()];
            index = 0;
            foreach (var service in BroadcastDiscovery.Services)
            {
                var item = new ListViewItem(service.DisplayName);
                item.ImageKey = GetChannelLogoKey(service.Logo);
                if (CurrentChannelListView == View.Details)
                {
                    item.SubItems.Add(service.DisplayDescription);
                    item.SubItems.Add(service.DisplayServiceType);
                    item.SubItems.Add(service.DisplayLocationUrl);
                    item.UseItemStyleForSubItems = false;
                    item.Font = ChannelListDetailsNameItemFont;
                }
                else
                {
                    item.UseItemStyleForSubItems = false;
                    item.SubItems.Add(service.DisplayServiceType).ForeColor = Color.Red;
                } // if-else
                item.Tag = service;
                item.Name = service.Key;
                listItems[index++] = item;
            } // foreach

            listViewChannels.BeginUpdate();
            listViewChannels.View = CurrentChannelListView;
            listViewChannels.Font = (CurrentChannelListView == View.Details) ? ChannelListDetailsFont : ChannelListTileFont;
            listViewChannels.Items.Clear();
            listViewChannels.Items.AddRange(listItems);
            listViewChannels.EndUpdate();
        } // FillListViewChannels

        private void BroadcastServiceChanged()
        {
            // TODO: cancel multicast services validation if active!

            SelectedBroadcastService = null;

            if ((BroadcastDiscovery == null) || (SelectedServiceProvider == null))
            {
                Properties.Settings.Default.LastSelectedService = null;
                Properties.Settings.Default.Save();

                labelListChannelsView.Enabled = false;
                radioListViewTile.Enabled = false;
                radioListViewDetails.Enabled = false;
                menuItemChannelListView.Enabled = false;
                menuItemChannelListSort.Enabled = false;
                listViewChannels.Enabled = false;
                listViewChannels.Items.Clear();
                menuItemChannelVerify.Enabled = false;
                menuItemChannelDetails.Enabled = false;
                buttonRecordChannel.Enabled = false;
                buttonDisplayChannel.Enabled = false;
                return;
            } // if

            labelListChannelsView.Enabled = true;
            radioListViewTile.Enabled = true;
            radioListViewDetails.Enabled = true;
            menuItemChannelListView.Enabled = true;
            menuItemChannelListSort.Enabled = true;
            listViewChannels.Enabled = true;
            menuItemChannelVerify.Enabled = true;
            if (listViewChannels.SelectedItems.Count == 0)
            {
                menuItemChannelDetails.Enabled = false;
                buttonRecordChannel.Enabled = false;
                buttonDisplayChannel.Enabled = false;
                return;
            } // if

            SelectedBroadcastService = listViewChannels.SelectedItems[0].Tag as UiBroadcastService;
            Properties.Settings.Default.LastSelectedService = SelectedBroadcastService.Key;
            Properties.Settings.Default.Save();

            menuItemChannelDetails.Enabled = true;
            buttonRecordChannel.Enabled = true;
            buttonDisplayChannel.Enabled = true;
        } // BroadcastServiceChanged

        private void ShowTvChannel()
        {
            if (SelectedBroadcastService == null) return;

            // TODO: player must be user-selectable
            var player = AppUiConfiguration.Current.User.Players[0];

            ExternalPlayer.Launch(player, SelectedBroadcastService, true);
        } // ShowTvChannel

        private string GetChannelLogoKey(ServiceLogo logo)
        {
            if (!imageListChannels.Images.ContainsKey(logo.Key))
            {
                using (var image = logo.GetImage(LogoSize.Size32, true))
                {
                    imageListChannels.Images.Add(logo.Key, image);
                } // using image
                using (var image = logo.GetImage(LogoSize.Size48, true))
                {
                    imageListChannelsLarge.Images.Add(logo.Key, image);
                } // using image
            } // if

            return logo.Key;
        } // GetChannelLogoKey

        #endregion
    } // class ChannelListForm
} // namespace

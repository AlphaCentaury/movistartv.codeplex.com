using DvbIpTypes.Schema2006;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Discovery;
using Project.DvbIpTv.UiServices.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.ChannelList
{
    public partial class SelectProviderDialog : Form
    {
        UiProviderDiscovery ProvidersDiscovery;

        public SelectProviderDialog()
        {
            InitializeComponent();
        } // constructor

        public UiServiceProvider SelectedServiceProvider
        {
            get;
            set;
        } // SelectedServiceProvider

        public static UiServiceProvider GetLastUserSelectedProvider()
        {
            var lastSelectedProvider = Properties.Settings.Default.LastSelectedServiceProvider;
            if (lastSelectedProvider == null) return null;

            var baseIpAddress = AppUiConfiguration.Current.User.ContentProvider.RootMulticastAddress;
            var discovery = AppUiConfiguration.Current.Cache.LoadXml<ServiceProviderDiscoveryXml>("ProviderDiscovery", baseIpAddress);
            if (discovery == null) return null;

            return UiProviderDiscovery.GetUiServiceProviderFromKey(discovery, lastSelectedProvider);
        } // GetLastUserSelectedProvider

        private void SelectProviderDialog_Load(object sender, EventArgs e)
        {
            if (SelectedServiceProvider == null)
            {
                SelectedIndexChanged();
            } // if
            LoadServiceProviderList(true);
        } // SelectProviderDialog_Load

        private void listViewServiceProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged();
        } // listViewServiceProviders_SelectedIndexChanged

        private void listViewServiceProviders_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedServiceProvider != null)
            {
                buttonOk.PerformClick();
            } // if
        } // listViewServiceProviders_DoubleClick

        private void buttonRefreshServiceProviderList_Click(object sender, EventArgs e)
        {
            LoadServiceProviderList(false);
        } // buttonRefreshServiceProviderList_Click

        private void buttonProviderDetails_Click(object sender, EventArgs e)
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

        private bool LoadServiceProviderList(bool fromCache)
        {
            try
            {
                ServiceProviderDiscoveryXml discovery;
                var baseIpAddress = AppUiConfiguration.Current.User.ContentProvider.RootMulticastAddress;

                // can load from cache?
                discovery = null;
                if (fromCache)
                {
                    discovery = AppUiConfiguration.Current.Cache.LoadXml<ServiceProviderDiscoveryXml>("ProviderDiscovery", baseIpAddress);
                    if (discovery == null)
                    {
                        return false;
                    } // if
                } // if

                if (discovery == null)
                {
                    var basePort = AppUiConfiguration.Current.User.ContentProvider.RootMulticastPort;

                    var download = new DvbStpDownloadHelper()
                    {
                        Request = new DvbStpDownloadRequest()
                        {
                            PayloadId = 0x01,
                            SegmentId = 0x00,
                            MulticastAddress = IPAddress.Parse(baseIpAddress),
                            MulticastPort = basePort,
                            Description = Properties.Texts.SPObtainingList,
                            DescriptionParsing = Properties.Texts.SPParsingList,
                            PayloadDataType = typeof(ServiceProviderDiscoveryXml)
                        },
                        TextUserCancelled = Properties.Texts.UserCancelListRefresh,
                        TextDownloadException = Properties.Texts.SPListUnableRefresh,
                    };
                    download.ShowDialog(this);
                    if (!download.IsOk) return false;

                    discovery = download.Response.DeserializedPayloadData as ServiceProviderDiscoveryXml;
                    AppUiConfiguration.Current.Cache.SaveXml("ProviderDiscovery", baseIpAddress, download.Response.Version, discovery);
                } // if
                
                ProvidersDiscovery = new UiProviderDiscovery(discovery);
                FillServiceProviderList();

                return true;
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, null,
                    Properties.Texts.SPListUnableRefresh, ex);
                return false;
            } // try-catch
        } // LoadServiceProviderList

        private void SelectedIndexChanged()
        {
            if (listViewServiceProviders.SelectedItems.Count > 0)
            {
                SelectedServiceProvider = listViewServiceProviders.SelectedItems[0].Tag as UiServiceProvider;
            }
            else
            {
                SelectedServiceProvider = null;
            } // if-else

            buttonProviderDetails.Enabled = (SelectedServiceProvider != null);
            buttonOk.Enabled = (SelectedServiceProvider != null);
        } // SelectedIndexChanged

        private void FillServiceProviderList()
        {
            ListViewItem[] listItems;
            ListViewItem selectedItem;
            int index;
            int maxWidth;

            listViewServiceProviders.BeginUpdate();
            listViewServiceProviders.Items.Clear();

            if (ProvidersDiscovery == null)
            {
                listViewServiceProviders.EndUpdate();
                return;
            } // if

            listItems = new ListViewItem[ProvidersDiscovery.Providers.Count()];
            index = 0;
            selectedItem = null;
            
            foreach (var provider in ProvidersDiscovery.Providers)
            {
                var item = new ListViewItem(provider.DisplayName);
                item.ImageKey = GetProviderLogoKey(provider.Logo);
                item.SubItems.Add(provider.DisplayDescription);
                item.Tag = provider;
                item.Name = provider.Key;
                if ((SelectedServiceProvider != null) && (provider.Key == SelectedServiceProvider.Key))
                {
                    selectedItem = item;
                } // if
                
                listItems[index++] = item;
            } // foreach
            listViewServiceProviders.Items.AddRange(listItems);

            // trick for calculating the tile size width
            // let the ListView do all of the math and item measuring
            listViewServiceProviders.View = View.Details;
            listViewServiceProviders.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            maxWidth = listViewServiceProviders.Columns[0].Width;
            listViewServiceProviders.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            maxWidth = Math.Max(maxWidth, listViewServiceProviders.Columns[1].Width);

            listViewServiceProviders.TileSize = new Size(imageListProvidersLarge.ImageSize.Width + 6 + maxWidth, imageListProvidersLarge.ImageSize.Height + 6);
            listViewServiceProviders.View = View.Tile;

            listViewServiceProviders.EndUpdate();

            if (selectedItem != null)
            {
                selectedItem.Selected = true;
                selectedItem.EnsureVisible();
            } // if
        } // FillServiceProviderList

        private string GetProviderLogoKey(ProviderLogo logo)
        {
            if (!imageListProvidersLarge.Images.ContainsKey(logo.Key))
            {
                using (var image = logo.GetImage(LogoSize.Size32, true))
                {
                    imageListProvidersLarge.Images.Add(logo.Key, image);
                } // using image
            } // if

            return logo.Key;
        } // GetProviderLogoKey
    } // class SelectProviderDialog
} // namespace

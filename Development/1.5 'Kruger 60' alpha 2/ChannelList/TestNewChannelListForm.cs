using Etsi.Ts102034.v010501.XmlSerialization;
using Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery;
using Etsi.Ts102034.v010501.XmlSerialization.PackageDiscovery;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Discovery;
using Project.DvbIpTv.UiServices.DvbStpClient;
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
    public partial class TestNewChannelListForm : Form
    {
        private UiBroadcastListManager ListManager;

        public TestNewChannelListForm()
        {
            InitializeComponent();

            SmallImageList = new ImageList();
            SmallImageList.ImageSize = BaseLogo.LogoSizeToSize(UiBroadcastListManager.SmallLogoSize);
            SmallImageList.ColorDepth = ColorDepth.Depth32Bit;

            LargeImageList = new ImageList();
            LargeImageList.ImageSize = BaseLogo.LogoSizeToSize(UiBroadcastListManager.LargeLogoSize);
            LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
        }

        public ImageList SmallImageList
        {
            get;
            private set;
        } // SmallImageList

        public ImageList LargeImageList
        {
            get;
            private set;
        } // LargeImageList

        private void TestNewChannelListForm_Load(object sender, EventArgs e)
        {
            var settings = UiBroadcastListSettings.GetDefaultSettings();

            var c = settings.ViewSettings.Details.Columns;
            c.Clear();
            c.Capacity = 23;
            for (int i = 1; i <= 22; i++)
            {
                c.Add((UiBroadcastListColumn)i);
            } // for

            ListManager = new UiBroadcastListManager(listViewChannelList, settings, SmallImageList, LargeImageList, true);

            comboListMode.SelectedIndex = (int)settings.CurrentMode;
            checkShowGridlines.Checked = settings.ShowGridlines;
            comboColumns.DisplayMember = "Value";
            comboColumns.ValueMember = "Key";
            var x = UiBroadcastListManager.GetSortedColumnNames();
            comboColumns.DataSource = x;
        }

        private void TestNewChannelListForm_Shown(object sender, EventArgs e)
        {
            var selectedServiceProvider = SelectProviderDialog.GetLastUserSelectedProvider();
            if (selectedServiceProvider != null)
            {
                var cachedDiscovery = AppUiConfiguration.Current.Cache.LoadXmlDocument<UiBroadcastDiscovery>("UiBroadcastDiscovery", selectedServiceProvider.Key);
                var services = (cachedDiscovery != null) ? cachedDiscovery.Document.Services : null;

                /*
                var countSD = 0;
                var countHD = 0;
                var countOther = 1;
                if (services != null)
                {
                    foreach (var service in services)
                    {
                        switch (service.ServiceType)
                        {
                            case "1": // SD TV
                            case "22": // SD TV (AVC)
                                service.ServiceLogicalNumber = string.Format("X{0:000}", countSD++);
                                break;
                            case "17": // HD TV
                            case "25": // GD TV (AVC)
                                service.ServiceLogicalNumber = string.Format("Y{0:000}", countHD++);
                                break;
                            default:
                                service.ServiceLogicalNumber = string.Format("Z{0:000}", countOther++);
                                break;
                        } // switch
                    } // foreach service
                } // if
                */

                ListManager.BroadcastServices = services;
            } // if
        }

        private void buttonApplyMode_Click(object sender, EventArgs e)
        {
            var newSettings = ListManager.ClonedSettings;
            newSettings.CurrentMode = (View)comboListMode.SelectedIndex;
            ListManager.Settings = newSettings;
        }

        private void checkShowGridlines_CheckedChanged(object sender, EventArgs e)
        {
            var newSettings = ListManager.ClonedSettings;
            newSettings.ShowGridlines = checkShowGridlines.Checked;
            ListManager.Settings = newSettings;
        }

        private void buttonNullSelection_Click(object sender, EventArgs e)
        {
            ListManager.SelectedService = null;
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            var newSettings = ListManager.ClonedSettings;
            newSettings.ApplyGlobalSortColumn = true;
            newSettings.GlobalSortColumns = new List<UiBroadcastListSortColumn>()
            {
                new UiBroadcastListSortColumn()
                {
                    Column = (UiBroadcastListColumn)comboColumns.SelectedValue,
                    Descending = (comboSortDirection.SelectedIndex == 1)
                }
            };
            ListManager.Settings = newSettings;
        }

        private class Test : IConfigurationFormItem
        {
            public string text;

            public UserControl owned;

            public Test(string text)
            {
                this.text = text;
                owned = new UserControl();
                owned.Controls.Add(new Label() { Text = text });
            }

            public UserControl UserInterfaceItem
            {
                get { return owned; }
            }

            public string ItemName
            {
                get { return text; }
            }

            public Image ItemImage
            {
                get { return Properties.Resources.Warning_48x48; }
            }

            public void CommitChanges()
            {
                
            }

            public void DiscardChanges()
            {
                
            }
        }

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            using (var form = new ConfigurationForm())
            {
                using (var editor = new UiBroadcastListSettingsEditor())
                {
                    editor.Settings = ListManager.Settings;
                    form.ConfigurationItems.Add(editor);
                    form.ConfigurationItems.Add(new Test("Idiomas"));
                    form.ConfigurationItems.Add(new Test("Telemetría"));
                    form.ConfigurationItems.Add(new Test("Avanzado"));

                    form.ShowDialog(this);
                } // using
            } // using
        }

        private void buttonTestMultiDownload_Click(object sender, EventArgs e)
        {
            var dlg = new DvbStpEnhancedDownloadDialog();
            var payloads = new List<UiDvbStpClientSegmentInfo>();
            payloads.Add(new UiDvbStpClientSegmentInfo()
            {
                DisplayName = "0x02: Broadcast discovery",
                PayloadId = 0x02,
                SegmentId = null, // any
                XmlType = typeof(BroadcastDiscoveryRoot)
            });
            payloads.Add(new UiDvbStpClientSegmentInfo()
            {
                DisplayName = "0x05: Package discovery",
                PayloadId = 0x05,
                SegmentId = null, // any
                XmlType = typeof(PackageDiscoveryRoot)
            });

            dlg.Request = new UiDvbStpEnhancedDownloadRequest()
            {
                Payloads = payloads,
                MulticastAddress = IPAddress.Parse("239.0.2.154"),
                MulticastPort = 3937,
                Description = Properties.Texts.BroadcastObtainingList,
                DescriptionParsing = Properties.Texts.BroadcastParsingList,
                AllowXmlExtraWhitespace = false,
                XmlNamespaceReplacer = NamespaceUnification.Replacer,
#if DEBUG
                DumpToFolder = AppUiConfiguration.Current.Folders.Cache,
#endif
            };
            dlg.ShowDialog(this);
        }
    }
}

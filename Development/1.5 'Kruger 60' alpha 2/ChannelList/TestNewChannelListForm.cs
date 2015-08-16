using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            var x = UiBroadcastListManager.SortedColumnNames;
            comboColumns.DataSource = x;
        }

        private void TestNewChannelListForm_Shown(object sender, EventArgs e)
        {
            var selectedServiceProvider = SelectProviderDialog.GetLastUserSelectedProvider();
            if (selectedServiceProvider != null)
            {
                var cachedDiscovery = AppUiConfiguration.Current.Cache.LoadXmlDocument<UiBroadcastDiscovery>("UiBroadcastDiscovery", selectedServiceProvider.Key);
                ListManager.BroadcastServices = (cachedDiscovery != null) ? cachedDiscovery.Document.Services : null;
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
            newSettings.GlobalSortColumn = new UiBroadcastListSortColumn()
            {
                Column = (UiBroadcastListColumn)comboColumns.SelectedValue,
                Descending = (comboSortDirection.SelectedIndex == 1)
            };
            ListManager.Settings = newSettings;
        }
    }
}

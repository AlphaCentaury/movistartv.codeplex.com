using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Configuration
{
    public partial class ConfigurationForm : Form
    {
        private ListViewItem SelectedConfigurationListItem;

        public ConfigurationForm()
        {
            InitializeComponent();
            ConfigurationItems = new List<IConfigurationFormItem>();
        } // constructor

        public IList<IConfigurationFormItem> ConfigurationItems
        {
            get;
            private set;
        } // ConfigurationItems

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            listViewConfigItems.TileSize = new Size(listViewConfigItems.Width -SystemInformation.VerticalScrollBarWidth - 2, listViewConfigItems.LargeImageList.ImageSize.Height + 6);

            foreach (var configItem in ConfigurationItems)
            {
                using (var img = configItem.ItemImage)
                {
                    listViewConfigItems.LargeImageList.Images.Add(img);
                } // using

                var item = new ListViewItem(configItem.ItemName);
                item.ImageIndex = imageListConfigItems.Images.Count - 1;
                item.Tag = configItem;

                listViewConfigItems.Items.Add(item);
            } // foreach

            if (listViewConfigItems.Items.Count > 0)
            {
                listViewConfigItems.Items[0].Selected = true;
            } // if
        }  // ConfigurationForm_Load

        private void listViewConfigItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newSelection = (listViewConfigItems.SelectedItems.Count > 0) ? listViewConfigItems.SelectedItems[0] : null;
            if (newSelection == null) return;

            SelectedConfigurationListItem = newSelection;
            var configItem = SelectedConfigurationListItem.Tag as IConfigurationFormItem;

            panelConfigItemUi.Controls.Clear();
            var ui = configItem.UserInterfaceItem;
            panelConfigItemUi.Controls.Add(ui);
            ui.Dock = DockStyle.Fill;
        }
    } // class ConfigurationForm
} // namespace

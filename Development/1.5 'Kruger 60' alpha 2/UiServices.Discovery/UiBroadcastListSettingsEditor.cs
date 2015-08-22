using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Logos;

namespace Project.DvbIpTv.UiServices.Discovery
{
    public partial class UiBroadcastListSettingsEditor : UserControl, IConfigurationFormItem
    {
        private ToolStripButton[] ListModeItems;
        private ISettingsEditorModeColumns[] EditorModeColumns;
        private SettingsEditorSorting[] EditorModeSorting;

        public UiBroadcastListSettingsEditor()
        {
            InitializeComponent();
        } // constructor

        public UiBroadcastListSettings Settings
        {
            get;
            set;
        } // Settings

        #region IConfigurationFormItem implementation

        public UserControl UserInterfaceItem
        {
            get { return this; }
        } // UserInterfaceItem

        public string ItemName
        {
            get { return "Lista de canales"; }
        } // ItemName

        public Image ItemImage
        {
            get { return Properties.Resources.BroadcastListSettings_32x32; }
        } // ItemImage

        public void CommitChanges()
        {
            throw new NotImplementedException();
        } // CommitChanges

        public void DiscardChanges()
        {
            throw new NotImplementedException();
        }  // DiscardChanges

        #endregion

        private void UiBroadcastListSettingsEditor_Load(object sender, EventArgs e)
        {
            var sortedColumns = UiBroadcastListManager.GetSortedColumnNames();
            var sortedColumnsNone = UiBroadcastListManager.GetSortedColumnNames(true);

            // General tab
            ListModeItems = new ToolStripButton[5];
            ListModeItems[0] = toolButtonDetails;
            ListModeItems[1] = toolButtonLarge;
            ListModeItems[2] = toolButtonSmall;
            ListModeItems[3] = toolButtonList;
            ListModeItems[4] = toolButtonTile;
            ListModeItems[0].Tag = View.Details;
            ListModeItems[1].Tag = View.LargeIcon;
            ListModeItems[2].Tag = View.SmallIcon;
            ListModeItems[3].Tag = View.List;
            ListModeItems[4].Tag = View.Tile;

            SetListMode(Settings.CurrentMode);

            checkShowInactive.Checked = Settings.ShowInactiveServices;
            checkShowHidden.Checked = Settings.ShowHiddenServices;
            checkShowOutOfPackage.Checked = Settings.ShowOutOfPackage;

            // Mode settings tab

            EditorModeColumns = new ISettingsEditorModeColumns[5];
            EditorModeColumns[0] = new SettingsEditorModeMultiColumn();
            EditorModeColumns[0].Columns = Settings.ViewSettings.Details.Columns;
            EditorModeColumns[1] = new SettingsEditorModeSingleColumn();
            EditorModeColumns[1].Columns = Settings.ViewSettings.LargeIcon.Columns;
            EditorModeColumns[2] = new SettingsEditorModeSingleColumn();
            EditorModeColumns[2].Columns = Settings.ViewSettings.SmallIcon.Columns;
            EditorModeColumns[3] = new SettingsEditorModeSingleColumn();
            EditorModeColumns[3].Columns = Settings.ViewSettings.List.Columns;
            EditorModeColumns[4] = new SettingsEditorModeTripleColumn();
            EditorModeColumns[4].Columns = Settings.ViewSettings.Tile.Columns;

            EditorModeSorting = new SettingsEditorSorting[5];
            EditorModeSorting[0] = new SettingsEditorSorting();
            EditorModeSorting[1] = new SettingsEditorSorting();
            EditorModeSorting[2] = new SettingsEditorSorting();
            EditorModeSorting[3] = new SettingsEditorSorting();
            EditorModeSorting[4] = new SettingsEditorSorting();

            foreach (var editor in EditorModeColumns)
            {
                editor.ColumnsList = sortedColumns;
                editor.ColumnsNoneList = sortedColumnsNone;
            } // foreach

            comboMode.SelectedIndex = 0;

            comboLogoSize.ValueMember = "Key";
            comboLogoSize.DisplayMember = "Value";
            comboLogoSize.DataSource = BaseLogo.GetListLogoSizes(true).AsReadOnly();
        } // UiBroadcastListSettingsEditor_Load

        #region General tab

        private void toolButtonDetails_Click(object sender, EventArgs e)
        {
            SetListMode(View.Details);
        } // toolButtonDetails_Click

        private void toolButtonLarge_Click(object sender, EventArgs e)
        {
            SetListMode(View.LargeIcon);
        } // toolButtonLarge_Click

        private void toolButtonSmall_Click(object sender, EventArgs e)
        {
            SetListMode(View.SmallIcon);
        } // toolButtonSmall_Click

        private void toolButtonList_Click(object sender, EventArgs e)
        {
            SetListMode(View.List);
        } // toolButtonList_Click

        private void toolButtonTile_Click(object sender, EventArgs e)
        {
            SetListMode(View.Tile);
        } // toolButtonTile_Click

        private void SetListMode(View mode)
        {
            Settings.CurrentMode = mode;
            foreach (var control in ListModeItems)
            {
                if (mode == (View)control.Tag)
                {
                    control.BackColor = SystemColors.Highlight;
                    control.ForeColor = SystemColors.HighlightText;
                }
                else
                {
                    control.BackColor = toolStripListMode.BackColor;
                    control.ForeColor = toolStripListMode.ForeColor;
                } // if-else
            } // foreach control
        } // SetListMode

        #endregion

        #region Mode settings tab

        private void comboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelModeColumns.Controls.Clear();
            panelModeColumns.Controls.Add(EditorModeColumns[comboMode.SelectedIndex].GetControl());
            panelModeColumns.Controls[0].Dock = DockStyle.Fill;

            panelModeSorting.Controls.Clear();
            panelModeSorting.Controls.Add(EditorModeSorting[comboMode.SelectedIndex]);
            panelModeSorting.Controls[0].Dock = DockStyle.Fill;

            var view = IndexToView(comboMode.SelectedIndex);
            comboLogoSize.SelectedValue = Settings[view].LogoSize;
            checkShowGridlines.Visible = (view == View.Details);
        }  // comboMode_SelectedIndexChanged

        private View IndexToView(int index)
        {
            switch (index)
            {
                case 0: return View.Details;
                case 1: return View.LargeIcon;
                case 2: return View.SmallIcon;
                case 3: return View.List;
                case 4: return View.Tile;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // IndexToView

        #endregion
    } // class UiBroadcastListSettingsEditor
} // namespace

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Discovery
{
    internal partial class SettingsEditorModeSingleColumn : UserControl, ISettingsEditorModeColumns
    {
        public SettingsEditorModeSingleColumn()
        {
            InitializeComponent();
        } // constructor

        public List<KeyValuePair<UiBroadcastListColumn, string>> ColumnsList
        {
            private get;
            set;
        } // Columns

        public List<KeyValuePair<UiBroadcastListColumn, string>> ColumnsNoneList
        {
            private get;
            set;
        } // Columns

        public IList<UiBroadcastListColumn> Columns
        {
            private get;
            set;
        } // Columns

        public IList<UiBroadcastListColumn> SelectedColumns
        {
            get
            {
                Columns[0] = (UiBroadcastListColumn)comboColumns.SelectedValue;

                return Columns;
            } // get
        } // SelectedColumns

        public Control GetControl()
        {
            return this;
        } // GetControl

        private void SettingsEditorModeMultiColumn_Load(object sender, EventArgs e)
        {
            comboColumns.DataSource = ColumnsList.AsReadOnly();
            comboColumns.SelectedValue = Columns[0];
        }  // SettingsEditorModeMultiColumn_Load
    } // class SettingsEditorModeSingleColumn
} // namespace

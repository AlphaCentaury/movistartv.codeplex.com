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
    internal partial class SettingsEditorModeTripleColumn : UserControl, ISettingsEditorModeColumns
    {
        public SettingsEditorModeTripleColumn()
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
                var result = new List<UiBroadcastListColumn>(3);

                var value =(UiBroadcastListColumn)comboFirstColumn.SelectedValue;
                result.Add(value);

                value = (UiBroadcastListColumn)comboSecondColumn.SelectedValue;
                if (value != UiBroadcastListColumn.None)
                {
                    result.Add(value);

                    value = (UiBroadcastListColumn)comboThirdColumn.SelectedValue;
                    if (value != UiBroadcastListColumn.None)
                    {
                        result.Add(value);
                    } // if
                } // if

                return result;
            } // get
        } // SelectedColumns

        private void SettingsEditorModeTripleColumn_Load(object sender, EventArgs e)
        {
            comboFirstColumn.DataSource = ColumnsList.AsReadOnly();
            comboSecondColumn.DataSource = ColumnsNoneList.AsReadOnly();
            comboThirdColumn.DataSource = ColumnsNoneList.AsReadOnly();

            switch (Columns.Count)
            {
                case 0:
                    comboFirstColumn.SelectedValue = UiBroadcastListColumn.NumberAndName;
                    comboSecondColumn.SelectedValue = UiBroadcastListColumn.None;
                    comboThirdColumn.SelectedValue = UiBroadcastListColumn.None;
                    break;
                case 1:
                    comboFirstColumn.SelectedValue = Columns[0];
                    comboSecondColumn.SelectedValue = UiBroadcastListColumn.None;
                    comboThirdColumn.SelectedValue = UiBroadcastListColumn.None;
                    break;
                case 2:
                    comboFirstColumn.SelectedValue = Columns[0];
                    comboSecondColumn.SelectedValue = Columns[1];
                    comboThirdColumn.SelectedValue = UiBroadcastListColumn.None;
                    break;
                default:
                    comboFirstColumn.SelectedValue = Columns[0];
                    comboSecondColumn.SelectedValue = Columns[1];
                    comboThirdColumn.SelectedValue = Columns[2];
                    break;
            } // switch
        } // SettingsEditorModeTripleColumn_Load

        public Control GetControl()
        {
            return this;
        } // GetControl

        private void comboSecondColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboThirdColumn.Enabled = ((UiBroadcastListColumn)comboSecondColumn.SelectedValue) != UiBroadcastListColumn.None;
            if (!comboThirdColumn.Enabled)
            {
                comboThirdColumn.SelectedValue = UiBroadcastListColumn.None;
            } // if
        } // comboSecondColumn_SelectedIndexChanged
    } // class SettingsEditorModeTripleColumn
} // namespace

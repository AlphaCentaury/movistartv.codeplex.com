// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Discovery.BroadcastList.Editors
{
    internal partial class SettingsEditorSorting : UserControl
    {
        private UiBroadcastListSortColumn[] SortColumns;
        private int ManualUpdateLock;

        public SettingsEditorSorting()
        {
            InitializeComponent();
        } // constructor

        public List<KeyValuePair<UiBroadcastListColumn, string>> ColumnsNoneList
        {
            private get;
            set;
        } // Columns

        public IList<UiBroadcastListSortColumn> Columns
        {
            private get;
            set;
        } // Columns

        public IList<UiBroadcastListSortColumn> SelectedColumns
        {
            get
            {
                return Columns;
            } // get
        } // SelectedColumns

        private void SettingsEditorSorting_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                ColumnsNoneList = UiBroadcastListManager.GetSortedColumnNames(true);
                Columns = new List<UiBroadcastListSortColumn>();
            } // if

            SortColumns = new UiBroadcastListSortColumn[3];
            for (int index = 0; index < Math.Min(3, Columns.Count); index++)
            {
                SortColumns[index] = Columns[index];
            } // for
            for (int index = Columns.Count; index < 3; index++)
            {
                SortColumns[index] = new UiBroadcastListSortColumn(UiBroadcastListColumn.None, false);
            } // for
            if (Columns.Count == 0)
            {
                SortColumns[0] = new UiBroadcastListSortColumn(UiBroadcastListColumn.Number, false);
            } // if

            ManualUpdateLock++;
            comboThirdColumn.DataSource = ColumnsNoneList.AsReadOnly();
            comboSecondColumn.DataSource = ColumnsNoneList.AsReadOnly();
            comboFirstColumn.DataSource = ColumnsNoneList.AsReadOnly();
            ManualUpdateLock--;

            comboThirdColumn.SelectedValue = SortColumns[2].Column;
            comboSecondColumn.SelectedValue = SortColumns[1].Column;
            comboFirstColumn.SelectedValue = SortColumns[0].Column;

            SetButtonDirectionStatus(buttonFirstDirection, 0, SortColumns[0].IsAscending);
            SetButtonDirectionStatus(buttonFirstDirection, 1, SortColumns[0].IsAscending);
            SetButtonDirectionStatus(buttonFirstDirection, 2, SortColumns[0].IsAscending);
        } // SettingsEditorSorting_Load

        private void comboFirstColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            var value = (UiBroadcastListColumn)comboFirstColumn.SelectedValue;
            SortColumns[0].Column = value;
            var enabled = (value != UiBroadcastListColumn.None);
            buttonFirstDirection.Enabled = enabled;

            comboSecondColumn.Enabled = enabled;
            comboSecondColumn.SelectedValue = ServiceSortComparer.GetSuggestedNextSortColumn(value);
            SetButtonDirectionStatus(buttonSecondDirection, 1, SortColumns[0].IsAscending);

            comboSecondColumn_SelectedIndexChanged(sender, e);
        } // comboFirstColumn_SelectedIndexChanged

        private void comboSecondColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            var value = (UiBroadcastListColumn)comboSecondColumn.SelectedValue;
            SortColumns[1].Column = value;
            var enabled = (value != UiBroadcastListColumn.None);
            buttonSecondDirection.Enabled = enabled;
            SetButtonDirectionStatus(buttonThirdDirection, 2, SortColumns[1].IsAscending);

            comboThirdColumn.Enabled = enabled;
            comboThirdColumn.SelectedValue = ServiceSortComparer.GetSuggestedNextSortColumn(value);

            comboThirdColumn_SelectedIndexChanged(sender, e);
        } // comboSecondColumn_SelectedIndexChanged

        private void comboThirdColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            var value = (UiBroadcastListColumn)comboThirdColumn.SelectedValue;
            SortColumns[2].Column = value;
            var enabled = (value != UiBroadcastListColumn.None);
            buttonThirdDirection.Enabled = enabled;
        }  // comboThirdColumn_SelectedIndexChanged

        private void buttonFirstDirection_Click(object sender, EventArgs e)
        {
            ToggleDirectionStatus(buttonFirstDirection, 0);
        } // buttonFirstDirection_Click

        private void buttonSecondDirection_Click(object sender, EventArgs e)
        {
            ToggleDirectionStatus(buttonSecondDirection, 1);
        } // buttonSecondDirection_Click

        private void buttonThirdDirection_Click(object sender, EventArgs e)
        {
            ToggleDirectionStatus(buttonThirdDirection, 2);
        } // buttonThirdDirection_Click

        private void ToggleDirectionStatus(Button button, int index)
        {
            SetButtonDirectionStatus(button, index, !SortColumns[index].IsAscending);
        } // ToggleDirectionStatus

        private void SetButtonDirectionStatus(Button button, int index, bool isAscending)
        {
            SortColumns[index].IsAscending = isAscending;
            button.Image = isAscending ? Properties.Resources.Action_SortAscending_16x16 : Properties.Resources.Action_SortDescending_16x16;
            toolTip.SetToolTip(button, isAscending ? Properties.SettingsEditor.SortAscendingTooltip : Properties.SettingsEditor.SortDescendingTooltip);
        } // SetButtonDirectionStatus
    } // class SettingsEditorSorting
}// namespace

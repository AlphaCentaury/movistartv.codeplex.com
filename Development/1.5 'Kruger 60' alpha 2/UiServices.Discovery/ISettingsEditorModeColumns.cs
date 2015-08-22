using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Discovery
{
    internal interface ISettingsEditorModeColumns
    {
        List<KeyValuePair<UiBroadcastListColumn, string>> ColumnsList
        {
            set;
        } // ColumnsList

        List<KeyValuePair<UiBroadcastListColumn, string>> ColumnsNoneList
        {
            set;
        } // ColumnsNoneList

        IList<UiBroadcastListColumn> Columns
        {
            set;
        } // Columns

        IList<UiBroadcastListColumn> SelectedColumns
        {
            get;
        } // SelectedColumns

        Control GetControl();
    } // internal interface ISettingsEditorModeColumns
} // namespace

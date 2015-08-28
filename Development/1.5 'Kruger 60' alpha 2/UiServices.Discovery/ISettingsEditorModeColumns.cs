// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.UiServices.Discovery.BroadcastList;
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

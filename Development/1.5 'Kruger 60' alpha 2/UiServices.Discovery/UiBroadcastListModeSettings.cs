using Project.DvbIpTv.Common.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Discovery
{
    [Serializable]
    public class UiBroadcastListModeSettings
    {
        public static UiBroadcastListModeSettings GetDefaultSettings(View mode)
        {
            var result = new UiBroadcastListModeSettings();
            result.SortColumn = new UiBroadcastListSortColumn(UiBroadcastListColumn.Number, false);

            switch (mode)
            {
                case View.Details:
                    result.Columns = new List<UiBroadcastListColumn>(5);
                    result.Columns.Add(UiBroadcastListColumn.Number);
                    result.Columns.Add(UiBroadcastListColumn.Name);
                    result.Columns.Add(UiBroadcastListColumn.Description);
                    result.Columns.Add(UiBroadcastListColumn.DvbType);
                    result.Columns.Add(UiBroadcastListColumn.LocationUrl);
                    break;
                case View.LargeIcon:
                    result.Columns = new List<UiBroadcastListColumn>(1);
                    result.Columns.Add(UiBroadcastListColumn.NumberAndNameCrlf);
                    break;
                case View.SmallIcon:
                    result.Columns = new List<UiBroadcastListColumn>(1);
                    result.Columns.Add(UiBroadcastListColumn.Number);
                    break;
                case View.List:
                    result.Columns = new List<UiBroadcastListColumn>(1);
                    result.Columns.Add(UiBroadcastListColumn.NumberAndName);
                    break;
                case View.Tile:
                    result.Columns = new List<UiBroadcastListColumn>(2);
                    result.Columns.Add(UiBroadcastListColumn.NumberAndName);
                    result.Columns.Add(UiBroadcastListColumn.DvbType);
                    break;
                default:
                    throw new IndexOutOfRangeException();
            } // switch

            return result;
        } // GetDefaultSettings

        [XmlArrayItem("Column")]
        public List<UiBroadcastListColumn> Columns
        {
            get;
            set;
        } // Columns

        public UiBroadcastListSortColumn SortColumn
        {
            get;
            set;
        } // SortColumn
    } // class UiBroadcastListViewModeSettings
} // namespace

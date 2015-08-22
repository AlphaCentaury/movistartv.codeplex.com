using Project.DvbIpTv.Common.Serialization;
using Project.DvbIpTv.UiServices.Configuration.Logos;
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
            result.SortColumns = ServiceSortComparer.GetSuggestedSortColumns(UiBroadcastListColumn.Number, false, 3);

            switch (mode)
            {
                case View.Details:
                    result.Columns = new List<UiBroadcastListColumn>(5);
                    result.Columns.Add(UiBroadcastListColumn.Number);
                    result.Columns.Add(UiBroadcastListColumn.Name);
                    result.Columns.Add(UiBroadcastListColumn.Description);
                    result.Columns.Add(UiBroadcastListColumn.DvbType);
                    result.Columns.Add(UiBroadcastListColumn.LocationUrl);
                    result.LogoSize = LogoSize.Size32;
                    break;
                case View.LargeIcon:
                    result.Columns = new List<UiBroadcastListColumn>(1);
                    result.Columns.Add(UiBroadcastListColumn.NumberAndNameCrlf);
                    result.LogoSize = LogoSize.Size48;
                    break;
                case View.SmallIcon:
                    result.Columns = new List<UiBroadcastListColumn>(1);
                    result.Columns.Add(UiBroadcastListColumn.Number);
                    result.LogoSize = LogoSize.Size32;
                    break;
                case View.List:
                    result.Columns = new List<UiBroadcastListColumn>(1);
                    result.Columns.Add(UiBroadcastListColumn.NumberAndName);
                    result.LogoSize = LogoSize.Size32;
                    break;
                case View.Tile:
                    result.Columns = new List<UiBroadcastListColumn>(2);
                    result.Columns.Add(UiBroadcastListColumn.NumberAndName);
                    result.Columns.Add(UiBroadcastListColumn.DvbType);
                    result.LogoSize = LogoSize.Size48;
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

        public List<UiBroadcastListSortColumn> SortColumns
        {
            get;
            set;
        } // SortColumns

        public LogoSize LogoSize
        {
            get;
            set;
        } // LogoSize
    } // class UiBroadcastListViewModeSettings
} // namespace

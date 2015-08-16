using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Discovery
{
    internal class ServiceSortComparer: IComparer<UiBroadcastService>
    {
        private UiBroadcastListSettings Settings;
        private UiBroadcastListColumn Sort;

        public ServiceSortComparer(UiBroadcastListSettings settings, UiBroadcastListColumn sort)
        {
            Settings = settings;
            Sort = sort;
        } // constructor

        public int Compare(UiBroadcastService x, UiBroadcastService y)
        {
            var sortColumn = Sort;
            var compare = 0;

            var loop = 0;
            while ((sortColumn != UiBroadcastListColumn.None) && (loop < 3))
            {
                var data1 = UiBroadcastListManager.GetColumnData(x, sortColumn);
                var data2 = UiBroadcastListManager.GetColumnData(y, sortColumn);
                compare = data1.CompareTo(data2);
                if (compare != 0) break;

                sortColumn = GetNextCompareColumn(sortColumn);
                loop++;
            } // while

            return compare;
        } // Compare

        public static UiBroadcastListColumn GetNextCompareColumn(UiBroadcastListColumn column)
        {
            // TODO: store this user-modifiable data into Settings or Config
            switch (column)
            {
                case UiBroadcastListColumn.None: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.Name: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.Number: return UiBroadcastListColumn.Name;
                case UiBroadcastListColumn.NumberAndName: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.NumberAndNameCrlf: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.NameAndNumber: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.Description: return UiBroadcastListColumn.Name;
                case UiBroadcastListColumn.DvbType: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.LocationUrl: return UiBroadcastListColumn.DvbType;
                case UiBroadcastListColumn.ShortName: return UiBroadcastListColumn.Name;
                case UiBroadcastListColumn.Genre: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.GenreCode: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.ParentalRating: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.ParentalRatingCode: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.ServiceId: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.FullServiceId: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.UserName: return UiBroadcastListColumn.OriginalName;
                case UiBroadcastListColumn.UserNumber: return UiBroadcastListColumn.Name;
                case UiBroadcastListColumn.OriginalName: return UiBroadcastListColumn.None;
                case UiBroadcastListColumn.OriginalNumber: return UiBroadcastListColumn.Name;
                case UiBroadcastListColumn.IsActive: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.IsEnabled: return UiBroadcastListColumn.Number;
                case UiBroadcastListColumn.LockLevel: return UiBroadcastListColumn.Number;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // GetNextCompareColumn
    } // class ServiceSortComparer
} // namespace

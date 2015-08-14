using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Discovery
{
    [Serializable]
    public class UiBroadcastListViewSettings
    {
        private int[] fieldColumnWidth;

        public UiBroadcastListViewSettings()
        {
        } // constructor

        public View CurrentMode
        {
            get;
            set;
        } // CurrentMode

        public UiBroadcastListViewModeSettings this[View mode]
        {
            get
            {
                switch (mode)
                {
                    case View.Details: return DetailsViewSettings;
                    case View.LargeIcon: return IconViewSettings;
                    case View.SmallIcon: return IconViewSettings;
                    case View.List: return ListViewSettings;
                    case View.Tile: return TileViewSettings;
                    default:
                        throw new IndexOutOfRangeException();
                } // switch
            } // get
            set
            {
                switch (mode)
                {
                    case View.Details: DetailsViewSettings = value; break;
                    case View.LargeIcon: IconViewSettings = value; break;
                    case View.SmallIcon: IconViewSettings = value; break;
                    case View.List: ListViewSettings = value; break;
                    case View.Tile: TileViewSettings = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                } // switch
            } // set
        } // UiBroadcastListViewModeSettings

        public UiBroadcastListViewModeSettings DetailsViewSettings
        {
            get;
            set;
        } // DetailsViewSettings

        public UiBroadcastListViewModeSettings IconViewSettings
        {
            get;
            set;
        } // IconViewSettings

        public UiBroadcastListViewModeSettings ListViewSettings
        {
            get;
            set;
        } // ListViewSettings

        public UiBroadcastListViewModeSettings TileViewSettings
        {
            get;
            set;
        } // TileViewSettings

        public int[] ColumnWidth
        {
            get
            {
                if (fieldColumnWidth == null)
                {
                    fieldColumnWidth = new int[21];
                } // if
                return fieldColumnWidth;
            }
            set
            {
                fieldColumnWidth = value;
            } // set
        } // ColumnWidth

        [XmlIgnore]
        public IList<UiBroadcastListViewColumn> CurrentColumns
        {
            get { return this[CurrentMode].Columns.AsReadOnly(); }
        } // CurrentColumns
    } // class UiBroadcastListViewSettings
} // namespace

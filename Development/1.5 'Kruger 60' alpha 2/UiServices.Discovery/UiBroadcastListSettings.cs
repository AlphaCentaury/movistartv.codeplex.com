// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common.Serialization;
using Project.DvbIpTv.UiServices.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Discovery
{
    [Serializable]
    public class UiBroadcastListSettings : ICloneable, IConfigurationItem
    {
        private int[] fieldColumnWidth;
        public static readonly Guid ConfigurationItemGuid = new Guid("{68B9F98B-DB50-4A08-AF04-35457F0224FB}");

        public class ModeViewSettings
        {
            public UiBroadcastListModeSettings Details
            {
                get;
                set;
            } // Details

            public UiBroadcastListModeSettings LargeIcon
            {
                get;
                set;
            } // LargeIcon

            public UiBroadcastListModeSettings SmallIcon
            {
                get;
                set;
            } // SmallIcon

            public UiBroadcastListModeSettings List
            {
                get;
                set;
            } // List

            public UiBroadcastListModeSettings Tile
            {
                get;
                set;
            } // Tile
        } // class ModeViewSettings

        #region Static methods

        public static UiBroadcastListSettings GetDefaultSettings()
        {
            var result = new UiBroadcastListSettings();

            result.CurrentMode = View.Tile;
            result.ShowGridlines = true;
            result.ShowInactiveServices = true;
            result.ShowOutOfPackage = true;

            result.ViewSettings = new ModeViewSettings();
            result.ViewSettings.Details = UiBroadcastListModeSettings.GetDefaultSettings(View.Details);
            result.ViewSettings.LargeIcon = UiBroadcastListModeSettings.GetDefaultSettings(View.LargeIcon);
            result.ViewSettings.SmallIcon = UiBroadcastListModeSettings.GetDefaultSettings(View.SmallIcon);
            result.ViewSettings.List = UiBroadcastListModeSettings.GetDefaultSettings(View.List);
            result.ViewSettings.Tile = UiBroadcastListModeSettings.GetDefaultSettings(View.Tile);

            result.GlobalSortColumns = ServiceSortComparer.GetSuggestedSortColumns(UiBroadcastListColumn.Number, true, 3);
            result.UseGlobalSortColumns = true;

            // force creation of ColumnWidth field
            var dummy = result.ColumnWidth[0];

            return result;
        } // GetDefaultSettings

        #endregion

        public UiBroadcastListSettings()
        {
            // no op
        } // constructor

        public View CurrentMode
        {
            get;
            set;
        } // CurrentMode

        [XmlIgnore]
        public UiBroadcastListModeSettings this[View mode]
        {
            get
            {
                switch (mode)
                {
                    case View.Details: return ViewSettings.Details;
                    case View.LargeIcon: return ViewSettings.LargeIcon;
                    case View.SmallIcon: return ViewSettings.SmallIcon;
                    case View.List: return ViewSettings.List;
                    case View.Tile: return ViewSettings.Tile;
                    default:
                        throw new IndexOutOfRangeException();
                } // switch
            } // get
            set
            {
                switch (mode)
                {
                    case View.Details: ViewSettings.Details = value; break;
                    case View.LargeIcon: ViewSettings.LargeIcon = value; break;
                    case View.SmallIcon: ViewSettings.SmallIcon = value; break;
                    case View.List: ViewSettings.List = value; break;
                    case View.Tile: ViewSettings.Tile = value; break;
                    default:
                        throw new IndexOutOfRangeException();
                } // switch
            } // set
        } // UiBroadcastListViewModeSettings

        public ModeViewSettings ViewSettings
        {
            get;
            set;
        } // ViewSettings

        [XmlArray("OverrideSortBy")]
        [XmlArrayItem("Column")]
        public List<UiBroadcastListSortColumn> GlobalSortColumns
        {
            get;
            set;
        } // GlobalSortColumns

        [DefaultValue(false)]
        [XmlElement("UseOverrideSortBy")]
        public bool UseGlobalSortColumns
        {
            get;
            set;
        } // UseGlobalSortColumn

        [DefaultValue(false)]
        public bool ShowInactiveServices
        {
            get;
            set;
        } // ShowInactiveServices

        [DefaultValue(false)]
        public bool ShowHiddenServices
        {
            get;
            set;
        } // ShowHiddenServices

        [DefaultValue(false)]
        public bool ShowGridlines
        {
            get;
            set;
        } // ShowGridlines

        [DefaultValue(false)]
        public bool ShowOutOfPackage
        {
            get;
            set;
        } // ShowOutOfPackage

        public int[] ColumnWidth
        {
            get
            {
                if (fieldColumnWidth == null)
                {
                    fieldColumnWidth = new int[23];
                } // if
                return fieldColumnWidth;
            }
            set
            {
                fieldColumnWidth = value;
            } // set
        } // ColumnWidth

        [XmlIgnore]
        public IList<UiBroadcastListColumn> CurrentColumns
        {
            get { return this[CurrentMode].Columns.AsReadOnly(); }
        } // CurrentColumns

        public object Clone()
        {
            return CloneSettings();
        } // Clone

        public UiBroadcastListSettings CloneSettings()
        {
            using (var buffer = new MemoryStream())
            {
                XmlSerialization.Serialize(buffer, this);
                buffer.Seek(0, SeekOrigin.Begin);
                return XmlSerialization.Deserialize<UiBroadcastListSettings>(buffer);
            } // using buffer
        } // CloneSettings

        #region IConfigurationItem implementation

        [XmlIgnore]
        public Guid ConfigurationId
        {
            get { return ConfigurationItemGuid; }
        } // ConfigurationId

        #endregion
    } // class UiBroadcastListViewSettings
} // namespace

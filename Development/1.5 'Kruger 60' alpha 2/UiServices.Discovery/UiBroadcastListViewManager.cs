using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project.DvbIpTv.UiServices.Common.Properties;

namespace Project.DvbIpTv.UiServices.Discovery
{
    public class UiBroadcastListViewManager: IDisposable
    {
        private IList<UiBroadcastService> fieldBroadcastServices;
        private UiBroadcastListViewSettings fieldSettings;
        private const int MinColumnWidth = 25;

        #region Static public methods

        public static IList<KeyValuePair<UiBroadcastListViewColumn, string>> ColumnNames
        {
            get
            {
                var result = new List<KeyValuePair<UiBroadcastListViewColumn, string>>(21);

                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.Number, Properties.ListViewManager.ColumnNumber));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.Name, Properties.ListViewManager.ColumnName));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.NumberAndName, Properties.ListViewManager.ColumnNumberAndName));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.NameAndNumber, Properties.ListViewManager.ColumnNameAndNumber));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.Description, Properties.ListViewManager.ColumnDescription));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.DvbType, Properties.ListViewManager.ColumnDvbType));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.LocationUrl, Properties.ListViewManager.ColumnLocationUrl));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.ShortName, Properties.ListViewManager.ColumnShortName));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.Genre, Properties.ListViewManager.ColumnGenre));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.GenreCode, Properties.ListViewManager.ColumnGenreCode));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.ParentalRating, Properties.ListViewManager.ColumnParentalRating));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.ParentalRatingCode, Properties.ListViewManager.ColumnParentalRatingCode));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.ServiceId, Properties.ListViewManager.ColumnServiceId));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.FullServiceId, Properties.ListViewManager.ColumnFullServiceId));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.UserName, Properties.ListViewManager.ColumnUserName));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.UserNumber, Properties.ListViewManager.ColumnUserNumber));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.OriginalName, Properties.ListViewManager.ColumnOriginalName));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.OriginalNumber, Properties.ListViewManager.ColumnOriginalNumber));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.IsActive, Properties.ListViewManager.ColumnIsActive));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.IsEnabled, Properties.ListViewManager.ColumnIsEnabled));
                result.Add(new KeyValuePair<UiBroadcastListViewColumn, string>(UiBroadcastListViewColumn.LockLevel, Properties.ListViewManager.ColumnLockLevel));

                return result.AsReadOnly();
            } // get
        } // ColumnNames

        public static IList<KeyValuePair<UiBroadcastListViewColumn, string>> SortedColumnNames
        {
            get
            {
                var names = ColumnNames;
                var q = from item in names
                        orderby item.Value
                        select item;

                var result = new List<KeyValuePair<UiBroadcastListViewColumn, string>>(names.Count);
                result.AddRange(q);

                return result.AsReadOnly();
            } // get
        } // SortedColumnNames

        public static string GetColumnName(UiBroadcastListViewColumn column)
        {
            switch (column)
            {
                case UiBroadcastListViewColumn.Number: return Properties.ListViewManager.ColumnNumberShort;
                case UiBroadcastListViewColumn.Name: return Properties.ListViewManager.ColumnNameShort;
                case UiBroadcastListViewColumn.NumberAndName: return Properties.ListViewManager.ColumnNumberAndNameShort;
                case UiBroadcastListViewColumn.NameAndNumber: return Properties.ListViewManager.ColumnNameAndNumberShort;
                case UiBroadcastListViewColumn.Description: return Properties.ListViewManager.ColumnDescriptionShort;
                case UiBroadcastListViewColumn.DvbType: return Properties.ListViewManager.ColumnDvbTypeShort;
                case UiBroadcastListViewColumn.LocationUrl: return Properties.ListViewManager.ColumnLocationUrlShort;
                case UiBroadcastListViewColumn.ShortName: return Properties.ListViewManager.ColumnShortNameShort;
                case UiBroadcastListViewColumn.Genre: return Properties.ListViewManager.ColumnGenreShort;
                case UiBroadcastListViewColumn.GenreCode: return Properties.ListViewManager.ColumnGenreCodeShort;
                case UiBroadcastListViewColumn.ParentalRating: return Properties.ListViewManager.ColumnParentalRatingShort;
                case UiBroadcastListViewColumn.ParentalRatingCode: return Properties.ListViewManager.ColumnParentalRatingCodeShort;
                case UiBroadcastListViewColumn.ServiceId: return Properties.ListViewManager.ColumnServiceIdShort;
                case UiBroadcastListViewColumn.FullServiceId: return Properties.ListViewManager.ColumnFullServiceIdShort;
                case UiBroadcastListViewColumn.UserName: return Properties.ListViewManager.ColumnUserNameShort;
                case UiBroadcastListViewColumn.UserNumber: return Properties.ListViewManager.ColumnUserNumberShort;
                case UiBroadcastListViewColumn.OriginalName: return Properties.ListViewManager.ColumnOriginalNameShort;
                case UiBroadcastListViewColumn.OriginalNumber: return Properties.ListViewManager.ColumnOriginalNumberShort;
                case UiBroadcastListViewColumn.IsActive: return Properties.ListViewManager.ColumnIsActiveShort;
                case UiBroadcastListViewColumn.IsEnabled: return Properties.ListViewManager.ColumnIsEnabledShort;
                case UiBroadcastListViewColumn.LockLevel: return Properties.ListViewManager.ColumnLockLevelShort;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // GetColumnName

        public static int GetColumnDefaultWidth(UiBroadcastListViewColumn column)
        {
            switch (column)
            {
                case UiBroadcastListViewColumn.Number: return 50;
                case UiBroadcastListViewColumn.Name: return 225;
                case UiBroadcastListViewColumn.NumberAndName: return 250;
                case UiBroadcastListViewColumn.NameAndNumber: return 250;
                case UiBroadcastListViewColumn.Description: return 200;
                case UiBroadcastListViewColumn.DvbType: return 100;
                case UiBroadcastListViewColumn.LocationUrl: return 150;
                case UiBroadcastListViewColumn.ShortName: return 125;
                case UiBroadcastListViewColumn.Genre: return 150;
                case UiBroadcastListViewColumn.GenreCode: return 100;
                case UiBroadcastListViewColumn.ParentalRating: return 150;
                case UiBroadcastListViewColumn.ParentalRatingCode: return 100;
                case UiBroadcastListViewColumn.ServiceId: return 75;
                case UiBroadcastListViewColumn.FullServiceId: return 125;
                case UiBroadcastListViewColumn.UserName: return 225;
                case UiBroadcastListViewColumn.UserNumber: return 50;
                case UiBroadcastListViewColumn.OriginalName: return 225;
                case UiBroadcastListViewColumn.OriginalNumber: return 50;
                case UiBroadcastListViewColumn.IsActive: return 50;
                case UiBroadcastListViewColumn.IsEnabled: return 50;
                case UiBroadcastListViewColumn.LockLevel: return 100;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // GetColumnDefaultWidth

        #endregion

        public UiBroadcastListViewManager(ListView listView, UiBroadcastListViewSettings settings)
        {
            if (listView == null) throw new ArgumentNullException("listView");
            if (settings == null) throw new ArgumentNullException("settings");

            ListView = listView;
            fieldSettings = settings;
        } // constructor

        public event EventHandler<ListStatusChangedEventArgs> StatusChanged;
        public event EventHandler<ListSelectionChangedEventArgs> SelectionChanged;

        public bool Disposed
        {
            get;
            private set;
        } // Disposed

        public ListView ListView
        {
            get;
            protected set;
        } // ListView

        public IList<UiBroadcastService> BroadcastServices
        {
            get
            {
                return fieldBroadcastServices;
            } // get
            set
            {
                fieldBroadcastServices = value;
                FillList();
                FireStatusChanged();
            } // set
        } // BroadcastServices

        public UiBroadcastListViewSettings Settings
        {
            get
            {
                return fieldSettings;
            } // get
            set
            {
                if (object.ReferenceEquals(fieldSettings, value))
                {
                    throw new InvalidOperationException();
                } // if

                ApplySettings(value);
            } // set
        } // Settings

        #region Events

        protected virtual void OnStatusChanged(object sender, ListStatusChangedEventArgs e)
        {
            if (StatusChanged != null)
            {
                StatusChanged(sender, e);
            } // if
        } // OnStatusChanged

        protected virtual void OnSelectionChanged(object sender, ListSelectionChangedEventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(sender, e);
            } // if
        } // OnSelectionChanged

        private void FireStatusChanged()
        {
            OnStatusChanged(this, new ListStatusChangedEventArgs(ListView.Items.Count > 0));
            FireSelectionChanged();
        } // FireStatusChanged

        private void FireSelectionChanged()
        {
            var listItem = (ListView.SelectedItems.Count == 0) ? null : ListView.SelectedItems[0];

            if (listItem == null)
            {
                OnSelectionChanged(this, new ListSelectionChangedEventArgs(-1, null));
            }
            else
            {
                OnSelectionChanged(this, new ListSelectionChangedEventArgs(listItem.Index, (UiBroadcastService) listItem.Tag));
            } // if-else
        } // FireSelectionChanged

        #endregion

        #region IDisposable implementation

        public virtual void Dispose()
        {
            if (Disposed) return;

            BroadcastServices = null;
            HookupEvents(false);
            ListView = null;

            Disposed = true;
        } // Dispose

        #endregion

        #region ListView event handlers

        private void HookupEvents(bool add)
        {
            if (add)
            {
                ListView.SelectedIndexChanged += ListView_SelectedIndexChanged;
            }
            else
            {
                ListView.SelectedIndexChanged -= ListView_SelectedIndexChanged;
            } // if-else
        }  // HookupEvents

        void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            FireSelectionChanged();
        } // ListView_SelectedIndexChanged

        #endregion

        #region ListView management

        private void ApplySettings(UiBroadcastListViewSettings newSettings)
        {
        } // ApplySettings

        private void BuildListLayout()
        {
            ListView.Items.Clear();
            ListView.Columns.Clear();

            var columns = Settings.CurrentColumns;
            foreach (var column in columns)
            {
                ListView.Columns.Add(GetColumnName(column), GetColumnWidth(column));
            } // foreach

            FillList();
            ApplySorting();
        } // BuildListLayout

        private void FillList()
        {
            ListView.Items.Clear();
            if (BroadcastServices.Count == 0) return;

            var items = new List<ListViewItem>(BroadcastServices.Count);
            var columns = Settings.CurrentColumns;

            foreach (var service in BroadcastServices)
            {
                var item = new ListViewItem();
                item.Tag = service;
                item.Name = service.Key;

                var subItems = new string[columns.Count];
                var index = (int)0;
                foreach (var column in columns)
                {
                    subItems[index++] = GetColumnData(service, column);
                } // foreach
                item.SubItems.AddRange(subItems);

                items.Add(item);
            } // foreach
        } // FillList

        private void ApplySorting()
        {
        } // ApplySorting

        private string GetColumnData(UiBroadcastService service, UiBroadcastListViewColumn column)
        {
            switch (column)
            {
                case UiBroadcastListViewColumn.Number:
                    return service.DisplayLogicalNumber;
                case UiBroadcastListViewColumn.Name:
                    return service.DisplayName;
                case UiBroadcastListViewColumn.NumberAndName:
                    return string.Format("{0} {1}", service.DisplayLogicalNumber, service.DisplayName);
                case UiBroadcastListViewColumn.NameAndNumber:
                    return string.Format("{0} {1}", service.DisplayName, service.DisplayLogicalNumber);
                case UiBroadcastListViewColumn.Description:
                    return service.DisplayDescription;
                case UiBroadcastListViewColumn.DvbType:
                    return service.DisplayServiceType;
                case UiBroadcastListViewColumn.LocationUrl:
                    return service.DisplayLocationUrl;
                case UiBroadcastListViewColumn.ShortName:
                    return service.DisplayShortName;
                case UiBroadcastListViewColumn.Genre:
                    return service.DisplayGenre;
                case UiBroadcastListViewColumn.GenreCode:
                    return service.DisplayGenreCode;
                case UiBroadcastListViewColumn.ParentalRating:
                    return service.DisplayParentalRating;
                case UiBroadcastListViewColumn.ParentalRatingCode:
                    return service.DisplayParentalRatingCode;
                case UiBroadcastListViewColumn.ServiceId:
                    return service.ServiceName;
                case UiBroadcastListViewColumn.FullServiceId:
                    return service.FullServiceName;
                case UiBroadcastListViewColumn.UserName:
                    return service.UserDisplayName;
                case UiBroadcastListViewColumn.UserNumber:
                    return service.UserLogicalNumber ?? Properties.Texts.ChannelNumberNone;
                case UiBroadcastListViewColumn.OriginalName:
                    return service.DisplayOriginalName;
                case UiBroadcastListViewColumn.OriginalNumber:
                    return service.ServiceLogicalNumber ?? Properties.Texts.ChannelNumberNone;
                case UiBroadcastListViewColumn.IsActive:
                    return (service.IsDead) ? Properties.Texts.No : Properties.Texts.Yes;
                case UiBroadcastListViewColumn.IsEnabled:
                    return (service.IsDisabled) ? Properties.Texts.No : Properties.Texts.Yes;
                case UiBroadcastListViewColumn.LockLevel:
                    return service.DisplayLockLevel;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // GetColumnData

        private int GetColumnWidth(UiBroadcastListViewColumn column)
        {
            var width = Settings.ColumnWidth[(int)column];
            if (width <= 0) return GetColumnDefaultWidth(column);
            else if (width < MinColumnWidth) return MinColumnWidth;
            else return width;
        } // GetColumnWidth

        #endregion
    } // UiBroadcastListViewManager
} // namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Discovery
{
    [Serializable]
    public class UiBroadcastListViewModeSettings
    {
        public List<UiBroadcastListViewColumn> Columns
        {
            get;
            set;
        } // Columns
    } // class UiBroadcastListViewModeSettings
} // namespace

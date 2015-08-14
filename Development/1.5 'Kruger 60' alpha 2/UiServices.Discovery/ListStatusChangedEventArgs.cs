using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Discovery
{
    public class ListStatusChangedEventArgs: EventArgs
    {
        public ListStatusChangedEventArgs()
        {
            // no op
        } // constructor

        public ListStatusChangedEventArgs(bool hasItems)
        {
            HasItems = hasItems;
        } // constructor

        public bool HasItems
        {
            get;
            set;
        } // HasItems
    } // class ListStatusChangedEventArgs
} // namespace

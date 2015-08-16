using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Discovery
{
    public class ListSelectionChangedEventArgs: EventArgs
    {
        public ListSelectionChangedEventArgs()
        {
            // no op
        } // constructor

        public ListSelectionChangedEventArgs(UiBroadcastService item)
        {
            Item = item;
        } // constructor

        public UiBroadcastService Item
        {
            get;
            set;
        } // Item
    } // class ListSelectionChangedEventArgs
} // namespace

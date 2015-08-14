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

        public ListSelectionChangedEventArgs(int index, UiBroadcastService item)
        {
            ItemIndex = index;
            Item = item;
        } // constructor

        public int ItemIndex
        {
            get;
            set;
        } // ItemIndex

        public UiBroadcastService Item
        {
            get;
            set;
        } // Item
    } // class ListSelectionChangedEventArgs
} // namespace

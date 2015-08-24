using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.DvbStpClient
{
    public class UiDvbStpEnhancedDownloadRequest: UiDvbStpBaseDownloadRequest
    {
        public IList<UiDvbStpClientSegmentInfo> Payloads
        {
            get;
            set;
        } // Payloads

        public bool KeepRawData
        {
            get;
            set;
        } // KeepRawData

        public bool AvoidDeserialization
        {
            get;
            set;
        } // AvoidDeserialization

#if DEBUG
        public string DumpToFolder
        {
            get;
            set;
        } // DumpToFolder
#endif
    } // class UiDvbStpEnhancedDownloadRequest
} // namespace

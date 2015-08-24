// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.DvbStp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.DvbStpClient
{
    public class UiDvbStpClientSegmentInfo: DvbStpClientSegmentInfo
    {
        public string DisplayName
        {
            get;
            set;
        } // DisplayName

        public Type XmlType
        {
            get;
            set;
        } // XmlType

        public object XmlDeserializedData
        {
            get;
            set;
        } // XmlDeserializedData
    } // class UiDvbStpClientSegmentInfo
} // namespace

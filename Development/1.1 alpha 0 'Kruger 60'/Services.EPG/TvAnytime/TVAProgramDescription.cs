// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTvServices.EPG.TvAnytime
{
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "ProgramDescription", Namespace = Common.DefaultXmlNamespace)]
    public class TVAProgramDescription
    {
        [XmlElement("ProgramLocationTable")]
        public TVAProgramLocationTable LocationTable
        {
            get;
            set;
        } // ProgramLocationTable
    } // class TVAProgramDescription
} // namespace

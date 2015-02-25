using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Schema2014.ContentProvider
{
    [Serializable]
    [XmlType(Namespace=SerializationCommon.XmlNamespace, AnonymousType=true)]
    public class SpFriendlyName
    {
        private string fieldValue;

        [XmlAttribute("domainName")]
        public string Domain
        {
            get;
            set;
        } // Domain

        [XmlText]
        public string Name
        {
            get { return fieldValue; }
            set { fieldValue = (value != null) ? value.Trim() : null; }
        } // Name
    } // class SpFriendlyName
} // namespace

using Project.DvbIpTv.Common.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Project.DvbIpTv.UiServices.Configuration
{
    [Serializable]
    public class XmlConfigurationItem
    {
        public static XmlElement GetXmlElement(IConfigurationItem item)
        {
            XmlDocument xDoc = new XmlDocument();
            XPathNavigator xNavigator = xDoc.CreateNavigator();
            using (XmlWriter writer = xNavigator.AppendChild())
            {
                XmlSerializer ser = new XmlSerializer(item.GetType());
                ser.Serialize(writer, (object)item);
            } // using

            var xAttr = xDoc.CreateAttribute("configurationId");
            xAttr.Value = item.ConfigurationId.ToString();
            xDoc.DocumentElement.Attributes.Append(xAttr);

            return xDoc.DocumentElement;
        } // GetXmlElement

        [XmlAnyElement]
        public List<XmlElement> XmlData
        {
            get;
            set;
        } // XmlData
    } // class XmlConfigurationItem
} // namespace

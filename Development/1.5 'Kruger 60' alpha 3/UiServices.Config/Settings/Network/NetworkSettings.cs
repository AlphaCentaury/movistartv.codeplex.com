// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.UiServices.Configuration.Schema2014.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Settings.Network
{
    [Serializable]
    [XmlRoot("Network", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class NetworkSettings : IConfigurationItem
    {
        public static NetworkSettings GetDefaultSettings()
        {
            var result = new NetworkSettings()
            {
                MulticastProxy = MulticastProxy.GetDefaultSettings()
            };

            return result;
        } // GetDefaultSettings

        public MulticastProxy MulticastProxy
        {
            get;
            set;
        } // MulticastProxy
    } // class NetworkSettings
} // namespace

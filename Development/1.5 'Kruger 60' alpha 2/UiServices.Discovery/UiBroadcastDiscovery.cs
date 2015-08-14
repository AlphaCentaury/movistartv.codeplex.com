// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Discovery
{
    [Serializable]
    [XmlRoot(ElementName="UI-BroadcastDiscovery", Namespace=SerializationCommon.XmlNamespace)]
    public class UiBroadcastDiscovery
    {
        private IDictionary<string, UiBroadcastService> ServicesDictionary;

        [XmlAttribute("version")]
        public int Version
        {
            get;
            set;
        } // Version

        public List<UiBroadcastService> Services
        {
            get;
            set;
        } // Services

        public UiBroadcastService this[int index]
        {
            get { return Services[index]; }
        } // this[index]

        public UiBroadcastService this[string serviceKey]
        {
            get
            {
                if (ServicesDictionary == null) BuildServicesDictionary();
                return ServicesDictionary[serviceKey];
            } // get
        } // this[string]

        public bool TryGetService(string serviceKey, out UiBroadcastService service)
        {
            if (ServicesDictionary == null) BuildServicesDictionary();
            return ServicesDictionary.TryGetValue(serviceKey, out service);
        } // TryGetService

        public UiBroadcastService TryGetService(string serviceKey)
        {
            UiBroadcastService service;

            if (ServicesDictionary == null) BuildServicesDictionary();
            return ServicesDictionary.TryGetValue(serviceKey, out service) ? service : null;
        } // TryGetService

        /// <remarks>Used by Serialization</remarks>
        protected UiBroadcastDiscovery()
        {
        } // constructor

        public UiBroadcastDiscovery(BroadcastDiscoveryXml discoveryXml, string providerDomainName, int version)
        {
            Create(discoveryXml, providerDomainName, version);
        } // constructor

        private void Create(BroadcastDiscoveryXml discoveryXml, string providerDomainName, int version)
        {
            var services = from offering in discoveryXml.BroadcastDiscovery
                           from list in offering.ServicesList
                           from service in list.Services
                           select service;

            var uiServices = from service in services
                             select new UiBroadcastService(service, providerDomainName);

            var uiServicesList = new List<UiBroadcastService>(services.Count());
            uiServicesList.AddRange(uiServices);

            Version = version;
            Services = uiServicesList;
        } // Create

        private void BuildServicesDictionary()
        {
            ServicesDictionary = new Dictionary<string, UiBroadcastService>(Services.Count);
            foreach (var service in Services)
            {
                ServicesDictionary.Add(service.Key, service);
            } // foreach
        } // BuildServicesDictionary
    } // class UiBroadcastDiscovery
} // namespace

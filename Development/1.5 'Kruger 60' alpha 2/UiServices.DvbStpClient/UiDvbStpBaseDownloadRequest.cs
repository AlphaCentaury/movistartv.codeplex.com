using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;

namespace Project.DvbIpTv.UiServices.DvbStpClient
{
    public abstract class UiDvbStpBaseDownloadRequest
    {
        public UiDvbStpBaseDownloadRequest()
        {
            DialogCloseDelay = 500;
        } // constructor
        
        public IPAddress MulticastAddress
        {
            get;
            set;
        } // MulticastAddress

        public int MulticastPort
        {
            get;
            set;
        } // MulticastPort

        public string Description
        {
            get;
            set;
        } // Description

        public string DescriptionParsing
        {
            get;
            set;
        } // DescriptionParsing

        public bool AllowXmlExtraWhitespace
        {
            get;
            set;
        } // AllowXmlExtraWhitespace

        public Func<string, string> XmlNamespaceReplacer
        {
            get;
            set;
        } // XmlNamespaceReplacer

        [DefaultValue(500)]
        public int DialogCloseDelay
        {
            get;
            set;
        } // DialogCloseDelay
    } // abstract class UiDvbStpBaseDownloadRequest
} // namespace

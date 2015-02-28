// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTvServices.EPG
{
    [Serializable()]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "Item", Namespace = Common.XmlNamespace)]
    public class EPGService
    {
        [XmlIgnore]
        public int DbServiceId
        {
            get;
            set;
        } // DbServiceId

        [XmlAttribute("version")]
        public int Version
        {
            get;
            set;
        } // Version

        [XmlAttribute("serviceId")]
        public string ServiceId
        {
            get;
            set;
        } // ServiceId

        [XmlIgnore]
        public string ServiceDisplayName
        {
            get;
            set;
        } // ServiceDisplayName

        [XmlArray("Events")]
        [XmlElement("Event")]
        public EPGEvent[] Events
        {
            get;
            set;
        } // Events

        public static EPGService FromItem(TvAnytime.TVASchedule schedule)
        {
            if (schedule == null) return null;

            var result = new EPGService()
            {
                Version = TryParseInt( schedule.Version, -1),
                ServiceId = schedule.ServiceIdRef
            };

            var events = new EPGEvent[schedule.Events.Length];
            for (int index = 0; index < events.Length; index++)
            {
                events[index] = EPGEvent.FromItem(schedule.Events[index]);
            } // for
            result.Events = events;

            return result;
        } // FromItem

        public static int TryParseInt(string s, int errValue)
        {
            int result;

            if (int.TryParse(s, out result)) return result;

            return errValue;
        } // TryParseInt
    } // class EPGService
} // namespace

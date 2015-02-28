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
    public class EPGEvent
    {
        [XmlIgnore]
        public int DbId
        {
            get;
            set;
        } // DbId

        [XmlAttribute("crid")]
        public string CRID
        {
            get;
            set;
        } // CRID

        [XmlElement("Title")]
        public string Title
        {
            get;
            set;
        } // Tile

        [XmlElement("Genre")]
        public EPGValue Genre
        {
            get;
            set;
        } // Genre

        [XmlElement("ParentalRating")]
        public EPGValue ParentalRating
        {
            get;
            set;
        } // ParentalRating

        [XmlElement("StartTime")]
        public DateTime StartTime
        {
            get;
            set;
        } // StartTime

        [XmlIgnore]
        public DateTime EndTime
        {
            get { return StartTime + Duration; }
        } // EndTime

        [XmlElement("Duration")]
        public TimeSpan Duration
        {
            get;
            set;
        } // Duration

        public static EPGEvent FromItem(TvAnytime.TVAScheduleEvent item)
        {
            if (item == null) return null;
            if (item.StartTime == null) return null;

            var result = new EPGEvent()
            {
                CRID = item.Program.CRID,
                StartTime = item.StartTime.Value,
                Duration = item.Duration
            };

            if (item.Description == null) return result;

            result.Title = item.Description.Title;
            result.Genre = EPGValue.ToValue(item.Description.Genre);
            result.ParentalRating = (item.Description.ParentalGuidance != null)? EPGValue.ToValue(item.Description.ParentalGuidance.ParentalRating) : null;

            return result;
        } // FromItem

        public override string ToString()
        {
            return Title;
        } // ToString
    } // EPGEvent
} // namespace

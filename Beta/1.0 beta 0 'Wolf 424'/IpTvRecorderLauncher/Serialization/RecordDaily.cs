﻿// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.RecorderLauncher.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public sealed class RecordDaily : RecordScheduleTime
    {
        public RecordDaily()
        {
            RecurEveryDays = 1;
        } // constructor

        public override RecordScheduleKind Kind
        {
            get { return RecordScheduleKind.Daily; }
        } // ScheduleKind

        public short RecurEveryDays
        {
            get;
            set;
        } // RecurEveryDays

        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            RecurEveryDays = 1;
        } // SetDefaultValues

        public override void Verbalize(StringBuilder builder)
        {
            string format;

            if (RecurEveryDays > 1)
            {
                format = "Daily recording, every {0} days, at {1:T}, with a safety margin of {2} minutes.";
            }
            else
            {
                format = "Daily recording, every day, at {1:T}, with a safety margin of {2} minutes.";
            } // if-else
            builder.AppendFormat(format, RecurEveryDays, StartDate, SafetyMarginTimeSpan.TotalMinutes);
            builder.AppendLine();

            VerbalizeStartExpiryDate(builder);
        } // Verbalize
    } // class RecordDaily
} // namespace

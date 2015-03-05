// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common.Serialization;
using Project.DvbIpTv.Services.EPG;
using Project.DvbIpTv.Services.EPG.Serialization;
using Project.DvbIpTv.Services.EPG.TvAnytime;
using Project.DvbIpTv.Services.SqlServerCE;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Project.DvbIpTv.Internal.Tools.ConsoleExperiments
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbFile = @"C:\Users\Developer\Documents\DVB-IPTV\MovistarTV (v1.5 Kruger-60)\Cache\EPG.sdf";

            //ProcessEpgFiles(dbFile);

            using (SqlCeConnection cn = DbServices.GetConnection(dbFile))
            {
                var serviceDbId = 3;

                var now = DateTime.Now;
                var epgData = EpgDbQuery.GetBeforeNowAndThenEvents(cn, serviceDbId, now);

                Console.WriteLine("BEFORE");
                DisplayEpgEvent(epgData[0]);
                Console.WriteLine("NOW");
                DisplayEpgEvent(epgData[1]);
                Console.WriteLine("THEN");
                DisplayEpgEvent(epgData[2]);

                Console.WriteLine("==================================");
                var today = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                var epgEvents = EpgDbQuery.GetDateRange(cn, serviceDbId, today, today + new TimeSpan(1, 0, 0, 0));
                foreach (var epgEvent in epgEvents)
                {
                    DisplayEpgEvent(epgEvent);
                } // foreach
            } // using
        }

        #region EPG files

        static void ProcessEpgFiles(string dbFile)
        {
            var testFolder = @"C:\Users\Developer\Documents\Visual Studio 2012\Projects\MovistarTV\Example data\239.0.2.131@3937";

            foreach (var filename in System.IO.Directory.GetFiles(testFolder, "*.xml"))
            {
                Console.WriteLine(System.IO.Path.GetFileNameWithoutExtension(filename));
                ProcessFile(filename, dbFile);
            } // foreach
        } // ProcessEpgFiles

        static void ProcessFile(string filename, string dbFile)
        {
            using (var cn = DbServices.GetConnection(dbFile))
            {
                ProcessFile(filename, cn);
                cn.Close();
            } // using cn
        } // ProcessFile

        static void ProcessFile(string filename, SqlCeConnection cn)
        {
            var tvaMain = ParseEPGFile(filename);
            if ((tvaMain == null) ||
                (tvaMain.ProgramDescription == null) ||
                (tvaMain.ProgramDescription.LocationTable == null) ||
                (tvaMain.ProgramDescription.LocationTable.Schedule == null))
            {
                Console.WriteLine("Empty or invalid data!");
                // there's nothing to process
                return;
            } // if
            var tvaSchedule = tvaMain.ProgramDescription.LocationTable.Schedule;

            var epgService = EpgService.FromItem(tvaSchedule);
            epgService.GetDatabaseId(cn);

            Console.WriteLine("{0} ({1}): {2} events", epgService.ServiceId, epgService.ServiceDatabaseId, epgService.Events.Length);

            // TODO: Provide fallback mechanism for deleting events pertaining to this service
            if (epgService.Events.Length == 0) return;

            var startTime = epgService.Events[0].StartTime;
            var today = new DateTime(startTime.Year, startTime.Month, startTime.Day);
            var tomorrow = today + new TimeSpan(1, 0, 0, 0);
            EpgDbSerialization.DeleteEvents(cn, epgService.ServiceDatabaseId, today, tomorrow);

            epgService.Save(cn);
        } // ProcessFile

        static TvaMain ParseEPGFile(string filename)
        {
            return XmlSerialization.Deserialize<TvaMain>(filename, true);
        } // ParseEPGFile

        #endregion

        #region EPG extraction & display

        private static EpgEvent GetEpgEvent(SqlCeConnection cn, DateTime start, bool after, int serviceDbId)
        {
            var cmd = new SqlCeCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = cn;

            if (!after)
            {
                cmd.CommandText = "SELECT TOP 1 XmlEpgData, XmlEpgDataAlt " +
                    "FROM Events " +
                    "WHERE ((StartTime <= ?) AND (ServiceDbId = ?)) " +
                    "ORDER BY StartTime DESC";
            }
            else
            {
                cmd.CommandText = "SELECT TOP 1 XmlEpgData, XmlEpgDataAlt " +
                    "FROM Events " +
                    "WHERE ((StartTime >= ?) AND (ServiceDbId = ?)) " +
                    "ORDER BY StartTime ASC";
            } // if-else

            cmd.Parameters.Add("@StartTime", System.Data.SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@ServiceDbId", System.Data.SqlDbType.Int).Value = serviceDbId;

            var reader = cmd.ExecuteReader();
            reader.Read();
            if (!reader.IsDBNull(0))
            {
                var data = reader.GetSqlBinary(0).Value;
                return XmlSerialization.Deserialize<EpgEvent>(data);
            }
            else if (!reader.IsDBNull(1))
            {
                var data = reader.GetSqlBinary(1).Value;
                return XmlSerialization.Deserialize<EpgEvent>(data);
            }
            else
            {
                return null;
            } // if-else
        } // GetEpgEvent

        public static void DisplayEpgEvent(EpgEvent epgEvent, bool withRating = false)
        {
            if (epgEvent == null)
            {
                Console.WriteLine("<NULL> event");
                return;
            } // if
            Console.WriteLine("{0:HH:mm:ss} > {1:HH:mm:ss}: {2}", epgEvent.StartTime, epgEvent.EndTime, epgEvent.Title);
            if (withRating)
            {
                Console.WriteLine("\t{0} | {1}", epgEvent.Genre.Description, epgEvent.ParentalRating.Description);
            } // if
        } // DisplayEpgEvent

        #endregion
    } // class Program
} // namespace
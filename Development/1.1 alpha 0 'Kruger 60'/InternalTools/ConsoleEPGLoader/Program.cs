// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common.Serialization;
using Project.DvbIpTv.DvbStp.Client;
using Project.DvbIpTv.Services.EPG;
using Project.DvbIpTv.Services.EPG.Serialization;
using Project.DvbIpTv.Services.EPG.TvAnytime;
using Project.DvbIpTv.Services.SqlServerCE;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Project.DvbIpTv.Internal.ConsoleEPGLoader
{
    class Program
    {
        internal static AutoResetEvent MainEvent;
        internal static string DbFile;
        internal static string XmlFilesPath;

        static void Main(string[] args)
        {
            DbFile = @"C:\Users\Developer\Documents\DVB-IPTV\MovistarTV (v1.5 Kruger-60)\Cache\EPG.sdf";
            XmlFilesPath = System.IO.Path.GetDirectoryName(DbFile) + "\\EPG";
            System.IO.Directory.CreateDirectory(XmlFilesPath);

            try
            {
                DisplayLogo();

                //PrepareDatabase();

                ProcessEpgSource(new List<KeyValuePair<IPAddress, short>>()
                    {
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.130"), 3937) },
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.131"), 3937) },
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.132"), 3937) },
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.133"), 3937) },
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.134"), 3937) },
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.135"), 3937) },
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.136"), 3937) },
                    });

                Log("Main thread {0} waiting for processing threads to end...", Thread.CurrentThread.ManagedThreadId);
                MainEvent = new AutoResetEvent(false);
                MainEvent.WaitOne();

                CompactDatabase();

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("UNEXPECTED EXCEPTION!");
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                MyApplication.HandleException(null, ex);
            } // try-catch
        } // Main

        static void DisplayLogo()
        {
            string copyright;

            // get copyright text
            object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 0)
            {
                copyright = "Copyright (C) http://movistartv.codeplex.com";
            }
            copyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

            Console.WriteLine(Properties.Texts.StartLogo, Assembly.GetEntryAssembly().GetName().Version, copyright);
            Console.WriteLine();
        } // DisplayLogo

        static void PrepareDatabase()
        {
            Console.Write("Deleting existing EPG data...");
            EpgDbSerialization.DeleteAllEvents(DbFile);
            Console.WriteLine(" ok");
        } // PrepareDatabase

        static void CompactDatabase()
        {
            Console.Write("Compacting EPG database...");

            try
            {
                using (var engine = new SqlCeEngine())
                {
                    engine.LocalConnectionString = "Data source=\"" + DbFile + "\";Password=\"movistartv.codeplex.com\"";
                    engine.Compact("Data source=;Password=\"movistartv.codeplex.com\"");
                } // using engine
            }
            catch (Exception ex)
            {
                Console.WriteLine(" error");
                Console.WriteLine(ex.Message);
            } // try-catch

            Console.WriteLine(" ok");
        } // CompactDatabase

        private static void ProcessEpgSource(IList<KeyValuePair<IPAddress, short>> addresses)
        {
            var list = addresses;

            ThreadPool.QueueUserWorkItem(delegate(object state) { ProcessEpgSourceAsync(list); });
        } // ProcessEpgSource

        private static void ProcessEpgSourceAsync(IList<KeyValuePair<IPAddress, short>> list)
        {
            var events = new AutoResetEvent[list.Count];

            for (int index = 0; index < list.Count; index++)
            {
                var myIndex = index;
                var address = list[myIndex];
                events[myIndex] = new AutoResetEvent(false);

                ThreadPool.QueueUserWorkItem(delegate(object o)
                {
                    Log("=== Loading EPG data from {0}:{1} ===", address.Key, address.Value);

                    var client = new DvbStp.Client.DvbStpClient(address.Key, address.Value);
                    client.DatagramReceived += Client_DatagramReceived;
                    client.SegmentDataDownloaded += Client_SegmentDataDownloaded;
                    client.SegmentReceived += Client_SegmentReceived;

                    client.DownloadSegments(null);

                    Log("=== EPG data from {0}:{1} downloaded ===", address.Key, address.Value);

                    events[myIndex].Set();
                });
            } // foreach

            foreach (var autoEvent in events)
            {
                autoEvent.WaitOne();
            } // foreach

            MainEvent.Set();
        } // ProcessEpgSourceAsync

        static int progressCount;

        static void Client_DatagramReceived(DvbStpClient client, byte payloadId, byte segmentIdNetworkHi, byte segmentIdNetworkLo, byte segmentVersion, bool filtered)
        {
            progressCount = (progressCount + 1) % 25;

            Console.Title = string.Format("Receiving EPG data {0}", new string('#', progressCount));
        } // Client_DatagramReceived

        static void Client_SegmentDataDownloaded(DvbStpClient client, SegmentAssembler segmentData)
        {
            Log("[{0}] Received {1}: {2:N0} bytes", client.MulticastIpAddress, segmentData.SegmentIdentity, segmentData.ReceivedBytes);
            ThreadPool.QueueUserWorkItem(delegate(object state) { ProcessEpgPayload(client.MulticastIpAddress, segmentData, DbFile); });
        } // Client_SegmentDataDownloaded

        static void Client_SegmentReceived(DvbStpClient client, DvbStpSegmentIdentity segmentIdentity, int round)
        {
            Console.WriteLine("[{0}] {1} round {2}", client.MulticastIpAddress, segmentIdentity, round);
        } // Client_SegmentReceived

        static void ProcessEpgPayload(IPAddress ipAddress, SegmentAssembler segmentData, string dbFile)
        {
            var data = segmentData.GetPayload();
            var file = string.Format("{0}_{1}.xml", ipAddress.ToString().Replace('.', '-'), segmentData.SegmentIdentity);

            System.IO.File.WriteAllBytes(System.IO.Path.Combine(XmlFilesPath, file), data);

            using (var cn = DbServices.GetConnection(dbFile))
            {
                ProcessEpgPayload(data, cn);
                cn.Close();
            } // using cn
        } // ProcessFile

        static void ProcessEpgPayload(byte[] data, SqlCeConnection cn)
        {
            var tvaMain = XmlSerialization.Deserialize<TvaMain>(data, true);
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

            Log(">> Service {0} (db {1}): {2} events (t{3})", epgService.ServiceId, epgService.ServiceDatabaseId, epgService.Events.Length, Thread.CurrentThread.ManagedThreadId);

            // TODO: Provide fallback mechanism for deleting events pertaining to this service
            if (epgService.Events.Length == 0) return;

            var startTime = epgService.Events[0].StartTime;
            var today = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0);
            var tomorrow = today + new TimeSpan(1, 0, 0, 0);
            EpgDbSerialization.DeleteEvents(cn, epgService.ServiceDatabaseId, today, tomorrow);

            epgService.Save(cn);
        } // ProcessFile

        static void Log(string text)
        {
            lock (DbFile)
            {
                Console.WriteLine(text);
            } // lock
        } // Log

        static void Log(string text, params object[] args)
        {
            Log(string.Format(text, args));
        } // Log
    } // class Program
} // namespace

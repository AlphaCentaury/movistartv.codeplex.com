// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common.Serialization;
using Project.DvbIpTv.Services.SqlServerCE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.Services.EPG.Serialization
{
    public static class EpgDbSerialization
    {
        #region Load methods

        #endregion

        #region Save methods

        public static void Save(this EpgService epgService, string dbFile)
        {
            using (var cn = DbServices.GetConnection(dbFile))
            {
                epgService.Save(cn);
                cn.Close();
            } // using cn
        } // Save

        public static void Save(this EpgService epgService, SqlCeConnection cn)
        {
            SqlCeTransaction trans;

            if ((epgService.Events == null) || (epgService.Events.Length == 0)) return;

            trans = null;
            try
            {
                trans = cn.BeginTransaction();

                epgService.GetDatabaseId(cn);

                using (var cmd = GetDbSaveCommand())
                {
                    cmd.Connection = cn;
                    SetDbSaveCommandData(cmd, epgService.ServiceDatabaseId, epgService.Version);
                    foreach (var epgEvent in epgService.Events)
                    {
                        SetDbSaveCommandData(cmd, epgEvent);
                        DbServices.Save((SqlCeConnection)null, cmd, "@XmlEpgData", "@XmlEpgDataAlt", epgEvent, false);
                    } // foreach
                } // using

                trans.Commit();
                trans = null; // avoid auto rollback
            }
            finally
            {
                // auto rollback if an exception is thrown
                if (trans != null) trans.Rollback();
            } // finally
        } // Save

        public static void Save(this EpgEvent epgEvent, string dbFile, int serviceDbId, int version)
        {
            var saveCmd = GetDbSaveCommand(epgEvent, serviceDbId, version);
            DbServices.Save(dbFile, saveCmd, "@XmlEpgData", epgEvent);
        } // Save

        public static void Save(this EpgEvent epgEvent, SqlCeConnection cn, int serviceDbId, int version)
        {
            var saveCmd = GetDbSaveCommand(epgEvent, serviceDbId, version);
            DbServices.Save(cn, saveCmd, "@XmlEpgData", epgEvent);
        } // Save

        public static void Save(this EpgEvent epgEvent, string dbFile, int serviceDbId, int version, TvAnytime.TvaScheduleEvent tvaEvent)
        {
            var saveCmd = GetDbSaveCommand(epgEvent, serviceDbId, version);
            DbServices.Save(dbFile, saveCmd, "@XmlEpgData", epgEvent, "XmlTvaData", tvaEvent);
        } // Save

        public static void Save(this EpgEvent epgEvent, SqlCeConnection cn, int serviceDbId, int version, TvAnytime.TvaScheduleEvent tvaEvent)
        {
            var saveCmd = GetDbSaveCommand(epgEvent, serviceDbId, version);
            DbServices.Save(cn, saveCmd, "@XmlEpgData", epgEvent);
        } // Save

        #endregion

        #region Delete methods

        public static int DeleteAllEvents(string dbFile)
        {
            var cmd = GetDeleteEventsCommand();
            return DbServices.Execute(dbFile, cmd);
        } // DeleteAllEvents

        public static int DeleteAllEvents(SqlCeConnection cn, int serviceDbId)
        {
            return DeleteEvents(cn, serviceDbId);
        } // DeleteAllEvents

        public static int DeleteEvents(string dbFile, int serviceDbId, DateTime? fromDate, DateTime? toDate)
        {
            var cmd = GetDeleteEventsCommand(serviceDbId, fromDate, toDate);
            return DbServices.Execute(dbFile, cmd);
        } // DeleteEvents

        public static int DeleteEvents(SqlCeConnection cn, int serviceDbId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var cmd = GetDeleteEventsCommand(serviceDbId, fromDate, toDate);
            return DbServices.Execute(cn, cmd);
        } // DeleteEvents

        #endregion

        #region Auxiliary methods

        private static SqlCeCommand GetDbSaveCommand()
        {
            var cmd = new SqlCeCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO [Events] (ServiceDbId, Version, StartTime, EndTime, DurationSeconds, XmlEpgData) VALUES (?, ?, ?, ?, ?, ?)";
            cmd.Parameters.Add("@ServiceDbId", SqlDbType.Int);
            cmd.Parameters.Add("@Version", SqlDbType.Int);
            cmd.Parameters.Add("@StartTime", SqlDbType.DateTime);
            cmd.Parameters.Add("@EndTime", SqlDbType.DateTime);
            cmd.Parameters.Add("@DurationSeconds", SqlDbType.Int);
            cmd.Parameters.Add("@XmlEpgData", SqlDbType.VarBinary, 4000);
            cmd.Parameters.Add("@XmlEpgDataAlt", SqlDbType.Image);

            return cmd;
        } // GetDbSaveCommand

        private static SqlCeCommand GetDbSaveCommand(EpgEvent epgEvent, int serviceDbId, int version)
        {
            var cmd = GetDbSaveCommand();
            SetDbSaveCommandData(cmd, serviceDbId, version);
            SetDbSaveCommandData(cmd, epgEvent);

            return cmd;
        } // GetDbLoadCommand

        private static void SetDbSaveCommandData(SqlCeCommand cmd, int serviceDbId, int version)
        {
            cmd.Parameters["@ServiceDbId"].Value = serviceDbId;
            cmd.Parameters["@Version"].Value = version;
        } // SetDbSaveCommandData

        private static void SetDbSaveCommandData(SqlCeCommand cmd, EpgEvent epgEvent)
        {
            cmd.Parameters["@StartTime"].Value = epgEvent.StartTime;
            cmd.Parameters["@EndTime"].Value = epgEvent.EndTime;
            cmd.Parameters["@DurationSeconds"].Value = epgEvent.Duration.TotalSeconds;
        } // SetDbSaveCommandData

        private static SqlCeCommand GetDeleteEventsCommand(int? serviceDbId = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var cmd = new SqlCeCommand();
            cmd.CommandType = CommandType.Text;
            if (serviceDbId != null) cmd.Parameters.Add("@ServiceDbId", SqlDbType.Int).Value = serviceDbId.Value;

            var cmdText = new StringBuilder();
            cmdText.Append("DELETE FROM [Events] WHERE ");

            if (serviceDbId != null)
            {
                cmdText.Append("(([ServiceDbId] = ?)");
            }
            else
            {
                cmdText.Append("((1 = 1)");
            } // if-else

            if (fromDate == null)
            {
                if (toDate == null)
                {
                    cmdText.Append(")");
                }
                else
                {
                    cmdText.Append(" AND ([StartTime] < ?))");
                    cmd.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = toDate.Value;
                } // if-else
            }
            else if (toDate == null)
            {
                cmdText.Append(" AND ([StartTime] >= ?))");
                cmd.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = fromDate.Value;
            }
            else
            {
                cmdText.Append(" AND ([StartTime] >= ?) AND ([EndTime] < ?))");
                cmd.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = fromDate.Value;
                cmd.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = toDate.Value;
            } // if-else

            cmd.CommandText = cmdText.ToString();
            return cmd;
        } // GetDeleteEventsCommand

        #endregion
    } // static class EpgDbSerialization
} // namespace

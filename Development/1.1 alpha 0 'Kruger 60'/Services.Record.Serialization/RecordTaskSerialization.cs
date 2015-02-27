using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Project.DvbIpTv.Services.Record.Serialization
{
    public class RecordTaskSerialization
    {
        public const int MaxTaskNameLength = 128;

        #region Load methods

        public static RecordTask LoadFromDatabase(Guid taskId, string dbFile)
        {
            byte[] data;

            using (var cn = GetDbConnection(dbFile))
            {
                using (var cmd = GetDbLoadCommand(cn, taskId))
                {
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow))
                    {
                        int index = reader.GetOrdinal("XmlData");
                        reader.Read();
                        var value = reader.GetValue(index);
                        data = (byte[])value;
                    } // using
                } // using
            } // using

            var serializer = new XmlSerializer(typeof(RecordTask));
            using (var memory = new MemoryStream(data))
            {
                return serializer.Deserialize(memory) as RecordTask;
            } // using
        } // LoadFromDatabase

        public static RecordTask LoadFromXmlString(string xml)
        {
            var serializer = new XmlSerializer(typeof(RecordTask));
            using (var input = new StringReader(xml))
            {
                return serializer.Deserialize(input) as RecordTask;
            } // using input
        } // LoadFromXml

        public static RecordTask LoadFromXmlFile(string filename)
        {
            var serializer = new XmlSerializer(typeof(RecordTask));
            using (var input = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return serializer.Deserialize(input) as RecordTask;
            } // using input
        } // LoadFromXmlFile

        #endregion

        #region Save methods

        public static void SaveToDatabase(RecordTask task, string dbFile)
        {
            using (var cn = GetDbConnection(dbFile))
            {
                using (var cmd = GetDbSaveCommand(cn,
                    task.TaskId,
                    task.Description.Name,
                    task.Description.TaskSchedulerName,
                    task.AdvancedSettings.TaskSchedulerFolder,
                    SaveToXmlBinary(task)))
                {
                    cmd.ExecuteNonQuery();
                } // using cmd
            } // using cn
        } // SaveToDatabase

        public static byte[] SaveToXmlBinary(RecordTask task)
        {
            using (var memory = new MemoryStream())
            {
                SaveToXmlStream(task, memory);
                return memory.ToArray();
            } // using memory
        } // SaveToXmlBinary

        public static string SaveToXmlString(RecordTask task)
        {
            var buffer = new StringBuilder();
            SaveToXmlString(task, buffer);
            return buffer.ToString();
        } // SaveToXmlString

        public static void SaveToXmlString(RecordTask task, StringBuilder buffer)
        {
            using (var writer = XmlWriter.Create(buffer, new XmlWriterSettings() { Indent = true }))
            {
                SaveToXmlWriter(task, writer);
            } // using
        } // SaveToXmlString

        public static void SaveToXmlFile(RecordTask task, string filename)
        {
            using (FileStream output = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                SaveToXmlStream(task, output);
            } // using
        } // SaveToXmlFile

        public static void SaveToXmlStream(RecordTask task, Stream stream)
        {
            var serializer = new XmlSerializer(typeof(RecordTask));
            serializer.Serialize(stream, task);
        } // SaveToXmlStream

        public static void SaveToXmlWriter(RecordTask task, XmlWriter writer)
        {
            var serializer = new XmlSerializer(typeof(RecordTask));
            serializer.Serialize(writer, task);
        } // SaveToXmlWriter

        #endregion

        #region Delete methods

        public static void DeleteFromDatabase(Guid taskId, string dbFile)
        {
            // TODO: Implement
            throw new NotImplementedException();
        } // DeleteFromDatabase

        public static bool TryDeleteFromDatabase(Guid taskId, string dbFile)
        {
            try
            {
                DeleteFromDatabase(taskId, dbFile);
                return true;
            }
            catch
            {
                return false;
            } // try-catch
        } // DeleteFromDatabase

        #endregion

        private static SqlCeConnection GetDbConnection(string dbFile)
        {
            SqlCeConnectionStringBuilder builder;

            builder = new SqlCeConnectionStringBuilder();
            builder.DataSource = dbFile;
            builder.Password = "movistartv.codeplex.com";

            var cn = new SqlCeConnection(builder.ConnectionString);
            cn.Open();

            return cn;
        } // GetDbConnection

        private static SqlCeCommand GetDbLoadCommand(SqlCeConnection cn)
        {
            var cmd = new SqlCeCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT [XmlData] FROM [Tasks] WHERE [TaskId] = ?";
            cmd.Parameters.Add("@TaskId", SqlDbType.UniqueIdentifier);
            cmd.Connection = cn;

            return cmd;
        } // GetDbLoadCommand

        private static SqlCeCommand GetDbLoadCommand(SqlCeConnection cn, Guid taskId)
        {
            var cmd = GetDbLoadCommand(cn);
            cmd.Parameters["@TaskId"].Value = taskId;

            return cmd;
        } // GetDbLoadCommand

        private static SqlCeCommand GetDbSaveCommand(SqlCeConnection cn)
        {
            var cmd = new SqlCeCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO [Tasks] (TaskId, TaskName, SchedulerName, SchedulerFolder, XmlData) VALUES (?, ?, ?, ?, ?)";
            cmd.Parameters.Add("@TaskId", SqlDbType.UniqueIdentifier);
            cmd.Parameters.Add("@TaskName", SqlDbType.NVarChar, MaxTaskNameLength);
            cmd.Parameters.Add("@SchedulerName", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@SchedulerFolder", SqlDbType.NVarChar, 128);
            cmd.Parameters.Add("@XmlData", SqlDbType.Image);
            cmd.Connection = cn;

            return cmd;
        } // GetDbSaveCommand

        private static SqlCeCommand GetDbSaveCommand(SqlCeConnection cn, Guid taskId, string taskName, string schedulerName, string schedulerFolder, byte[] xmlData)
        {
            var cmd = GetDbSaveCommand(cn);
            cmd.Parameters["@TaskId"].Value = taskId;
            cmd.Parameters["@TaskName"].Value = taskName;
            cmd.Parameters["@SchedulerName"].Value = schedulerName;
            cmd.Parameters["@SchedulerFolder"].Value = schedulerFolder;
            cmd.Parameters["@XmlData"].Value = xmlData;

            return cmd;
        } // GetDbLoadCommand
    } // class RecordTaskSerialization
} // namespace

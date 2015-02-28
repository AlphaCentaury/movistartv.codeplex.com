using Project.DvbIpTv.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.Services.SqlServerCE
{
    public class DbServices
    {
        public static T LoadFromDatabase<T>(string dbFile, SqlCeCommand loadCommand, string xmlDataColumnName) where T : class
        {
            using (var cn = GetDbConnection(dbFile))
            {
                return LoadFromDatabase<T>(cn, loadCommand, xmlDataColumnName);
            } // using cn
        } // LoadFromDatabase

        public static T LoadFromDatabase<T>(SqlCeConnection cn, SqlCeCommand loadCommand, string xmlDataColumnName) where T : class
        {
            byte[] data;

            using (var cmd = loadCommand)
            {
                loadCommand.Connection = cn;
                using (var reader = cmd.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow))
                {
                    int index = reader.GetOrdinal(xmlDataColumnName);
                    reader.Read();
                    var value = reader.GetValue(index);
                    data = (byte[])value;
                } // using reader
            } // using cmd

            using (var memory = new MemoryStream(data))
            {
                return XmlSerialization.Deserialize<T>(memory);
            } // using
        } // LoadFromDatabase

        public static int SaveToDatabase<T>(string dbFile, SqlCeCommand saveCommand, string xmlDataParameterName, T obj) where T : class
        {
            using (var cn = GetDbConnection(dbFile))
            {
                return SaveToDatabase<T>(cn, saveCommand, xmlDataParameterName, obj);
            } // using cn
        } // SaveToDatabase<T>

        public static int SaveToDatabase<T>(SqlCeConnection cn, SqlCeCommand saveCommand, string xmlDataParameterName, T obj) where T : class
        {
            byte[] data;

            using (var memory = new MemoryStream())
            {
                XmlSerialization.Serialize<T>(memory, obj);
                data = memory.ToArray();
            } // using memory

            using (var cmd = saveCommand)
            {
                saveCommand.Connection = cn;
                saveCommand.Parameters[xmlDataParameterName].Value = data;

                return cmd.ExecuteNonQuery();
            } // using cmd
        } // SaveToDatabase<T>

        public static int SaveToDatabase<T1, T2>(string dbFile, SqlCeCommand saveCommand, string xmlData1ParameterName, T1 obj1, string xmlData2ParameterName, T2 obj2)
            where T1 : class
            where T2 : class
        {
            using (var cn = GetDbConnection(dbFile))
            {
                return SaveToDatabase<T1, T2>(cn, saveCommand, xmlData1ParameterName, obj1, xmlData2ParameterName, obj2);
            } // using cn
        } // SaveToDatabase<T1, T2>

        public static int SaveToDatabase<T1, T2>(SqlCeConnection cn, SqlCeCommand saveCommand, string xmlData1ParameterName, T1 obj1, string xmlData2ParameterName, T2 obj2)
            where T1 : class
            where T2 : class
        {
            byte[] data1, data2;

            using (var memory = new MemoryStream())
            {
                XmlSerialization.Serialize<T1>(memory, obj1);
                data1 = memory.ToArray();
            } // using memory
            using (var memory = new MemoryStream())
            {
                XmlSerialization.Serialize<T2>(memory, obj2);
                data2 = memory.ToArray();
            } // using memory

            using (var cmd = saveCommand)
            {
                saveCommand.Connection = cn;
                saveCommand.Parameters[xmlData1ParameterName].Value = data1;
                saveCommand.Parameters[xmlData2ParameterName].Value = data2;

                return cmd.ExecuteNonQuery();
            } // using cmd
        } // SaveToDatabase<T1,T2>

        public static SqlCeConnection GetDbConnection(string dbFile)
        {
            SqlCeConnectionStringBuilder builder;

            builder = new SqlCeConnectionStringBuilder();
            builder.DataSource = dbFile;
            builder.Password = "movistartv.codeplex.com";

            var cn = new SqlCeConnection(builder.ConnectionString);
            cn.Open();

            return cn;
        } // GetDbConnection
    } // class DbServices
} // namespace

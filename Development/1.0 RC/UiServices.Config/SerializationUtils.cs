using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration
{
    public static class SerializationUtils
    {
        public static T LoadFromXml<T>(string path) where T: class
        {
            var serializer = new XmlSerializer(typeof(T));
            using (FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return serializer.Deserialize(input) as T;
            } // using input
        } // LoadFromXml<T>

        public static void SaveToXml<T>(T obj, string path, Encoding encoding) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using (FileStream output = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                using (var outputWriter = new StreamWriter(output, encoding))
                {
                    serializer.Serialize(outputWriter, obj);
                } // using outputWriter
            } // using output
        } // SaveToXml<T>
    } // class SerializationUtils
} // namespace

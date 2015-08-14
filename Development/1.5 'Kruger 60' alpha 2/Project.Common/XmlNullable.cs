using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.Common
{
    [Serializable]
    public class XmlNullable<T>
        where T : struct
    {
        public T Value
        {
            get { return Nullable.Value; }
        } // Value

        [XmlIgnore]
        public bool HasValue
        {
            get { return Nullable.HasValue; }
        } // HasValue

        [XmlIgnore]
        public T? Nullable
        {
            get;
            set;
        } // Nullable

        public string XmlValue
        {
            get
            {
                return (Nullable.HasValue) ? Nullable.Value.ToString() : null;
            } // get
            set
            {
                if (value != null)
                {
                    Nullable = (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
                }
                else
                {
                    Nullable = null;
                } // if-else
            } // set
        } // XmlValue
    } // class XmlNullable<T>
} // namespace

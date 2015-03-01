// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common.Serialization;
using Project.DvbIpTvServices.EPG;
using Project.DvbIpTvServices.EPG.TvAnytime;
using System;
using System.Collections.Generic;
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
            //var testFile = @"C:\Users\Developer\Documents\Visual Studio 2012\Projects\MovistarTV\Example data\239.0.2.131@3937\Payload 0xF1 Segment 0xE00B v01.xml";
            var testFile = @"C:\Users\Developer\Documents\Visual Studio 2012\Projects\MovistarTV\Example data\239.0.2.131@3937\Payload 0xF1 Segment 0xE00A v00.xml";
            var main = ParseEPGFile(testFile);

            var epgService = EPGService.FromItem(main.ProgramDescription.LocationTable.Schedule);
        }

        static TVAMain ParseEPGFile(string filename)
        {
            return XmlSerialization.Deserialize<TVAMain>(filename, true);
        } // ParseEPGFile
    } // class Program
} // namespace
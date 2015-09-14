// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Services.EPG;
using Project.DvbIpTv.Services.EPG.Movistar;
using Project.DvbIpTv.Services.EPG.TvAnytime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.EPG
{
    public class MovistarEpgInfoData
    {
        public string Crid;
        public TvaScheduleEvent TvaEvent;
        public EpgEvent EpgEvent;
        public MovistarEpgInfo MovistarInfo;

        public int Index;
        public bool PreviousEnabled;
        public bool NextEnabled;

        public MovistarEpgInfo GetInfo()
        {
            if (MovistarInfo != null)
            {
                return MovistarInfo;
            } // if

            throw new NotImplementedException();
        } // GetInfo
    } // class MovistarEpgInfoData
} // namespace

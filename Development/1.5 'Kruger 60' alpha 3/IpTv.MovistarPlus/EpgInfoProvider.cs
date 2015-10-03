using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.DvbIpTv.Core.IpTvProvider.EPG;
using Project.DvbIpTv.Services.EPG;
using Project.DvbIpTv.UiServices.Discovery;

namespace Project.DvbIpTv.IpTvProvider.MovistarPlus
{
    internal class EpgInfoProvider: IEpgInfoProvider
    {
        #region IEpgInfoProvider Members

        // TODO: get from xml settings, updated from http://www-60.svc.imagenio.telefonica.net:2001/appserver/mvtv.do?action=getConfigurationParams
        private const string EpgThumbnailScheme = "http";
        private const string EpgThumbnailHost = "www-60.svc.imagenio.telefonica.net";
        private const int EpgThumbnailPort = 2001;
        private const string EpgThumbnailLandscapeSubPath = "landscape/";
        private const string EpgThumbnailBigSubPath = "big/";
        private const string EpgThumbnailUrlFormat = "appclient/incoming/covers/programmeImages/{0}{1}{2}/{3}.jpg";

        public EpgInfoProviderCapabilities Capabilities
        {
            get
            {
                return EpgInfoProviderCapabilities.ExtendedInfo |
                    EpgInfoProviderCapabilities.IndependentProgramThumbnail;
            } // get
        } // Capabilities

        public ProgramEpgInfo GetEpgInfo(UiBroadcastService service, EpgEvent epgEvent)
        {
            throw new NotImplementedException();
        } // GetEpgInfo

        public string GetEpgProgramThumbnailUrl(UiBroadcastService service, EpgEvent epgEvent)
        {
            try
            {
                var crid = new Uri(epgEvent.CRID);
                var components = crid.AbsolutePath.Split('/');
                if (components.Length != 4) return null;
                if (components[2] != components[3]) return null;
                if (components[3].Length < 5) return null;

                var movistarSeriesId = components[1];
                var movistarContentIdRoot = components[3].Substring(0, 4);
                var movistarContentId = components[3];

                var builder = new UriBuilder();
                builder.Scheme = EpgThumbnailScheme;
                builder.Host = EpgThumbnailHost;
                builder.Port = EpgThumbnailPort;
                builder.Path = string.Format(EpgThumbnailUrlFormat, EpgThumbnailLandscapeSubPath, EpgThumbnailBigSubPath,
                    movistarContentIdRoot, movistarContentId);

                return builder.Uri.ToString();
            }
            catch
            {
                // ignore
                return null;
            } // try-catch
        } // GetEpgProgramThumbnailUrl

        #endregion
    } // class EpgInfoProvider
} // namespace

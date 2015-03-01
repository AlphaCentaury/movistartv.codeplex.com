// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Project.DvbIpTv.DvbStp.Client
{
    public sealed class DvbStpSimpleClient : DvbStpBaseClient
    {
        private DateTime StartTime;
        private SegmentAssembler SegmentData;

        public DvbStpSimpleClient(IPAddress ip, int port)
            : base(ip, port)
        {
            MaxCycleTime = 30000; // milliseconds
            MaxDowloadRestartCount = 5;
        } // constructor

        public int ReceivedSections
        {
            get { return (SegmentData != null) ? SegmentData.ReceivedSections : 0; }
        } // ReceivedSections

        public int SectionCount
        {
            get { return (SegmentData != null) ? SegmentData.LastSectionNumber + 1 : 0; }
        } // SectionCount

        public byte SegmentVersion
        {
            get { return (SegmentData != null) ? SegmentData.SegmentVersion : (byte)0; }
        } // SegmentVersion

        public int MaxCycleTime
        {
            get;
            set;
        } // MaxCycleTime

        public int DowloadRestartCount
        {
            get;
            private set;
        } // DowloadRestartCount

        public int MaxDowloadRestartCount
        {
            get;
            private set;
        } // MaxDowloadRestartCount

        public event EventHandler<DvbStpSimpleClientSectionReceivedEventArgs> SectionReceived;
        public event EventHandler<DvbStpSimpleClientPayloadSectionReceivedEventArgs> PayloadSectionReceived;
        public event EventHandler<DvbStpSimpleClientPayloadSectionReceivedEventArgs> DownloadStarted;
        public event EventHandler<DvbStpSimpleClientPayloadSectionReceivedEventArgs> DownloadCompleted;
        public event EventHandler<DvbStpSimpleClientDownloadRestartedEventArgs> DownloadRestarted;

        public byte[] GetPayload(byte payloadId, short? segmentId)
        {
            byte[] expectedSegmentId;
            byte receivedPayloadId;
            byte receivedSegmentIdNetworkHi;
            byte receivedSegmentIdNetworkLo;

            try
            {
                CancelRequested = false;
                expectedSegmentId = segmentId.HasValue ? BitConverter.GetBytes(IPAddress.HostToNetworkOrder(segmentId.Value)) : null;
                Clean();

                Connect();
                StartTime = DateTime.Now;
                while (!CancelRequested)
                {
                    CheckTimeout();
                    Receive(false);
                    if (Header.Version != 0) continue;

                    // extract basic section information
                    receivedPayloadId = DatagramData[4];
                    receivedSegmentIdNetworkLo = DatagramData[5];
                    receivedSegmentIdNetworkHi = DatagramData[6];

                    // notify reception of section
                    if (SectionReceived != null) OnSectionReceived(receivedPayloadId, receivedSegmentIdNetworkLo, receivedSegmentIdNetworkHi);

                    // quick filtering of payloadId & segment
                    if (receivedPayloadId != payloadId) continue;
                    if (expectedSegmentId == null)
                    {
                        // accept first segment received and then igonre other segments
                        expectedSegmentId = new byte[]
                        {
                            receivedSegmentIdNetworkLo,
                            receivedSegmentIdNetworkHi
                        };
                    }
                    else
                    {
                        if (receivedSegmentIdNetworkLo != expectedSegmentId[0]) continue;
                        if (receivedSegmentIdNetworkHi != expectedSegmentId[1]) continue;
                    } // if-else

                    // requested payloadId & segmentId found!
                    DecodeHeader(true);

                    // have we just received a "first" section of the payload?
                    if (SegmentData == null)
                    {
                        InitSectionData();
                    } // if

                    // store data
                    StoreSectionData();

                    // notify reception of a requested section
                    if (PayloadSectionReceived != null) OnPayloadSectionReceived();

                    // got all sections?
                    if (SegmentData.IsSegmentComplete)
                    {
                        break;
                    } // if
                } // while

                if (CancelRequested) return null;
                if (DownloadCompleted != null) OnDownloadCompleted();

                return SegmentData.GetPayload();
            }
            finally
            {
                Clean();
            } // finally
        } // GetPayload

        public override void Close()
        {
            base.Close();
            Clean();
        } // Close

        private void InitSectionData()
        {
            SegmentData = new SegmentAssembler(Header.PayloadId, Header.SegmentId, Header.SegmentVersion, Header.LastSectionNumber);
            StartTime = DateTime.Now; // reset timeout

            // notify start of download
            if (DownloadStarted != null)
            {
                OnDownloadStarted();
            } // if
        } // InitSectionData

        private void StoreSectionData()
        {
            // reset timeout
            StartTime = DateTime.Now;

            // version change?
            if (Header.SegmentVersion != SegmentVersion)
            {
                // increment restart count
                DowloadRestartCount++;

                // avoid infinite restart loops
                if (DowloadRestartCount > MaxDowloadRestartCount)
                {
                    throw new TimeoutException();
                } // if

                // notify of download restart
                if (DownloadRestarted != null) OnDowloadRestarted();

                // start over
                SegmentData.NewVersionReset(Header.SegmentVersion, Header.LastSectionNumber);
            } // if

            SegmentData.AddSectionData(Header.SectionNumber, DatagramData, Header.PayloadOffset, Header.PayloadSize);
        } // StoreSectionData

        private void CheckTimeout()
        {
            TimeSpan elapsed;

            elapsed = DateTime.Now - StartTime;
            if (elapsed.TotalMilliseconds > MaxCycleTime) throw new TimeoutException();
        } // CheckTimeout

        private void Clean()
        {
            SegmentData = null;
        } // Clean

        private void OnSectionReceived(byte receivedPayloadId, byte receivedSegmentIdNetworkLo, byte receivedSegmentIdNetworkHi)
        {
            DvbStpSimpleClientSectionReceivedEventArgs e;

            e = new DvbStpSimpleClientSectionReceivedEventArgs()
            {
                DatagramCount = this.DatagramCount,
                PayloadId = receivedPayloadId,
                SegmentIdNetworkLo = receivedSegmentIdNetworkLo,
                SegmentIdNetworkHi = receivedSegmentIdNetworkHi,
                SegmentVersion = DatagramData[7]
            };

            SectionReceived(this, e);
        } // OnSectionReceived

        private void OnPayloadSectionReceived()
        {
            PayloadSectionReceived(this, GetPayloadSectionReceivedEventArgs());
        } // OnPayloadSectionReceived

        private DvbStpSimpleClientPayloadSectionReceivedEventArgs GetPayloadSectionReceivedEventArgs()
        {
            return new DvbStpSimpleClientPayloadSectionReceivedEventArgs()
            {
                PayloadId = Header.PayloadId,
                SegmentId = Header.SegmentId,
                SegmentVersion = Header.SegmentVersion,
                SectionCount = this.SectionCount,
                SectionsReceived = this.ReceivedSections,
                SectionNumber = Header.SectionNumber,
            };
        } // GetPayloadSectionReceivedEventArgs

        private void OnDownloadStarted()
        {
            DownloadStarted(this, GetPayloadSectionReceivedEventArgs());
        } // OnDownloadStarted

        private void OnDownloadCompleted()
        {
            DownloadCompleted(this, GetPayloadSectionReceivedEventArgs());
        } // OnDownloadCompleted

        private void OnDowloadRestarted()
        {
            DvbStpSimpleClientDownloadRestartedEventArgs e;

            e = new DvbStpSimpleClientDownloadRestartedEventArgs()
            {
                PayloadId = Header.PayloadId,
                SegmentId = Header.SegmentId,
                OldVersion = SegmentVersion,
                NewVersion = Header.SegmentVersion,
            };

            DownloadRestarted(this, e);
        } // OnDownloadRestarted
    } // sealed class DvbStpSimpleClient
} // namespace

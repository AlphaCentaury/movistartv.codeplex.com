// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project.DvbIpTv.Services.EPG;
using Project.DvbIpTv.Services.EPG.Serialization;
using Project.DvbIpTv.Services.SqlServerCE;

namespace Project.DvbIpTv.UiServices.EPG
{
    public partial class EpgMiniBar : UserControl
    {
        private EpgEvent[] epgEvents;
        private int epgIndex;

        public enum Button
        {
            Back,
            Forward,
            Details,
            FullView
        } // enum Button

        public event EventHandler<EpgMiniBarButtonClickedEventArgs> ButtonClicked;
        public event EventHandler<EpgMiniBarNavigationButtonsChangedEventArgs> NavigationButtonsChanged;

        public EpgMiniBar()
        {
            InitializeComponent();
            AutoRefresh = true;
        } // constructor

        [DefaultValue(true)]
        public bool AutoRefresh
        {
            get;
            set;
        } // AutoRefresh

        public bool DetailsButtonEnabled
        {
            get { return buttonDetails.Enabled; }
            set { buttonDetails.Enabled = value; }
        }  // DetailsButtonEnabled

        public string EpgDatabase
        {
            get;
            private set;
        } // EpgDatabase

        public string FullServiceName
        {
            get;
            private set;
        } // FullServiceName

        public string FullAlternateServiceName
        {
            get;
            private set;
        } // FullAlternateServiceName

        public DateTime ReferenceTime
        {
            get;
            private set;
        } // ReferenceTime

        public void DisplayEpgEvents(Image channelLogo, string fullServiceName, string fullAlternateServiceName, DateTime referenceTime, string epgDatabase)
        {
            pictureChannelLogo.Image = channelLogo;
            EpgDatabase = epgDatabase;
            FullServiceName = fullServiceName;
            FullAlternateServiceName = fullAlternateServiceName;
            ReferenceTime = referenceTime;

            LoadEpgEvents();
        } // DisplayEpgEvents

        public EpgEvent[] GetEpgEvents()
        {
            if (epgEvents == null) return null;

            var result = new EpgEvent[epgEvents.Length];
            Array.Copy(epgEvents, result, epgEvents.Length);

            return result;
        } // GetEpgEvents

        public void GoBack()
        {
            if (epgIndex < 0) return;
            DisplayEpgEvent(epgIndex - 1);
        } // GoBack

        public void GoForward()
        {
            if (epgIndex > 2) return;
            DisplayEpgEvent(epgIndex + 1);
        } // GoFoward

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (ButtonClicked != null)
            {
                ButtonClicked(this, new EpgMiniBarButtonClickedEventArgs(Button.Back));
            } // if

            GoBack();
        } // buttonBack_Click

        private void buttonForward_Click(object sender, EventArgs e)
        {
            if (ButtonClicked != null)
            {
                ButtonClicked(this, new EpgMiniBarButtonClickedEventArgs(Button.Forward));
            } // if

            GoForward();
        } // buttonForward_Click

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            if (ButtonClicked == null) return;

            ButtonClicked(this, new EpgMiniBarButtonClickedEventArgs(Button.Details));
        } // buttonDetails_Click

        private void buttonFullview_Click(object sender, EventArgs e)
        {
            if (ButtonClicked == null) return;

            ButtonClicked(this, new EpgMiniBarButtonClickedEventArgs(Button.FullView));
        } // buttonFullview_Click

        private void LoadEpgEvents()
        {
            int serviceDbId;

            using (var cn = DbServices.GetConnection(EpgDatabase))
            {
                serviceDbId = EpgDbQuery.GetDatabaseIdForServiceId(FullServiceName, cn);
                epgEvents = EpgDbQuery.GetBeforeNowAndThenEvents(cn, serviceDbId, ReferenceTime);

                // try alternate service if no EPG data
                if ((epgEvents == null) && (FullAlternateServiceName != null))
                {
                    serviceDbId = EpgDbQuery.GetDatabaseIdForServiceId(FullAlternateServiceName, cn);
                    epgEvents = EpgDbQuery.GetBeforeNowAndThenEvents(cn, serviceDbId, ReferenceTime);
                } // if

                DisplayEpgEvents();
            } // using
        } // LoadEpgEvents

        private void DisplayEpgEvents()
        {
            buttonFullview.Enabled = (epgEvents != null);

            if (epgEvents == null)
            {
                DisplayEpgEvent(0);
            }
            else
            {
                if (epgEvents[1] != null)
                {
                    DisplayEpgEvent(1);
                }
                else if (epgEvents[0] != null)
                {
                    DisplayEpgEvent(0);
                }
                else if (epgEvents[2] != null)
                {
                    DisplayEpgEvent(2);
                }
                else
                {
                    DisplayEpgEvent(1);
                } // if-else
            } // if-else
        } // DisplayEpgEvents

        private void DisplayEpgEvent(int index)
        {
            epgIndex = index;

            epgProgressBar.Visible = (index == 1);
            labelEndTime.Visible = (index == 1);

            var epgEvent = (epgEvents != null) ? epgEvents[epgIndex] : null;

            buttonDetails.Enabled = DetailsButtonEnabled && (epgEvent != null);
            if (epgEvent == null)
            {
                labelProgramTitle.Text = "EPG information is not available"; // "Información EPG no disponible";
                labelStartTime.Text = null;
                labelEllapsed.Text = null;
                EnableBackForward(false, false);
                return;
            } // if

            labelProgramTitle.Text = epgEvent.Title;

            switch (epgIndex)
            {
                case 0:
                    labelStartTime.Text = string.Format("{0:t} a {1:t}", epgEvent.StartTime, epgEvent.EndTime);
                    labelEllapsed.Text = string.Format("Ended {0:N0} minutes ago", (ReferenceTime - epgEvent.EndTime).TotalMinutes);
                    //labelEllapsed.Text = string.Format("Terminó hace {0:N0} minutos", (ReferenceTime - epgEvent.EndTime).TotalMinutes);
                    EnableBackForward(false, epgEvents[1] != null);
                    break;
                case 1:
                    labelStartTime.Text = string.Format("{0:HH:mm}", epgEvent.StartTime);
                    labelEndTime.Text = string.Format("{0:t}", epgEvent.EndTime);
                    epgProgressBar.MaximumValue = epgEvent.Duration.TotalMinutes;
                    epgProgressBar.Value = (ReferenceTime - epgEvent.StartTime).TotalMinutes;
                    //labelEllapsed.Text = string.Format("Empezó hace {0:N0} minutos", epgProgressBar.Value);
                    labelEllapsed.Text = string.Format("Started {0:N0} minutes ago", epgProgressBar.Value);
                    EnableBackForward(epgEvents[0] != null, epgEvents[2] != null);
                    break;
                default:
                    labelStartTime.Text = string.Format("{0:t} a {1:t}", epgEvent.StartTime, epgEvent.EndTime);
                    //labelEllapsed.Text = string.Format("Empezará en {0:N0} minutos", (epgEvent.StartTime - ReferenceTime).TotalMinutes);
                    labelEllapsed.Text = string.Format("Will start in {0:N0} minutes", (epgEvent.StartTime - ReferenceTime).TotalMinutes);
                    EnableBackForward(epgEvents[1] != null, false);
                    break;
            } // switch
        } // DisplayEpgEvent

        private void EnableBackForward(bool back, bool forward)
        {
            buttonBack.Enabled = back;
            buttonForward.Enabled = forward;

            if (NavigationButtonsChanged != null)
            {
                NavigationButtonsChanged(this, new EpgMiniBarNavigationButtonsChangedEventArgs(back, forward));
            } // if
        } // EnableBackForward
    } // class EpgMiniBar
} // namespace

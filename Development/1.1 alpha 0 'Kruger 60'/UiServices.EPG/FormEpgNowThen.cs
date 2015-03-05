// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common.Serialization;
using Project.DvbIpTv.Services.EPG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.EPG
{
    public partial class FormEpgNowThen : Form
    {
        private static Image EpgLoadingProgramImage;
        private static Image EpgNoProgramImage;

        public FormEpgNowThen()
        {
            InitializeComponent();
        } // constructor

        public static void ShowEpgEvents(Image channelLogo, string channelName, EpgEvent[] epg, IWin32Window owner, DateTime referenceTime) 
        {
            if (EpgLoadingProgramImage == null)
            {
                EpgLoadingProgramImage = Properties.Resources.EpgLoadingProgramImage;
                EpgNoProgramImage = Properties.Resources.EpgNoProgramImage;
            } // if

            using (var form = new FormEpgNowThen())
            {
                form.DisplayData(channelLogo, channelName, epg, referenceTime);
                form.ShowDialog(owner);
            } // using form
        } // ShowEpgBasicData

        private void FormBasicEpgData_Load(object sender, EventArgs e)
        {
        } // FormBasicEpgData_Load

        private void pictureBox_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                (sender as PictureBox).Image = EpgNoProgramImage;
            } // if
        } // pictureBox_LoadCompleted

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void DisplayData(Image channelLogo, string channelName, EpgEvent[] epg, DateTime referenceTime)
        {
            pictureChannelLogo.Image = channelLogo;
            labelChannelName.Text = channelName;

            DisplayData((epg == null) ? null : epg[0], labelBeforeTime, labelBeforeTitle, labelBeforeDetails, pictureBoxBefore, referenceTime);
            DisplayData((epg == null) ? null : epg[1], labelNowTime, labelNowTitle, labelNowDetails, pictureBoxNow, referenceTime);
            DisplayData((epg == null) ? null : epg[2], labelThenTime, labelThenTitle, labelThenDetails, pictureBoxThen, referenceTime);
        } // DisplayData

        private void DisplayData(EpgEvent epg, Label time, Label title, Label details, PictureBox picture, DateTime referenceTime)
        {
            string timeFormat;

            time.Visible = (epg != null);
            details.Visible = (epg != null);
            picture.ImageLocation = null;

            if (epg == null)
            {
                title.Text = "Información EPG no disponible";
                picture.Image = EpgNoProgramImage;
            }
            else
            {
                if ((epg.StartTime.Day != referenceTime.Day) || (epg.StartTime.Month != referenceTime.Month) || (epg.StartTime.Year != referenceTime.Year))
                {
                    timeFormat = "{0:dd/MM/yyyy HH:mm:ss} hasta {1:HH:mm:ss} ({2:N0} minutos)";
                }
                else
                {
                    timeFormat = "{0:HH:mm:ss} hasta {1:HH:mm:ss} ({2:N0} minutos)";
                } // if-else

                title.Text = epg.Title;
                time.Text = string.Format(timeFormat, epg.StartTime, epg.EndTime, epg.Duration.TotalMinutes);
                details.Text = string.Format("{0} / {1}", (epg.Genre != null) ? epg.Genre.Description : "Género no disponible",
                    (epg.ParentalRating != null)? epg.ParentalRating.Description : "Clasificación no disponible");

                var cridUri = new Uri(epg.CRID);
                var path = cridUri.LocalPath.Split('/');
                var crid = path[2];
                var baseCrid = crid.Substring(0, 4);

                picture.Image = EpgLoadingProgramImage;
                picture.ImageLocation = string.Format("http://172.26.22.23:2001/appclient/incoming/covers/programmeImages/landscape/big/{0}/{1}.jpg", baseCrid, crid);
            } // if-else
        }  // DisplayData
    }
}

// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

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
    public partial class MovistarEpgInfoDialog : Form
    {
        private Encoding Ansi1252Encoding;

        public MovistarEpgInfoDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Epg;
            Ansi1252Encoding = Encoding.GetEncoding(1252);
        } // constructor

        public Func<int, MovistarEpgInfoData> NavigationCallback
        {
            get;
            set;
        } // NavigationCallback

        public MovistarEpgInfoData CurrentEpgInfo
        {
            get;
            set;
        } // CurrentEpgInfo

        private void MovistarEpgInfoDialog_Load(object sender, EventArgs e)
        {
            buttonPrevious.Enabled = (NavigationCallback != null);
            buttonNext.Enabled = (NavigationCallback != null);

            if ((CurrentEpgInfo == null) && (NavigationCallback == null))
            {
                return;
            } // if

            if (CurrentEpgInfo == null)
            {
                CurrentEpgInfo = NavigationCallback(-1);
            } // if
            DisplayEpgInfo();
        } // Load

        private void DisplayEpgInfo()
        {
            StringBuilder rtf;
            
            buttonPrevious.Enabled = CurrentEpgInfo.PreviousEnabled;
            buttonNext.Enabled = CurrentEpgInfo.NextEnabled;

            var info = CurrentEpgInfo.GetInfo();

            rtf = new StringBuilder();
            rtf.AppendLine(Properties.EpgRtf.Header);

            var title = Extract(info.LongTitle, info.Title, "<Null>");
            rtf.AppendFormat(Properties.EpgRtf.ProgramTitle, title);
            var originalTitle = Extract(info.OriginalLongTitle, info.OriginalTitle, null);
            if (originalTitle != null)
            {
                rtf.AppendFormat(Properties.EpgRtf.ProgramDescription, originalTitle);
            }
            rtf.AppendLine();

            var description = Extract(info.Description, "<Null>");
            rtf.AppendFormat(Properties.EpgRtf.ProgramDescription, description);
            rtf.AppendLine();

            rtf.AppendLine(Properties.EpgRtf.ValuesHeader);
            rtf.AppendFormat(Properties.EpgRtf.Value, ToUnicodeRtf("(País)"), "(Value)");
            rtf.AppendFormat(Properties.EpgRtf.Value, "(Property)", "(Value)");
            rtf.AppendFormat(Properties.EpgRtf.Value, "(Property)", "(Value)");
            rtf.AppendLine(Properties.EpgRtf.ValuesFooter);

            rtf.AppendLine(Properties.EpgRtf.Footer);

            richTextProgramData.Rtf = rtf.ToString();

        } // DisplayEpgInfo

        private string Extract(string[] longText, string[] shortText, string defaultText)
        {
            string text;

            text = ((longText != null) && (longText.Length > 0)) ? longText[0] : null;
            if (string.IsNullOrEmpty(text)) text = null;

            if (text == null)
            {
                text = ((shortText != null) && (shortText.Length > 0)) ? shortText[0] : null;
                if (string.IsNullOrEmpty(text)) text = null;
            } // if

            return ToUnicodeRtf(text ?? defaultText);
        } // Extract

        private string Extract(string[] longText, string shortText, string defaultText)
        {
            string lngText;

            lngText = ((longText != null) && (longText.Length > 0)) ? longText[0] : null;
            if (string.IsNullOrEmpty(lngText)) lngText = null;
            var result = lngText ?? (shortText ?? defaultText);

            return ToUnicodeRtf(result);
        } // Extract

        private string Extract(string text, string defaultText)
        {
            if (string.IsNullOrEmpty(text)) text = null;
            var result = text ?? defaultText;

            return ToUnicodeRtf(result);
        } // Extract

        private string ToUnicodeRtf(string text)
        {
            if (text == null) return null;

            // quick check
            bool found = false;
            for (int index = 0; index < text.Length; index++)
            {
                if (text[index] > 127) { found = true; break; }
            } // for

            if (!found) return text;

            var buffer = new StringBuilder();
            for (int index = 0; index < text.Length; index++)
            {
                var c = text[index];
                if (c < 127)
                {
                    if (c == '\\')
                    {
                        buffer.Append("\\\\");
                    }
                    else
                    {
                        buffer.Append(c);
                    }
                }
                else
                {
                    var ansiChar = Ansi1252Encoding.GetBytes(new char[] { c })[0];
                    var ansiCharRtf = (ansiChar <= 127) ? ansiChar.ToString() : string.Format("\\'{0:x0}", ansiChar);
                    buffer.AppendFormat("\\u{0}{1}", (c <= 32767) ? (int)c : ((int)c) - 32768, ansiCharRtf);
                } // if-else
            } // for

            return buffer.ToString();
        }  // ToUnicodeRtf

        private void contextRtfMenuCopy_Click(object sender, EventArgs e)
        {
            richTextProgramData.Copy();
        }

        private void contextRtfMenuSelectAll_Click(object sender, EventArgs e)
        {
            richTextProgramData.SelectAll();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            CurrentEpgInfo = NavigationCallback(CurrentEpgInfo.Index - 1);
            DisplayEpgInfo();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            CurrentEpgInfo = NavigationCallback(CurrentEpgInfo.Index + 1);
            DisplayEpgInfo();
        }
    }
}

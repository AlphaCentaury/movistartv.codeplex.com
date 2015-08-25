// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.DvbStpClient
{
    internal partial class HelpDialog : Form
    {
        public HelpDialog()
        {
            InitializeComponent();
        } // constructor

        private void Help_Load(object sender, EventArgs e)
        {
            richTextHelp.Rtf = Properties.Texts.RtfTroubleshootingGuide;
        } // Help_Load

        private void richTextHelp_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Launcher.OpenUrl(this, e.LinkText);
        } // richTextHelp_LinkClicked
    } // class HelpDialog
} // namespace

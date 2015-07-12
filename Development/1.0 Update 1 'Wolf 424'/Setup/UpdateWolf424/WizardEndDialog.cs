using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.Setup.UpdateWolf424
{
    public partial class WizardEndDialog : Form
    {
        public WizardEndDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.UpdateIcon;
        } // constructor

        public DialogResult EndResult
        {
            get;
            set;
        } // EndResult

        private void WizardEndDialog_Load(object sender, EventArgs e)
        {
            switch (EndResult)
            {
                case System.Windows.Forms.DialogResult.OK:
                    pictureEndIcon.Image = Properties.Resources.TickGreen_64x64;
                    labelEndTitle.Text = string.Format(Properties.Resources.EndTextOk, Program.TargetProductName, Program.UpdateProductName);
                    labelEndText.Text = string.Format(Properties.Resources.EndTitleOk, Program.TargetProductName, Program.UpdateProductName);
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                    pictureEndIcon.Image = Properties.Resources.Warning_64x64;
                    labelEndTitle.Text = string.Format(Properties.Resources.EndTitleCancel, Program.TargetProductName, Program.UpdateProductName);
                    labelEndText.Text = string.Format(Properties.Resources.EndTextCancel, Program.TargetProductName, Program.UpdateProductName);
                    break;
                default:
                    pictureEndIcon.Image = Properties.Resources.Exclamation_64x64;
                    labelEndTitle.Text = string.Format(Properties.Resources.EndTitleAbort, Program.TargetProductName, Program.UpdateProductName);
                    labelEndText.Text = string.Format(Properties.Resources.EndTextAbort, Program.TargetProductName, Program.UpdateProductName);
                    break;
            } // switch
        } // WizardEndDialog_Load
    } // class WizardEndDialog
} // namespace

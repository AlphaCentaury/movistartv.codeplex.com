using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.Setup.UpdateWolf424
{
    public partial class WizardWelcomeDialog : Form
    {
        public WizardWelcomeDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.UpdateIcon;
        }

        private void WizardWelcomeDialog_Load(object sender, EventArgs e)
        {
            labelWelcomeTitle.Text = string.Format(labelWelcomeTitle.Text, Program.TargetProductName, Program.UpdateProductName);
            labelWelcomeText.Text = string.Format(labelWelcomeText.Text, Program.TargetProductName, Program.UpdateProductName);
        } // WizardWelcomeDialog_Load

        private void WizardWelcomeDialog_Shown(object sender, EventArgs e)
        {
            buttonNext.Focus();
        } // WizardWelcomeDialog_Shown
    }
}

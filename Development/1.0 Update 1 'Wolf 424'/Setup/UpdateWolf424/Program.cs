using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Project.DvbIpTv.Setup.UpdateWolf424
{
    static class Program
    {
        internal static string TargetProductName
        {
            get;
            private set;
        } // TargetProductName

        internal static string UpdateProductName
        {
            get;
            private set;
        } // 

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DialogResult endResult = DialogResult.Abort;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UiCulture:
            using (var dlg = new SelectUiCultureDialog())
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
            } // using

            TargetProductName = Properties.Resources.TargetProductName;
            UpdateProductName = Properties.Resources.UpdateProductName;

            Welcome:
            using (var dlg = new WizardWelcomeDialog())
            {
                switch (dlg.ShowDialog())
                {
                    case DialogResult.No: goto UiCulture;
                    case DialogResult.Cancel: goto End;
                } // switch
            } // using

            Eula:
            using (var dlg = new WizardEulaDialog())
            {
                switch (dlg.ShowDialog())
                {
                    case DialogResult.No: goto Welcome;
                    case DialogResult.Cancel: goto End;
                } // switch
            } // using

            Upgrade:
            endResult = DialogResult.OK;

            End:
            using (var dlg = new WizardEndDialog())
            {
                dlg.EndResult = endResult;
                dlg.ShowDialog();
            } // using
        }
    }
}

using Project.DvbIpTv.UiServices.Forms.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Project.DvbIpTv.ChannelList
{
    internal class MyApplicationContext : SplashApplicationContext
    {
        public int ExitCode
        {
            get;
            set;
        } // ExitCode

        #region SplashApplicationContext implementation

        protected override System.Drawing.Image SetupSplashScreen(System.Windows.Forms.Label progressLabel)
        {
            progressLabel.Location = new System.Drawing.Point(40, 320);
            progressLabel.Size = new System.Drawing.Size(320, 50);
            progressLabel.Text = Properties.InvariantTexts.SplashScreenDefaultStatus;

            return Properties.Resources.SplashScreenBackground;
        } // SetupSplashScreen

        /// <remarks>There's no need for a try/catch block, as the background worker takes care of any unhandled exception and makes it available in RunWorkerCompletedEventArgs</remarks>
        protected override object DoBackgroundWork()
        {
            // set culture (main thread & worker thread)
            CallForegroundAction(ForceUiCulture, Thread.CurrentThread, false);

            // load app config
            DisplayProgress(Properties.Texts.MyAppCtxLoadingConfig, true);
            if (!LoadConfiguration()) return -1;

            return 0;
        } // DoBackgroundWork

        protected override void BackgroundWorkCompleted(System.ComponentModel.RunWorkerCompletedEventArgs result)
        {
            if (result.Error != null)
            {
                MyApplication.HandleException(null, Properties.Texts.MyAppCtxExceptionCaption, Properties.Texts.MyAppCtxExceptionMsg, result.Error);
                ExitCode = -1;
                return;
            } // if
            if (result.Cancelled)
            {
                ExitCode = -2;
                return;
            } // if

            DisplayProgress(Properties.Texts.MyAppCtxStarting, false);
        } // BackgroundWorkCompleted

        protected override void DoDisplayMessage(IWin32Window splashScreen, string caption, string message, MessageBoxIcon icon)
        {
            MyApplication.HandleException(splashScreen, caption, message, icon, null);
        } // DoDisplayMessage

        protected override void DoDisplayException(IWin32Window splashScreen, string caption, string message, MessageBoxIcon icon, Exception exception)
        {
            MyApplication.HandleException(splashScreen, caption, message, icon, exception);
        } // DoDisplayException

        protected override Form GetMainForm()
        {
            return new ChannelListForm();
        } // GetMainForm

        #endregion

        #region Initialization methods

        private bool LoadConfiguration()
        {
            try
            {
                return MyApplication.LoadConfig();
            }
            catch (Exception ex)
            {
                DisplayException(Properties.Texts.MyAppLoadConfigExceptionCaption,
                    Properties.Texts.MyAppLoadConfigException,
                    MessageBoxIcon.Exclamation,
                    false, true, ex);
                return false;
            } // try-catch
        } // LoadConfiguration

        private static void ForceUiCulture(object data)
        {
            MyApplication.ForceUiCulture(Environment.GetCommandLineArgs(), Properties.Settings.Default.ForceUiCulture);
            var backgroundThread = data as Thread;
            backgroundThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            backgroundThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
        } // ForceUiCulture

        #endregion
    } // class MyApplicationContext
} // namespace

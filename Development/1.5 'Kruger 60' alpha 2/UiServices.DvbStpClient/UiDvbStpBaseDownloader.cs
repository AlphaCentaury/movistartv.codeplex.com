using Microsoft.SqlServer.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.DvbStpClient
{
    public abstract class UiDvbStpBaseDownloader
    {
        // "Input" properties

        public string CaptionUserCancelled
        {
            get;
            set;
        } // CaptionUserCancelled

        public string TextUserCancelled
        {
            get;
            set;
        } // TextUserCancelled

        public string CaptionDownloadException
        {
            get;
            set;
        } // CaptionDownloadException

        public string TextDownloadException
        {
            get;
            set;
        } // TextDownloadException

        public Action<IWin32Window, string, string, Exception> HandleException
        {
            get;
            set;
        } // HandleException

        // "Return" properties

        public bool IsOk
        {
            get;
            private set;
        } // IsOk

        public UiDvbStpBaseDownloader()
        {
            CaptionUserCancelled = Properties.Texts.HelperUserCancelledCaption;
            CaptionDownloadException = Properties.Texts.HelperExceptionCaption;
        } // constructor

        public void Download(IWin32Window owner)
        {
            var response = ShowDialog(owner);

            IsOk = ((response.UserCancelled == false) && (response.DownloadException == null));

            if (response.UserCancelled)
            {
                var box = new ExceptionMessageBox()
                {
                    Caption = CaptionUserCancelled,
                    Text = TextUserCancelled,
                    Beep = true,
                    Symbol = ExceptionMessageBoxSymbol.Information
                };
                box.Show(owner);

                return;
            } // if

            if (response.DownloadException != null)
            {
                if (HandleException != null)
                {
                    HandleException(owner, CaptionDownloadException, TextDownloadException, response.DownloadException);
                } // if

                return;
            } // if
        } // Download

        protected abstract UiDvbStpBaseDownloadResponse ShowDialog(IWin32Window owner);
    } // abstract class UiDvbStpBaseDownloader
} // namespace

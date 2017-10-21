using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using VTS.Shared.DomainObjects;
using VTSWeb.Common;
using VTSWeb.DomainObjects.Psa;
using VTSWeb.Localization;
using VTSWeb.Presentation.Psa;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.Presentation.Workspace.Common.TreeItems
{
    public partial class TraceTreeItemControl : UserControl
    {
        public TraceTreeItemControl()
        {
            InitializeComponent();
            this.MouseRightButtonDown += (s, e) =>
            {
                e.Handled = true;
            };
        }

        private void DeleteTraceClicked(object sender, RoutedEventArgs e)
        {
            Confirm();
        }

        private void Success()
        {
            string dataDeletedHeader =
                CodeBehindStringResolver.Resolve("DataDeletedText");
            string dataDeletedText =
                CodeBehindStringResolver.Resolve("SelectedDataDeletedText");
            MessageBox.Show(dataDeletedHeader,
                dataDeletedText, MessageBoxButton.OK);
        }

        private void Error(Exception e, string msg)
        {
            string error = CodeBehindStringResolver.Resolve("ErrorText");
            MessageBox.Show(error, msg, MessageBoxButton.OK);
        }

        private void Confirm()
        {
            DataDeletionConfirmationWindow window = 
                new DataDeletionConfirmationWindow();
            window.Closed += OnConfirmationWindowClosed;
            window.Show();
        }

        private void OnConfirmationWindowClosed(object sender, EventArgs e)
        {
            ChildWindow w = sender as ChildWindow;
            if (w.DialogResult != null && w.DialogResult.Value)
            {
                PsaTraceViewModel traceViewModel = DataContext as PsaTraceViewModel;
                if (traceViewModel == null)
                {
                    throw new ArgumentException("Wrong ViewModel type here!");
                }
                PsaTrace trace = traceViewModel.Model;
                VtsWebServiceClient service = new VtsWebServiceClient();
                service.DeleteTraceCompleted += OnTraceDeleted;
                service.DeleteTraceAsync(trace.Id, LoggedUserContext.LoggedUser.Login, 
                    LoggedUserContext.LoggedUser.PasswordHash);
            }
        }

        private void OnTraceDeleted(object s, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Error(e.Error, e.Error.Message);
            }
        }
    }
}

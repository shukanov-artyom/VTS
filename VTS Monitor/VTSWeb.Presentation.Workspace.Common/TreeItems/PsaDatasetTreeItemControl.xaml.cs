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
    public partial class PsaDatasetTreeItemControl : UserControl
    {
        public PsaDatasetTreeItemControl()
        {
            InitializeComponent();
            this.MouseRightButtonDown += (s, e) =>
            {
                e.Handled = true;
            };
        }

        private void DeleteDatasetClicked(object sender, RoutedEventArgs e)
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
            MessageBox.Show(msg);
        }

        private void Confirm()
        {
            DataDeletionConfirmationWindow w = 
                new DataDeletionConfirmationWindow();
            w.Closed += OnConfirmationWindowClosed;
            w.Show();
        }

        private void OnConfirmationWindowClosed(object sender, EventArgs e)
        {
            ChildWindow w = sender as ChildWindow;
            if (w.DialogResult != null && w.DialogResult.Value)
            {
                PsaDatasetViewModel traceViewModel =
                    DataContext as PsaDatasetViewModel;
                if (traceViewModel == null)
                {
                    throw new ArgumentException("Wrong ViewModel type here!");
                }
                PsaDataset dataset = traceViewModel.Model;
                VtsWebServiceClient service = new VtsWebServiceClient();
                service.DeleteDatasetCompleted += ServiceOnDeleteDatasetCompleted;
                service.DeleteDatasetAsync(dataset.Id, LoggedUserContext.LoggedUser.Login,
                    LoggedUserContext.LoggedUser.PasswordHash);
            }
        }

        private void ServiceOnDeleteDatasetCompleted(object sender, 
            AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Error(e.Error, e.Error.Message);
            }
        }
    }
}

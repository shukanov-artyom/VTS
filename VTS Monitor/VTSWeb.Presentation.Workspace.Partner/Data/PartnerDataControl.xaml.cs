using System;
using System.Windows;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Workspace.Partner.Data
{
    public partial class PartnerDataControl : NavigatablePage
    {
        public event EventHandler DataUploadClicked;
        public event EventHandler ViewUploadedDataClicked;
        public event EventHandler DataAnalysisClicked;
        public event EventHandler EventsOperationsClicked;

        public PartnerDataControl()
        {
            InitializeComponent();
            InitializeHeader("Data");
        }

        private void PartnerDataAnalysisButtonClick(object sender, 
            RoutedEventArgs e)
        {
            if (DataAnalysisClicked != null)
            {
                DataAnalysisClicked.Invoke(sender, e);
            }
        }

        private void PartnerUploadDataButtonClick(object sender, RoutedEventArgs e)
        {
            if (DataUploadClicked != null)
            {
                DataUploadClicked.Invoke(sender, e);
            }
        }

        private void PartnerSavedDataButtonClick(object sender, RoutedEventArgs e)
        {
            if (ViewUploadedDataClicked != null)
            {
                ViewUploadedDataClicked.Invoke(sender, e);
            }
        }

        private void EventsClicked(object sender, RoutedEventArgs e)
        {
            if (EventsOperationsClicked != null)
            {
                EventsOperationsClicked.Invoke(sender, e);
            }
        }
    }
}

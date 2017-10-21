using System;
using System.Windows;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Workspace.Client
{
    public partial class ClientDesktopControl : NavigatablePage
    {
        public event EventHandler SavedDataClicked;
        public event EventHandler DataAnalysisClicked;
        public event EventHandler ServiceLogClicked;

        public ClientDesktopControl()
        {
            InitializeComponent();
            InitializeHeader("Desktop");
        }

        private void SavedDataButtonClick(object sender, RoutedEventArgs e)
        {
            if (SavedDataClicked != null)
            {
                SavedDataClicked.Invoke(this, e);
            }
        }

        private void DataAnalysisButtonClicked(object sender, RoutedEventArgs e)
        {
            if (DataAnalysisClicked != null)
            {
                DataAnalysisClicked.Invoke(this, e);
            }
        }

        private void ButtonServiceLogClicked(object sender, RoutedEventArgs e)
        {
            if (ServiceLogClicked != null)
            {
                ServiceLogClicked.Invoke(this, e);
            }
        }
    }
}

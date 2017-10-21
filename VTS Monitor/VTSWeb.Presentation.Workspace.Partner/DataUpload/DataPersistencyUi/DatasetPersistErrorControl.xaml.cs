using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Presentation.Workspace.Partner.DataUpload.DataPersistencyUi
{
    public partial class DatasetPersistErrorControl : UserControl
    {
        public event EventHandler OkClicked;

        public DatasetPersistErrorControl()
        {
            InitializeComponent();
        }

        private void OkButtonClickOnError(object sender, RoutedEventArgs e)
        {
            if (OkClicked != null)
            {
                OkClicked.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

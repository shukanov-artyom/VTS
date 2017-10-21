using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Presentation.Workspace.Partner.DataUpload.DataPersistencyUi
{
    public partial class DataPersistedControl : UserControl
    {
        public event EventHandler OkClicked;

        public DataPersistedControl()
        {
            InitializeComponent();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            if (OkClicked != null)
            {
                OkClicked.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

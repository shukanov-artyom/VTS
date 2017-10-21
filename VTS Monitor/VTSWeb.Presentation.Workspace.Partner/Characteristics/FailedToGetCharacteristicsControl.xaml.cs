using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Presentation.Workspace.Partner.Characteristics
{
    public partial class FailedToGetCharacteristicsControl : UserControl
    {
        public event EventHandler RetryClicked;

        public FailedToGetCharacteristicsControl()
        {
            InitializeComponent();
        }

        public FailedToGetCharacteristicsControl(string msg)
            : this()
        {
            textBlockErrorMessage.Text = msg;
        }

        private void OnRetryClicked(object sender, RoutedEventArgs e)
        {
            if (RetryClicked != null)
            {
                RetryClicked.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

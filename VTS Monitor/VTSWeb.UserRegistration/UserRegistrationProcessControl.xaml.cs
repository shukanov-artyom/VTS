using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.UserRegistration
{
    public partial class UserRegistrationProcessControl : UserControl
    {
        public UserRegistrationProcessControl()
        {
            InitializeComponent();
        }

        public void SetFinishedState()
        {
            progressBar.Visibility = Visibility.Collapsed;
            stackPanelFinished.Visibility = Visibility.Visible;
        }
    }
}

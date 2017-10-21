using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.UserRegistration.ClientRegistration
{
    public partial class ClientRegistrationWindow : ChildWindow
    {
        public ClientRegistrationWindow()
        {
            InitializeComponent();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}


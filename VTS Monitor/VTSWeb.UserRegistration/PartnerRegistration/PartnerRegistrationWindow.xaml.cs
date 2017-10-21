using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.UserRegistration.PartnerRegistration
{
    public partial class PartnerRegistrationWindow : ChildWindow
    {
        public PartnerRegistrationWindow()
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


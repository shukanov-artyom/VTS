using System;
using System.Windows;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Workspace.Partner
{
    public partial class PartnerDesktopControl : NavigatablePage
    {
        public event EventHandler ClientsClicked;
        public event EventHandler PartnerDataClicked;
        public event EventHandler VehiclesClicked;

        public PartnerDesktopControl()
        {
            InitializeComponent();
            InitializeHeader("Desktop");
        }

        private void VehiclesButtonClicked(object sender, RoutedEventArgs e)
        {
            if (VehiclesClicked != null)
            {
                VehiclesClicked.Invoke(this, EventArgs.Empty);
            }
        }

        private void ClientsButtonClick(object sender, RoutedEventArgs e)
        {
            if (ClientsClicked != null)
            {
                ClientsClicked.Invoke(this, EventArgs.Empty);
            }
        }

        private void PartnerDataButtonClick(object sender, RoutedEventArgs e)
        {
            if (PartnerDataClicked != null)
            {
                PartnerDataClicked.Invoke(sender, e);
            }
        }
    }
}

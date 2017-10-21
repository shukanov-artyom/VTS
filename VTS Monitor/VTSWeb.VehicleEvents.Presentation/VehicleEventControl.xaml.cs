using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.VehicleEvents.Presentation
{
    public partial class VehicleEventControl : UserControl
    {
        public VehicleEventControl()
        {
            InitializeComponent();
        }

        private void OnRemindDateUnchecked(object sender, RoutedEventArgs e)
        {
            VehicleEventViewModel viewModel = 
                DataContext as VehicleEventViewModel;
            viewModel.RedMileage = null;
        }
    }
}

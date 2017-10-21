using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Agent.Common.Presentation.Vehicles;

namespace Agent.Workspace.Views.Clienting
{
    /// <summary>
    /// Interaction logic for VehicleSelectionWindow.xaml
    /// </summary>
    public partial class VehicleSelectionWindow : Window
    {
        private readonly ObservableCollection<VehicleViewModel> vehicles;

        private VehicleViewModel selectedVehicle;

        public VehicleSelectionWindow()
        {
            InitializeComponent();
        }

        public VehicleViewModel SelectedVehicle
        {
            get
            {
                return selectedVehicle;
            }
        }

        public VehicleSelectionWindow(ObservableCollection<VehicleViewModel> vehicles)
            : this()
        {
            this.vehicles = vehicles;
            comboBoxVehicles.ItemsSource = vehicles;
        }

        private void OnVehicleSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            selectedVehicle = e.AddedItems[0] as VehicleViewModel;
            buttonOk.IsEnabled = true;
        }

        private void OnOkClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}

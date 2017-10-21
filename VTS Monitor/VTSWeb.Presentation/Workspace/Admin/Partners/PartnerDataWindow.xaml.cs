using System;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.Vehicles;
using VTSWeb.Presentation.Graph;
using VTSWeb.Presentation.Workspace.Common;
using VTSWeb.Presentation.Workspace.Common.Data;

namespace VTSWeb.Presentation.Workspace.Admin.Partners
{
    public partial class PartnerDataWindow : ChildWindow
    {
        private VTSWeb.DomainObjects.Partner partner;
        private SavedDataPresenter presenter;

        public PartnerDataWindow()
        {
            InitializeComponent();
        }

        public PartnerDataWindow(VTSWeb.DomainObjects.Partner partner)
            : this()
        {
            this.partner = partner;
            VehicleSelectionControl vehicleSelectionControl =
                controlVehicleSelection.InnerContent as VehicleSelectionControl;
            if (vehicleSelectionControl == null)
            {
                throw new Exception("Wrong control at place");
            }
            vehicleSelectionControl.InitializeVehicles(partner);
            vehicleSelectionControl.VehicleSelected += OnVehicleSelected;
            UpdateControlSize();
            ((VehicleDatasetsTreeControl)treeControlVehicleDatasets.
                InnerContent).UpdateComplete += OnTreeUpdateComplete;
            presenter = new SavedDataPresenter(
                (VehicleDatasetsTreeControl)treeControlVehicleDatasets.InnerContent,
                (DataGraphsControl)controlUpperLowerControls.InnerContent);
        }

        private void OnVehicleSelected(
            object sender, SelectionChangedEventArgs e)
        {
            SetWaitingMode(true);
            VehicleViewModel selectedVehicleViewModel =
                e.AddedItems[0] as VehicleViewModel;
            ((VehicleDatasetsTreeControl)treeControlVehicleDatasets.InnerContent).
                    UpdateForVehicle(selectedVehicleViewModel.Model);
        }

        private void OnOkClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void UpdateControlSize()
        {
            treeControlVehicleDatasets.Height =
                ApplicationSizeKeeper.Height - 350;
            treeControlVehicleDatasets.InnerContent.Width =
                ApplicationSizeKeeper.Width * 2 / 5 -380;
            controlUpperLowerControls.Height =
                treeControlVehicleDatasets.Height;
            controlUpperLowerControls.InnerContent.Width =
                ApplicationSizeKeeper.Width * 3 / 5 - 260;
        }

        private void OnTreeUpdateComplete(object sender, EventArgs e)
        {
            SetWaitingMode(false);
        }

        private void SetWaitingMode(bool wait)
        {
            controlVehicleSelection.IsEnabled = !wait;
        }
    }
}


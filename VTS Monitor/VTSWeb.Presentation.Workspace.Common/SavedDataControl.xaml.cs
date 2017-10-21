using System;
using System.Windows.Controls;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.Presentation.Common.Vehicles;
using VTSWeb.Presentation.Graph;
using VTSWeb.Presentation.Workspace.Common.Data;

namespace VTSWeb.Presentation.Workspace.Common
{
    public partial class SavedDataControl : NavigatablePage
    {
        private VehicleViewModel selectedVehicle;
        private SavedDataPresenter presenter;

        public SavedDataControl()
        {
            InitializeComponent();
            InitializeHeader("Data");
            ((VehicleSelectionControl)controlVehicleSelection.InnerContent).
                VehicleSelected += SelectedVehicleChanged;
            ApplicationSizeKeeper.AppResized += OnAppSizeChanged;
            UpdateControlSize();
            ((VehicleDatasetsTreeControl)treeControlVehicleDatasets.
                InnerContent).UpdateComplete += OnTreeUpdateComplete;
            presenter = new SavedDataPresenter(
                (VehicleDatasetsTreeControl)treeControlVehicleDatasets.
                    InnerContent,
                (DataGraphsControl)controlUpperLowerControls.
                    InnerContent);
        }

        private void SelectedVehicleChanged(object sender,
            SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                SetWaitingMode(true);
                selectedVehicle = e.AddedItems[0] as VehicleViewModel;
                ((VehicleDatasetsTreeControl)treeControlVehicleDatasets.InnerContent).
                    UpdateForVehicle(selectedVehicle.Model);
            }
            else
            {
                ((VehicleDatasetsTreeControl)treeControlVehicleDatasets.
                    InnerContent).UpdateForVehicle(null);
            }
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow wnd = new ErrorWindow(e, msg);
            wnd.Closed += DialogWindowStatus.OnDialogClosed;
            wnd.Show();
        }

        private void OnAppSizeChanged(object sender, EventArgs e)
        {
            UpdateControlSize();
        }

        private void UpdateControlSize()
        {
            double height = ApplicationSizeKeeper.Height - 210;

            double totalWidth = ApplicationSizeKeeper.Width;
            double something = 28;
            double leftWidth = totalWidth * 2 / 5 - something;
            double rightWidth = totalWidth * 3 / 5 - something;

            treeControlVehicleDatasets.Height = height;
            controlUpperLowerControls.Height = height;

            treeControlVehicleDatasets.InnerContent.Width = leftWidth;
            controlUpperLowerControls.InnerContent.Width = rightWidth;
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

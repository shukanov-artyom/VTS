using System;
using System.Windows.Controls;
using VTSWeb.AnalysisCore;
using VTSWeb.AnalysisCore.Interfaces;
using VTSWeb.AnalysisCore.Presentation;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.Presentation.Common.Vehicles;

namespace VTSWeb.Presentation.Workspace.Common.Analytic
{
    public partial class AutomaticDataAnalysisControl : NavigatablePage
    {
        private VehicleViewModel selectedVehicle;

        public AutomaticDataAnalysisControl()
        {
            InitializeComponent();
            InitializeHeader("AutomaticDataAnalysis");
            VehSelectionControl.VehicleSelected += OnVehicleSelected;
            ApplicationSizeKeeper.AppResized += OnAppResized;
            UpdateSize();
        }

        private VehicleSelectionControl VehSelectionControl
        {
            get
            {
                return (VehicleSelectionControl)controlVehicleSelection.
                    InnerContent;
            }
        }

        private VehicleAnalyticModelControl AnalyticModelView
        {
            get
            {
                return controlAnalysisDataPlaceholder.
                    InnerContent as VehicleAnalyticModelControl;
            }
        }

        private void OnVehicleSelected(object sender, 
            SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                SetWaitingMode(true);
                selectedVehicle = e.AddedItems[0] as VehicleViewModel;
                InitializeTheAnalysisCore();
            }
        }

        private void InitializeTheAnalysisCore()
        {
            Process();
        }

        private void Process()
        {
            AnalysisEngine engine = new AnalysisEngine(selectedVehicle.Model,
                EngineFinished, OnError);
            engine.Start();
        }

        private void EngineFinished(IVehicleAnalyticModel vehicleAnalyticModel)
        {
            SetWaitingMode(false);
            AnalyticItemViewModel vm = 
                new AnalyticItemViewModel(vehicleAnalyticModel);
            AnalyticModelView.DataContext = vm;
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow w = new ErrorWindow(e, msg);
            w.Show();
            controlVehicleSelection.SetWaitingMode(false);
        }

        private void SetWaitingMode(bool wait)
        {
            controlVehicleSelection.SetWaitingMode(wait);
        }

        private void OnAppResized(object sender, EventArgs e)
        {
            UpdateSize();
        }

        private void UpdateSize()
        {
            AnalyticModelView.Height = ApplicationSizeKeeper.Height - 240;
            AnalyticModelView.Width = ApplicationSizeKeeper.Width - 40;
        }
    }
}

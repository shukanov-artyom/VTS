using System;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Workspace.Common;
using VTSWeb.Presentation.Workspace.Common.Analytic;
using VTSWeb.VehicleEvents.Presentation;

namespace VTSWeb.Presentation.Workspace.Client
{
    public partial class ClientWorkspaceControl : UserControl
    {
        private ClientDesktopControl desktop =
            new ClientDesktopControl();
        private WorkspaceController controller;

        private SavedDataControl partnerSavedDataControl;
        private DataAnalysisTypeSelectionControl analysisSelectionControl;
        private AutomaticDataAnalysisControl automaticDataAnalysisControl;
        private ManualDataAnalysisControl manualDataAnalysisControl;
        private VehicleEventsControl serviceLogControl;

        public ClientWorkspaceControl()
        {
            InitializeComponent();

            controller = new WorkspaceController(
                contentControlWorkspace, buttonForward, buttonBackward,
                textBlockForward, textBlockBackwards, 
                textBlockCurrentPageHeader);
            controller.NavigateToPage(desktop);
            desktop.DataAnalysisClicked += OnDataAnalysisClicked;
            desktop.SavedDataClicked += OnSavedDataClicked;
            desktop.ServiceLogClicked += OnVehicleEventsClicked;
        }

        private void ButtonBackwardsClick(object sender, RoutedEventArgs e)
        {
            controller.NavigateBack();
        }

        private void ButtonForwardClick(object sender, RoutedEventArgs e)
        {
            controller.NavigateForward();
        }

        private void OnDataAnalysisClicked(object sender, EventArgs e)
        {
            if (analysisSelectionControl == null)
            {
                analysisSelectionControl =
                    new DataAnalysisTypeSelectionControl();
                analysisSelectionControl.AutomaticParametersMonitoringSelected +=
                    OnAutomaticAnalysisSelected;
                analysisSelectionControl.ManualParametersMonitoringSelected +=
                    OnManualAnalysisSelected;
            }
            NavigateToPage(analysisSelectionControl);
        }

        private void OnSavedDataClicked(object sender, EventArgs e)
        {
            if (partnerSavedDataControl == null)
            {
                partnerSavedDataControl = new SavedDataControl();
            }
            NavigateToPage(partnerSavedDataControl);
        }

        private void OnVehicleEventsClicked(object sender, EventArgs e)
        {
            if (serviceLogControl == null)
            {
                serviceLogControl = new VehicleEventsControl();
            }
            NavigateToPage(serviceLogControl);
        }

        private void OnAutomaticAnalysisSelected(object s, EventArgs e)
        {
            if (automaticDataAnalysisControl == null)
            {
                automaticDataAnalysisControl =
                    new AutomaticDataAnalysisControl();
            }
            controller.NavigateToPage(automaticDataAnalysisControl);
        }

        private void OnManualAnalysisSelected(object s, EventArgs e)
        {
            if (manualDataAnalysisControl == null)
            {
                manualDataAnalysisControl =
                    new ManualDataAnalysisControl();
            }
            controller.NavigateToPage(manualDataAnalysisControl);
        }

        private void NavigateToPage(NavigatablePage page)
        {
            controller.NavigateToPage(page);
        }
    }
}

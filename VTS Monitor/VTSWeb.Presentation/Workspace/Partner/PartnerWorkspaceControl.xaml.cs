using System;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.Presentation.Workspace.Common;
using VTSWeb.Presentation.Workspace.Common.Analytic;
using VTSWeb.Presentation.Workspace.Partner.Data;
using VTSWeb.Presentation.Workspace.Partner.DataUpload;
using VTSWeb.Presentation.Workspace.Partner.EndUsers;
using VTSWeb.Presentation.Workspace.Partner.Vehicles;
using VTSWeb.VehicleEvents.Presentation;

namespace VTSWeb.Presentation.Workspace.Partner
{
    public partial class PartnerWorkspaceControl : UserControl
    {
        private PartnerDesktopControl desktop = new PartnerDesktopControl();
        private WorkspaceController controller;
        private ClientsControl clientsControl;
        private PartnerDataControl partnerDataControl;
        private SavedDataControl datacontrol;
        private DataAnalysisTypeSelectionControl dataAnalysisTypeSelectionControl;
        private AutomaticDataAnalysisControl automaticDataAnalysisControl;
        private ManualDataAnalysisControl manualDataAnalysisControl;
        private PartnerDataUploadControl uploadDataControl;
        private VehiclesControl vehiclesControl;
        private VehicleEventsControl vehicleEventsControl;

        public PartnerWorkspaceControl()
        {
            InitializeComponent();
            desktop.VehiclesClicked += ShowVehicles;
            desktop.ClientsClicked += ShowClients;
            desktop.PartnerDataClicked += ShowData;

            controller = new WorkspaceController(
                contentControlWorkspace, buttonForward, buttonBackward, 
                textBlockForward, textBlockBackwards, 
                textBlockCurrentPageHeader);
            controller.NavigateToPage(desktop);
        }

        private void ShowClients(object sender, EventArgs e)
        {
            if (clientsControl == null)
            {
                clientsControl = new ClientsControl();
            }
            controller.NavigateToPage(clientsControl);
        }

        private void ShowData(object sender, EventArgs e)
        {
            if (partnerDataControl == null)
            {
                partnerDataControl = new PartnerDataControl();
                partnerDataControl.DataAnalysisClicked += ShowDataAnalysis;
                partnerDataControl.DataUploadClicked += ShowUploadData;
                partnerDataControl.EventsOperationsClicked += ShowEvents;
                partnerDataControl.ViewUploadedDataClicked += ShowSavedData;
            }
            controller.NavigateToPage(partnerDataControl);
        }

        private void ShowSavedData(object sender, EventArgs e)
        {
            if (datacontrol == null)
            {
                datacontrol = new SavedDataControl();
            }
            controller.NavigateToPage(datacontrol);
        }

        private void ShowEvents(object sender, EventArgs e)
        {
            if (vehicleEventsControl == null)
            {
                vehicleEventsControl = new VehicleEventsControl();
            }
            controller.NavigateToPage(vehicleEventsControl);
        }

        private void ShowDataAnalysis(object sender, EventArgs e)
        {
            if (dataAnalysisTypeSelectionControl == null)
            {
                dataAnalysisTypeSelectionControl = new DataAnalysisTypeSelectionControl();
                dataAnalysisTypeSelectionControl.AutomaticParametersMonitoringSelected +=
                    ShowAutomaticParametersMonitoring;
                dataAnalysisTypeSelectionControl.ManualParametersMonitoringSelected +=
                    ShowManualParametersMonitoring;
            }
            controller.NavigateToPage(dataAnalysisTypeSelectionControl);
        }

        private void ShowAutomaticParametersMonitoring(
            object sender, EventArgs e)
        {
            if (automaticDataAnalysisControl == null)
            {
                automaticDataAnalysisControl = 
                    new AutomaticDataAnalysisControl();
            }
            controller.NavigateToPage(automaticDataAnalysisControl);
        }

        private void ShowManualParametersMonitoring(object sender, EventArgs e)
        {
            if (manualDataAnalysisControl == null)
            {
                manualDataAnalysisControl = new ManualDataAnalysisControl();
            }
            controller.NavigateToPage(manualDataAnalysisControl);
        }

        private void ShowUploadData(object sender, EventArgs e)
        {
            if (uploadDataControl == null)
            {
                uploadDataControl = new PartnerDataUploadControl();
            }
            controller.NavigateToPage(uploadDataControl);
        }

        private void ShowVehicles(object sender, EventArgs e)
        {
            if (vehiclesControl == null)
            {
                vehiclesControl = new VehiclesControl();
            }
            controller.NavigateToPage(vehiclesControl);
        }

        private void ButtonBackwardsClick(object sender, RoutedEventArgs e)
        {
            controller.NavigateBack();
        }

        private void ButtonForwardClick(object sender, RoutedEventArgs e)
        {
            controller.NavigateForward();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Agent.Common.Instance;
using Agent.Connector.Presentation.PSA.Workspace;
using Agent.Workspace.ViewModels;

namespace Agent.Workspace.Filtering
{
    internal class PsaPreExportDataValidator
    {
        private readonly IEnumerable<ExportableVehicleViewModel> byVehicles;

        public PsaPreExportDataValidator(
            IEnumerable<ExportableVehicleViewModel> byVehicles)
        {
            this.byVehicles = byVehicles;
        }

        public bool Validate()
        {
            foreach (ExportableVehicleViewModel vehicleViewModel in byVehicles)
            {
                foreach (ExportablePsaTraceViewModel trace in vehicleViewModel.
                    Traces.Where(t => t.IsSelectedForExport))
                {
                    if (trace.Mileage == 0)
                    {
                        ShowWarning();
                        return false;
                    }
                }
            }
            return true;
        }

        private void ShowWarning()
        {
            MileageAbsenceWarningWindow window = 
                new MileageAbsenceWarningWindow();
            window.Owner = MainWindowKeeper.MainWindowInstance as Window;
            window.ShowDialog();
        }
    }
}

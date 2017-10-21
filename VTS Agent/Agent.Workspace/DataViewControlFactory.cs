using System;
using System.Windows.Controls;
using Agent.Common.Presentation;
using Agent.Common.Presentation.Controls;
using Agent.Common.Presentation.Data;
using Agent.Connector.Presentation.PSA.Workspace;
using Agent.Workspace.ViewModels;
using Agent.Workspace.Views;

namespace Agent.Workspace
{
    public class DataViewControlFactory
    {
        private readonly PsaParameterDataViewModel param;
        private readonly ExportableVehicleViewModel veh;
        private readonly IPsaParametersSetViewModel pset;
        private readonly ExportablePsaTraceViewModel trace;

        public DataViewControlFactory(ViewModelBase vm)
        {
            if (vm == null)
            {
                throw new ArgumentNullException("vm");
            }
            param = vm as PsaParameterDataViewModel;
            veh = vm as ExportableVehicleViewModel;
            pset = vm as IPsaParametersSetViewModel;
            trace = vm as ExportablePsaTraceViewModel;
        }

        public Control CreateDataSettingsView()
        {
            if (param != null)
            {
                return new ParameterDetailsControl(param);
            }
            if (pset != null)
            {
                return new PsaParametersSetDetailsControl(pset);
            }
            if (veh != null || trace != null)
            {
                return null;
            }
            throw new NotImplementedException();
        }

        public Control CreateDataView(Control settingsControl)
        {
            if (param != null)
            {
                return new PsaParameterDataGraphControl{ DataContext = param};
            }
            if (pset != null)
            {
                return new PsaParametersSetTabbedView(
                    settingsControl as PsaParametersSetDetailsControl);
            }
            if (veh != null || trace != null)
            {
                return null;
            }
            throw new NotImplementedException();
        }
    }
}

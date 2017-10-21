using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Agent.Common.Presentation;
using Agent.Common.Presentation.Vehicles;
using Agent.Logging;
using Agent.Network.Monitor.VtsWebService;
using Agent.Workspace.ViewModels.Chronology;
using VTS.Shared;

namespace Agent.Workspace.ViewModels
{
    public class ChronologyDataViewModel : VehicleSelectionViewModel
    {
        private ChronoFolderViewModel rootFolder;

        public ObservableCollection<ViewModelBase> RootFolder
        {
            get
            {
                if (rootFolder == null)
                {
                    return null;
                }
                return rootFolder.Children;
            }
        }

        protected override void OnVehicleSelected()
        {
            UpdateChronoParametersTreeForVehicle(SelectedVehicle);
        }

        private void UpdateChronoParametersTreeForVehicle(VehicleViewModel vehicle)
        {
            rootFolder = new ChronoFolderViewModel("Root", vehicle.Vin);
            OnPropertyChanged("RootFolder");
            try
            {
                rootFolder.Children.Clear();
                VtsWebServiceClient service = new VtsWebServiceClient();
                List<AnalyticRuleType> types = service.
                    GetAvailableAnalyticStatisticsTypesForVehicle(vehicle.Vin).
                    Select(r => (AnalyticRuleType) r).ToList();
                foreach (AnalyticRuleType ruleType in types)
                {
                    RuleTypeTreePathResolver resolver =
                        new RuleTypeTreePathResolver(ruleType);
                    rootFolder.PutParameter(resolver.GetSplitPath(), ruleType);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Was unable to retrieve chronological data for vehicle.");
            }
            finally
            {
                StopWaiting();
            }
        }
    }
}

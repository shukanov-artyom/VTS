using System;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Models.Settings.Persistency.PetrolEngineIgnition;
using VTSWeb.AnalysisCore.Models.Settings.PetrolEngineIgnition;
using VTSWeb.AnalysisCore.Recognition;

namespace VTSWeb.AnalysisCore.Models.PetrolEngineIgnition
{
    public class AnalyticModelFactoryPetrolEngineIgnition : AnalyticModelFactoryBase
    {
        private AnalyticModelSettingsFetchingFactoryPetrolEngineIgnition settingsFetcher;

        public AnalyticModelFactoryPetrolEngineIgnition(VehicleInformation vehicleInformation,
            AnalyticModelFactoryFinishedCallback callback)
            : base(callback)
        {
            settingsFetcher = 
                new AnalyticModelSettingsFetchingFactoryPetrolEngineIgnition(
                    vehicleInformation, SettingsFetched, SettingsFetchFailed);
        }

        public override void CreateAsync()
        {
            settingsFetcher.FetchAsync();
        }

        private void SettingsFetched(AnalyticModelSettings settings)
        {
            Result = new AnalyticModelPetrolEngineIgnition(settings 
                as AnalyticModelSettingsPetrolEngineIgnition);
            Finished = true;
            HasError = false;
            Error = null;
            Callback.Invoke(this);
        }

        private void SettingsFetchFailed(Exception e, string msg)
        {
            Finished = true;
            HasError = true;
            Error = e;
            Result = null;
            Callback.Invoke(this);
        }
    }
}

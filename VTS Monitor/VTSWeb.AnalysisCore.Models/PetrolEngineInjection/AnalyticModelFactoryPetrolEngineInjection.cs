using System;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Models.Settings.Persistency.PetrolEngineInjection;
using VTSWeb.AnalysisCore.Models.Settings.PetrolEngineInjection;
using VTSWeb.AnalysisCore.Recognition;

namespace VTSWeb.AnalysisCore.Models.PetrolEngineInjection
{
    public class AnalyticModelFactoryPetrolEngineInjection : AnalyticModelFactoryBase
    {
        private AnalyticModelSettingsFetchingFactoryPetrolEngineInjection settingsFetcher;

        public AnalyticModelFactoryPetrolEngineInjection(
            VehicleInformation information, 
            AnalyticModelFactoryFinishedCallback callback)
            : base(callback)
        {
            settingsFetcher = new AnalyticModelSettingsFetchingFactoryPetrolEngineInjection(
                information, SettingsFetched, SettingsFetchFailed);
        }

        public override void CreateAsync()
        {
            settingsFetcher.FetchAsync();
        }

        private void SettingsFetched(AnalyticModelSettings settings)
        {
            Result = new AnalyticModelPetrolEngineInjection(settings 
                as AnalyticModelSettingsPetrolEngineInjection);
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

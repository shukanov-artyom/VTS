using System;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Models.Settings.Persistency.PetrolEnginePurification;
using VTSWeb.AnalysisCore.Models.Settings.PetrolEnginePurification;
using VTSWeb.AnalysisCore.Recognition;

namespace VTSWeb.AnalysisCore.Models.PetrolEnginePurification
{
    public class AnalyticModelFactoryPetrolEnginePurification :
        AnalyticModelFactoryBase
    {
        private AnalyticModelSettingsFetchingFactoryPetrolEnginePurification
            settingsFetcher;

        public AnalyticModelFactoryPetrolEnginePurification(
            VehicleInformation vehicleInformation, 
            AnalyticModelFactoryFinishedCallback callback)
            : base(callback)
        {
            settingsFetcher =
            new AnalyticModelSettingsFetchingFactoryPetrolEnginePurification(
                vehicleInformation, SettingsFetched, SettingsFetchFailed);
        }

        public override void CreateAsync()
        {
            settingsFetcher.FetchAsync();
        }

        private void SettingsFetched(AnalyticModelSettings settings)
        {
            Result = new AnalyticModelPetrolEnginePurification(settings 
                as AnalyticModelSettingsPetrolEnginePurification);
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

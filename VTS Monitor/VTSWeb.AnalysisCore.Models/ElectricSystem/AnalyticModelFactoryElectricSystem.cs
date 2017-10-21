using System;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Models.Settings.ElectricSystem;
using VTSWeb.AnalysisCore.Models.Settings.Persistency.ElectricSystem;
using VTSWeb.AnalysisCore.Recognition;

namespace VTSWeb.AnalysisCore.Models.ElectricSystem
{
    public class AnalyticModelFactoryElectricSystem : AnalyticModelFactoryBase
    {
        private AnalyticModelSettingsFetchingFactoryElectricSystem
            settingsFetcher;

        private VehicleInformation vehicleInformation;

        public AnalyticModelFactoryElectricSystem(
            VehicleInformation vehicleInformation, 
            AnalyticModelFactoryFinishedCallback callback)
            : base(callback)
        {
            this.vehicleInformation = vehicleInformation;
            settingsFetcher = new AnalyticModelSettingsFetchingFactoryElectricSystem(
                vehicleInformation, SettingsFetched, SettingsFetchFailed);
        }

        public override void CreateAsync()
        {
            settingsFetcher.FetchAsync();
        }

        private void SettingsFetched(AnalyticModelSettings settings)
        {
            Result = new AnalyticModelElectricSystem(settings 
                as AnalyticModelSettingsElectricSystem);
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

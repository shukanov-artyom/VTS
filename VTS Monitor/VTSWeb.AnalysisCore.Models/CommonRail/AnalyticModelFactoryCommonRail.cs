using System;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Models.Settings.CommonRail;
using VTSWeb.AnalysisCore.Models.Settings.Persistency.CommonRail;
using VTSWeb.AnalysisCore.Recognition;

namespace VTSWeb.AnalysisCore.Models.CommonRail
{
    public class AnalyticModelFactoryCommonRail : AnalyticModelFactoryBase
    {
        private AnalyticModelSettingsFetchingFactoryCommonRail settingsFetcher;

        public AnalyticModelFactoryCommonRail(
            VehicleInformation vehicleInformation, 
            AnalyticModelFactoryFinishedCallback callback)
            : base(callback)
        {
            settingsFetcher = 
                new AnalyticModelSettingsFetchingFactoryCommonRail(
                    vehicleInformation, SettingsFetched, SettingsFetchFailed);
        }

        public override void CreateAsync()
        {
            settingsFetcher.FetchAsync();
        }

        private void SettingsFetched(AnalyticModelSettings settings)
        {
            Result = new AnalyticModelCommonRail(settings as
                AnalyticModelSettingsCommonRail);
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

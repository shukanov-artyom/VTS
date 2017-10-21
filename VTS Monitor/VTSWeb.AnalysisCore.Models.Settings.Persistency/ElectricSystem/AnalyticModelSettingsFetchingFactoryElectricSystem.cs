using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTSWeb.AnalysisCore.Models.Settings.ElectricSystem;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.Common;


namespace VTSWeb.AnalysisCore.Models.Settings.Persistency.ElectricSystem
{
    public class AnalyticModelSettingsFetchingFactoryElectricSystem :
        AnalyticModelSettingsFetchingFactoryBase
    {
        private AnalyticModelSettingsFetchedCallback callback;

        public AnalyticModelSettingsFetchingFactoryElectricSystem(
            VehicleInformation vehicleInformation,
            AnalyticModelSettingsFetchedCallback callback,
            ErrorCallbackDelegate errorCallback)
            :base (errorCallback, vehicleInformation)
        {
            this.callback = callback;
        }

        protected override void SuccessfullyFetched(IList<AnalyticRuleSettings> ruleSettings)
        {
            AnalyticModelSettingsElectricSystem result =
                new AnalyticModelSettingsElectricSystem(ruleSettings);
            callback.Invoke(result);
        }

        protected override IList<AnalyticRuleType> RequiredRuleTypes
        {
            get
            {
                return AnalyticModelSettingsElectricSystem.
                    GetRequiredRuleTypes().ToList();
            }
        }
    }
}

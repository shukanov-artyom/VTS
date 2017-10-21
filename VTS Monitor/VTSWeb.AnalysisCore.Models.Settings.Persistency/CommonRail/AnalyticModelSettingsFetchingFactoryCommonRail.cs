using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTSWeb.AnalysisCore.Models.Settings.CommonRail;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.AnalysisCore.Recognition.Engines;
using VTSWeb.Common;


namespace VTSWeb.AnalysisCore.Models.Settings.Persistency.CommonRail
{
    public class AnalyticModelSettingsFetchingFactoryCommonRail :
        AnalyticModelSettingsFetchingFactoryBase
    {
        private AnalyticModelSettingsFetchedCallback successCallback;

        public AnalyticModelSettingsFetchingFactoryCommonRail(
            VehicleInformation vehicleInformation,
            AnalyticModelSettingsFetchedCallback successCallback,
            ErrorCallbackDelegate errorCallback)
            : base(errorCallback, vehicleInformation)
        {
            if (vehicleInformation.Engine.InjectionType 
                != InjectionType.CommonRail)
            {
                throw new Exception(@"Unexpected injection type!. 
                    Engine should be common rail!");
            }
            this.successCallback = successCallback;
        }

        protected override void SuccessfullyFetched(IList<AnalyticRuleSettings> settings)
        {
            AnalyticModelSettingsCommonRail result = 
                new AnalyticModelSettingsCommonRail(settings);
            successCallback.Invoke(result);
        }

        protected override IList<AnalyticRuleType> RequiredRuleTypes
        {
            get
            {
                return AnalyticModelSettingsCommonRail.
                    GetRequiredRuleTypes().ToList();
            }
        }
    }
}

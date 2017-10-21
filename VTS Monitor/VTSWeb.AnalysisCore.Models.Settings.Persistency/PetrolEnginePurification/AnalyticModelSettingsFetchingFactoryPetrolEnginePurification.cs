using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTSWeb.AnalysisCore.Models.Settings.PetrolEnginePurification;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.AnalysisCore.Recognition.Engines;
using VTSWeb.Common;


namespace VTSWeb.AnalysisCore.Models.Settings.Persistency.PetrolEnginePurification
{
    public class AnalyticModelSettingsFetchingFactoryPetrolEnginePurification
        : AnalyticModelSettingsFetchingFactoryBase
    {
        private AnalyticModelSettingsFetchedCallback successCallback;

        public AnalyticModelSettingsFetchingFactoryPetrolEnginePurification(
            VehicleInformation vehicleInformation,
            AnalyticModelSettingsFetchedCallback successCallback,
            ErrorCallbackDelegate errorCallback)
            : base(errorCallback, vehicleInformation)
        {
            if (vehicleInformation.Engine.FuelType != FuelType.Petrol)
            {
                throw new Exception(@"Should be petrol engine!");
            }
            this.successCallback = successCallback;
        }

        protected override IList<AnalyticRuleType> RequiredRuleTypes
        {
            get
            {
                return AnalyticModelSettingsPetrolEnginePurification.
                    GetRequiredRuleTypes().ToList();
            }
        }

        protected override void SuccessfullyFetched(
            IList<AnalyticRuleSettings> ruleSettings)
        {
            AnalyticModelSettingsPetrolEnginePurification result =
                new AnalyticModelSettingsPetrolEnginePurification(ruleSettings);
            successCallback.Invoke(result);
        }
    }
}

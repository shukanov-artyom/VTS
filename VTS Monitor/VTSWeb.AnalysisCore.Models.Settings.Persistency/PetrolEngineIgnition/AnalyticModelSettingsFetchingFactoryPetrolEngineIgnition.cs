using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTSWeb.AnalysisCore.Models.Settings.PetrolEngineIgnition;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.AnalysisCore.Recognition.Engines;
using VTSWeb.Common;


namespace VTSWeb.AnalysisCore.Models.Settings.Persistency.PetrolEngineIgnition
{
    public class AnalyticModelSettingsFetchingFactoryPetrolEngineIgnition :
        AnalyticModelSettingsFetchingFactoryBase
    {
        private AnalyticModelSettingsFetchedCallback successCallback;

        public AnalyticModelSettingsFetchingFactoryPetrolEngineIgnition(
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

        protected override void SuccessfullyFetched(
            IList<AnalyticRuleSettings> ruleSettings)
        {
            AnalyticModelSettingsPetrolEngineIgnition result =
                new AnalyticModelSettingsPetrolEngineIgnition(ruleSettings);
            successCallback.Invoke(result);
        }

        protected override IList<AnalyticRuleType> RequiredRuleTypes
        {
            get
            {
                return AnalyticModelSettingsPetrolEngineIgnition.
                    GetRequiredRuleTypes().ToList();
            }
        }
    }
}

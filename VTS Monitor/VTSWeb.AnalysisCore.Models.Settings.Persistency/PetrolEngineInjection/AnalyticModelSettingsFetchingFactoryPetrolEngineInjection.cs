using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTSWeb.AnalysisCore.Models.Settings.PetrolEngineInjection;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.AnalysisCore.Recognition.Engines;
using VTSWeb.Common;


namespace VTSWeb.AnalysisCore.Models.Settings.Persistency.PetrolEngineInjection
{
    public class AnalyticModelSettingsFetchingFactoryPetrolEngineInjection 
         : AnalyticModelSettingsFetchingFactoryBase
    {
        private AnalyticModelSettingsFetchedCallback successCallback;

        public AnalyticModelSettingsFetchingFactoryPetrolEngineInjection(
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
                return AnalyticModelSettingsPetrolEngineInjection.
                    GetRequiredRuleTypes().ToList();
            }
        }

        protected override void SuccessfullyFetched(
            IList<AnalyticRuleSettings> settings)
        {
            AnalyticModelSettingsPetrolEngineInjection result =
                new AnalyticModelSettingsPetrolEngineInjection(settings);
            successCallback.Invoke(result);
        }
    }
}

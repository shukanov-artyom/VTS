using System;
using System.Collections.Generic;
using VTS.Shared;
using VTSWeb.AnalysisCore.Recognition;

namespace VTSWeb.AnalysisCore.Models.Settings.Persistency
{
    public abstract class AnalyticModelSettingsFetchingFactoryBase : 
        IAnalyticModelSettingsFetchingFactory
    {
        private ErrorCallbackDelegate errorCallback;
        private VehicleInformation vehicleInformation;

        protected AnalyticModelSettingsFetchingFactoryBase(
            ErrorCallbackDelegate errorCallback,
            VehicleInformation vehicleInformation)
        {
            this.errorCallback = errorCallback;
            this.vehicleInformation = vehicleInformation;
        }

        protected abstract IList<AnalyticRuleType> RequiredRuleTypes
        {
            get;
        }

        public void FetchAsync()
        {
            EngineFamilyType requiredFamily =
                vehicleInformation.Engine.Family.Type;
            EngineType requiredEngineType = vehicleInformation.Engine.Type;
            AnalyticRuleSettingsPersistency persistency =
                new AnalyticRuleSettingsPersistency(SuccessfullyFetched, errorCallback);
            persistency.FetchRulesRangeByPriorityForEngine(RequiredRuleTypes,
                requiredFamily, requiredEngineType);
        }

        protected abstract void SuccessfullyFetched(
            IList<AnalyticRuleSettings> settings);
    }
}

using System;
using VTS.Shared;
using VTSWeb.Common;

namespace VTSWeb.AnalysisCore.Models.Settings.Persistency
{
    public class AnalyticRuleSettingsCreator
    {
        private SuccessCallbackDelegate successCallback;
        private ErrorCallbackDelegate errorCallback;
        private SuccessCallbackDelegate alreadyExistsCallback;
        private AnalyticRuleSettings settingsToCreate;

        public AnalyticRuleSettingsCreator(
            SuccessCallbackDelegate successCallback, 
            ErrorCallbackDelegate errorCallback, 
            SuccessCallbackDelegate alreadyExistsCallback)
        {
            this.errorCallback = errorCallback;
            this.alreadyExistsCallback = alreadyExistsCallback;
            this.successCallback = successCallback;
        }

        public void Create(AnalyticRuleSettings settings)
        {
            settingsToCreate = settings;
            AnalyticRuleSettingsPersistency persistency = 
                new AnalyticRuleSettingsPersistency(
                    SingleFetchedCallback, errorCallback);
            persistency.CheckNewlyCreatedSignature(settings.RuleType, 
                settings.EngineFamilyType, settings.EngineType);
        }

        private void SingleFetchedCallback(AnalyticRuleSettings item)
        {
            if (item != null)
            {
                alreadyExistsCallback.Invoke();
            }
            else
            {
                PersistItem(settingsToCreate);
            }
        }

        private void PersistItem(AnalyticRuleSettings item)
        {
            AnalyticRuleSettingsPersistency persistency = 
                new AnalyticRuleSettingsPersistency(
                    successCallback, errorCallback);
            persistency.Persist(item);
        }
    }
}

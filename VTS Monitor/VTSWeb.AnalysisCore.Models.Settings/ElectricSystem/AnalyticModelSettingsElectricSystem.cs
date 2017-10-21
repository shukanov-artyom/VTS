using System;
using System.Collections.Generic;
using VTS.Shared;


namespace VTSWeb.AnalysisCore.Models.Settings.ElectricSystem
{
    public class AnalyticModelSettingsElectricSystem : AnalyticModelSettings
    {
        public AnalyticModelSettingsElectricSystem(IList<AnalyticRuleSettings> settings)
        {
            foreach (AnalyticRuleType ruleType in GetRequiredRuleTypes())
            {
                RegisterRequiredType(ruleType);
            }
            foreach (AnalyticRuleSettings ruleSettings in settings)
            {
                RulesSettings.Add(ruleSettings);
            }
            CheckAndThrow();
        }

        public static IEnumerable<AnalyticRuleType> GetRequiredRuleTypes()
        {
            yield return AnalyticRuleType.EngineStartUndervoltage;
        }
    }
}

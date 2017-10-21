using System;
using System.Collections.Generic;
using VTS.Shared;


namespace VTSWeb.AnalysisCore.Models.Settings.PetrolEngineInjection
{
    public class AnalyticModelSettingsPetrolEngineInjection : 
        AnalyticModelSettings
    {
        public AnalyticModelSettingsPetrolEngineInjection(
            IList<AnalyticRuleSettings> settings)
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
            yield return AnalyticRuleType.InjectionTimeAt1000Rpm;
            yield return AnalyticRuleType.InjectionTimeAt2000Rpm;
            yield return AnalyticRuleType.InjectionTimeAt3000Rpm;
            yield return AnalyticRuleType.InjectionTimeStartupPeak;
        }
    }
}

using System;
using System.Collections.Generic;
using VTS.Shared;


namespace VTSWeb.AnalysisCore.Models.Settings.PetrolEnginePurification
{
    public class AnalyticModelSettingsPetrolEnginePurification:
        AnalyticModelSettings
    {
        public AnalyticModelSettingsPetrolEnginePurification(
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
            yield return AnalyticRuleType.LambdaUpperVoltageAt1000Rpm;
            yield return AnalyticRuleType.LambdaUpperVoltageAt2000Rpm;
            yield return AnalyticRuleType.LambdaUpperVoltageAt3000Rpm;

            yield return AnalyticRuleType.LambdaLowerVoltageAt1000Rpm;
            yield return AnalyticRuleType.LambdaLowerVoltageAt2000Rpm;
            yield return AnalyticRuleType.LambdaLowerVoltageAt3000Rpm;
        }
    }
}

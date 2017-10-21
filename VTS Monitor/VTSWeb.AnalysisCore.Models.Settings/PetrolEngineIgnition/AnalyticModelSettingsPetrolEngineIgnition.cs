using System;
using System.Collections.Generic;
using VTS.Shared;


namespace VTSWeb.AnalysisCore.Models.Settings.PetrolEngineIgnition
{
    public class AnalyticModelSettingsPetrolEngineIgnition : 
        AnalyticModelSettings
    {
        public AnalyticModelSettingsPetrolEngineIgnition(
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
            yield return AnalyticRuleType.Cylinder1CoilChargeTime1000Rpm;
            yield return AnalyticRuleType.Cylinder1CoilChargeTime2000Rpm;
            yield return AnalyticRuleType.Cylinder1CoilChargeTime3000Rpm;

            yield return AnalyticRuleType.Cylinder2CoilChargeTime1000Rpm;
            yield return AnalyticRuleType.Cylinder2CoilChargeTime2000Rpm;
            yield return AnalyticRuleType.Cylinder2CoilChargeTime3000Rpm;

            yield return AnalyticRuleType.Cylinder3CoilChargeTime1000Rpm;
            yield return AnalyticRuleType.Cylinder3CoilChargeTime2000Rpm;
            yield return AnalyticRuleType.Cylinder3CoilChargeTime3000Rpm;

            yield return AnalyticRuleType.Cylinder4CoilChargeTime1000Rpm;
            yield return AnalyticRuleType.Cylinder4CoilChargeTime2000Rpm;
            yield return AnalyticRuleType.Cylinder4CoilChargeTime3000Rpm;

            // Cylinder coil startup chare time peak
            yield return AnalyticRuleType.Cylinder1CoilStartupChargeTimePeak;
            yield return AnalyticRuleType.Cylinder2CoilStartupChargeTimePeak;
            yield return AnalyticRuleType.Cylinder3CoilStartupChargeTimePeak;
            yield return AnalyticRuleType.Cylinder4CoilStartupChargeTimePeak;
        }
    }
}

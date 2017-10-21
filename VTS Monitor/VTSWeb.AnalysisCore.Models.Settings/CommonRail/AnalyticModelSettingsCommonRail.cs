using System;
using System.Collections.Generic;
using VTS.Shared;


namespace VTSWeb.AnalysisCore.Models.Settings.CommonRail
{
    public class AnalyticModelSettingsCommonRail : AnalyticModelSettings
    {
        public static IEnumerable<AnalyticRuleType> GetRequiredRuleTypes()
        {
            /*yield return AnalyticRuleType.InjectorCorrectionAt1000Rpm;
            yield return AnalyticRuleType.InjectorCorrectionAt2000Rpm;
            yield return AnalyticRuleType.InjectorCorrectionAt3000Rpm;*/

            yield return AnalyticRuleType.Injector1CorrectionAt1000Rpm;
            yield return AnalyticRuleType.Injector1CorrectionAt2000Rpm;
            yield return AnalyticRuleType.Injector1CorrectionAt3000Rpm;

            yield return AnalyticRuleType.Injector2CorrectionAt1000Rpm;
            yield return AnalyticRuleType.Injector2CorrectionAt2000Rpm;
            yield return AnalyticRuleType.Injector2CorrectionAt3000Rpm;

            yield return AnalyticRuleType.Injector3CorrectionAt1000Rpm;
            yield return AnalyticRuleType.Injector3CorrectionAt2000Rpm;
            yield return AnalyticRuleType.Injector3CorrectionAt3000Rpm;

            yield return AnalyticRuleType.Injector4CorrectionAt1000Rpm;
            yield return AnalyticRuleType.Injector4CorrectionAt2000Rpm;
            yield return AnalyticRuleType.Injector4CorrectionAt3000Rpm;

            yield return AnalyticRuleType.FuelPressureDelta1000Rpm;
            yield return AnalyticRuleType.FuelPressureDelta2000Rpm;
            yield return AnalyticRuleType.FuelPressureDelta3000Rpm;

            yield return AnalyticRuleType.FuelPressureRegulatorCurrent1000Rpm;
            yield return AnalyticRuleType.FuelPressureRegulatorCurrent2000Rpm;
            yield return AnalyticRuleType.FuelPressureRegulatorCurrent3000Rpm;
        }

        public AnalyticModelSettingsCommonRail(
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
    }
}

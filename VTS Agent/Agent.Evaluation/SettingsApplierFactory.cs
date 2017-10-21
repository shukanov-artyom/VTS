using System;
using VTS.Agent.BusinessObjects;
using VTS.Agent.BusinessObjects.Enums;
using VTS.Shared;

namespace Agent.Evaluation
{
    internal static class SettingsApplierFactory
    {
        public static IAnalyticRuleSettingsApplier CreateFor(AnalyticRuleSettings settings)
        {
            switch (settings.RuleType)
            {
                case AnalyticRuleType.Injector1CorrectionAt1000Rpm:
                case AnalyticRuleType.Injector1CorrectionAt2000Rpm:
                case AnalyticRuleType.Injector1CorrectionAt3000Rpm:
                case AnalyticRuleType.Injector2CorrectionAt1000Rpm:
                case AnalyticRuleType.Injector2CorrectionAt2000Rpm:
                case AnalyticRuleType.Injector2CorrectionAt3000Rpm:
                case AnalyticRuleType.Injector3CorrectionAt1000Rpm:
                case AnalyticRuleType.Injector3CorrectionAt2000Rpm:
                case AnalyticRuleType.Injector3CorrectionAt3000Rpm:
                case AnalyticRuleType.Injector4CorrectionAt1000Rpm:
                case AnalyticRuleType.Injector4CorrectionAt2000Rpm:
                case AnalyticRuleType.Injector4CorrectionAt3000Rpm:
                    return new AnalyticRuleSettingsApplierByAbsoluteValue(settings);
                default:
                    return new AnalyticRuleSettingsApplierDefault(settings);
            }
        }
    }
}

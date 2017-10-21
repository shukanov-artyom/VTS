using System;
using System.Collections.Generic;
using VTS.Shared;
using VTSWebService.AnalysisCore.Common;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Statistics
{
    public static class RuleTypeToRpm
    {
        private static IDictionary<AnalyticRuleType, CheckpointRpm> mapping =
            new Dictionary<AnalyticRuleType, CheckpointRpm>();

        static RuleTypeToRpm()
        {
            mapping[AnalyticRuleType.LambdaLowerVoltageAt1000Rpm] = CheckpointRpm.Rpm1000;
            mapping[AnalyticRuleType.LambdaLowerVoltageAt2000Rpm] = CheckpointRpm.Rpm2000;
            mapping[AnalyticRuleType.LambdaLowerVoltageAt3000Rpm] = CheckpointRpm.Rpm3000;

            mapping[AnalyticRuleType.LambdaUpperVoltageAt1000Rpm] = CheckpointRpm.Rpm1000;
            mapping[AnalyticRuleType.LambdaUpperVoltageAt2000Rpm] = CheckpointRpm.Rpm2000;
            mapping[AnalyticRuleType.LambdaUpperVoltageAt3000Rpm] = CheckpointRpm.Rpm3000;

            mapping[AnalyticRuleType.Injector1CorrectionAt1000Rpm] = CheckpointRpm.Rpm1000;
            mapping[AnalyticRuleType.Injector1CorrectionAt2000Rpm] = CheckpointRpm.Rpm2000;
            mapping[AnalyticRuleType.Injector1CorrectionAt3000Rpm] = CheckpointRpm.Rpm3000;

            mapping[AnalyticRuleType.Injector2CorrectionAt1000Rpm] = CheckpointRpm.Rpm1000;
            mapping[AnalyticRuleType.Injector2CorrectionAt2000Rpm] = CheckpointRpm.Rpm2000;
            mapping[AnalyticRuleType.Injector2CorrectionAt3000Rpm] = CheckpointRpm.Rpm3000;

            mapping[AnalyticRuleType.Injector3CorrectionAt1000Rpm] = CheckpointRpm.Rpm1000;
            mapping[AnalyticRuleType.Injector3CorrectionAt2000Rpm] = CheckpointRpm.Rpm2000;
            mapping[AnalyticRuleType.Injector3CorrectionAt3000Rpm] = CheckpointRpm.Rpm3000;

            mapping[AnalyticRuleType.Injector4CorrectionAt1000Rpm] = CheckpointRpm.Rpm1000;
            mapping[AnalyticRuleType.Injector4CorrectionAt2000Rpm] = CheckpointRpm.Rpm2000;
            mapping[AnalyticRuleType.Injector4CorrectionAt3000Rpm] = CheckpointRpm.Rpm3000;

            mapping[AnalyticRuleType.FuelPressureDelta1000Rpm] = CheckpointRpm.Rpm1000;
            mapping[AnalyticRuleType.FuelPressureDelta2000Rpm] = CheckpointRpm.Rpm2000;
            mapping[AnalyticRuleType.FuelPressureDelta3000Rpm] = CheckpointRpm.Rpm3000;

            mapping[AnalyticRuleType.FuelPressureRegulatorCurrent1000Rpm] = CheckpointRpm.Rpm1000;
            mapping[AnalyticRuleType.FuelPressureRegulatorCurrent2000Rpm] = CheckpointRpm.Rpm2000;
            mapping[AnalyticRuleType.FuelPressureRegulatorCurrent3000Rpm] = CheckpointRpm.Rpm3000;

            mapping[AnalyticRuleType.InjectionTimeAt1000Rpm] = CheckpointRpm.Rpm1000;
            mapping[AnalyticRuleType.InjectionTimeAt2000Rpm] = CheckpointRpm.Rpm2000;
            mapping[AnalyticRuleType.InjectionTimeAt3000Rpm] = CheckpointRpm.Rpm3000;
        }

        public static CheckpointRpm Map(AnalyticRuleType ruleType)
        {
            return mapping[ruleType];
        }
    }
}

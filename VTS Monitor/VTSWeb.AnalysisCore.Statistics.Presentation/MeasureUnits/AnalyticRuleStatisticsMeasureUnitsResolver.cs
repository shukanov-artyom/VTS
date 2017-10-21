using System;
using System.Collections.Generic;
using VTS.Shared;


namespace VTSWeb.AnalysisCore.Statistics.Presentation.MeasureUnits
{
    public static class AnalyticRuleStatisticsMeasureUnitsResolver
    {
        private static Dictionary<AnalyticRuleType, Unit> mapping = 
            new Dictionary<AnalyticRuleType, Unit>();

        static AnalyticRuleStatisticsMeasureUnitsResolver()
        {
            mapping.Add(AnalyticRuleType.FuelPressureDelta1000Rpm, Unit.Bar);
            mapping.Add(AnalyticRuleType.FuelPressureDelta2000Rpm, Unit.Bar);
            mapping.Add(AnalyticRuleType.FuelPressureDelta3000Rpm, Unit.Bar);

            mapping.Add(AnalyticRuleType.FuelPressureRegulatorCurrent1000Rpm, Unit.Amper);
            mapping.Add(AnalyticRuleType.FuelPressureRegulatorCurrent2000Rpm, Unit.Amper);
            mapping.Add(AnalyticRuleType.FuelPressureRegulatorCurrent3000Rpm, Unit.Amper);

            mapping.Add(AnalyticRuleType.Injector1CorrectionAt1000Rpm, Unit.MgPerStroke);
            mapping.Add(AnalyticRuleType.Injector1CorrectionAt2000Rpm, Unit.MgPerStroke);
            mapping.Add(AnalyticRuleType.Injector1CorrectionAt3000Rpm, Unit.MgPerStroke);
            mapping.Add(AnalyticRuleType.Injector2CorrectionAt1000Rpm, Unit.MgPerStroke);
            mapping.Add(AnalyticRuleType.Injector2CorrectionAt2000Rpm, Unit.MgPerStroke);
            mapping.Add(AnalyticRuleType.Injector2CorrectionAt3000Rpm, Unit.MgPerStroke);
            mapping.Add(AnalyticRuleType.Injector3CorrectionAt1000Rpm, Unit.MgPerStroke);
            mapping.Add(AnalyticRuleType.Injector3CorrectionAt2000Rpm, Unit.MgPerStroke);
            mapping.Add(AnalyticRuleType.Injector3CorrectionAt3000Rpm, Unit.MgPerStroke);
            mapping.Add(AnalyticRuleType.Injector4CorrectionAt1000Rpm, Unit.MgPerStroke);
            mapping.Add(AnalyticRuleType.Injector4CorrectionAt2000Rpm, Unit.MgPerStroke);
            mapping.Add(AnalyticRuleType.Injector4CorrectionAt3000Rpm, Unit.MgPerStroke);

            mapping.Add(AnalyticRuleType.LambdaUpperVoltageAt1000Rpm, Unit.Millivolt);
            mapping.Add(AnalyticRuleType.LambdaUpperVoltageAt2000Rpm, Unit.Millivolt);
            mapping.Add(AnalyticRuleType.LambdaUpperVoltageAt3000Rpm, Unit.Millivolt);
            mapping.Add(AnalyticRuleType.LambdaLowerVoltageAt1000Rpm, Unit.Millivolt);
            mapping.Add(AnalyticRuleType.LambdaLowerVoltageAt2000Rpm, Unit.Millivolt);
            mapping.Add(AnalyticRuleType.LambdaLowerVoltageAt3000Rpm, Unit.Millivolt);

            mapping.Add(AnalyticRuleType.Cylinder1CoilChargeTime1000Rpm, Unit.Millisec);
            mapping.Add(AnalyticRuleType.Cylinder1CoilChargeTime2000Rpm, Unit.Millisec);
            mapping.Add(AnalyticRuleType.Cylinder1CoilChargeTime3000Rpm, Unit.Millisec);

            mapping.Add(AnalyticRuleType.Cylinder2CoilChargeTime1000Rpm, Unit.Millisec);
            mapping.Add(AnalyticRuleType.Cylinder2CoilChargeTime2000Rpm, Unit.Millisec);
            mapping.Add(AnalyticRuleType.Cylinder2CoilChargeTime3000Rpm, Unit.Millisec);

            mapping.Add(AnalyticRuleType.Cylinder3CoilChargeTime1000Rpm, Unit.Millisec);
            mapping.Add(AnalyticRuleType.Cylinder3CoilChargeTime2000Rpm, Unit.Millisec);
            mapping.Add(AnalyticRuleType.Cylinder3CoilChargeTime3000Rpm, Unit.Millisec);

            mapping.Add(AnalyticRuleType.Cylinder4CoilChargeTime1000Rpm, Unit.Millisec);
            mapping.Add(AnalyticRuleType.Cylinder4CoilChargeTime2000Rpm, Unit.Millisec);
            mapping.Add(AnalyticRuleType.Cylinder4CoilChargeTime3000Rpm, Unit.Millisec);

            mapping.Add(AnalyticRuleType.Cylinder1CoilStartupChargeTimePeak, Unit.Millisec);
            mapping.Add(AnalyticRuleType.Cylinder2CoilStartupChargeTimePeak, Unit.Millisec);
            mapping.Add(AnalyticRuleType.Cylinder3CoilStartupChargeTimePeak, Unit.Millisec);
            mapping.Add(AnalyticRuleType.Cylinder4CoilStartupChargeTimePeak, Unit.Millisec);

            mapping.Add(AnalyticRuleType.InjectionTimeAt1000Rpm, Unit.Millisec);
            mapping.Add(AnalyticRuleType.InjectionTimeAt2000Rpm, Unit.Millisec);
            mapping.Add(AnalyticRuleType.InjectionTimeAt3000Rpm, Unit.Millisec);

            mapping.Add(AnalyticRuleType.InjectionTimeStartupPeak, Unit.Millisec);

            mapping.Add(AnalyticRuleType.EngineStartUndervoltage, Unit.Percent);
        }

        public static Unit Resolve(AnalyticRuleType ruleType)
        {
            return mapping[ruleType];
        }
    }
}

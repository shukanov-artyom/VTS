using System;
using VTS.Shared;
using AnalyticStatisticsItem = VTS.Shared.DomainObjects.AnalyticStatisticsItem;

namespace VTSWebService.AnalysisCore.Aggregation
{
    public static class AggregatorFactory
    {
        public static IAggregator Create(AnalyticStatisticsItem source)
        {
            switch (source.Type)
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
                    return new InjectorCorrectionForRpmAggregator(source);

                // Common Rail - fuel pressure difference
                case AnalyticRuleType.FuelPressureDelta1000Rpm:
                case AnalyticRuleType.FuelPressureDelta2000Rpm:
                case AnalyticRuleType.FuelPressureDelta3000Rpm:
                // Petrol engine purification
                case AnalyticRuleType.LambdaUpperVoltageAt1000Rpm:
                case AnalyticRuleType.LambdaUpperVoltageAt2000Rpm:
                case AnalyticRuleType.LambdaUpperVoltageAt3000Rpm:
                case AnalyticRuleType.LambdaLowerVoltageAt1000Rpm:
                case AnalyticRuleType.LambdaLowerVoltageAt2000Rpm:
                case AnalyticRuleType.LambdaLowerVoltageAt3000Rpm:
                // engine startup undervoltage
                case AnalyticRuleType.EngineStartUndervoltage:
                // cylinder coils charge time for different rpms
                case AnalyticRuleType.Cylinder1CoilChargeTime1000Rpm:
                case AnalyticRuleType.Cylinder1CoilChargeTime2000Rpm:
                case AnalyticRuleType.Cylinder1CoilChargeTime3000Rpm:
                case AnalyticRuleType.Cylinder2CoilChargeTime1000Rpm:
                case AnalyticRuleType.Cylinder2CoilChargeTime2000Rpm:
                case AnalyticRuleType.Cylinder2CoilChargeTime3000Rpm:
                case AnalyticRuleType.Cylinder3CoilChargeTime1000Rpm:
                case AnalyticRuleType.Cylinder3CoilChargeTime2000Rpm:
                case AnalyticRuleType.Cylinder3CoilChargeTime3000Rpm:
                case AnalyticRuleType.Cylinder4CoilChargeTime1000Rpm:
                case AnalyticRuleType.Cylinder4CoilChargeTime2000Rpm:
                case AnalyticRuleType.Cylinder4CoilChargeTime3000Rpm:
                // cylinder coils peak timing on startup
                case AnalyticRuleType.Cylinder1CoilStartupChargeTimePeak:
                case AnalyticRuleType.Cylinder2CoilStartupChargeTimePeak:
                case AnalyticRuleType.Cylinder3CoilStartupChargeTimePeak:
                case AnalyticRuleType.Cylinder4CoilStartupChargeTimePeak:
                // injection time for petrol engines
                case AnalyticRuleType.InjectionTimeAt1000Rpm:
                case AnalyticRuleType.InjectionTimeAt2000Rpm:
                case AnalyticRuleType.InjectionTimeAt3000Rpm:
                // injection time peak for petrol engines
                case AnalyticRuleType.InjectionTimeStartupPeak:
                // fuel regulator current for common rail engines
                case AnalyticRuleType.FuelPressureRegulatorCurrent1000Rpm:
                case AnalyticRuleType.FuelPressureRegulatorCurrent2000Rpm:
                case AnalyticRuleType.FuelPressureRegulatorCurrent3000Rpm:
                    return new StandardAggregator(source);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}

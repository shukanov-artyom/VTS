using System;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.AnalysisCore.Statistics.Generation.CommonRail.Injectors;
using VTSWeb.AnalysisCore.Statistics.Generation.ElectricSystem;
using VTSWeb.AnalysisCore.Statistics.Generation.PetrolEngineIgnition;
using VTSWeb.AnalysisCore.Statistics.Generation.PetrolEngineInjection;
using VTSWeb.AnalysisCore.Statistics.Generation.PetrolEnginePurification.LambdaProbes;



namespace VTSWeb.AnalysisCore.Statistics.Generation
{
    public class FitterFactory
    {
        private VehicleInformation info;

        public FitterFactory(VehicleInformation info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            this.info = info;
        }

        public IFitter Create(AnalyticRuleType ruleType)
        {
            // Injector corrections
            /*if (ruleType == AnalyticRuleType.InjectorCorrectionAt1000Rpm || 
                ruleType == AnalyticRuleType.InjectorCorrectionAt2000Rpm || 
                ruleType == AnalyticRuleType.InjectorCorrectionAt3000Rpm)
            {
                return new FitterInjectorsCorrectionForRpm(info, ruleType);
            }*/

            if (ruleType == AnalyticRuleType.Injector1CorrectionAt1000Rpm ||
                ruleType == AnalyticRuleType.Injector1CorrectionAt2000Rpm ||
                ruleType == AnalyticRuleType.Injector1CorrectionAt3000Rpm ||
                ruleType == AnalyticRuleType.Injector2CorrectionAt1000Rpm ||
                ruleType == AnalyticRuleType.Injector2CorrectionAt2000Rpm ||
                ruleType == AnalyticRuleType.Injector2CorrectionAt3000Rpm ||
                ruleType == AnalyticRuleType.Injector3CorrectionAt1000Rpm ||
                ruleType == AnalyticRuleType.Injector3CorrectionAt2000Rpm ||
                ruleType == AnalyticRuleType.Injector3CorrectionAt3000Rpm ||
                ruleType == AnalyticRuleType.Injector4CorrectionAt1000Rpm ||
                ruleType == AnalyticRuleType.Injector4CorrectionAt2000Rpm ||
                ruleType == AnalyticRuleType.Injector4CorrectionAt3000Rpm)
            {
                return new FitterInjectorCorrectionForRpm(info, ruleType);
            }

            // Labmda
            if (ruleType == AnalyticRuleType.LambdaUpperVoltageAt1000Rpm ||
                ruleType == AnalyticRuleType.LambdaUpperVoltageAt2000Rpm || 
                ruleType == AnalyticRuleType.LambdaUpperVoltageAt3000Rpm)
            {
                return new FitterUpperLambdaVoltage(info, ruleType);
            }

            if (ruleType == AnalyticRuleType.LambdaLowerVoltageAt1000Rpm || 
                ruleType == AnalyticRuleType.LambdaLowerVoltageAt2000Rpm || 
                ruleType == AnalyticRuleType.LambdaLowerVoltageAt3000Rpm)
            {
                return new FitterLowerLambdaVoltage(info, ruleType);
            }

            // engine startup undervoltage
            if (ruleType == AnalyticRuleType.EngineStartUndervoltage)
            {
                return new FitterStartupUndervoltage(info);
            }

            // Fuel Pressure Delta (Common Rail diesel)
            if (ruleType == AnalyticRuleType.FuelPressureDelta1000Rpm || 
                ruleType == AnalyticRuleType.FuelPressureDelta2000Rpm || 
                ruleType == AnalyticRuleType.FuelPressureDelta3000Rpm)
            {
                return new RpmCorrelationFitter(
                    PsaParameterType.FuelSystemPressureDelta, 5, info, ruleType);
            }

            if (ruleType == AnalyticRuleType.FuelPressureRegulatorCurrent1000Rpm ||
                ruleType == AnalyticRuleType.FuelPressureRegulatorCurrent2000Rpm ||
                ruleType == AnalyticRuleType.FuelPressureRegulatorCurrent3000Rpm)
            {
                return new RpmCorrelationFitter(
                    PsaParameterType.FuelRegulatorCurrent, 5, info, ruleType);
            }

            // cyl coil charge timing
            if (ruleType == AnalyticRuleType.Cylinder1CoilChargeTime1000Rpm ||
                ruleType == AnalyticRuleType.Cylinder1CoilChargeTime2000Rpm ||
                ruleType == AnalyticRuleType.Cylinder1CoilChargeTime3000Rpm ||

                ruleType == AnalyticRuleType.Cylinder2CoilChargeTime1000Rpm ||
                ruleType == AnalyticRuleType.Cylinder2CoilChargeTime2000Rpm ||
                ruleType == AnalyticRuleType.Cylinder2CoilChargeTime3000Rpm ||

                ruleType == AnalyticRuleType.Cylinder3CoilChargeTime1000Rpm ||
                ruleType == AnalyticRuleType.Cylinder3CoilChargeTime2000Rpm ||
                ruleType == AnalyticRuleType.Cylinder3CoilChargeTime3000Rpm ||

                ruleType == AnalyticRuleType.Cylinder4CoilChargeTime1000Rpm ||
                ruleType == AnalyticRuleType.Cylinder4CoilChargeTime2000Rpm ||
                ruleType == AnalyticRuleType.Cylinder4CoilChargeTime3000Rpm)
            {
                return new FitterCylinderCoilChargeTimeForRpm(info, ruleType);
            }

            // cyl coil charge startup peak 
            if (ruleType == AnalyticRuleType.Cylinder1CoilStartupChargeTimePeak ||
                ruleType == AnalyticRuleType.Cylinder2CoilStartupChargeTimePeak ||
                ruleType == AnalyticRuleType.Cylinder3CoilStartupChargeTimePeak ||
                ruleType == AnalyticRuleType.Cylinder4CoilStartupChargeTimePeak)
            {
                return new FitterCylinderCoilStartupChargeTimePeak(info, ruleType);
            }

            // Injection time for different rpms
            if (ruleType == AnalyticRuleType.InjectionTimeAt1000Rpm ||
                ruleType == AnalyticRuleType.InjectionTimeAt2000Rpm ||
                ruleType == AnalyticRuleType.InjectionTimeAt3000Rpm)
            {
                return new RpmCorrelationFitter(
                    PsaParameterType.InjectionTime, 5, info, ruleType);
            }

            if (ruleType == AnalyticRuleType.InjectionTimeStartupPeak)
            {
                return new FitterInjectionTimeStartupPeak(info);
            }

            // fitter not found for rule type!
            throw new NotImplementedException();
        }
    }
}

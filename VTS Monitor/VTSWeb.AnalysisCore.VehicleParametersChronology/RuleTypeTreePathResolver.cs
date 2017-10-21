using System;
using VTS.Shared;


namespace VTSWeb.AnalysisCore.VehicleParametersChronology
{
    public class RuleTypeTreePathResolver
    {
        private readonly AnalyticRuleType ruleType;

        public RuleTypeTreePathResolver(AnalyticRuleType ruleType)
        {
            this.ruleType = ruleType;
        }

        public string GetPath()
        {
            switch (ruleType)
            {
                case AnalyticRuleType.Injector1CorrectionAt1000Rpm:
                case AnalyticRuleType.Injector1CorrectionAt2000Rpm:
                case AnalyticRuleType.Injector1CorrectionAt3000Rpm:
                    return "CommonRail/InjectorsCorrections/InjectorCorrections1";
                case AnalyticRuleType.Injector2CorrectionAt1000Rpm:
                case AnalyticRuleType.Injector2CorrectionAt2000Rpm:
                case AnalyticRuleType.Injector2CorrectionAt3000Rpm:
                    return "CommonRail/InjectorsCorrections/InjectorCorrections2";
                case AnalyticRuleType.Injector3CorrectionAt1000Rpm:
                case AnalyticRuleType.Injector3CorrectionAt2000Rpm:
                case AnalyticRuleType.Injector3CorrectionAt3000Rpm:
                    return "CommonRail/InjectorsCorrections/InjectorCorrections3";
                case AnalyticRuleType.Injector4CorrectionAt1000Rpm:
                case AnalyticRuleType.Injector4CorrectionAt2000Rpm:
                case AnalyticRuleType.Injector4CorrectionAt3000Rpm:
                    return "CommonRail/InjectorsCorrections/InjectorCorrections4";
                case AnalyticRuleType.FuelPressureDelta1000Rpm:
                case AnalyticRuleType.FuelPressureDelta2000Rpm:
                case AnalyticRuleType.FuelPressureDelta3000Rpm:
                    return "CommonRail/FuelPressureDelta";
                case AnalyticRuleType.FuelPressureRegulatorCurrent1000Rpm:
                case AnalyticRuleType.FuelPressureRegulatorCurrent2000Rpm:
                case AnalyticRuleType.FuelPressureRegulatorCurrent3000Rpm:
                    return "CommonRail/FuelPressureRegulatorCurrent";
                case AnalyticRuleType.LambdaUpperVoltageAt1000Rpm:
                case AnalyticRuleType.LambdaUpperVoltageAt2000Rpm:
                case AnalyticRuleType.LambdaUpperVoltageAt3000Rpm:
                    return "PetrolEnginePurification/LambdaProbes/UpperLambdaProbe";
                case AnalyticRuleType.LambdaLowerVoltageAt1000Rpm:
                case AnalyticRuleType.LambdaLowerVoltageAt2000Rpm:
                case AnalyticRuleType.LambdaLowerVoltageAt3000Rpm:
                    return "PetrolEnginePurification/LambdaProbes/LowerLambdaProbe";
                case AnalyticRuleType.Cylinder1CoilChargeTime1000Rpm:
                case AnalyticRuleType.Cylinder1CoilChargeTime2000Rpm:
                case AnalyticRuleType.Cylinder1CoilChargeTime3000Rpm:
                    return "PetrolEngineIgnition/CylinderCoilChargeTime1";
                case AnalyticRuleType.Cylinder2CoilChargeTime1000Rpm:
                case AnalyticRuleType.Cylinder2CoilChargeTime2000Rpm:
                case AnalyticRuleType.Cylinder2CoilChargeTime3000Rpm:
                    return "PetrolEngineIgnition/CylinderCoilChargeTime2";
                case AnalyticRuleType.Cylinder3CoilChargeTime1000Rpm:
                case AnalyticRuleType.Cylinder3CoilChargeTime2000Rpm:
                case AnalyticRuleType.Cylinder3CoilChargeTime3000Rpm:
                    return "PetrolEngineIgnition/CylinderCoilChargeTime3";
                case AnalyticRuleType.Cylinder4CoilChargeTime1000Rpm:
                case AnalyticRuleType.Cylinder4CoilChargeTime2000Rpm:
                case AnalyticRuleType.Cylinder4CoilChargeTime3000Rpm:
                    return "Ignition/CylinderCoilChargeTime4";
                case AnalyticRuleType.Cylinder1CoilStartupChargeTimePeak:
                case AnalyticRuleType.Cylinder2CoilStartupChargeTimePeak:
                case AnalyticRuleType.Cylinder3CoilStartupChargeTimePeak:
                case AnalyticRuleType.Cylinder4CoilStartupChargeTimePeak:
                    return "PetrolEngineInjection/CylinderCoilsChargeStartupTimePeak";
                case AnalyticRuleType.InjectionTimeAt1000Rpm:
                case AnalyticRuleType.InjectionTimeAt2000Rpm:
                case AnalyticRuleType.InjectionTimeAt3000Rpm:
                    return "PetrolEngineInjection/PetrolEngineInjectionTime";
                case AnalyticRuleType.InjectionTimeStartupPeak:
                    return "PetrolEngineInjection";
                case AnalyticRuleType.EngineStartUndervoltage:
                    return "ElectricSystem";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

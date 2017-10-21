using System;

namespace VTS.Shared
{
    public enum AnalyticRuleType
    {
        // For Common Rail diesel engines
        FuelPressureDelta1000Rpm,
        FuelPressureDelta2000Rpm,
        FuelPressureDelta3000Rpm,

        FuelPressureRegulatorCurrent1000Rpm,
        FuelPressureRegulatorCurrent2000Rpm,
        FuelPressureRegulatorCurrent3000Rpm,

        Injector1CorrectionAt1000Rpm,
        Injector1CorrectionAt2000Rpm,
        Injector1CorrectionAt3000Rpm,
        Injector2CorrectionAt1000Rpm,
        Injector2CorrectionAt2000Rpm,
        Injector2CorrectionAt3000Rpm,
        Injector3CorrectionAt1000Rpm,
        Injector3CorrectionAt2000Rpm,
        Injector3CorrectionAt3000Rpm,
        Injector4CorrectionAt1000Rpm,
        Injector4CorrectionAt2000Rpm,
        Injector4CorrectionAt3000Rpm,

        // For petrol engines
        LambdaUpperVoltageAt1000Rpm = 1000,
        LambdaUpperVoltageAt2000Rpm,
        LambdaUpperVoltageAt3000Rpm,

        LambdaLowerVoltageAt1000Rpm,
        LambdaLowerVoltageAt2000Rpm,
        LambdaLowerVoltageAt3000Rpm,

        // Cylinder coils charge timing
        Cylinder1CoilChargeTime1000Rpm = 1010,
        Cylinder1CoilChargeTime2000Rpm,
        Cylinder1CoilChargeTime3000Rpm,

        Cylinder2CoilChargeTime1000Rpm,
        Cylinder2CoilChargeTime2000Rpm,
        Cylinder2CoilChargeTime3000Rpm,

        Cylinder3CoilChargeTime1000Rpm,
        Cylinder3CoilChargeTime2000Rpm,
        Cylinder3CoilChargeTime3000Rpm,

        Cylinder4CoilChargeTime1000Rpm,
        Cylinder4CoilChargeTime2000Rpm,
        Cylinder4CoilChargeTime3000Rpm,

        // Cylinder coil startup chare time peak
        Cylinder1CoilStartupChargeTimePeak = 1030,
        Cylinder2CoilStartupChargeTimePeak,
        Cylinder3CoilStartupChargeTimePeak,
        Cylinder4CoilStartupChargeTimePeak,

        // Injection time - petrol engines
        InjectionTimeAt1000Rpm,
        InjectionTimeAt2000Rpm,
        InjectionTimeAt3000Rpm,

        InjectionTimeStartupPeak,

        // For all engines
        EngineStartUndervoltage = 1100,
    }
}

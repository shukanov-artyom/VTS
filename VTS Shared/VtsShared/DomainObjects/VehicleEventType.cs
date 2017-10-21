using System;

namespace VTS.Shared.DomainObjects
{
    public enum VehicleEventType
    {
        TimingBeltChange = 1,
        OilChange = 10,
        OilFilterChange = 20,
        GeneratorBeltChange = 30,
        AirFilterChange = 40,
        CabinFilterChange = 50,
        CoolantPumpChange = 60,
        CoolantChange = 70,
        Other = 80,
        EngineRepair = 90,
        SuspensionRepair = 100,
        BodyRepair = 110,
        CabinRepair = 120,
        CoolantSystemRepair = 130,
        ConditioningRepair = 140,
        WheelsRepair = 150,
        GlassMirrorsRepair = 160,
        Insurance = 170
    }
}

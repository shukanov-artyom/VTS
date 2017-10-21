using System;
using System.Collections.Generic;
using Agent.Common.Data;
using Agent.Localization;

using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA
{
    public class PsaParametersSetTypeMapper
    {
        private static readonly IDictionary<string, PsaParametersSetType> mapping = 
            new Dictionary<string, PsaParametersSetType>(StringComparer.OrdinalIgnoreCase);

        static PsaParametersSetTypeMapper()
        {
            //@P110797-THESAU@\*1
            mapping.Add(Decode("9NEOUBUQsCktoWrSsujW6X7g35MANvOJOtGdGKDprH4="), 
                PsaParametersSetType.BasicParameters1);
            //@P6604-CITACT@\*1
            mapping.Add(Decode("DOhAR+MWh3qEUZfpHvCvQMG47pqmU1KNNk0g7PehH2k="), 
                PsaParametersSetType.BasicParameters1);
            //@P110797-THESAU@\*2
            mapping.Add(Decode("9NEOUBUQsCktoWrSsujW6edJ74kP/50FrV+akIlkGIc="),
                PsaParametersSetType.BasicParameters2);
            //@P6604-CITACT@\*2
            mapping.Add(Decode("DOhAR+MWh3qEUZfpHvCvQOEv1eN53ka3on309JN8/L0="), 
                PsaParametersSetType.BasicParameters2);
            //@P107237-THESAU
            mapping.Add(Decode("0Vhm4Sx6mdhj11eOaQ454w=="),
                 PsaParametersSetType.ElectricSystemInfo);
            //@P3226-CITACT
            mapping.Add(Decode("TTilPlja9oBql0S73mbNKg=="),
                PsaParametersSetType.ElectricSystemInfo);
            //@P113429-THESAU
            mapping.Add(Decode("Lv4Fp/kdmLURvf4PhYpZqQ=="),
                PsaParametersSetType.InjectionInformation);
            //@P33397-CITACT
            mapping.Add(Decode("wQ9h7KU/oIZJblO5h13lEg=="),
                PsaParametersSetType.InjectionInformation);
            //@P279-CITACT@\*-@P8873-CITACT
            mapping.Add(Decode("FY/Fcp2uyivrpvkUYmYrmLkPCG2v7Y4DQObssB3Zxfg="),
                PsaParametersSetType.DrivingInfo);
            //@P39717-THESAU
            mapping.Add(Decode("k7hkkZ3U566LKUxZmlx80w=="), 
                PsaParametersSetType.DrivingInfo);
            //@P5805-CITACT
            mapping.Add(Decode("Wz9vO0+2Yyu4g84jlC4zxg=="),
                PsaParametersSetType.DrivingInfo);
            //@P10575-CITACT
            mapping.Add(Decode("cQUUDHi3rZ25cPUo/gmzmQ=="), 
                PsaParametersSetType.OdometerScreenState);
            //@P106180-THESAU
            mapping.Add(Decode("RRp5kDFzYzKSaZUXQCI+hw=="),
                PsaParametersSetType.ParticleFilterInfo);
            //@P6161-CITACT
            mapping.Add(Decode("tEoAW5AfShlc+rpvAawuGw=="), 
                PsaParametersSetType.ParticleFilterInfo);
            //@P3658-CITACT
            mapping.Add(Decode("dQ8AhuF2f+rzvopbWZZxMA=="), 
                PsaParametersSetType.ControlPanelParameters);
            //@P90043-THESAU
            mapping.Add(Decode("Vvb6q/Q0FnpNiyPoyKpccg=="),
                PsaParametersSetType.BasicBlocksSupply);
            //@P18279-CITACT
            mapping.Add(Decode("hk1z7qhWzlejNjcDRs6lWg=="), 
                PsaParametersSetType.BasicBlocksSupply);
            //@P6615-CITACT
            mapping.Add(Decode("UO5/xo5IHFmN6w39x1aNRw=="), 
                PsaParametersSetType.ClimateDevices);
            //@P110879-THESAU
            mapping.Add(Decode("U7goPfbtDQMLnWPrFwR1qQ=="),
                PsaParametersSetType.TripData);
            //@P2016-CITACT
            mapping.Add(Decode("fZZog2nwZ4fem0CX4hhH5Q=="), 
                PsaParametersSetType.TripData);
            //@P54051-THESAU
            mapping.Add(Decode("+5GfloLMUPR8o0gvf5Suow=="), 
                PsaParametersSetType.AirIntake);
            //@P1711-THESAU@\*
            mapping.Add(Decode("IVBddP57FNzFpqJAHEwKTETmCd1z7W/ScG6tkrBT2t4="), 
                PsaParametersSetType.Ignition);
            //@P4043-CITACT
            mapping.Add(Decode("ZqBZP9I9OwRQNiLRbdy9EA=="),
                PsaParametersSetType.Ignition);
            //@P9619-THESAU@\*
            mapping.Add(Decode("3gOHvPk8NoGje0KuwP5JaYqg5lE7OCnLe9zgxBQfEAQ="), 
                PsaParametersSetType.Purification);
            //@P31100-CITACT
            mapping.Add(Decode("5YKI5UvoWtF4krMn0/kvhQ=="), 
                PsaParametersSetType.FapAdditiveParameters);
            //@P111487-THESAU
            mapping.Add(Decode("5TqDqpGej4Sm9UFa+BeBsg=="), 
                PsaParametersSetType.DifferentElectricSupplyAndImmobilizer);
            //@P111486-THESAU
            mapping.Add(Decode("10gIVJH7gW3IRxCkG3sbPg=="),
                PsaParametersSetType.EngineCoolingAndClimateUnit);
            // @P109209-THESAU
            mapping.Add(Decode("J6A3Wd8lkhwsPsFQ4Z+Jsg=="), 
                PsaParametersSetType.DynamicInformation);
            //@P106639-THESAU
            mapping.Add(Decode("zLQuo1XswXVFNVADsCTPnw=="), 
                PsaParametersSetType.ElectricPumpData);
            //@P1735-THESAU@\*
            mapping.Add(Decode("YXtKzZvPnNausLwwruAWGFwZgt1ulFV8Vj+djTyssA4="),
                PsaParametersSetType.HeatingAndVentilation);
            //@P1948-THESAU
            mapping.Add(Decode("fwRMBPcFj4MA8g/zDuETqA=="), 
                PsaParametersSetType.FuelTankCap);
            //@P108909-THESAU
            mapping.Add(Decode("4l0S5HLMUA6/+zN1D+G2EQ=="), 
                PsaParametersSetType.HeaterSensorsAndFanState);
            //@P8919-CITACT
            mapping.Add(Decode("ze5EjOCFtVyYJdoZaav1kQ=="), 
                PsaParametersSetType.TyresPressure);
            //@P6616-CITACT
            mapping.Add(Decode("KdE+PJztSWpRVThs0Z7TAg=="), 
                PsaParametersSetType.Lighting);
            // @P439-CITACT
            mapping.Add(Decode("62KF6bGfrWk0C5K5rJnfrw=="), 
                PsaParametersSetType.Alarm);
            //@P3306-CITACT
            mapping.Add(Decode("pKmrGZeTGUIkuvQVa91Rcg=="), 
                PsaParametersSetType.Locks);
            //@P6665-CITACT@\+@T-@\+@P10783-CITACT
            mapping.Add(Decode("FDC0OznICNHWrqCJIuSMAewwNtyv67uxgbsO9B9ismdKkQCmsdgN2kfPXECbGWxb"), 
                PsaParametersSetType.MeasuredData);
            //@P6665-CITACT@\+@T-@\+@P6683-CITACT
            mapping.Add(Decode("FDC0OznICNHWrqCJIuSMAeN/w9PgpokdtpfC45ADxERlLsDFSS32IZpJ2plFd1Fq"), 
                PsaParametersSetType.InstrumentalIndication);
            //@P4012-citact
            mapping.Add(Decode("3g5mhjleWlvT3zvHibJllg=="), 
                PsaParametersSetType.VisibilityControl);
            //@P2322-CITACT
            mapping.Add(Decode("+G5EfVcMcFJZgpVIqDAdkg=="), 
                PsaParametersSetType.Doors);
            //@P5313-CITACT
            mapping.Add(Decode("T9zGnltXj3571bKmXUlf7Q=="), 
                PsaParametersSetType.ControlPanelElementsState);
            //@P3129-CITACT
            mapping.Add(Decode("zUgviWtsr54lokkkyfZ/lQ=="), 
                PsaParametersSetType.Variables);
            //@P10115-CITACT
            mapping.Add(Decode("YRPry+Joz+KuQeztRSeJmQ=="), 
                PsaParametersSetType.AggregatesState);
            //@P10039-CITACT
            mapping.Add(Decode("b5MYmjI4UCgQGWIgQvfmBg=="), 
                PsaParametersSetType.LoadSheddingLevel);
            //@P31106-CITACT@\+@P21305-CITACT
            mapping.Add(Decode("qgHKZA8lFE8Zrx1mPPBXn06cF34CmhGV751Wfhk3u8I="), 
                PsaParametersSetType.TotalAdditiveAmountDepositedCounter1);
            //@P17142-CITACT
            mapping.Add(Decode("W5lzBGALdUjnRklhHAA5VA=="),
                PsaParametersSetType.TotalAdditiveAmountInjectedCounter2);
            //@P105964-THESAU
            mapping.Add(Decode("1VGR6U/CHuT1z54Dmv0eHg=="), 
                PsaParametersSetType.DrivingInfo);
            //@P105487-THESAU@\*
            mapping.Add(Decode("kEpiKRvYfX4ITFqxVgNSWY78yMbBTzhP2fWeQx6U0og="), 
                PsaParametersSetType.VentilationDistribution);
            //@P19421-CITACT
            mapping.Add(Decode("Re5NCIA/rDdcPOSnjT3MfQ=="), 
                PsaParametersSetType.InstrumentationMeasurements);
            //@P19420-CITACT
            mapping.Add(Decode("kMdK9/5R28nBCJACth1SmA=="), 
                PsaParametersSetType.DrivingInfo);
            //@P16124-CITACT
            mapping.Add(Decode("ebX/j9CApGOwi43nzMfGBA=="),
                PsaParametersSetType.ElectricPumpData);
            //@P95862-THESAU@\*
            mapping.Add(@"@P95862-THESAU@\*", 
                PsaParametersSetType.RadioConfiguration);
            mapping.Add(@"@P124842-THESAU", 
                PsaParametersSetType.AirIntake);
            // @P103478-THESAU@\*
            mapping.Add(@"@P103478-THESAU@\*", 
                PsaParametersSetType.Purification);
            mapping.Add(@"@P26898-CITACT", PsaParametersSetType.ParametersSet1);
            mapping.Add("@P90030-THESAU@\\*", PsaParametersSetType.ControlButtonsStates);
            mapping.Add("@P298-THESAU@\\*", PsaParametersSetType.SoftwareVersions);
            mapping.Add(@"@P106646-THESAU", PsaParametersSetType.AudioAndComputer);
            mapping.Add(@"@P106672-THESAU", PsaParametersSetType.UnitsAndNotations);
            mapping.Add(@"@P87490-THESAU", PsaParametersSetType.BoardComputer);
            mapping.Add("@P115128-THESAU@\\*", PsaParametersSetType.RadioButtons);
            mapping.Add("@P110814-THESAU", PsaParametersSetType.AdditionalGearAndSafety);

            mapping.Add("@P17630-CITACT", PsaParametersSetType.BasicParameters2);
            mapping.Add("@P17631-CITACT", PsaParametersSetType.BasicParameters3);
            mapping.Add("@P3229-CITACT", PsaParametersSetType.ParametersSet2);
            mapping.Add("@P13091-CITACT", PsaParametersSetType.CommandsAndRelays);
            mapping.Add("@P27621-CITACT", PsaParametersSetType.Immobilizer);
            mapping.Add("@P20120-CITACT", PsaParametersSetType.LoadSheddingLevel);
            mapping.Add(@"@P279-CITACT@\*-@\+@P8873-CITACT", PsaParametersSetType.AdditionalGearAndSafety);
            mapping.Add(@"@P6640-CITACT@\*", PsaParametersSetType.Keys);
            mapping.Add(@"@P8168-CITACT", PsaParametersSetType.ParametersSet3);
            mapping.Add(@"@P6873-CITACT", PsaParametersSetType.MixingState);
            //mapping.Add("", PsaParametersSetType.);
            //mapping.Add("", PsaParametersSetType.);
            //mapping.Add("", PsaParametersSetType.);
            //mapping.Add("", PsaParametersSetType.);
            //mapping.Add("", PsaParametersSetType.);
        }

        public static PsaParametersSetType Get(string rawName)
        {
            if (string.IsNullOrEmpty(rawName))
            {
                return PsaParametersSetType.Mixed;
            }
            if (mapping.ContainsKey(rawName))
            {
                return mapping[rawName];
            }
            return PsaParametersSetType.Unsupported;
        }

        public static string GetName(PsaParametersSetType type)
        {
            return CodeBehindStringResolver.Resolve(type.ToString());
        }

        private static string Decode(string text)
        {
            return Cipher.Decrypt(text, "System.Int32");
        }
    }
}

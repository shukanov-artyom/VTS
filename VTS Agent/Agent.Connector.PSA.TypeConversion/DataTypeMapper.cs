using System;
using System.Collections.Generic;
using Agent.Common.Data;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.TypeConversion
{
    internal static class DataTypeMapper
    {
        private static readonly IDictionary<string, PsaParameterType> Mapping = 
            new Dictionary<string, PsaParameterType>(StringComparer.OrdinalIgnoreCase);

        static DataTypeMapper()
        {
            Fill();
        }

        private static string Decode(string s)
        {
            return Cipher.Decrypt(s, "System.FileInfo");
        }

        private static void Fill()
        {
            // @P5424-CITACT
            Mapping.Add(Decode("gnvpQ6HufAQPeEDajnu9BA=="), PsaParameterType.EngineRpm);

            // @P34627-THESAU
            Mapping.Add(Decode("rnNnd//aYV4ezfHgFf/4Sw=="), PsaParameterType.EngineRpm);

            // @P106740-THESAU
            Mapping.Add(Decode("7/yrm4vsYwLCfenzbYcIvQ=="), PsaParameterType.FuelSystemPressure);

            // @P8120-CITACT
            Mapping.Add(Decode("dD5VDfZ1WajY2jtTO3d6aQ=="), PsaParameterType.FuelSystemPressure);

            // @P111624-THESAU
            Mapping.Add(Decode("YdNTRjkn7+V2p5rh1xYtvA=="), PsaParameterType.FuelSystemPressureDelta);

            // @P36090-CITACT
            Mapping.Add(Decode("sE/HwmS6F4mWetfv04GsVw=="), PsaParameterType.FuelSystemPressureDelta);

            // @P85703-THESAU@\*1
            Mapping.Add(Decode("r7+fncqd1NOrYZIXGHnSYzXZksXKYihUVZJ+JLUD6QA="), PsaParameterType.Injector1Correction);

            // @P5187-CITACT@\*1
            Mapping.Add(Decode("lE2dDYRV2gvBSz7EmEOcMyd9HhQQR45ISZJ2pMpLQgs="), PsaParameterType.Injector1Correction);

            // @P85703-THESAU@\*2
            Mapping.Add(Decode("r7+fncqd1NOrYZIXGHnSY0NlHUmU5yP2QKgol6wen0U="), PsaParameterType.Injector2Correction);

            // @P5187-CITACT@\*2
            Mapping.Add(Decode("lE2dDYRV2gvBSz7EmEOcM5sJP5KtY2sddmq1YoNejSk="), PsaParameterType.Injector2Correction);

            // @P85703-THESAU@\*3
            Mapping.Add(Decode("r7+fncqd1NOrYZIXGHnSY2QwmRRen+XS8KtYd2w6Wa0="), PsaParameterType.Injector3Correction);

            // @P5187-CITACT@\*3
            Mapping.Add(Decode("lE2dDYRV2gvBSz7EmEOcM9KP2Zw7VwhO3PMC+Yiv9e4="), PsaParameterType.Injector3Correction);

            // @P85703-THESAU@\*4
            Mapping.Add(Decode("r7+fncqd1NOrYZIXGHnSY/YnFWHEtQVG133v/6BwoJo="), PsaParameterType.Injector4Correction);

            // @P5187-CITACT@\*4
            Mapping.Add(Decode("lE2dDYRV2gvBSz7EmEOcM/26jxqruBoT+OVgmr7Zl28="), PsaParameterType.Injector4Correction);

            // @P106754-THESAU
            Mapping.Add(Decode("iMsNXFIHw8gIgTcqhbtpTA=="), PsaParameterType.CamshaftCrankshaftSync);

            // @P36087-CITACT
            Mapping.Add(Decode("+3LUsiCkcfzbe2SEIjbcVg=="), PsaParameterType.CamshaftCrankshaftSync);

            // @P112123-THESAU
            Mapping.Add(Decode("w2UczEl46G66IwLaj2Sq6Q=="), PsaParameterType.DieselPressureRegulatorRatio);

            // @P35639-CITACT
            Mapping.Add(Decode("584bYY1iqXhj4K9JFCqCQw=="), PsaParameterType.DieselPressureRegulatorRatio);

            // @P106714-THESAU
            Mapping.Add(Decode("5vIaeCXEz+xaM/a7dNet8g=="), PsaParameterType.InjectionVolumeSetting);

            // @P7014-CITACT
            Mapping.Add(Decode("b1pRprmtHYAfvEXxY5K4tw=="), PsaParameterType.InjectionVolumeSetting);

            // @P106761-THESAU@\*1@\*4
            Mapping.Add(Decode("pWZE6WU/veUSrvGswpZcLIILyw+20+U+XmDxMW9eCJI="), PsaParameterType.VoltageInjector1and4);

            // @P10260-CITACT@\*1@\*4
            Mapping.Add(Decode("RpmjzVYc5VmsJJGDvfeNfLlZgfpWy6OESiYVex0Msno="), PsaParameterType.VoltageInjector1and4);

            // @P106761-THESAU@\*2@\*3
            Mapping.Add(Decode("pWZE6WU/veUSrvGswpZcLFsIJIJZwM9Ev4iGIRG27YA="), PsaParameterType.VoltageInjector2and3);

            // @P10260-CITACT@\*2@\*3
            Mapping.Add(Decode("RpmjzVYc5VmsJJGDvfeNfFxoElULqTAZImx1POxSGx0="), PsaParameterType.VoltageInjector2and3);

            // @P2755-CITACT@\*
            Mapping.Add(Decode("A9jnP4jBU4LhxdVWya3PxLMPiBUcE/jeXsvSS7Aup2A="), PsaParameterType.InjectorsVoltage);

            // @P84198-THESAU
            // DEBITAIR
            Mapping.Add(Decode("8hOYpnLnrTH1INFEiEoggw=="), PsaParameterType.MeasuredAirVolume);

            // @P36091-CITACT
            Mapping.Add(Decode("G67JGBTQU+oUg1S+zWAYfg=="), PsaParameterType.MeasuredAirVolume);

            // @P106747-THESAU
            Mapping.Add(Decode("BWTXCR4jbsGH346uWJnH4A=="), PsaParameterType.ExhaustCycleRatio);

            // @P35640-CITACT
            Mapping.Add(Decode("Nah+rHNBYuMen6OwUQLN3g=="), PsaParameterType.ExhaustCycleRatio);

            // @P111625-THESAU
            Mapping.Add(Decode("jYdEszNHH9undugX384XKw=="), PsaParameterType.FuelRegulatorCurrent);

            // @P35642-CITACT
            Mapping.Add(Decode("LpVKAzuiX6RjN1XJ+lsoWw=="), PsaParameterType.FuelRegulatorCurrent);

            // @P106707-THESAU
            Mapping.Add(Decode("9YRuX2U/6/ytdcGhqRApRQ=="), PsaParameterType.InjectionAdvance);

            // @P2763-CITACT
            Mapping.Add(Decode("K0RFqpjjuhFetz9wvx76fQ=="), PsaParameterType.InjectionAdvance);

            // @P106706-THESAU
            Mapping.Add(Decode("Ka/eBbvqP1p3gRnQkmvozg=="), PsaParameterType.BasicInjectionAdvance);

            // @P2754-CITACT
            Mapping.Add(Decode("H8Zl20wQpwbiFfF6RujwUQ=="), PsaParameterType.BasicInjectionAdvance);

            // @P106725-THESAU
            Mapping.Add(Decode("Ur+ZArBayqPvAxU3pmn9Yg=="), PsaParameterType.ThirdCylOff);

            // @P10261-CITACT@\+@P24379-CITACT
            Mapping.Add(Decode("kd3O912hxdBhzRIgPYmD8J3RKtWpIfV+GrqQE0Q6t3w="), PsaParameterType.ThirdCylOff);

            // @P106741-THESAU
            Mapping.Add(Decode("4/T+6IcB2YM6++bVHElghg=="), PsaParameterType.TurbinePressure);

            // @P10533-CITACT
            Mapping.Add(Decode("PbJTy3KvsZT0XtghuEc5qw=="), PsaParameterType.TurbinePressure);

            // @P106716-THESAU@\*
            Mapping.Add(Decode("yCzJHxGcr34fTU9+T+dAPCmyy+upaO4ZDgKyGtADLLE="), PsaParameterType.NominalTurbinePressure);

            // @P10279-CITACT
            Mapping.Add(Decode("v6c6HNGWevpPP85NcVLhmA=="), PsaParameterType.NominalTurbinePressure);

            // @P106745-THESAU
            Mapping.Add(Decode("cDR0UTFsQZJikggMkSr1/A=="), PsaParameterType.TurbineCycleRatio);

            // @P35643-CITACT
            Mapping.Add(Decode("KUHQMkNp1uBe7PDIeOJ2gw=="), PsaParameterType.TurbineCycleRatio);

            // @P44504-THESAU
            Mapping.Add(Decode("kb5r7sxdXzaLQ0vMEjEKhw=="), PsaParameterType.AtmosphericPressure);

            // @P26901-CITACT
            Mapping.Add(Decode("yozIqu5p7C1WYfLw7PPzVA=="), PsaParameterType.AtmosphericPressure);

            // @P36092-CITACT
            Mapping.Add(Decode("AMK+Pbo1GM7mRZuFu40eVw=="), PsaParameterType.AirTemperature);

            // @P4548-THESAU
            Mapping.Add(Decode("F1nXRN9YpP6ZhFZfs3eWaQ=="), PsaParameterType.AirTemperature);

            // @P106758-THESAU@\*
            Mapping.Add(Decode("yt8BunJsjazlCywkD4hL1r6sgqFPb/GF90weTp+3EAA="), PsaParameterType.FuelTemperature);

            // @P5199-CITACT
            Mapping.Add(Decode("JodfgrIjhnlGhVfqlXBAfg=="), PsaParameterType.FuelTemperature);

            // @P35641-CITACT
            Mapping.Add(Decode("vPrXjb8qvv6tRg+QB1SJbg=="), PsaParameterType.ExhaustRecirculationGateCycle);

            // @P106744-THESAU
            Mapping.Add(Decode("2V/ZRo+IRFf/Mvh6EiOWZg=="), PsaParameterType.ExhaustRecirculationGateCycle);

            // @P7902-CITACT
            Mapping.Add(Decode("NfiqvM88QVCBrG948XZ63w=="), PsaParameterType.BatteryVoltage);

            // @P3126-CITACT
            Mapping.Add(Decode("elhRocQe3xRr6u3DvJ0q8A=="), PsaParameterType.BatteryVoltage);

            // @P4525-THESAU@\*
            Mapping.Add(Decode("N91QKSqB5mWrDJCglXWQk/zMBXaVbOEWFQ7M856pdfs="), PsaParameterType.BatteryVoltage);

            // @P105988-THESAU@\*1@\+@T(@\+@P44699-THESAU@\+@T)
            Mapping.Add(Decode("Pfh1zkKB5HTqT34asB3Ci83QxGRFpOivVNdGoOYWYPb0yB1e60jTRcHrGjZ6QrWpKnAYR1wBO4aLtt/gVC+l+w=="), PsaParameterType.Sensor1Voltage);

            // @P36093-CITACT@\*1
            Mapping.Add(Decode("+j0/rNidCn2iKM5B0y9waq1fmOKtZD8LjYKGpzAiFYE="), PsaParameterType.Sensor1Voltage);

            // @P105988-THESAU@\*2@\+@T(@\+@P90468-THESAU@\*)
            Mapping.Add(Decode("Pfh1zkKB5HTqT34asB3Cizxt171KD/v8Olu5G1a+sUvROoDzZx1/Qn4jRn/yYLDN"), PsaParameterType.Sensor2Voltage);

            // @P36094-CITACT@\*2
            Mapping.Add(Decode("spDWUjafPnVRmkSbpnPIz9mwAVfNEC92lZoW5J5xLsA="), PsaParameterType.Sensor2Voltage);

            // @P107892-THESAU@\*
            Mapping.Add(Decode("R2p2c6nv/zBDA3lvI7hcPRVio1OvtWAM0VhAor0iz2Q="), PsaParameterType.PreHeatRelay);

            // @P2652-CITACT
            Mapping.Add(Decode("3PHo1UCZSprk6vnSsv7a2Q=="), PsaParameterType.PreHeatRelay);

            // @P106724-THESAU
            Mapping.Add(Decode("+AZ5NuNbtI8OTEOBVxqkww=="), PsaParameterType.ConditioningCutOffRequest);

            // @P10262-CITACT
            Mapping.Add(Decode("hnfYJRDWCtdpvLQad3Yltg=="), PsaParameterType.ConditioningCutOffRequest);

            // @P110462-THESAU@\*
            Mapping.Add(Decode("hBZjSwWRyj9KLsRjS3SiVCrD4a5mlnoic9eeQy/LFEw="), PsaParameterType.RelayLowSpeedVent);

            // @P35644-CITACT
            Mapping.Add(Decode("oZuTmXzqzNtriOgF9/f6RA=="), PsaParameterType.RelayLowSpeedVent);

            // @P112937-THESAU@\*
            Mapping.Add(Decode("5sAIIS8YxFE0T0mFOOGTg+0f+sihCIVrvAl8GU0aiJg="), PsaParameterType.SpeedVent);

            // @P36095-CITACT@\*
            Mapping.Add(Decode("HWxgZQy9HyjrOeCnoQkIXkBqvK8ec4Aa0Z/SJFe5/Ek="), PsaParameterType.SpeedVent);

            // @P1543-CITACT@\*
            Mapping.Add(Decode("InBjP2jhhIQrTiTCizY8QCtXzUq2BBq7cs8VooIR5JM="), PsaParameterType.InjectionTime);

            // @P4534-THESAU
            Mapping.Add(Decode("AEfvDZD3CKWt5Tvdp1eTzQ=="), PsaParameterType.InjectionTime);

            // @P4896-CITACT
            Mapping.Add(Decode("qJ9qwgoHNCWL72WBotYY9g=="), PsaParameterType.InjectionOffWhenSlowdown);

            // @P124840-THESAU
            Mapping.Add(Decode("N4ekuKpO2sCGWSE/FbkNng=="), PsaParameterType.InjectionOffWhenSlowdown);

            // @P12030-CITACT
            Mapping.Add(Decode("/4XWegWqZuENNNDUnZQ+dw=="), PsaParameterType.UpperOxygenSensorVoltage);

            // @P124850-THESAU
            Mapping.Add(Decode("yWPHcLFKECwOfev93q0snA=="), PsaParameterType.UpperOxygenSensorVoltage);

            // @P12031-CITACT
            Mapping.Add(Decode("ZlKULTOz8LEpnydnX1wYZQ=="), PsaParameterType.LowerOxygenVoltage);

            // @P124852-THESAU
            Mapping.Add(Decode("XBB8bTR3A4l4vG2WHyWmcA=="), PsaParameterType.LowerOxygenVoltage);

            // @P2286-CITACT
            Mapping.Add(Decode("VBy7V56SeKx01sH5csupaA=="), PsaParameterType.MixRegState);

            // @P128676-THESAU
            Mapping.Add(Decode("f9L/jFLTheJUs9KhiTKOHQ=="), PsaParameterType.MixRegState);

            // @P104601-THESAU
            Mapping.Add(Decode("DzVAC1/wBn11ddEmcOyvsQ=="), PsaParameterType.ParticleFilterStatus);

            // @P27002-CITACT
            Mapping.Add(Decode("eHMrTgRGvQ0/jdlFGq2f9Q=="), PsaParameterType.ParticleFilterStatus);

            // @P110081-THESAU
            Mapping.Add(Decode("N8Ed/ALmxYs19/0+9AR/IQ=="), PsaParameterType.RegenerationAssistanceRequest);

            // @P10534-CITACT
            Mapping.Add(Decode("ymJXNe9i/Zc3jR1lzSGS4A=="), PsaParameterType.RegenerationAssistanceRequest);

            // @P106755-THESAU@\*
            Mapping.Add(Decode("/bCZ8FTBAonmVTpAZOuSiRCmvhEF/BMce13unpgjdJM="), PsaParameterType.CatConverterUpstreamTemperature);

            // @P10284-CITACT
            Mapping.Add(Decode("szr6q8sHV0ILgzW/JwsFSw=="), PsaParameterType.CatConverterUpstreamTemperature);

            // @P106756-THESAU@\*
            Mapping.Add(Decode("1geLG6O6AR1qucAoz1R5iLZ351gYtnIlsNdqsf6aNzw="), PsaParameterType.CatConverterDownstreamTemperature);

            // @P10285-CITACT
            Mapping.Add(Decode("6VcwQfLibuYaJPNF0a8IVw=="), PsaParameterType.CatConverterDownstreamTemperature);

            // @P106726-THESAU
            Mapping.Add(Decode("fZKqbcY8o5zZI+yV4mxpDQ=="), PsaParameterType.ParticleFilterInletOutletPressureDifference);

            // @P27676-CITACT
            Mapping.Add(Decode("vKsx2NmkzMTBVQqxhnqFQg=="), PsaParameterType.ParticleFilterInletOutletPressureDifference);

            // @P106721-THESAU
            Mapping.Add(Decode("2FBl8sICmqmL75USC/7dKg=="), PsaParameterType.AirFlowVolume);

            // @P10287-CITACT
            Mapping.Add(Decode("xGarthfaZnVLDRbvWaB8cw=="), PsaParameterType.AirFlowVolume);

            // @P116727-THESAU@\*
            Mapping.Add(Decode("QEkLEcNkCzmPcdGgFfMwGRTCvo42ow/SPzecf2CpJYw="), PsaParameterType.InletAirHeaterThrottleOpenCycleRatio);

            // @P35646-CITACT
            Mapping.Add(Decode("VvxpMbcD8D6p8T1+jVKa1g=="), PsaParameterType.InletAirHeaterThrottleOpenCycleRatio);

            // @P106720-THESAU
            Mapping.Add(Decode("60az9X2ObsxaW8bt7paNCg=="), PsaParameterType.PostInjectionFlow);

            // @P10289-CITACT
            Mapping.Add(Decode("x0X2IezrQuWDaUQK5u1XEA=="), PsaParameterType.PostInjectionFlow);

            // @P103912-THESAU
            Mapping.Add(Decode("66EgKvZ3KYWH/zCW5YP7KQ=="), PsaParameterType.PostInjectionPhasing);

            // @P10290-CITACT
            Mapping.Add(Decode("iCBRhbTAjoEa2ZLzVocFew=="), PsaParameterType.PostInjectionPhasing);

            // @P106742-THESAU
            Mapping.Add(Decode("lhFwynK6QSMiZTFdnj06Gg=="), PsaParameterType.TotalFuelAdditiveQuantity);

            // @P35647-CITACT
            Mapping.Add(Decode("MgsDPvWcAg5riAIvMQiRFQ=="), PsaParameterType.TotalFuelAdditiveQuantity);

            // @P106735-THESAU
            Mapping.Add(Decode("8RoKDhBqw/ZV2FBBwmF9oQ=="), PsaParameterType.DistanceTravelledSinceLastRegeneration);

            // @P10536-CITACT
            Mapping.Add(Decode("PRoS7kF3vQfkfxDmGS7beQ=="), PsaParameterType.DistanceTravelledSinceLastRegeneration);

            // @P106736-THESAU@\*5
            Mapping.Add(Decode("CNgZ0CMqxecNBghaSnzyqLoPvYm1dst7Hp/l9VPxNrg="), PsaParameterType.AverageDistanceOfLast5Regenerations);

            // @P10292-CITACT@\*5
            Mapping.Add(Decode("3muSgxU8CgXY7FnBgsPm49n2ZZagTffvsPDdrzlnVLY="), PsaParameterType.AverageDistanceOfLast5Regenerations);

            // @P111628-THESAU
            Mapping.Add(Decode("pbsaeQEElQrK4BozzUC+aw=="), PsaParameterType.ActivationOfElectricalConsumers);

            // @P12392-CITACT
            Mapping.Add(Decode("yyRYYj/X7x9ROiF6mCkR2A=="), PsaParameterType.ActivationOfElectricalConsumers);

            // @P5423-CITACT
            Mapping.Add(Decode("xvtXES5YCROmwvb8B4c1uQ=="), PsaParameterType.VehicleSpeed);

            // @P59531-THESAU
            Mapping.Add(Decode("NJXqhZBD4QEsCGG8zbyMFw=="), PsaParameterType.VehicleSpeed);

            // @P59531-THESAU@\*
            Mapping.Add(Decode("+3YtvNrtzP9dfWL5I5Z3KVyZUGUFdoECybVjSzmgCYU="), PsaParameterType.VehicleSpeed);

            // @P12694-THESAU@\*
            Mapping.Add(Decode("H1mDt6RRUKTb+KO1IfVWf6xJPVxlNdHCHBEt+CUsdMc="), PsaParameterType.VehicleSpeed);

            // @P13738-CITACT
            Mapping.Add(Decode("IevQx/BdwRB4QRAE4w17ew=="), PsaParameterType.AcceleratorPedalPosition);

            // @P36098-CITACT
            Mapping.Add(Decode("8BV3MyiAg/ia5m/DQZOhvw=="), PsaParameterType.AutomaticGearboxRecognition);

            // @P21727-CITACT
            Mapping.Add(Decode("NclNWCnaaBzy1O01pmqzaA=="), PsaParameterType.MainBrakeSignal);

            // @P21728-CITACT
            Mapping.Add(Decode("jBGbT5NqvnOQdXxkmxTW6w=="), PsaParameterType.RedundantBrakeSignal);

            // @P21729-CITACT
            Mapping.Add(Decode("+Y7oPCih9l/GNKkYOgcf+g=="), PsaParameterType.ClutchPedalSignal);

            // @P21730-CITACT@\*2
            Mapping.Add(Decode("7IXsfMeDLlqOTIw7bdvBlLm+6eQa2kHRv6hOJrjn2uk="), PsaParameterType.AcceleratorPedalHardSpotSignal2);

            // @P21730-CITACT@\*1
            Mapping.Add(Decode("7IXsfMeDLlqOTIw7bdvBlNpCQJ4wAsKoqHRAMfamLSU="), PsaParameterType.AcceleratorPedalHardSpotSignal1);

            // @P124843-THESAU
            Mapping.Add(Decode("UKhOfY5txeU7f/I8mwafdQ=="), PsaParameterType.ThrottleStatus);

            // @P112875-THESAU
            Mapping.Add(Decode("RO3U26Tcs7cAPvyTRHUp+Q=="), PsaParameterType.ThrottlePositionVoltage);

            // @P56982-THESAU
            Mapping.Add(Decode("escUVw63+pnxScC1nYOtrg=="), PsaParameterType.ThrottleAngle);

            // @P32381-THESAU
            Mapping.Add(Decode("f+aiejz7BAf3U36iIEZepA=="), PsaParameterType.ManifoldPressure);

            // @P4571-THESAU
            Mapping.Add(Decode("KlsQSfhZWXLI06LWnpPJ6A=="), PsaParameterType.CanisterSolenoidValveStatus);

            // @P112031-THESAU
            Mapping.Add(Decode("U4DxA2G4fZvEGSG85HwmqQ=="), PsaParameterType.CanisterElectrovalveOCR);

            // @P81592-THESAU
            Mapping.Add(Decode("P9OzSSHepvcnUVZ+/Km4Gw=="), PsaParameterType.IgnitionAdvance);

            //@P8084-CITACT
            Mapping.Add(Decode("YqlZ5Ssn7EZwIrdIeVL9Lg=="), PsaParameterType.IgnitionAdvance);

            // @P111757-THESAU@\*@T1/4
            Mapping.Add(Decode("Qe+toOG13dMsYU6dqXI1F0ghquZ1mTF16VvSoMwAT9o="), PsaParameterType.CylinderCoilChargeTime14);
            //@P12120-CITACT@\*1/4
            Mapping.Add(Decode("rkKnP53jksvqKgiPGIhAgp5a5er4+hOhpHhEESKaPzs="), PsaParameterType.CylinderCoilChargeTime14);

            // @P111757-THESAU@\*@T2/3
            Mapping.Add(Decode("Qe+toOG13dMsYU6dqXI1FxWmdDHxEpf/H9pFEMwwnf0="), PsaParameterType.CylinderCoilChargeTime23);
            //@P12120-CITACT@\*2/3
            Mapping.Add(Decode("rkKnP53jksvqKgiPGIhAghWeQ/4sGI6VQD6A7hZupj4="), PsaParameterType.CylinderCoilChargeTime23);

            // @P124851-THESAU
            Mapping.Add(Decode("5oDPkDvszUM+qBD89ZfASg=="), PsaParameterType.HigherOxygenSensorState);

            // @P87030-THESAU@\*
            Mapping.Add(Decode("lqOe32QSUocflYBRROqEHIhOwHNweIG0j4dcPxatTdE="), PsaParameterType.LowerOxygenSensorState);

            // @P112874-THESAU
            Mapping.Add(Decode("CRCuXjZU+w0GczWxH4K4Ig=="), PsaParameterType.CatalyticConverterCheck);

            // @P91477-THESAU@\*
            Mapping.Add(Decode("vwpEwzMtbvHPbAh//ecnq+E+XxjxrmZyv6gi6Gk6zZo="), PsaParameterType.FanAssemblyLowSpeedControl);

            // @P95482-THESAU@\*
            Mapping.Add(Decode("LLQIlPj1LYLCsDAIFhFqwP5/RwjjgUQhIk1xsnLPcgE="), PsaParameterType.FanAssemblyHighSpeedControl);

            // @P86968-THESAU
            Mapping.Add(Decode("uyHId43fdher/7q6rK/aUQ=="), PsaParameterType.MeasuredFanSpeed);

            // @P62146-THESAU
            Mapping.Add(Decode("f+77aT3+3+awttksBsP3Rg=="), PsaParameterType.AirConditioningAuthorisation);

            // @P86855-THESAU@\*
            Mapping.Add(Decode("PaZhZcXsLW63ploUT0Zgd/j0L6k1+1ID4DfKxRWr2kM="), PsaParameterType.AirConditioningRequestInput);

            // @P58853-THESAU
            Mapping.Add(Decode("tKWx4LvGoZz9MnkiacSEEA=="), PsaParameterType.EngineTorque);

            // @P105983-THESAU
            Mapping.Add(Decode("M12Pxho+jOUrF4mnbyDKaQ=="), PsaParameterType.GearboxGear);

            // @P36097-CITACT
            Mapping.Add(Decode("IJyLB1pZMwfjCkY3aIRTfQ=="), PsaParameterType.GearboxGear);

            // @P50143-THESAU
            Mapping.Add(Decode("7McjZnvA+OvXPaj0FDuFrg=="), PsaParameterType.SupplyRelayControl);

            // @P111491-THESAU
            Mapping.Add(Decode("yQeJ1G0lzQ+KUZiSkFClJg=="), PsaParameterType.ECULocking);

            // @P105556-THESAU
            Mapping.Add(Decode("RTpjPwFw8ugEhHc1RhT1ag=="), PsaParameterType.CodedEngineImmobiliserProgrammingStatus);

            // @P112018-THESAU
            Mapping.Add(Decode("pQbp7cTnXCFREgpa0IK0zw=="), PsaParameterType.ProblemsDetectedWhenTransmittingTheUnlockCode);

            // @P119345-THESAU
            Mapping.Add(Decode("jaC6HtY6g48e3ITvQf55AQ=="), PsaParameterType.EspEcuSupplyVoltage);

            // @P107571-THESAU
            Mapping.Add(Decode("HHfJOywhpweX2Uvsogquiw=="), PsaParameterType.FrontLeftWheelSpeed);

            // @P107570-THESAU
            Mapping.Add(Decode("xZrLPfOsOQGKTwjwXLqz4A=="), PsaParameterType.FrontRightWheelSpeed);

            // @P99124-THESAU
            Mapping.Add(Decode("/U8zHL89XeO4BtLlzargFA=="), PsaParameterType.RearLeftWheelSpeed);

            // @P99123-THESAU
            Mapping.Add(Decode("iW2G0TbLKtHR9me4XSqTKQ=="), PsaParameterType.RearRightWheelSpeed);

            // @P4158-THESAU
            Mapping.Add(Decode("dJTKiE4ezKGr6eP08Cw4ig=="), PsaParameterType.BrakeFluidLevel);

            // @P110493-THESAU
            Mapping.Add(Decode("vGP6SDO5QHvjFh6rl+Gukg=="), PsaParameterType.BrakeLiningsWear);

            // @P8335-THESAU@\+@P108919-THESAU@\*
            Mapping.Add(Decode("Vgj33kvIB2WtvCHObBxzxCes/SVxoRtzHTY9AYGbEjbFKihZmMYpuVhTsyntlP2T"), PsaParameterType.SpeedElectropumpAssemblyMotor);

            // @P59533-THESAU
            Mapping.Add(Decode("il8S0M1IbpCBOqacu2zFLA=="), PsaParameterType.SteeringWheelSpeed);

            // @P108824-THESAU
            Mapping.Add(Decode("cb6RIKDgcClGuiT3JFNVxg=="), PsaParameterType.SteeringWheelRotationDirection);

            // @P9770-THESAU
            Mapping.Add(Decode("ccb5hmazz7q5BZdlxGKEOg=="), PsaParameterType.EngineOilTemperature);

            // @P61834-THESAU@\*@\+@P108919-THESAU@\*
            Mapping.Add(Decode("+4LhXWSDqcDLWdyQyS9k8RC+zIca+IgAoOk6YrjiFONfTKihEAW59bzlAYp9SeNZ"), PsaParameterType.ElectropumpAssemblyMotorCurrent);

            // @P32335-THESAU@\*@\+@P10169-THESAU@\*@\+@P108919-THESAU@\*
            Mapping.Add(Decode("lU86Fj+P6WBGxZg6UdeNd2PmFpZ2WY1Ss4HlpAsxc+eAcFBUzp6NZxnJZlnbNBphwQaWG5CF59j34eECOE4Zbw=="), PsaParameterType.ElectropumpAssemblyMotorTerminalsVoltage);

            // @P14784-THESAU
            Mapping.Add(Decode("nUe6WFg6yxfx+Lv/4ItWmg=="), PsaParameterType.EngineRunningInformation);

            // @P5261-CITACT
            Mapping.Add(Decode("FL8MOaHDtZ3e6TaaRLakjg=="), PsaParameterType.BrightnessButton);

            // @P12059-CITACT
            Mapping.Add(Decode("C9tVpRH3md6USCT7urI8+Q=="), PsaParameterType.OdometerResetButton);

            // @P3675-CITACT
            Mapping.Add(Decode("HB37wr0dzQN3d3HAWRPDLQ=="), PsaParameterType.TripometerDisplayUnits);

            // @P3672-CITACT
            Mapping.Add(Decode("UyqEXphtlHx+fNjdg90zYg=="), PsaParameterType.MaintenanceIndicator);

            // @P3671-CITACT
            Mapping.Add(Decode("jAmEZ8hFUQz9mmCF2IUfhA=="), PsaParameterType.OilLevelFunction);

            // @P3670-CITACT
            Mapping.Add(Decode("aPjtOEHUMMLKk+1vrkX2yQ=="), PsaParameterType.Gearbox);

            // @P111696-THESAU
            Mapping.Add(Decode("/ilsWkWjnpaNLaBN42uTww=="), PsaParameterType.RearElectricWindowSwitchesSupply);

            // @P8875-CITACT
            Mapping.Add(Decode("vl+JxjY8Wxymjd2PNu/GZQ=="), PsaParameterType.RearElectricWindowSwitchesSupply);

            // @P6606-CITACT
            Mapping.Add(Decode("MviZiBn/aVdQOYNPe5J0Hw=="), PsaParameterType.PowerSupplyOfRainSensor);

            // @P80788-THESAU@\*
            Mapping.Add(Decode("Z8D8vfJ5o5lh6na0J5sX5fQ6pGCaAiDJie7Al0SI+ww="), PsaParameterType.PowerSupplyOfRainSensor);

            // @P117206-THESAU@\+@T(@\+@P125280-THESAU@\+@T)
            Mapping.Add(Decode("EueoLIoVW4NjJXQuhQsWwgUtOneQeUaanBG2SzWilsrNR9a0c6rgZE/buChRogm7"), PsaParameterType.EngineRunningInformation);

            // @P17647-CITACT
            Mapping.Add(Decode("OXEDXm3jUvM/2XqcKk5sqQ=="), PsaParameterType.EngineRunningInformation);

            // @P120085-THESAU
            Mapping.Add(Decode("TTUOFWKxdzTOI7Qgfztm3g=="), PsaParameterType.RearElectricWindowSwitchesDeactivation);

            // @P8877-CITACT
            Mapping.Add(Decode("bZZiovkLP48uURzouJdBdw=="), PsaParameterType.RearElectricWindowSwitchesDeactivation);

            // @P119078-THESAU
            Mapping.Add(Decode("vX4usP+K+Yip4OR7WBGu/w=="), PsaParameterType.EconomyModeStatus);

            // @P6607-CITACT
            Mapping.Add(Decode("GFQ+iow4mi8gDKN8vvM65g=="), PsaParameterType.EconomyModeStatus);

            // @P119080-THESAU
            Mapping.Add(Decode("rSDFCECSA5WU60KuvwPOmw=="), PsaParameterType.FactoryModeStatus);

            // @P3934-CITACT
            Mapping.Add(Decode("DBtN0f9P89ciCrVz3+BPyg=="), PsaParameterType.FactoryModeStatus);

            // @P119759-THESAU
            Mapping.Add(Decode("QpQTUaxhqT4C8W7R9L9idw=="), PsaParameterType.PresenceOfPlusAccessories);

            // @P15638-CITACT
            Mapping.Add(Decode("GeAzi1v2rWNNHPVs+CPOow=="), PsaParameterType.PresenceOfPlusAccessories);

            // @P119760-THESAU
            Mapping.Add(Decode("QpEJyYUebb0zp2XwbovK8A=="), PsaParameterType.PresenceOfPlusIgnition);

            // @P15639-CITACT
            Mapping.Add(Decode("7w4MuA2DkMp3NEwQL8nqoA=="), PsaParameterType.PresenceOfPlusIgnition);

            // @P107026-THESAU@\*
            Mapping.Add(Decode("KqFVO8LlOFqHfefZHCYaUVuSxmb12DQWn6mXaIDPOGE="), PsaParameterType.AlternatorExcitationVoltage);

            // @P8888-CITACT
            Mapping.Add(Decode("ikadwchEFkh54CP0QuBCeg=="), PsaParameterType.AlternatorExcitationVoltage);

            // @P117207-THESAU
            Mapping.Add(Decode("4WK4QRojekUGeSwzHZtCGw=="), PsaParameterType.FuelPumpSupplyCutOff);

            // @P8898-CITACT
            Mapping.Add(Decode("5OurhgcFEWaOPu512uZlGg=="), PsaParameterType.FuelPumpSupplyCutOff);

            // @P8899-CITACT
            Mapping.Add(Decode("oPPjvrJkw80PImsvH4+d1g=="), PsaParameterType.FuelPumpRelayOutputStatus);

            // @P108453-THESAU@\*
            Mapping.Add(Decode("o2qTtwZC0JsQcyPh0U/N4RgXSoEBrX4k6qOkAaYYoO8="), PsaParameterType.AirPumpControl);

            // @P8900-CITACT
            Mapping.Add(Decode("9U7pYrh3FS6Hkb+sK6vOSw=="), PsaParameterType.AirPumpControl);

            // @P8901-CITACT
            Mapping.Add(Decode("inPZQEzIYCidsObx8F7vxQ=="), PsaParameterType.DieselWaterAirPumpRelayOutputStatus);

            // @P107397-THESAU
            Mapping.Add(Decode("xXJfBNkhobSHByg88xa95A=="), PsaParameterType.BodyworkVanPlusStatus);

            // @P8878-CITACT
            Mapping.Add(Decode("y86Xi8rr+R+5kXOyr0DkHw=="), PsaParameterType.BodyworkVanPlusStatus);

            // @P111697-THESAU
            Mapping.Add(Decode("vVGKs8CetY2IIquG0bspYQ=="), PsaParameterType.ComfortVanPlusStatus);

            // @P8879-CITACT
            Mapping.Add(Decode("uq9DpRcsO2OgqHrAjgyuMw=="), PsaParameterType.ComfortVanPlusStatus);

            // @P100329-THESAU
            Mapping.Add(Decode("bY67SyWCh9VZiW0K3fHaOQ=="), PsaParameterType.AirConditioningCompressorControl);

            // @P15630-CITACT
            Mapping.Add(Decode("MiMqsw+Q5+2UuTms9Ln57g=="), PsaParameterType.AirConditioningCompressorControl);

            // @P13879-CITACT
            Mapping.Add(Decode("kaCw248jPvoZIswk/W1aSQ=="), PsaParameterType.FanMidSpeedControl);

            // @P120093-THESAU@\*
            Mapping.Add(Decode("WQo9OEC/i8unJrDpjksjPNmT5MYkdR6A2DN9Fw8+nvI="), PsaParameterType.FanAssemblyControl);

            // @P111700-THESAU
            Mapping.Add(Decode("4KR67eYcJKR2+W08bVcsFA=="), PsaParameterType.EvaporatorSensorTemperature);

            // @P8904-CITACT
            Mapping.Add(Decode("6FtGLFJDO7x2M3LbVVQ3Eg=="), PsaParameterType.EvaporatorSensorTemperature);

            // @P120094-THESAU
            Mapping.Add(Decode("7XLXc/FnYkI0OsuVk1cfpQ=="), PsaParameterType.EvaporatorIcingUpSafetyStatus);

            // @P8907-CITACT
            Mapping.Add(Decode("wZckyIksR7PTETEwP7bIBA=="), PsaParameterType.EvaporatorIcingUpSafetyStatus);

            // @P8909-CITACT
            Mapping.Add(Decode("UhtagDiIOdgruIoaiWI5ZA=="), PsaParameterType.AirConditioningCompressorRelayOutput);

            // @P15537-CITACT
            Mapping.Add(Decode("WFq+WAitCP86/jlteBFu3g=="), PsaParameterType.FanRelayOutput);

            // @P90120-THESAU
            Mapping.Add(Decode("ZsWNfx6dvGJUsVMsP3BpNg=="), PsaParameterType.AverageJourneyConsumption);

            // @P6684-CITACT
            Mapping.Add(Decode("9KoMR4jX+mXGOpAKbPMDDA=="), PsaParameterType.AverageJourneyConsumption);

            // @P107136-THESAU
            Mapping.Add(Decode("TwNUTWf1JJt0mSuwtg7O3A=="), PsaParameterType.AverageFuelConsumption);

            // @P6685-CITACT
            Mapping.Add(Decode("PH1radLiBNwxhTPZkKSMXw=="), PsaParameterType.AverageFuelConsumption);

            // @P108490-THESAU
            Mapping.Add(Decode("bGyFjM6BaVLDmYCdpL6p7Q=="), PsaParameterType.SmoothedConsumption);

            // @P6687-CITACT
            Mapping.Add(Decode("V6fQUqusjtGww4Cy0vtY5w=="), PsaParameterType.SmoothedConsumption);

            // @P107566-THESAU
            Mapping.Add(Decode("Yv9o3bVhqAMu5iGSH2iGIQ=="), PsaParameterType.TotalMileage);

            // @P4840-CITACT
            Mapping.Add(Decode("mDgKkn02PKuhU+nE7mfb3Q=="), PsaParameterType.TotalMileage);

            // @P36096-CITACT
            Mapping.Add(Decode("Bq87sMdp0LyOYDhfi70Qiw=="), PsaParameterType.MeasuredFanSpeed);

            // @P15487-CITACT
            Mapping.Add(Decode("UeW6GK/vgi2kCBJJ/eOd2A=="), PsaParameterType.EspDeactivationRequest);

            // @P15763-CITACT
            Mapping.Add(Decode("9ewNasp+oyaxRBxz7Ehh1g=="), PsaParameterType.AutomaticGearboxSnowProgramRequest);

            // @P16239-CITACT
            Mapping.Add(Decode("scpyImUIyiOd3LaR9YOgvg=="), PsaParameterType.ParkingBrake);

            // @P2323-CITACT
            Mapping.Add(Decode("FDy3hweOPK9+DjoRaSFkbQ=="), PsaParameterType.BrakePedal);

            // @P4846-CITACT
            Mapping.Add(Decode("82DU8AQQ/WktquFswPbzxw=="), PsaParameterType.ReverseGearInfo);

            // @P8471-citact
            Mapping.Add(Decode("vz4ReoKqfemOW3MyL+RhCA=="), PsaParameterType.DriverSeatBeltFastened);

            // @P8472-CITACT
            Mapping.Add(Decode("U9cYauy3FCEiCyYcrA352g=="), PsaParameterType.PassengerSeatBeltFastened);

            // @P15602-CITACT
            Mapping.Add(Decode("kGBSPuXOyZdNR/UIcGh6qA=="), PsaParameterType.AutomaticGearboxInParkPosition);

            // @P19825-CITACT
            Mapping.Add(Decode("8Hz2YXHJ9qKyWcGGjNsXoQ=="), PsaParameterType.FrontLhWheelTyreDeflationDetectionSensorStatus);

            // @P19826-CITACT
            Mapping.Add(Decode("0+KHOS5bOZ9HB7v0/knjRA=="), PsaParameterType.FrontRhWheelTyreDeflationDetectionSensorStatus);

            // @P19827-CITACT
            Mapping.Add(Decode("1gpOVRzG3h+sA9rdqlsGwA=="), PsaParameterType.RearRhWheelTyreDeflationDetectionSensorStatus);

            // @P19828-CITACT
            Mapping.Add(Decode("C9TubDaldWORPf8w9Tcvbg=="), PsaParameterType.RearLhWheelTyreDeflationDetectionSensorStatus);

            // @P20086-CITACT
            Mapping.Add(Decode("Ah9FLtX9IfYnLsmJqu8xtQ=="), PsaParameterType.ValidityOfTyreDeflationSensorStatusParameters);

            // @P8919-CITACT@\+@T-@\+@P19833-CITACT
            Mapping.Add(Decode("N4pwtLHDBr50U3ycUzwfz7YAHC9EeDEY6NRmDs77dslVGsU5PMw69Cc6ZWKy8H5h"), PsaParameterType.DeflationDetectionFrontLhTyrePressure);

            // @P8919-CITACT@\+@T-@\+@P19834-CITACT
            Mapping.Add(Decode("N4pwtLHDBr50U3ycUzwfz4ExqQ33vJS19GhsOaonOJYO0JqDuT+cQEjqxCZQYk/7"), PsaParameterType.DeflationDetectionFrontRhTyrePressure);

            // @P8919-CITACT@\+@T-@\+@P19835-CITACT
            Mapping.Add(Decode("N4pwtLHDBr50U3ycUzwfz1vG67b67BXZUFys0AN9jblcO2iGCyN2b3lqsgpKPwQS"), PsaParameterType.DeflationDetectionRearRhTyrePressure);

            // @P8919-CITACT@\+@T-@\+@P19836-CITACT
            Mapping.Add(Decode("N4pwtLHDBr50U3ycUzwfz5IchstFa+ZCessCxndtL7qfg6oyqRSI09b1kk2gn6k4"), PsaParameterType.DeflationDetectionRearLhTyrePressure);

            // @P19829-citact@\*1
            Mapping.Add(Decode("p+Whl1PytOUYpBSdT6yCefP82wJDFCgCHNipBHr7UUM="), PsaParameterType.FrontLhTyrePressureStatus);

            // @P19830-citact@\*2
            Mapping.Add(Decode("n6WU96oRSB6vdXfQmfmMNmlk8i6iqdAJj7NmRZwF+D8="), PsaParameterType.FrontRhTyrePressureStatus);

            // @P19831-citact@\*3
            Mapping.Add(Decode("JJfSfvfv5atUIEx2QsP4/kOLUxjIVC68NzZFpDYAufk="), PsaParameterType.RearRhTyrePressureStatus);

            // @P19832-citact@\*4
            Mapping.Add(Decode("91FIoveJ7j/QQ8liC/YtRazW5fJj4OJEdOpwtgDTJEc="), PsaParameterType.RearLhTyrePressureStatus);

            // @P6617-citact
            Mapping.Add(Decode("kN+XYlsxJbCQty6fxnik8Q=="), PsaParameterType.FrontSillLightingControl);

            // @P8945-CITACT
            Mapping.Add(Decode("eOeJYloZf2eM+4F4sUwotg=="), PsaParameterType.RearFogLampsControl);

            // @P4795-CITACT
            Mapping.Add(Decode("PiYAgpLga1kMrvumC7sr5g=="), PsaParameterType.RightHandIndicatorsControl);

            // @P4796-CITACT
            Mapping.Add(Decode("38CWXUPG4ptgnrpHJUEnmg=="), PsaParameterType.LeftHandIndicatorsControl);

            // @P5975-CITACT
            Mapping.Add(Decode("goHO8Pv6kX/nlqNBriQxcA=="), PsaParameterType.BootLightingCommand);

            // @P6618-CITACT
            Mapping.Add(Decode("u0stpfb+ydeKjihyfPRHHA=="), PsaParameterType.InteriorLampControl);

            // @P12713-CITACT
            Mapping.Add(Decode("UeASI7SJS4MEvngP7xZ/pg=="), PsaParameterType.HornControl);

            // @P8948-CITACT
            Mapping.Add(Decode("gMZV9j9lhFxC8YC0SFOYUg=="), PsaParameterType.DippedBeamControl);

            // @P8950-CITACT
            Mapping.Add(Decode("t3zMtm3HGxLjqawySjRSlQ=="), PsaParameterType.MainBeamControl);

            // @P6626-CITACT
            Mapping.Add(Decode("TfLImm0n6lb5RTb2N0x8Kw=="), PsaParameterType.InteriorLampRequest);

            // @P6633-CITACT
            Mapping.Add(Decode("x+aUDtE6irbOL/YFcPIDNg=="), PsaParameterType.AutomaticLightingMode);

            // @P15873-CITACT
            Mapping.Add(Decode("cudRPcsXhJOaLMzdD6cFrg=="), PsaParameterType.HeadlampBeamCorrectorFault);

            // @P8965-CITACT
            Mapping.Add(Decode("0GNPLBbMRahkv86c5uB7jQ=="), PsaParameterType.InteriorLampControlRelayOutput);

            // @P8966-CITACT
            Mapping.Add(Decode("cN0IuXveHA7ugz/vlHPiKw=="), PsaParameterType.BootLampControlRelayOutput);

            // @P8968-CITACT
            Mapping.Add(Decode("6LMgz1DLeEWYEdHMPZhDTw=="), PsaParameterType.RhAndLhIndicatorRelayOutput);

            // @P8969-CITACT
            Mapping.Add(Decode("SN4QsNKn8v6jd7kERlyVCw=="), PsaParameterType.SidelampsControl);

            // @P6638-CITACT
            Mapping.Add(Decode("UTX1RAMFT+j22dX8IoxaTQ=="), PsaParameterType.VolumetricBreakinDetection);

            // @P5307-CITACT
            Mapping.Add(Decode("bEYz61u4Me/7yVVfe4N7zg=="), PsaParameterType.VolumetricProtectionDeactivation);

            // @P6947-CITACT
            Mapping.Add(Decode("yybp4kSvqkJh6sMq6BGBhQ=="), PsaParameterType.BonnetLock);

            // @P5506-CITACT
            Mapping.Add(Decode("9pFjSU2A6622AdGPMaSUvw=="), PsaParameterType.BootLock);

            // @P4818-CITACT
            Mapping.Add(Decode("10Ur+F3EOvfMvRu/1wpI1g=="), PsaParameterType.LockLockingControl);

            // @P4819-CITACT
            Mapping.Add(Decode("eqX1GC6kLmb4Ggg7y38Z7w=="), PsaParameterType.LockUnlockingControl);

            // @P9518-CITACT
            Mapping.Add(Decode("QCuJ9SXVKCEYfXKtEcqSbQ=="), PsaParameterType.DeadlockingControl);

            // @P23075-CITACT@\*
            Mapping.Add(Decode("ynLovBrq4yFvcqOLFJqm8DK09+ZRlNbZKlRFzY+m1BY="), PsaParameterType.BootLockingControl);

            // @P8993-CITACT
            Mapping.Add(Decode("WPqQCelODp914jAx++Juew=="), PsaParameterType.RearRightHandDoorSwitch);

            // @P8994-CITACT
            Mapping.Add(Decode("6AFQ88RnNnO5WabwFTpPSg=="), PsaParameterType.RearLeftHandDoorSwitch);

            // @P23076-CITACT
            Mapping.Add(Decode("cnhEAYbuemPkrXPmLlDHhg=="), PsaParameterType.BootOpeningRequest);

            // @P9008-CITACT
            Mapping.Add(Decode("nRQK3nSXdv9tYjxO+Wv7Qg=="), PsaParameterType.LocksLockingRelayOutput);

            // @P9009-CITACT
            Mapping.Add(Decode("Th/nT4wMrMPH0lUMMnLapw=="), PsaParameterType.LocksUnlockingRelayOutput);

            // @P9010-CITACT
            Mapping.Add(Decode("B6mQl8W15jHJs1e0omfFbg=="), PsaParameterType.LocksDeadlockingRelayOutput);

            // @P9011-CITACT
            Mapping.Add(Decode("Xwst+T7xuACLHJBoHXBDaA=="), PsaParameterType.BootOpeningRelayOutput);

            // @P9013-CITACT
            Mapping.Add(Decode("0Yu5OSohdt60QxyEQJ967Q=="), PsaParameterType.FrontRightHandLockUnlockingInformation);

            // @P9012-CITACT
            Mapping.Add(Decode("bIsbZawWP50Xmoiy8kbcAw=="), PsaParameterType.FrontLeftHandLockUnlockingInformation);

            // @P9014-CITACT
            Mapping.Add(Decode("ZPTfLcMwTZaB3EE1bJIgbA=="), PsaParameterType.ChildSafetyActivationControl);

            // @P9015-CITACT
            Mapping.Add(Decode("vIZ0wegQTrAmGyFDEC9Q4A=="), PsaParameterType.ChildSafetyDeactivationControl);

            // @P9021-CITACT
            Mapping.Add(Decode("TjRv6MFyAp+cSkrun5hnIzeDeqtByk1sc3GLIZqdwto="), PsaParameterType.CoolantLevelWarning);
            Mapping.Add(Decode("FUMSAyfoRA27m38zFeBsKw=="), PsaParameterType.CoolantLevelWarning);

            // @P3837-CITACT
            Mapping.Add(Decode("RMFeS/KZ65RDUXqVrzCltg=="), PsaParameterType.OilPressureWarning);

            // @P16232-CITACT
            Mapping.Add(Decode("WqFM8wAJUZqP6lDPqgCxyw=="), PsaParameterType.WaterInDiesel);

            // @P6668-CITACT
            Mapping.Add(Decode("1iLNyzd/M2PYzB2ldcZJxw=="), PsaParameterType.MeasuredOilLevel);

            // @P3830-CITACT
            Mapping.Add(Decode("wnDhEqx4Zbeyz6b3b646bQ=="), PsaParameterType.GrossFuelLevel);

            // @P63485-THESAU
            Mapping.Add(Decode("5tFev+nBpOOCeiJ8yMopbA=="), PsaParameterType.ExteriorAirTemperature);

            // @P18875-CITACT
            Mapping.Add(Decode("YzTSzgQO9C9hs+UtL5xnjg=="), PsaParameterType.ExteriorAirTemperature);

            // @P3241-THESAU
            Mapping.Add(Decode("rXZtI8IRSRZm3za/1VoNBA=="), PsaParameterType.EngineCoolantTemperature);

            // @P6066-CITACT
            Mapping.Add(Decode("7IQi7SGemPPS3w36GYdYOg=="), PsaParameterType.EngineCoolantTemperature);

            // @P19737-CITACT
            Mapping.Add(Decode("PZ3vZ/bJ67iUpnj2Vswi/g=="), PsaParameterType.EngineCoolantTemperature);

            // @P2956-CITACT
            Mapping.Add(Decode("iEWlerTwN+Iui5hY322KmQ=="), PsaParameterType.EngineCoolantTemperature);

            // @P4545-THESAU
            Mapping.Add(Decode("zjP9meTj3YUy/8jRkTKqvg=="), PsaParameterType.EngineCoolantTemperature);

            // @P24202-CITACT
            Mapping.Add(Decode("GVzr94hNgxYANjS+ju5q/g=="), PsaParameterType.EngineCoolantTemperature);

            // @P6671-CITACT
            Mapping.Add(Decode("/AqEkcUntrpGIk8n7Qwjnw=="), PsaParameterType.EngineOilTemperature);

            // @P12449-CITACT
            Mapping.Add(Decode("5TnakR09HAllikWCkv7rQQ=="), PsaParameterType.FilteredFuelLevel);

            // @P6672-CITACT
            Mapping.Add(Decode("6dB/bYhZaYm/ix4dVu2XwQ=="), PsaParameterType.DisplayedFuelLevel);

            // @P6673-CITACT
            Mapping.Add(Decode("rSDpG3Ajkv7OsOdbZzhnDw=="), PsaParameterType.NumberOfServicesPerformed);

            // @P6676-CITACT
            Mapping.Add(Decode("ZNY7Dmn1IDznZ9SQTyHzXA=="), PsaParameterType.FuelSenderImpedance);

            // @P15489-CITACT
            Mapping.Add(Decode("kxclN+j7UxUMN8ZaXRz1rA=="), PsaParameterType.LowBrakeFluidWarning);

            // @P6431-citact@\+@T-@\+@P10054-CITACT
            Mapping.Add(Decode("SPupl58J++jwAXPnOod7C6kGy1Kov4h9luuVnGZhO8/TExPDqU4/ZOedBuOgmVq4"), PsaParameterType.SunlightSensorDuskValue);

            // @P9135-CITACT
            Mapping.Add(Decode("eDUUswRTLiRlDTD7daz44A=="), PsaParameterType.VehicleProtectionStatusLedControl);

            // @P22113-citact
            Mapping.Add(Decode("RB45/RLGnsZPoW7JvEezcQ=="), PsaParameterType.LowScreenWashLevelDisplay);

            // @P6691-CITACT
            Mapping.Add(Decode("WGl3Qd3W/l4DLewF8P3PPQ=="), PsaParameterType.RearRhWindowRaiseControl);

            // @P6692-CITACT
            Mapping.Add(Decode("TMO9b7FNemprvCwcjer5tQ=="), PsaParameterType.RearLhWindowRaiseControl);

            // @P6693-CITACT
            Mapping.Add(Decode("vrkuDZ4z8v/6H8ZSvPXneA=="), PsaParameterType.RearRhWindowLowerControl);

            // @P6694-CITACT
            Mapping.Add(Decode("4RT3mOoEbIW+Ah1r5Svlcw=="), PsaParameterType.RearLhWindowLowerControl);

            // @P9157-CITACT
            Mapping.Add(Decode("FsZBV+FAkpq36/atuKHlvQ=="), PsaParameterType.RearScreenWiperControl);

            // @P8497-CITACT
            Mapping.Add(Decode("6t7dvR2SUDGksgGbt1x0NQ=="), PsaParameterType.WindscreenWiperControlAtHighSpeed);

            // @P6697-CITACT
            Mapping.Add(Decode("xe1HKztiyzCR/ZPQOaa/Nw=="), PsaParameterType.SlowSpeedWindscreenWiperControl);

            // @P8735-citact
            Mapping.Add(Decode("Dj3llSYoHprH1fAaAaJGEw=="), PsaParameterType.WindscreenWiperControl);

            // @P22561-CITACT@\*(1)
            Mapping.Add(Decode("osZFJov7/eg0ricaC3GNoNWhhCH/PhmNSMT8q10pfHU="), PsaParameterType.WindscreenWiperControl);

            // @P9158-CITACT
            Mapping.Add(Decode("wmP2B5Fd9RAJN59LFvc73w=="), PsaParameterType.RearScreenWashControl);

            // @P22562-CITACT@\*(1)
            Mapping.Add(Decode("PdQ4Nae0LXt+lNRLHsRMQ/xXeWTO2alIb0U2HgrliGM="), PsaParameterType.RearScreenWashControl);

            // @P6699-CITACT
            Mapping.Add(Decode("xOkXwCXMIHs0pvPPqvJ1bw=="), PsaParameterType.WindscreenWasherControl);

            // @P22563-CITACT@\*(1)
            Mapping.Add(Decode("ufrSiO76b4yxYiqjvqrsgFoqRL2XpdtfaRFbtwbi+LM="), PsaParameterType.WindscreenWasherControl);

            // @P6700-CITACT
            Mapping.Add(Decode("DFjYKrf14gnr8rpd05M02w=="), PsaParameterType.HeadlampWasherControl);

            // @P22564-CITACT@\*(1)
            Mapping.Add(Decode("q6ht8oazfm6dg+ZrU9rtGhhgriAqB81QHBdhcgywIeg="), PsaParameterType.HeadlampWasherControl);

            // @P4860-CITACT
            Mapping.Add(Decode("zlbb8p7lNEAybIv1+B/+tA=="), PsaParameterType.RearWiperParkedPositionInformation);

            // @P4861-CITACT
            Mapping.Add(Decode("w1KKYmoXUJ+6A8RNVKqpdg=="), PsaParameterType.WindscreenWiperParkedPositionInformation);

            // @P33627-CITACT@T(1)
            Mapping.Add(Decode("To+07irR+wT75Tirr4MTFFwaqYGwxmSdY97NKsEM928="), PsaParameterType.WindscreenWiperParkedPositionInformation);

            // @P5294-CITACT
            Mapping.Add(Decode("F5RHuSNnF4RTVFJBQO246A=="), PsaParameterType.HeatedRearScreenPushButton);

            // @P5292-CITACT
            Mapping.Add(Decode("eyQwAwsk9WY4LlD44Uumug=="), PsaParameterType.HazardWarningLampPushButton);

            // @P5293-CITACT
            Mapping.Add(Decode("e+o4wMI24dFOH9Y1naJh0A=="), PsaParameterType.AirConditioningPushButton);

            // @P5296-CITACT
            Mapping.Add(Decode("v3cBlQH8AwqgvK5ZnAKQ0A=="), PsaParameterType.ControlPanelBrightnessLevel);

            // @P14602-CITACT
            Mapping.Add(Decode("U0wjYSuJLIQMj/lzVvTA6w=="), PsaParameterType.SuspensionControlButton);

            // @P27289-CITACT
            Mapping.Add(Decode("kZ1gO6gY7mwnY8s2/eVk+w=="), PsaParameterType.AirTurbineSpeed);

            // @P10655-CITACT
            Mapping.Add(Decode("5TC1n0upL0vK/qMNmlXuPQ=="), PsaParameterType.MeteringPumpSpeed);

            // @P27287-CITACT
            Mapping.Add(Decode("Vh0sdTTR741r+p+iFVtCwQ=="), PsaParameterType.SparkPlugSpeed);

            // @P27291-CITACT
            Mapping.Add(Decode("KrcsvcHGCW/h9GBBaneTKw=="), PsaParameterType.AdditionalHeatingStatus);

            // @P3218-CITACT
            Mapping.Add(Decode("lyKRvY8kDQ8XHrFXhFeXfA=="), PsaParameterType.HeatingStatus);

            // @P10661-CITACT
            Mapping.Add(Decode("ZMkqnW6tcOucXOxpVUe4tQ=="), PsaParameterType.FlamePresence);

            // @P28098-CITACT
            Mapping.Add(Decode("joBQ74j3+GL+MJJh/78eYQ=="), PsaParameterType.IncandescentElementStatus);

            // @P28096-CITACT
            Mapping.Add(Decode("3mD3k/yP9A+/RKzhUbJGZA=="), PsaParameterType.FuelPumpStatus);

            // @P3157-CITACT
            Mapping.Add(Decode("+Ui40xBVGHyQ8403luFslw=="), PsaParameterType.AirTurbine);

            // @P3159-CITACT
            Mapping.Add(Decode("pV0qNkAP7r5leJD5Y8eozA=="), PsaParameterType.WaterPump);

            // @P12460-CITACT
            Mapping.Add(Decode("5CkTyG2Pyb9TQtVIqk1Nkg=="), PsaParameterType.NoLoadShedding);

            // @P20121-CITACT
            Mapping.Add(Decode("j4gBEy9eZHiqOFxp3eF2dQ=="), PsaParameterType.PreheaterPlugLoadShedding);

            // @P22037-CITACT
            Mapping.Add(Decode("Y6F6/nEP1yC6bzUN6+sArQ=="), PsaParameterType.ForcedFanUnitMidSpeedLoadShedding);

            // @P22038-CITACT
            Mapping.Add(Decode("uOR2AbBnWeelSWJ+Lx8Paw=="), PsaParameterType.ForcedFanUnitLowSpeedLoadShedding);

            // @P12639-CITACT
            Mapping.Add(Decode("i72sKCqtEnMQQ5mk+jsz2A=="), PsaParameterType.ForcedDeicingLoadShedding);

            // @P12640-CITACT
            Mapping.Add(Decode("kpQv7zIuTqXmBvbQRjcJkA=="), PsaParameterType.DeicingLoadShedding);

            // @P12641-CITACT
            Mapping.Add(Decode("Vb3eaGJZsBQQC02D8TJwhA=="), PsaParameterType.PartialHeatingLoadShedding);

            // @P12642-CITACT
            Mapping.Add(Decode("xtbZ88nIjkw2kewU8ChFlQ=="), PsaParameterType.CompleteHeatingLoadShedding);

            // @P12643-CITACT
            Mapping.Add(Decode("yfaHeuDQyUAL2eKc1ZE/Gw=="), PsaParameterType.AirConditioningBlowerLoadShedding);

            // @P12644-CITACT
            Mapping.Add(Decode("euMNbfe6SeAi07LFrrVYMQ=="), PsaParameterType.AirConditioningCompressorLoadShedding);

            // @P1948-THESAU
            Mapping.Add(Decode("3lvHPrC6vD0ejJjTkfKTnQ=="), PsaParameterType.ReservoirCap);

            // @P12350-THESAU
            Mapping.Add(Decode("sDvhPlzN3APVmKodelaRZw=="), PsaParameterType.ReservoirCap);

            // @P108879-THESAU
            Mapping.Add(Decode("qy6v9CaNNyhYL/TC/UoVzw=="), PsaParameterType.PlusVanSupply);

            // @P108901-THESAU@\*
            Mapping.Add(Decode("S82M9KgWXwDGTkvX9L4hFw6eXGbU1pXnoeCLiE4C5H4="), PsaParameterType.LeftBlownAirTemperature);

            // @P108902-THESAU@\*
            Mapping.Add(Decode("nlDPflKtb7SOIUZHe3dxVEsp0r8KLa0Aw0UMJ02Ol1s="), PsaParameterType.RightBlownAirTemperature);

            // @P90450-THESAU@\*
            Mapping.Add(Decode("r1besk2D6q0JeHN7XH5+arGeplibbq8s4DU53hCuXJk="), PsaParameterType.BlowerCopyVoltage);

            // @P16566-THESAU@\*@\+@P3296-THESAU
            Mapping.Add(Decode("DXjVyoMzK91IkM21In52fk6uJ4qVknkOLWu956vm5FMx4ndlkKkLAA+windrezpY"), PsaParameterType.VoltageValueOnBlower);

            // @P108913-THESAU@\*@\+@P37652-THESAU
            Mapping.Add(Decode("1q7mLoHfzdI8A4yvLJabJLRdDcyJ6vyXF0G6pn5MALe29tu12q+jTXKoR6dMiZZo"), PsaParameterType.MixingStatusLeft);

            // @P108882-THESAU
            Mapping.Add(Decode("fhUXqvkNmEqGGRQe7HUhYA=="), PsaParameterType.MixingStatusRight);

            // @P108881-THESAU
            Mapping.Add(Decode("vBbYlpUSqbbfvu+YIJ2DNQ=="), PsaParameterType.DistributionStatus);

            // @P108884-THESAU
            Mapping.Add(Decode("9Pr6xPHHDlUk36sCnvzfmw=="), PsaParameterType.RecirculationStatus);

            // @P106565-THESAU
            Mapping.Add(Decode("ZF7sI93xgdpm+6d9c2VDyA=="), PsaParameterType.ConditionerCoolingCircuitPressure);

            // @P6014-CITACT
            Mapping.Add(Decode("sOnFY27aZttP0om9rGYcNg=="), PsaParameterType.LowFapAdditiveLevel);

            // @P17140-CITACT
            Mapping.Add(Decode("WWtggzHPXh8hyoKYbmGKVQ=="), PsaParameterType.ReservoirCap);

            // @P31106-CITACT@\+@P21305-CITACT
            Mapping.Add(Decode("Y0JWhtYG9ro0P1hxvZ72xg7sayDOMZPLgSs71hFGjAw="), PsaParameterType.TotalAdditiveAmountDepositedCounter1);

            // @P17142-CITACT
            Mapping.Add(Decode("95DrdB0xSuUNbSkIH7aooA=="), PsaParameterType.TotalAdditiveAmountDepositedCounter2);

            // @P22223-CITACT@\*(1)
            Mapping.Add(Decode("nrtFlOvRDhY2sfIiKvNfysfuEL8tICNEzjwsU2LNbc8="), PsaParameterType.ReverseGearInfo);

            // @P23184-CITACT@\*(1)
            Mapping.Add(Decode("PlYtI1pm2ibJ8WxYbb2N8t08xxPkCwsNhp1wMI7UpKs="), PsaParameterType.AutomaticGearboxInParkPosition);

            // @P25705-CITACT
            Mapping.Add(Decode("3fIbzQQOQxOFh/roEUirfg=="), PsaParameterType.AutomaticGearboxRecognition); // TODO : Check it, possibly it's some new type

            // @P6668-CITACT@\+@T(1)
            Mapping.Add(Decode("5aBLywkseD1SUL7S4V/z4hBP8TdiX/waUKLoJyp27C4="), PsaParameterType.MeasuredOilLevel);

            // @P6671-CITACT@\+@T(1)
            Mapping.Add(Decode("Bbz2Uw0+lxyhOqZFMZX9fqHjn/CMDe+ZJ98HOMRZ7fg="), PsaParameterType.EngineOilTemperature);

            // @P22559-CITACT@\*(1)
            Mapping.Add(Decode("c38MocBtERYa7VnaU2gyD3Y0ESvvCHTP2WFsrxD2TtM="), PsaParameterType.OilPressureWarning);

            // @P18701-CITACT
            Mapping.Add(Decode("RcgQMtKRaPHP77UJXjA5NA=="), PsaParameterType.SteeringWheelRotationAngleDataPresence);

            // @P22577-CITACT
            Mapping.Add(Decode("N0GPn0Yxsz/Yb0hiJRoEPA=="), PsaParameterType.SteeringWheelSpeed);

            // @P18706-CITACT
            Mapping.Add(Decode("1VJG5UmMnSquDxeD8GxyaQ=="), PsaParameterType.SteeringWheelRotationDirection);

            // @P18707-CITACT
            Mapping.Add(Decode("wY03Rc970M9KWK2twmE8vA=="), PsaParameterType.SteeringElectropumpTemperature);

            // @P18710-CITACT
            Mapping.Add(Decode("p7RGfRsXMptdn0XpLNnw2Q=="), PsaParameterType.ElectropumpAssemblyMotorCurrent);

            // @P16126-CITACT
            Mapping.Add(Decode("CB7O2J+b7uKg5dxfTHMGoA=="), PsaParameterType.ElectropumpAssemblyMotorTerminalsVoltage);

            // @P8876-CITACT
            Mapping.Add(Decode("glt+Lt7m2NfvXqzgfCQzzg=="), PsaParameterType.EngineRunningInformation);

            // @P18708-CITACT
            Mapping.Add(Decode("GqZYdfEmZC9FAQYNor3esw=="), PsaParameterType.SpeedElectropumpAssemblyMotor);

            // @P18709-CITACT
            Mapping.Add(Decode("xqiT0DVhs30SmEY/JzSZkA=="), PsaParameterType.VehicleSpeedSignalPresence);

            // @P107329-THESAU@\*
            Mapping.Add(@"@P107329-THESAU@\*", PsaParameterType.QualityOfRadioSignal);

            // @P90267-THESAU
            Mapping.Add(@"@P90267-THESAU", PsaParameterType.LevelOfRadioSignal);

            // @P4525-THESAU
            Mapping.Add(@"@P4525-THESAU", PsaParameterType.BatteryVoltage);

            // @P124868-THESAU@\*
            Mapping.Add(@"@P124868-THESAU@\*", PsaParameterType.InjectionTime);

            // @P124844-THESAU@\\*
            Mapping.Add(@"@P124844-THESAU@\*", PsaParameterType.ThrottleAngle);

            // @P105976-THESAU@\*1/4
            Mapping.Add(@"@P105976-THESAU@\*1/4", PsaParameterType.CylinderCoilChargeTime14);

            // @P105976-THESAU@\*2/3
            Mapping.Add(@"@P105976-THESAU@\*2/3", PsaParameterType.CylinderCoilChargeTime23);

            // @P128677-THESAU
            Mapping.Add(@"@P128677-THESAU", PsaParameterType.MixRegStateInput);

            // @P128677-THESAU
            Mapping.Add(@"@P128678-THESAU", PsaParameterType.MixRegStateOutput);

            //@P87549-THESAU
            Mapping.Add(@"@P87549-THESAU", PsaParameterType.MeasuredFanSpeed);

            //@P111490-THESAU
            Mapping.Add(@"@P111490-THESAU", PsaParameterType.FanRelayOutput);

            //@P106566-THESAU@\*
            Mapping.Add(@"@P106566-THESAU@\*", PsaParameterType.MeasuredFanSpeed);

            //@P124860-THESAU
            Mapping.Add(@"@P124860-THESAU", PsaParameterType.AirConditioningRequestInput);

            //@P128686-THESAU
            Mapping.Add(@"@P128686-THESAU", PsaParameterType.ConditionerCoolingCircuitPressure);

            //@P106752-THESAU
            Mapping.Add(@"@P106752-THESAU", PsaParameterType.AutomaticGearboxRecognition);

            //@P142406-THESAU
            Mapping.Add(@"@P142406-THESAU", PsaParameterType.ClutchPedalSignal);

            //@P111516-THESAU
            Mapping.Add(@"@P111516-THESAU", PsaParameterType.ECULocking);

            //@P21933-THESAU@\*
            Mapping.Add(@"@P21933-THESAU@\*", PsaParameterType.ThrottleAngle);

            Mapping.Add("@P115095-THESAU@\\*@\\+@P74318-THESAU", PsaParameterType.RadioSearchButtonMinus); // RECHERCHEMOINS 
            Mapping.Add("@P115130-THESAU@\\*@\\+@P74317-THESAU", PsaParameterType.RadioSearchButtonPlus); // RECHERCHEPLUS
            Mapping.Add("@P115096-THESAU@\\*-", PsaParameterType.RadioVolumeButtonPlus); // VOLUMEPLUS
            Mapping.Add("@P115096-THESAU@\\*+", PsaParameterType.RadioVolumeButtonMinus); // VOLUMEMOINS
            Mapping.Add(@"@P119093-THESAU", PsaParameterType.ParkingBrake); // ETATFREINPARKON // состояние парковочного тормоза

            Mapping.Add(@"@P107413-THESAU", PsaParameterType.AirPumpControl); // MODESHOWROOM // Commande de la pompe a air
            Mapping.Add(@"@P108500-THESAU", PsaParameterType.EspDeactivationRequest); // MP_DEMANDE_INHIB_CDS // Demande inhibition CDS/ESP
            Mapping.Add("@P107443-THESAU@\\*", PsaParameterType.AutomaticGearboxSnowProgramRequest); // DEMPROGBVANEIGE // Demande de programme BVA neige
            Mapping.Add("@P107444-THESAU@\\*", PsaParameterType.RequestForBVAProgramSport); //DEMPROGBVASPORT // Demande de programme BVA sport

            Mapping.Add(@"@P110819-THESAU", PsaParameterType.RequestForBVAProgramSportState); // ETATDEMPROGBVASPORT // 
            Mapping.Add(@"@P101399-THESAU", PsaParameterType.EspDeactivationRequest); // MP_MP_DEMANDE_INHIB_CDS9 // Demande inhibition CDS/ESP /// ???
            Mapping.Add(@"@P123207-THESAU", PsaParameterType.DriverSeatBeltFastened); // MP_CEINTURE_CONDUCTEUR_BOUCLEE // Ceinture de circuite avant gauche bouclee
            Mapping.Add(@"@P123208-THESAU", PsaParameterType.PassengerSeatBeltFastened); // MP_CEINTURE_PASSAGER_BOUCLEE // Ceinture de circuite avant droite bouclee
            Mapping.Add(@"@P107461-THESAU", PsaParameterType.AutomaticGearboxInParkPosition); // BVAPARKCONTEMBR // BVA Parking/Contact Embrayage
            Mapping.Add("@P107409-THESAU@\\+@T(+VAN)", PsaParameterType.FuelPumpSupplyCutOff); // ALIMPRESCARCONF // Commande de coupure de la pompe а carburant
            Mapping.Add(@"@P87031-THESAU@\*", PsaParameterType.UpperOxygenSensorVoltage); // MP_TENSION_SONDE_OXYGENE_AMONT // Tension sonde oxygиne amont
            Mapping.Add(@"@P87032-THESAU@\*", PsaParameterType.LowerOxygenVoltage); // MP_TENSION_SONDE_OXYGENE_AVAL // Tension sonde oxygиne aval
            Mapping.Add("@P87033-THESAU", PsaParameterType.MixRegState); // MP_ETAT_REGULATION_RICHESSE_SUP // Etat de rйgulation richesse
            Mapping.Add("@P107127-THESAU", PsaParameterType.AudioButton); // MP_VIN // Etat lu du bouton 'AUDIO' du dйsignateur
            Mapping.Add("@P107128-THESAU", PsaParameterType.VinCodeVerification); // MPVERIFCODEVIN // Vйrification du code VIN
            Mapping.Add("@P106104-THESAU", PsaParameterType.UsageGeographicalZone); // MPZONEUTILIZATION // Zone gйographique d?utilisation
            Mapping.Add("@P106106-THESAU", PsaParameterType.FaderFunction); // MPETATFADER // Fonction Fader
            Mapping.Add("@P95881-THESAU@\\*", PsaParameterType.FrequencyBandAM); // MPETATBANDEAM // Bande de frйquence AM
            Mapping.Add("@P93500-THESAU", PsaParameterType.VolumeLevelDependencyFromVehicleSpeed); // MP_ASSERV_NIV_SONORE // asservissement du niveau sonore en fonction de la vitesse vehicule
            Mapping.Add("@P106107-THESAU", PsaParameterType.AuxiliaryAudioInputActivationState); // MP_ETAT_INPUT_AUDIO // Etat d?activation de l?entrйe audio auxiliaire
            Mapping.Add("@P106665-THESAU", PsaParameterType.VolumeLevelCorrectionAlgorythm); // MPTABLEVOLUME // Loi de correction du niveau de volume 
            Mapping.Add("@P114375-THESAU", PsaParameterType.SensibilityCurveLoDx); // MPCOURBSENSI // Courbe de sensibilitй LO/DX
            Mapping.Add("@P107126-THESAU@\\*", PsaParameterType.LightingTableConfiguration); // MP_TABLECLAIR_MPM1 // Configuration table d'йclairage
            Mapping.Add("@P121718-THESAU", PsaParameterType.HeatingFramePushState); // MP_ETAT_LUNNETTE_CHAUFFANTE // EntrйePushLunetteChauffante
            Mapping.Add("@P121719-THESAU", PsaParameterType.AlarmButtonState); // MP_ETAT_PUSH_WARNING // EntrйePushWarning
            Mapping.Add("@P115259-THESAU", PsaParameterType.AirConditioningPushButton); // MP_ETAT_CLIM_AC_ON // EntrйePushClim'AC/ON
            Mapping.Add("@P83661-THESAU", PsaParameterType.BrightnessLevelRheostat); // NIVLUMINOCOMBINE !!OR!! P_NIV_RHEOSTAT // NiveauDeLuminositйRhйostat
            Mapping.Add("@P107229-THESAU@\\*", PsaParameterType.CentralLockButtonState); // MP_ETAT_CONDAMNATION_CENTRALISEE // EntrйePushCondamnationCentralisйe
            Mapping.Add("@P92724-THESAU@\\*@\\+@P104784-THESAU", PsaParameterType.CdEjectButtonState); // CDEBOUTONCDS // Etat du bouton йjection du disque
            Mapping.Add("@P128811-THESAU@\\*", PsaParameterType.OdometerResetButton); // MP_TOUCH_RAZ_ODOMETRE // Touche de RAZ odomиtre
            Mapping.Add("@P110364-THESAU@\\*@\\+@T(+)", PsaParameterType.BrightnessPlusSwitchState); // TOUCHREGLLUMPLUS // Etat du contacteur luminositй Plus
            Mapping.Add("@P112828-THESAU", PsaParameterType.PsaCalcDecale); // REF_PSA_CALC_DECALE
            Mapping.Add("@P108792-THESAU@\\*", PsaParameterType.AVInputSensors); // MP_CONFIG_SIGNE_CAPTEURS_AV // Signe capteurs
            Mapping.Add("@P8105-THESAU@\\*@\\*", PsaParameterType.SupplierCode); // FOURNISSEUR // Code Fourniseur
            Mapping.Add("@P106095-THESAU", PsaParameterType.SoftwareVersion); // ID_VERSION_LOGICIEL // Version logiciel
            Mapping.Add("@P106096-THESAU", PsaParameterType.DiagnosticsVersionIndex); // VERSION_DIAG // Indice d'йvolution du Diagnostic
            Mapping.Add("@P10502-THESAU", PsaParameterType.ProgrammingOfExteriorTemperature); // MP_PROG_TEMP_EXT // Programmation de l?affichage de la tempйrature extйrieure
            Mapping.Add("@P107185-THESAU@\\*", PsaParameterType.ConfiguredAutoradioPresenceOnVanBus); // MP_PRESENCE_RADIO // ConfigurationDePrйsenceD'unAutoradioSurLeRйseauVAN
            Mapping.Add("@P107186-THESAU", PsaParameterType.InformationAboutAutoVolumeRadioFunction); // OPTGESTIONVOL !!! MP_INFOS_RADIO_AUTOMATIQUE // InformationAl'EMFDeLaConnexionаUnAutoradioComportantLaFonctionVolumeAutomatique
            Mapping.Add("@P107187-THESAU@\\*", PsaParameterType.BoardComputerOption); // MPI_OPT_ORDI_BORD // OptionOrdinateurDeBord.
            Mapping.Add("@P107189-THESAU@\\*", PsaParameterType.CDChangerPresenceOnVANBusSetting); // MP_PRESENCE_CD_CHANGEUR // ConfigurationDePrйsenceD'unCDChangeurSurLeRйseauVAN.
            Mapping.Add("@P107192-THESAU@\\*", PsaParameterType.OverspeedingAlertFunction); // MP_ACTIVE_FONCT_SURVITESSE // ActivationFonctionAlerteSurvitesse.
            Mapping.Add("@P115132-THESAU", PsaParameterType.DateDisplayNotation); // MP_NOTATION_DATE // TypeDeNotationDelaDate:Anglo-saxonneMMJJAA,FranзaiseJJMMAA
            Mapping.Add("@P107193-THESAU", PsaParameterType.VolumeAndDistanceUnits); // VOLUMEDIST // Unitй de volume et de distance
            Mapping.Add("@P115133-THESAU", PsaParameterType.TemperatureUnit); // TEMPERATURE // Unitйsd'AffichageDeTempйrature(°Cou°F)
            Mapping.Add("@P93510-THESAU", PsaParameterType.DisplayColorSchemeType); // TYPEAFFICHAGE !!!! MP_AFFICH_POSIT_NEGAT // Typed'Affichage:PositifPourTexteNoirSurFondClair,NйgatifPourTexteClairSurFondNoir
            Mapping.Add("@P87490-THESAU", PsaParameterType.BoardComputerState); //  MP_ETAT_ORDI_BORD !!! ETATBOUTON ODB // EtatLuDuBoutonOrdinateurDeBord.

            Mapping.Add("@P23850-CITACT", PsaParameterType.ParticleFilterStatus); // ETATFAP 
            Mapping.Add("@P40514-CITACT@\\*5", PsaParameterType.AverageDistanceOfLast5Regenerations); // DISTMOY5REGENERATION // 
            Mapping.Add("@P18572-CITACT", PsaParameterType.DistanceTravelledSinceLastRegeneration); // DISTDERREGENERATION 
            Mapping.Add("@P35748-CITACT", PsaParameterType.MassAdditiveFap); // MASSADDFAP (potentially new parameter) // Masse  totale  d?additif cumulй dans le Filtre A Particules
            Mapping.Add("@P39070-CITACT", PsaParameterType.TotalFuelAdditiveQuantity); // VOLADDFAP (pot new ) // Volume d?additif restant dans le rйservoir // Оставшийся объём
            Mapping.Add("@P10286-CITACT", PsaParameterType.ParticleFilterInletOutletPressureDifference); // DELTAFAP // DIFFPRESSIONFAP_EDC15
            Mapping.Add("@P18566-CITACT", PsaParameterType.AirCollectorTemperature); // TEMPERATUREAIRCOLLECT
            Mapping.Add("@P17640-CITACT", PsaParameterType.AirTemperature); // TEMPERATUREAIRDEBIT // TEMPERATUREAIRADMISS
            Mapping.Add("@P8117-CITACT", PsaParameterType.MeasuredInjectionVolume); // DEBITINJECTE
            Mapping.Add("@P26080-CITACT@\\+@P24379-CITACT", PsaParameterType.InletAirHeaterThrottleOpenCycleRatio); // RCOEVRAA
            Mapping.Add("@P10538-CITACT", PsaParameterType.DefaultAdditiveSystem); // MP_DEFAUT_SYS_ADDITIVATION

            Mapping.Add("@P22727-CITACT", PsaParameterType.CamshaftCrankshaftSync); // // SYNCHROAACVILEBRE // SYNCHROMOTEUR
            Mapping.Add("@P4958-CITACT", PsaParameterType.FuelSystemPressure); // MP_CONSIGNE_PRESSION_CARBURANT // PressionRail //
            Mapping.Add("@P22247-CITACT", PsaParameterType.DieselPressureRegulatorRatio); // RCOREGULDEBITCARB
            Mapping.Add("@P10258-CITACT@\\*1", PsaParameterType.Injector1Correction);
            Mapping.Add("@P10258-CITACT@\\*3", PsaParameterType.Injector3Correction);
            Mapping.Add("@P10258-CITACT@\\*4", PsaParameterType.Injector4Correction);
            Mapping.Add("@P10258-CITACT@\\*2", PsaParameterType.Injector2Correction);
            Mapping.Add("@P14465-CITACT", PsaParameterType.HighPressureFuelPumpFuelSupply); // DEBITCARB // DEBIT_CARB
            Mapping.Add("@P17632-CITACT", PsaParameterType.InjectorsCommandError); // COMMANDINJECTEUR // ETATERRCMDEINJ
            Mapping.Add("@P2965-CITACT", PsaParameterType.MeasuredAirVolume); // DEBITAIR 
            Mapping.Add(@"@P2951-CITACT", PsaParameterType.RequestedAirVolumeFlow); // CONSIGNEDEBITAIR

            Mapping.Add(@"@P4662-CITACT", PsaParameterType.TransponderLabelRecognized);
            Mapping.Add(@"@P112741-THESAU@\*", PsaParameterType.Vin);

            Mapping.Add(@"@P20122-CITACT", PsaParameterType.NIVDELEST3);
            Mapping.Add(@"@P17514-CITACT", PsaParameterType.NIVDELEST4);
            Mapping.Add(@"@P17515-CITACT@\*1", PsaParameterType.NIVDELEST7);
            Mapping.Add(@"@P17516-CITACT@\*1@\*2", PsaParameterType.NIVDELEST8);
            Mapping.Add(@"@P87035-THESAU@\*", PsaParameterType.ReportedGearboxGearRatio);

            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
            //Mapping.Add(@"", PsaParameterType);
        }

        private static HashSet<string> set = new HashSet<string>();

        public static PsaParameterType GetType(string val)
        {
            if (Mapping.ContainsKey(val))
            {
                return Mapping[val];
            }
            set.Add(val);
            return PsaParameterType.Unsupported;
        }
    }
}

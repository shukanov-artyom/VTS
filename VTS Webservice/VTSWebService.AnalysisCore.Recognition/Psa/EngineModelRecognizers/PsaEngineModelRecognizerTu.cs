using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class PsaEngineModelRecognizerTu : PsaEngineModelRecognizer
    {
        public PsaEngineModelRecognizerTu(EngineFamily family)
            : base(family)
        {

        }

        public override Engine Recognize(
            VehicleCharacteristics characteristics)
        {
            Engine result = new Engine();
            result.Family = Family;
            string engineModelValue = characteristics.GetEngineModelString();
            //string generalInfoValue = characteristics.GeneralVehicleInfo;
            if (engineModelValue.Contains("TU9"))
            {
                result.Type = EngineType.TU9K;
                result.InjectionType = InjectionType.Carburettor;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU9";
            }
            else if (engineModelValue.Contains("TU1F"))
            {
                result.Type = EngineType.TU1F2K;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU1 F2K";
            }
            else if (engineModelValue.Contains("TU1JP"))
            {
                result.Type = EngineType.TU1JP;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU1 JP";
            }
            else if (engineModelValue.Contains("TU1M")
                || engineModelValue.Contains("TU1Z"))
            {
                result.Type = EngineType.TU1MZ;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU1 M/Z";
            }
            else if (engineModelValue.Contains("TU1K"))
            {
                result.Type = EngineType.TU1K;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU1 K";
            }
            else if (engineModelValue.Contains("TU2"))
            {
                // Not very much supported
                // old rally engines
                result.Type = EngineType.TU2A;
            }
            else if (engineModelValue.Contains("TU3A"))
            {
                result.Type = EngineType.TU3A;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU3 A";
            }
            else if (engineModelValue.Contains("TU3F2K"))
            {
                result.Type = EngineType.TU3FJ2K;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU3 F2/K";
            }
            else if (engineModelValue.Contains("TU3FJ2"))
            {
                result.Type = EngineType.TU3FJ2Z;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU3 FJ2/Z";
            }
            else if (engineModelValue.Contains("TU3JP"))
            {
                result.Type = EngineType.TU3JP;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU3 JP";
            }
            else if (engineModelValue.Contains("TU3M"))
            {
                result.Type = EngineType.TU3MZ;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU3 M/Z";
            }
            else if (engineModelValue.Contains("TU3S"))
            {
                result.Type = EngineType.TU3S;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU3 S";
            }
            else if (engineModelValue.Contains("ET3J4"))
            {
                result.Type = EngineType.ET3J4;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "ET3 J4";
            }
            else if (engineModelValue.Contains("TU5J4"))
            {
                result.Type = EngineType.TU5J4;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU5 J4";
            }
            else if (engineModelValue.Contains("TU5") &&
                engineModelValue.Contains("JP4S"))
            {
                result.Type = EngineType.TU5JP4S;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU5 JP4S";
            }
            else if (engineModelValue.Contains("TU5") &&
                engineModelValue.Contains("JP4"))
            {
                result.Type = EngineType.TU5JP4;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU5 JP4";
            }
            else if (engineModelValue.Contains("TU5") &&
                engineModelValue.Contains("JP"))
            {
                result.Type = EngineType.TU5JP;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU5 JP";
            }
            else if (engineModelValue.Contains("TU5J2") ||
                engineModelValue.Contains("TU5L3"))
            {
                result.Type = EngineType.TU5J2L3;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU5 J2/L3";
            }
            else if (engineModelValue.Contains("TU1") &&
                engineModelValue.Contains("A"))
            {
                result.Type = EngineType.TU1A;
                result.InjectionType = InjectionType.Injector;
                result.FuelType = FuelType.Petrol;
                result.DisplayName = "TU1A";
            }
            else
            {
                throw new NotSupportedException(engineModelValue);
            }
            return result;
        }
    }
}

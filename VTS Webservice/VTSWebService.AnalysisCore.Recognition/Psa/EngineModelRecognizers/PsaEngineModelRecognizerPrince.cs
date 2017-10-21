using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class PsaEngineModelRecognizerPrince : PsaEngineModelRecognizer
    {
        public PsaEngineModelRecognizerPrince(EngineFamily family)
            : base(family)
        {

        }

        public override Engine Recognize(
            VehicleCharacteristics characteristics)
        {
            Engine result = new Engine();
            result.Family = Family;
            string engineModelValue = characteristics.GetEngineModelString();
            string generalInfoValue = characteristics.GeneralVehicleInfo;

            if (engineModelValue.ToUpper().Contains("EP3"))
            {
                result.Type = EngineType.EP3;
                result.FuelType = FuelType.Petrol;
                result.InjectionType = InjectionType.Injector;
                result.DisplayName = "EP3";
            }
            else if (engineModelValue.ToUpper().Contains("EP6") &&
                engineModelValue.ToUpper().Contains("DTS"))
            {
                result.Type = EngineType.EP6DTS;
                result.FuelType = FuelType.Petrol;
                result.InjectionType = InjectionType.Injector;
                result.DisplayName = "EP6 DTS";
            }
            else if (engineModelValue.ToUpper().Contains("EP6") &&
                engineModelValue.ToUpper().Contains("CDTX"))
            {
                result.Type = EngineType.EP6CDTX;
                result.FuelType = FuelType.Petrol;
                result.InjectionType = InjectionType.Injector;
                result.DisplayName = "EP6 CDTX";
            }
            else if (engineModelValue.ToUpper().Contains("EP6DT"))
            {
                if (generalInfoValue.Contains("140"))
                {
                    result.Type = EngineType.EP6DT140;
                }
                else if (generalInfoValue.Contains("150"))
                {
                    result.Type = EngineType.EP6DT;
                }
                else
                {
                    // TODO: exception?
                    result.Type = EngineType.EP6DT140;
                }
                result.FuelType = FuelType.Petrol;
                result.InjectionType = InjectionType.Injector;
                result.DisplayName = "EP6 DT";
            }
            else if (engineModelValue.ToUpper().Contains("EP6") &&
            engineModelValue.ToUpper().Contains("CDT"))
            {
                result.Type = EngineType.EP6CDT;
                result.FuelType = FuelType.Petrol;
                result.InjectionType = InjectionType.Injector;
                result.DisplayName = "EP6 CDT";
            }
            else if (engineModelValue.ToUpper().Contains("EP6C") ||
                engineModelValue.ToUpper().Contains("EP6"))
            {
                result.Type = EngineType.EP6;
                result.FuelType = FuelType.Petrol;
                result.InjectionType = InjectionType.Injector;
                result.DisplayName = "EP6";
            }
            else
            {
                throw new NotSupportedException(engineModelValue);
            }

            return result;
        }
    }
}

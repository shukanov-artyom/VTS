using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class PsaEngineModelRecognizerEb : PsaEngineModelRecognizer
    {
        public PsaEngineModelRecognizerEb(EngineFamily family)
            : base(family)
        {
        }

        public override Engine Recognize(VehicleCharacteristics characteristics)
        {
            Engine result = new Engine();
            result.Family = Family;
            string engineModelValue = characteristics.GetEngineModelString();
            //string generalInfoValue = characteristics.GeneralVehicleInfo;
            if (engineModelValue.ToUpper().Contains("M"))
            {
                result.Type = EngineType.EB2M;
                result.DisplayName = "EB2 M";
                result.FuelType = FuelType.Petrol;
                result.InjectionType = InjectionType.Injector;
            }
            else if (engineModelValue.Contains("0"))
            {
                result.Type = EngineType.EB0;
                result.DisplayName = "EB0";
                result.FuelType = FuelType.Petrol;
                result.InjectionType = InjectionType.Injector;
            }
            else
            {
                throw new NotSupportedException(engineModelValue);
            }
            return result;
        }
    }
}

using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class EngineModelRecognizerKr : PsaEngineModelRecognizer
    {
        public EngineModelRecognizerKr(EngineFamily family)
            : base(family)
        {
        }

        public override Engine Recognize(VehicleCharacteristics characteristics)
        {
            Engine result = new Engine();
            result.Family = Family;
            result.FuelType = FuelType.Petrol;
            result.InjectionType = InjectionType.Injector;
            string engineModelValue = characteristics.GetEngineModelString();
            //string generalInfoValue = characteristics.GeneralVehicleInfo;
            if (engineModelValue.Contains("384F"))
            {
                result.Type = EngineType.KR_384F;
                result.DisplayName = "384F (1KR-FE)";
            }
            else
            {
                throw new NotSupportedException(engineModelValue);
            }
            return result;
        }
    }
}

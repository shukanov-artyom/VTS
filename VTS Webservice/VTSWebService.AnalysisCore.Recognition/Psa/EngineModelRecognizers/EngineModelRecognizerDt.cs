using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class EngineModelRecognizerDt : PsaEngineModelRecognizer
    {
        public EngineModelRecognizerDt(EngineFamily fam)
            : base(fam)
        {
        }

        public override Engine Recognize(VehicleCharacteristics characteristics)
        {
            Engine result = new Engine();
            result.Family = Family;
            result.FuelType = FuelType.Diesel;
            result.InjectionType = InjectionType.Injector;
            string engineModelValue = characteristics.GetEngineModelString();
            //string generalInfoValue = characteristics.GeneralVehicleInfo;
            if (engineModelValue.Contains("DT") && engineModelValue.Contains("20C"))
            {
                result.Type = EngineType.DT20C;
                result.DisplayName = "DT20C";
            }
            else
            {
                throw new NotSupportedException(engineModelValue);
            }
            return result;
        }
    }
}

using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class PsaEngineModelRecognizerDk : PsaEngineModelRecognizer
    {
        public PsaEngineModelRecognizerDk(EngineFamily fam)
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
            if (engineModelValue.Contains("ATE"))
            {
                result.Type = EngineType.DK5ATE;
                result.DisplayName = "DK5 ATE";
            }
            else if (engineModelValue.Contains("5"))
            {
                result.Type = EngineType.DK5;
                result.DisplayName = "DK5";
            }
            else
            {
                throw new NotSupportedException(engineModelValue);
            }
            return result;
        }
    }
}

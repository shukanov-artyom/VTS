using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class PsaEngineModelRecognizerEc : PsaEngineModelRecognizer
    {
        public PsaEngineModelRecognizerEc(EngineFamily fam)
            : base(fam)
        {
        }

        public override Engine Recognize(VehicleCharacteristics characteristics)
        {
            Engine result = new Engine();
            result.Family = Family;
            string engineModelValue = characteristics.GetEngineModelString();
            //string generalInfoValue = characteristics.GeneralVehicleInfo;
            result.InjectionType = InjectionType.Injector;
            result.FuelType = FuelType.Petrol;
            if (engineModelValue.ToUpper().Contains("5"))
            {
                result.Type = EngineType.EC5;
                result.DisplayName = "EC5";
            }
            else if (engineModelValue.ToUpper().Contains("8"))
            {
                result.Type = EngineType.EC8;
                result.DisplayName = "EC8";
            }
            else
            {
                throw new NotSupportedException(engineModelValue);
            }
            return result;
        }
    }
}

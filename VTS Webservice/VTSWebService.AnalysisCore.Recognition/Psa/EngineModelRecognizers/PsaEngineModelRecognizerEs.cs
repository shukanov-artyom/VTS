using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class PsaEngineModelRecognizerEs : PsaEngineModelRecognizer
    {
        public PsaEngineModelRecognizerEs(EngineFamily fam)
            : base(fam)
        {
        }

        public override Engine Recognize(VehicleCharacteristics characteristics)
        {
            Engine result = new Engine();
            result.FuelType = FuelType.Petrol;
            result.InjectionType = InjectionType.Injector;
            result.Family = Family;
            string engineModelValue = characteristics.GetEngineModelString();
            //string generalInfoValue = characteristics.GeneralVehicleInfo;
            if (engineModelValue.Contains("J4S"))
            {
                result.DisplayName = "ES9 J4S";
                result.Type = EngineType.ES9J4S;
            }
            else if (engineModelValue.Contains("J4") ||
                engineModelValue.Contains("L7X"))
            {
                result.DisplayName = "ES9 J4/L7X";
                result.Type = EngineType.ES9J4L7X;
            }
            else if (engineModelValue.Contains("A"))
            {
                result.DisplayName = "ES9 IA";
                result.Type = EngineType.ES9IA;
            }
            else
            {
                throw new NotSupportedException(engineModelValue);
            }
            return result;
        }
    }
}

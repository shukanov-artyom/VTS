using System;
using VTS.AnalysisCore.Common;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal class EngineModelRecognizerMMC : PsaEngineModelRecognizer
    {
        public EngineModelRecognizerMMC(EngineFamily fam)
            : base(fam)
        {
        }

        public override Engine Recognize(VehicleCharacteristics characteristics)
        {
            Engine result = new Engine();
            result.Family = Family;
            result.FuelType = FuelType.Unknown;
            result.InjectionType = InjectionType.Injector;
            string engineModelValue = characteristics.GetEngineModelString();
            //string generalInfoValue = characteristics.GeneralVehicleInfo;
            if (engineModelValue.ToUpper().Contains("MMC") && 
                engineModelValue.ToUpper().Contains("2.4L"))
            {
                result.Type = EngineType.MMC24;
                result.DisplayName = "Mitsubishi 2.4L";
            }
            else
            {
                throw new NotSupportedException(engineModelValue);
            }
            return result;
        }
    }
}

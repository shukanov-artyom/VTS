using System;
using VTS.AnalysisCore.Common;
using VTS.Shared.DomainObjects;


namespace VTSWebService.AnalysisCore.Recognition.Psa
{
    internal class PsaVehicleModelRecognizer : IVehicleModelRecognizer
    {
        public string Recognize(VehicleCharacteristics characteristics)
        {
            return characteristics.GetVehicleModelString();
        }
    }
}

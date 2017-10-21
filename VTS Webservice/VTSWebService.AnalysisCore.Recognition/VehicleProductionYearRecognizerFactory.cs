using System;
using VTS.Shared;
using VTSWebService.AnalysisCore.Recognition.Psa;

namespace VTSWebService.AnalysisCore.Recognition
{
    internal static class VehicleProductionYearRecognizerFactory
    {
        public static IVehicleProductionYearRecognizer Create(Manufacturer manufacturer)
        {
            switch (manufacturer)
            {
                case Manufacturer.Citroen:
                case Manufacturer.Peugeot:
                    return new PsaVehicleProductionYearRecognizer();
                case Manufacturer.Opel:
                    throw new NotImplementedException();
                default: 
                    throw new NotSupportedException("Manufacturer not supported");
            }
        }
    }
}

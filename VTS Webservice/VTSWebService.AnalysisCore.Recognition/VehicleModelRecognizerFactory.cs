using System;
using VTS.Shared;
using VTSWebService.AnalysisCore.Recognition.Psa;

namespace VTSWebService.AnalysisCore.Recognition
{
    internal class VehicleModelRecognizerFactory
    {
        public static IVehicleModelRecognizer Create(Manufacturer manufacturer)
        {
            switch (manufacturer)
            {
                case Manufacturer.Citroen:
                case Manufacturer.Peugeot:
                    return new PsaVehicleModelRecognizer();
                case Manufacturer.Opel:
                    throw new NotImplementedException();
                default: throw new NotSupportedException("Manufacturer not supported");
            }
        }
    }
}

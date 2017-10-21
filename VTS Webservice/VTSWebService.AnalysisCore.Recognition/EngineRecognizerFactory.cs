using System;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Recognition.Opel;
using VTSWebService.AnalysisCore.Recognition.Psa;

namespace VTSWebService.AnalysisCore.Recognition
{
    public class EngineRecognizerFactory
    {
        private readonly Manufacturer man;
        private readonly VehicleCharacteristics characteristics;

        public EngineRecognizerFactory(
            string vin,
            VehicleCharacteristics characteristics)
        {
            Manufacturer? man1 = VinChecker.GetManufacturer(vin);
            if (man1 == null)
            {
                throw new Exception("Manufacturer not recognized");
            }
            man = man1.Value;
            this.characteristics = characteristics;
        }

        public IEngineRecognizer Create()
        {
            switch (man)
            {
                case Manufacturer.Citroen:
                case Manufacturer.Peugeot:
                    return new PsaEngineRecognizer(characteristics);
                case Manufacturer.Opel:
                    return new OpelEngineRecognizer(characteristics);
                default:
                    throw new Exception("Unknown manufacturer");
            }
        }
    }
}

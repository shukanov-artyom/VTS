using System;
using VTS.AnalysisCore.Common;
using VTS.Shared.DomainObjects;


namespace VTSWebService.AnalysisCore.Recognition.Opel
{
    public class OpelEngineRecognizer : IEngineRecognizer
    {
        private readonly VehicleCharacteristics characteristics;

        public OpelEngineRecognizer(VehicleCharacteristics characteristics)
        {
            this.characteristics = characteristics;
        }

        public Engine Recognize()
        {
            throw new NotImplementedException();
        }
    }
}

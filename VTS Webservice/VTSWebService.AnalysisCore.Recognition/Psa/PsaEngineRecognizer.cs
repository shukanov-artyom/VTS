using System;
using VTS.AnalysisCore.Common;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Recognition.Psa.EngineFamilyRecognizers;

namespace VTSWebService.AnalysisCore.Recognition.Psa
{
    internal class PsaEngineRecognizer : IEngineRecognizer
    {
        private VehicleCharacteristics vehicleCharacteristics;

        public PsaEngineRecognizer(
            VehicleCharacteristics vehicleCharacteristics)
        {
            if (vehicleCharacteristics == null)
            {
                throw new ArgumentNullException("vehicleCharacteristics");
            }
            this.vehicleCharacteristics = vehicleCharacteristics;
        }

        public Engine Recognize()
        {
            EngineFamily family = RecognizeEngineFamily();
            PsaEngineModelRecognizerFactory engineRecognizerFactory =
               new PsaEngineModelRecognizerFactory(family);
            IPsaEngineModelRecognizer engineModelRecognizer =
                engineRecognizerFactory.Create();
            return engineModelRecognizer.Recognize(vehicleCharacteristics);
        }

        private EngineFamily RecognizeEngineFamily()
        {
            EngineFamily family = new EngineFamily();
            PsaEngineFamilyRecognizer engineFamilyRecognizer =
                new PsaEngineFamilyRecognizer(vehicleCharacteristics);
            family.Type = engineFamilyRecognizer.Recognize();
            /*family.Link = EngineFamilyInfoStore.GetLink(family.Type);
            family.DisplayName =
                EngineFamilyInfoStore.GetDisplayName(family.Type);*/
            return family;
        }
    }
}

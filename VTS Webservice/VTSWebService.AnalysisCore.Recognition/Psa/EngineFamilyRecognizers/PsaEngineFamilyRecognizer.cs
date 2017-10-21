using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineFamilyRecognizers
{
    internal class PsaEngineFamilyRecognizer
    {
        private readonly VehicleCharacteristics characteristics;

        public PsaEngineFamilyRecognizer(
            VehicleCharacteristics characteristics)
        {
            if (characteristics == null)
            {
                throw new ArgumentNullException("characteristics");
            }
            this.characteristics = characteristics;
        }

        public EngineFamilyType Recognize()
        {
            string engineFamilyString =
                characteristics.GetEngineFamilyString();
            string engineModelString =
                characteristics.GetEngineModelString();
            EngineFamilyType engineFamilyType = RecognizeEngineFamily(
                engineFamilyString, engineModelString);
            return engineFamilyType;
        }

        // Selects which recognition way to choose
        private EngineFamilyType RecognizeEngineFamily(
            string engineFamilyString,
            string engineModelString)
        {
            if (!String.IsNullOrEmpty(engineFamilyString))
            {
                return RecognizeEngineFamilyString(
                    engineFamilyString, engineModelString);
            }
            return RecognizeEngineFamilyByEngineModel(engineModelString);
        }

        /// <summary>
        /// Recognizes EngineFamily by EngineFamily string -
        /// It's a main first strategy to determine the Family
        /// </summary>
        private EngineFamilyType RecognizeEngineFamilyString(
            string engineFamilyString, string engineModelString)
        {
            EngineFamilyRecognizerByFamilyValue recognizer =
                new EngineFamilyRecognizerByFamilyValue(
                    engineFamilyString, engineModelString);
            return recognizer.Recognize();
        }

        /// <summary>
        /// Backup engine family recognition strategy
        /// </summary>
        /// <param name="engineModelString"></param>
        /// <returns></returns>
        private EngineFamilyType RecognizeEngineFamilyByEngineModel(
            string engineModelString)
        {
            EngineFamilyRecognizerByEngineValue recognizer =
                new EngineFamilyRecognizerByEngineValue(engineModelString);
            return recognizer.Recognize();
        }
    }
}

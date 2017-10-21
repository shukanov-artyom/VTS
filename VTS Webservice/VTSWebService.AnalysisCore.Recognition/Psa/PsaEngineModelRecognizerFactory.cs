using System;
using VTS.AnalysisCore.Common;
using VTS.Shared;
using VTSWebService.AnalysisCore.Enums;
using VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers;

namespace VTSWebService.AnalysisCore.Recognition.Psa
{
    public class PsaEngineModelRecognizerFactory
    {
        private EngineFamily engineFamily;

        public PsaEngineModelRecognizerFactory(EngineFamily engineFamily)
        {
            this.engineFamily = engineFamily;
        }

        public IPsaEngineModelRecognizer Create()
        {
            if (engineFamily.Type == EngineFamilyType.DV)
            {
                return new PsaEngineModelRecognizerDv(engineFamily);
            }
            if (engineFamily.Type == EngineFamilyType.EWDW)
            {
                return new PsaEngineModelRecognizerEwDw(engineFamily);
            }
            if (engineFamily.Type == EngineFamilyType.TU)
            {
                return new PsaEngineModelRecognizerTu(engineFamily);
            }
            if (engineFamily.Type == EngineFamilyType.Prince)
            {
                return new PsaEngineModelRecognizerPrince(engineFamily);
            }
            if (engineFamily.Type == EngineFamilyType.XU)
            {
                return new PsaEngineModelRecognizerXu(engineFamily);
            }
            if (engineFamily.Type == EngineFamilyType.XUD)
            {
                return new PsaEngineModelRecognizerXud(engineFamily);
            }
            if (engineFamily.Type == EngineFamilyType.ES)
            {
                return new PsaEngineModelRecognizerEs(engineFamily);
            }
            if (engineFamily.Type == EngineFamilyType.DK)
            {
                return new PsaEngineModelRecognizerDk(engineFamily);
            }
            if (engineFamily.Type == EngineFamilyType.EB)
            {
                return new PsaEngineModelRecognizerEb(engineFamily);
            }
            if (engineFamily.Type == EngineFamilyType.EC)
            {
                return new PsaEngineModelRecognizerEc(engineFamily);
            }
            if (engineFamily.Type == EngineFamilyType.KR)
            {
                return new EngineModelRecognizerKr(engineFamily);
            }
            if (engineFamily.Type == EngineFamilyType.DT)
            {
                return new EngineModelRecognizerDt(engineFamily);
            }
            if (engineFamily.Type == EngineFamilyType.MMC)
            {
                return new EngineModelRecognizerMMC(engineFamily);
            }
            throw new NotSupportedException("Engine Family unknown.");
        }
    }
}

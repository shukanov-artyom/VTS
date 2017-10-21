using System;
using VTS.AnalysisCore.Common;
using VTS.Shared.DomainObjects;


namespace VTSWebService.AnalysisCore.Recognition.Psa.EngineModelRecognizers
{
    internal abstract class PsaEngineModelRecognizer : IPsaEngineModelRecognizer
    {
        private EngineFamily family;

        protected PsaEngineModelRecognizer(EngineFamily family)
        {
            if (family == null)
            {
                throw new ArgumentNullException("family");
            }
            this.family = family;
        }

        protected EngineFamily Family
        {
            get
            {
                return family;
            }
        }

        public abstract Engine Recognize(
            VehicleCharacteristics characteristics);
    }
}

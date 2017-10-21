using System;
using VTS.AnalysisCore.Common;
using VTS.Shared.DomainObjects;


namespace VTSWebService.AnalysisCore.Recognition.Psa
{
    public interface IPsaEngineModelRecognizer
    {
        Engine Recognize(VehicleCharacteristics characteristics);
    }
}

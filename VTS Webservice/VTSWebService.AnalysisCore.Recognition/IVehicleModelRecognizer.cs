using System;
 
using VTS.Shared.DomainObjects;

namespace VTSWebService.AnalysisCore.Recognition
{
    public interface IVehicleModelRecognizer
    {
        string Recognize(VehicleCharacteristics characteristics);
    }
}

using System;
using VTS.Shared.DomainObjects;


namespace VTSWebService.AnalysisCore.Recognition
{
    public interface IVehicleProductionYearRecognizer
    {
        int Recognize(VehicleCharacteristics characteristics);
    }
}

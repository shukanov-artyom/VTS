using System;
using VTSWeb.AnalysisCore.Recognition;

namespace VTSWeb.AnalysisCore.Interfaces
{
    public interface IVehicleAnalyticModel : IAnalyticModel
    {
        VehicleInformation VehicleInfo
        {
            get;
            set;
        }
    }
}

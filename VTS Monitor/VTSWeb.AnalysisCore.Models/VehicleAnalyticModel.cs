using System;
using VTSWeb.AnalysisCore.Interfaces;
using VTSWeb.AnalysisCore.Recognition;

namespace VTSWeb.AnalysisCore.Models
{
    public class VehicleAnalyticModel : AnalyticModel, IVehicleAnalyticModel
    {
        public VehicleInformation VehicleInfo
        {
            get;
            set;
        }
    }
}

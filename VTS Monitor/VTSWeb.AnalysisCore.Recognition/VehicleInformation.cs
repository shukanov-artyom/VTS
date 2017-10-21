using System;

namespace VTSWeb.AnalysisCore.Recognition
{
    public class VehicleInformation
    {
        private readonly string vin;

        public VehicleInformation(string vin)
        {
            if (String.IsNullOrEmpty(vin))
            {
                throw new ArgumentNullException("vin");
            }
            this.vin = vin;
        }

        public string Vin
        {
            get
            {
                return vin;
            }
        }

        public int ProductionYear
        {
            get;
            set;
        }

        public Engine Engine
        {
            get;
            set;
        }

        public string VehicleModel
        {
            get;
            set;
        }
    }
}

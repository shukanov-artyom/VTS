using System;

namespace VTS.Site.AnalysisCore.Recognition
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

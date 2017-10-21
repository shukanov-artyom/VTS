using System;

namespace VTS.Site.VehicleData.Connectors.Opel
{
    public class OldVehicleNotSupportedException : Exception
    {
        public OldVehicleNotSupportedException(string vin)
            : base (vin)
        {
            
        }
    }
}

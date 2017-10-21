using System;

namespace VTS.Site.VehicleData.Connectors.Opel
{
    public class VinDecodeException : Exception
    {
        public VinDecodeException(string vin)
            : base(vin)
        {
            
        }
    }
}

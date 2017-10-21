using System;
using VTS.Shared;
using VTSWebService.VendorInfo.Citroen;
using VTSWebService.VendorInfo.Peugeot;

namespace VTSWebService.VendorInfo
{
    public class VendorCharacteristicsProviderFactory
    {
        public static IVendorCharacteristicsProvider Create(Manufacturer manufacturer)
        {
            switch (manufacturer)
            {
                case Manufacturer.Citroen:
                    return new CitroenVehicleCharacteristicsProvider();
                case Manufacturer.Peugeot:
                    return new PeugeotVehicleCharacteristicsProvider();
                case Manufacturer.Opel:
                    throw new NotImplementedException();
                default:
                    throw new NotSupportedException("Manufacturer not supported");
            }
        }
    }
}

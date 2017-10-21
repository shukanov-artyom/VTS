using System;
using VTS.Shared.DomainObjects;


namespace VTSWebService.VendorInfo
{
    public interface IVendorCharacteristicsProvider
    {
        VehicleCharacteristics GetByVin(string vin, 
            string preferredLanguage);
    }
}

using System;
using System.Net;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using VTSWebService.DomainObjects.Assemblers;
using VTSWebService.VendorInfo.PsaCommon;

namespace VTSWebService.VendorInfo.Citroen
{
    public class CitroenVehicleCharacteristicsProvider : 
        IVendorCharacteristicsProvider
    {
        public VehicleCharacteristics GetByVin(string vin, 
            string preferredLanguage)
        {
            ServiceCitroenComWebConnector connector =
                new ServiceCitroenComWebConnector(preferredLanguage);
            string result;
            try
            {
                connector.Connect();
                result = connector.Retrieve(vin);
                connector.Disconnect();
            }
            catch (WebException)
            {
                result = null;
                //throw;
            }
            VehicleCharacteristicsPageParser parser =
                new VehicleCharacteristicsPageParser(result,
                    preferredLanguage);
            VehicleCharacteristicsDto dto = parser.Parse();
            dto.Vin = vin;
            return VehicleCharacteristicsAssembler.FromDtoToDomainObject(dto);
        }
    }
}

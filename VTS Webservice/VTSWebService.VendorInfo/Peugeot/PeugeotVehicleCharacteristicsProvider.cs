using System;
using System.Net;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using VTSWebService.DomainObjects.Assemblers;
using VTSWebService.VendorInfo.PsaCommon;

namespace VTSWebService.VendorInfo.Peugeot
{
    public class PeugeotVehicleCharacteristicsProvider : IVendorCharacteristicsProvider
    {
        public VehicleCharacteristics GetByVin(string vin, 
            string preferredLanguage)
        {
            PublicServiceboxPeugeotComWebConnector connector =
                new PublicServiceboxPeugeotComWebConnector(
                    preferredLanguage);
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

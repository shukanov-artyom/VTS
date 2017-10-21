using VTS.Site.DomainObjects.VendorData;

namespace VTS.Site.VehicleData.Connectors
{
    public interface IManufacturerWebConnector
    {
        void Connect();

        VehicleCharacteristics Retrieve(string vin);

        void Disconnect();
    }
}

using System;
using VTS.Site.AnalysisCore.Common;
using VTS.Site.DomainObjects.Enums;
using VTS.Site.DomainObjects.VendorData;
using VTS.Site.VehicleData.Connectors;

namespace VTS.Site.VehicleData
{
    public class VendorInfoRetriever
    {
        private const string DefaultLang = "en_GB";

        private readonly string vin;

        public VendorInfoRetriever(string vin)
        {
            this.vin = vin;
        }

        public VehicleCharacteristics Get()
        {
            Manufacturer? manufacturer = VinChecker.GetManufacturer(vin);
            if (manufacturer == null)
            {
                return null;
            }
            ManufacturerWebConnectorFactory factory = 
                new ManufacturerWebConnectorFactory(DefaultLang);
            IManufacturerWebConnector connector = 
                factory.GetConnector(manufacturer.Value);
            VehicleCharacteristics result;
            try
            {
                connector.Connect();
                result = connector.Retrieve(vin);
                connector.Disconnect();
            }
            catch (Exception)
            {
                // TODO : Message about xception
                return null;
            }
            return result;
        }
    }
}

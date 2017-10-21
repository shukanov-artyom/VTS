using System;
using VTS.Site.DomainObjects.Enums;
using VTS.Site.VehicleData.Connectors;
using VTS.Site.VehicleData.Connectors.Opel;
using VTS.Site.VehicleData.Connectors.Psa;

namespace VTS.Site.VehicleData
{
    internal class ManufacturerWebConnectorFactory
    {
        private readonly string defaultLang = "en_GB";

        public ManufacturerWebConnectorFactory(string defaultLang)
        {
            this.defaultLang = defaultLang;
        }

        public IManufacturerWebConnector GetConnector(Manufacturer manufacturer)
        {
            switch (manufacturer)
            {
                case Manufacturer.Citroen:
                    return new ServiceCitroenComWebConnector(defaultLang);
                case Manufacturer.Peugeot:
                    return new PublicServiceboxPeugeotComWebConnector(defaultLang);
                case Manufacturer.Opel:
                    return new ElcatsRuWebConnector();
                default:
                    throw new Exception("Unknown manufacturer!");
            }
        }
    }
}

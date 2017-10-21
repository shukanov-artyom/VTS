using System.Collections.Generic;
using VTS.Shared.DomainObjects;

namespace VTS.Site.DomainObjects.VendorData
{
    public class VehicleCharacteristics : DomainObject
    {
        private readonly string vin;
        private readonly IList<VehicleCharacteristicsItemsGroup> itemGroups =
            new List<VehicleCharacteristicsItemsGroup>();

        public VehicleCharacteristics(string vin)
        {
            this.vin = vin;
        }

        public long VehicleId { get; set; }

        public string Language { get; set; }

        public string GeneralVehicleInfo { get; set; }

        public string Vin
        {
            get
            {
                return vin;
            }
        }

        public IList<VehicleCharacteristicsItemsGroup> ItemsGroups
        {
            get
            {
                return itemGroups;
            }
        }
    }
}

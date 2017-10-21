using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects;

namespace VTSWeb.VendorData
{
    public class VehicleCharacteristics : DomainObject
    {
        private IList<VehicleCharacteristicsItemsGroup> itemGroups = 
            new List<VehicleCharacteristicsItemsGroup>();

        public long VehicleId { get; set; }

        public string Language { get; set; }

        public string GeneralVehicleInfo { get; set; }

        public IList<VehicleCharacteristicsItemsGroup> ItemsGroups
        {
            get
            {
                return itemGroups;
            }
        }
    }
}

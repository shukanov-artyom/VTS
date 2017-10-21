using System.Collections.Generic;
using VTS.Shared.DomainObjects;

namespace VTS.Site.DomainObjects.VendorData
{
    public class VehicleCharacteristicsItemsGroup : DomainObject
    {
        private IList<VehicleCharacteristicsItem> items =
            new List<VehicleCharacteristicsItem>();

        public long CharacteristicsId { get; set; }

        public string Name { get; set; }

        public IList<VehicleCharacteristicsItem> Items
        {
            get
            {
                return items;
            }
        }
    }
}

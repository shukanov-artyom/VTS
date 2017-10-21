using System;
using System.Collections.Generic;

namespace VTS.Shared.DomainObjects
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

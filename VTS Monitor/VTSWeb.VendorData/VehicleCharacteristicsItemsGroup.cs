﻿using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects;

namespace VTSWeb.VendorData
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

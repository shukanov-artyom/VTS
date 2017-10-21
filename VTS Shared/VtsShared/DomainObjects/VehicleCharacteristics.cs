using System;
using System.Collections.Generic;

namespace VTS.Shared.DomainObjects
{
    public class VehicleCharacteristics : DomainObject
    {
        private readonly IList<VehicleCharacteristicsItemsGroup> itemGroups =
            new List<VehicleCharacteristicsItemsGroup>();

        public string Vin { get; set; }

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

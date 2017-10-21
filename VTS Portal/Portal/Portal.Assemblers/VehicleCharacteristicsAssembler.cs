using System;
using Portal.Service.Service;
using VTS.Shared.DomainObjects;

namespace Portal.Assemblers
{
    public static class VehicleCharacteristicsAssembler
    {
        public static VehicleCharacteristics FromDtoToDomainObject(VehicleCharacteristicsDto source)
        {
            VehicleCharacteristics target = new VehicleCharacteristics();
            target.Id = source.Id;
            target.GeneralVehicleInfo = source.GeneralVehicleInfo;
            target.Vin = source.Vin;
            target.Language = source.Language;
            foreach (VehicleCharacteristicsItemsGroupDto group in source.ItemGroups)
            {
                target.ItemsGroups.Add(VehicleCharacteristicsItemsGroupAssembler.
                    FromDtoToDomainObject(group));
            }
            return target;
        }
    }
}

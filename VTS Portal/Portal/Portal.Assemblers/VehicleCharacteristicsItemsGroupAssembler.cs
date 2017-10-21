using System;
using Portal.Service.Service;
using VTS.Shared.DomainObjects;

namespace Portal.Assemblers
{
    public static class VehicleCharacteristicsItemsGroupAssembler
    {
        public static VehicleCharacteristicsItemsGroup FromDtoToDomainObject(
            VehicleCharacteristicsItemsGroupDto source)
        {
            VehicleCharacteristicsItemsGroup target =
                new VehicleCharacteristicsItemsGroup();
            target.Id = source.Id;
            target.CharacteristicsId = source.CharacteristicsId;
            target.Name = source.Name;
            foreach (VehicleCharacteristicsItemDto item in source.Items)
            {
                target.Items.Add(VehicleCharacteristicsItemAssembler.
                    FromDtoToDomainObject(item));
            }
            return target;
        }
    }
}

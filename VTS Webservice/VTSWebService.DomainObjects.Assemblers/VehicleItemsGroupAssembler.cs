using System;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using VehicleCharacteristicsItemsGroupEntity = 
    VTSWebService.DataAccess.VehicleCharacteristicsItemGroup;
using VehicleCharacteristicsItemEntity =
    VTSWebService.DataAccess.VehicleCharacteristicsItem;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class VehicleItemsGroupAssembler
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

        public static VehicleCharacteristicsItemsGroupDto FromDomainObjectToDto(VehicleCharacteristicsItemsGroup source)
        {
            VehicleCharacteristicsItemsGroupDto target =
                new VehicleCharacteristicsItemsGroupDto();
            target.Id = source.Id;
            target.CharacteristicsId = source.CharacteristicsId;
            target.Name = source.Name;
            foreach (VehicleCharacteristicsItem item in source.Items)
            {
                target.Items.Add(VehicleCharacteristicsItemAssembler.
                    FromDomainObjectToDto(item));
            }
            return target;
        }

        public static VehicleCharacteristicsItemsGroup FromEntityToDomainObject(
            VehicleCharacteristicsItemsGroupEntity source)
        {
            VehicleCharacteristicsItemsGroup target =
                new VehicleCharacteristicsItemsGroup();
            target.Id = source.Id;
            target.CharacteristicsId = source.VehicleCharacteristicsEntityId;
            target.Name = source.Name;
            foreach (VehicleCharacteristicsItemEntity item in 
                source.VehicleCharacteristicsItem)
            {
                target.Items.Add(VehicleCharacteristicsItemAssembler.
                    FromEntityToDomainObject(item));
            }
            return target;
        }

        public static VehicleCharacteristicsItemsGroupEntity FromDomainObjectToEntity(
            VehicleCharacteristicsItemsGroup source)
        {
            VehicleCharacteristicsItemsGroupEntity target =
                new VehicleCharacteristicsItemsGroupEntity();
            target.Id = source.Id;
            target.VehicleCharacteristicsEntityId = source.CharacteristicsId;
            target.Name = source.Name;
            foreach (VehicleCharacteristicsItem item in source.Items)
            {
                target.VehicleCharacteristicsItem.Add(VehicleCharacteristicsItemAssembler.
                    FromDomainObjectToEntity(item));
            }
            return target;
        }
    }
}

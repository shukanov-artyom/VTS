using System;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using VehicleCharacteristicsItemsGroupEntity = 
    VTSWebService.DataAccess.VehicleCharacteristicsItemGroup;
using VehicleCharacteristicsItemEntity =
    VTSWebService.DataAccess.VehicleCharacteristicsItem;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class VehicleCharacteristicsItemAssembler
    {
        public static VehicleCharacteristicsItem FromDtoToDomainObject(
            VehicleCharacteristicsItemDto source)
        {
            VehicleCharacteristicsItem target = 
                new VehicleCharacteristicsItem();
            target.Id = source.Id;
            target.GroupId = source.GroupId;
            target.Name = source.Name;
            target.Value = source.Value;
            return target;
        }

        public static VehicleCharacteristicsItemDto FromDomainObjectToDto(VehicleCharacteristicsItem source)
        {
            VehicleCharacteristicsItemDto target =
                new VehicleCharacteristicsItemDto();
            target.Id = source.Id;
            target.GroupId = source.GroupId;
            target.Name = source.Name;
            target.Value = source.Value;
            return target;
        }

        public static VehicleCharacteristicsItem FromEntityToDomainObject(
            VehicleCharacteristicsItemEntity source)
        {
            VehicleCharacteristicsItem target =
                new VehicleCharacteristicsItem();
            target.Id = source.Id;
            target.GroupId = source.VehicleCharacteristicsItemsGroupEntityId;
            target.Name = source.Name;
            target.Value = source.Value;
            return target;
        }

        public static VehicleCharacteristicsItemEntity FromDomainObjectToEntity(
            VehicleCharacteristicsItem source)
        {
            VehicleCharacteristicsItemEntity target =
                new VehicleCharacteristicsItemEntity();
            target.Id = source.Id;
            target.VehicleCharacteristicsItemsGroupEntityId = source.GroupId;
            target.Name = source.Name;
            target.Value = source.Value;
            return target;
        }
    }
}

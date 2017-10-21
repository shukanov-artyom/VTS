using System;
using Agent.Network.Monitor.VtsWebService;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
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
    }
}

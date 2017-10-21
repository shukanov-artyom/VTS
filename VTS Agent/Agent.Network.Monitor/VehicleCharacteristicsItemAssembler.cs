using System;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
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
    }
}

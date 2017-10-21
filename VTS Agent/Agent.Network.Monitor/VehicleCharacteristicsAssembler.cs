using System;
using Agent.Network.Monitor.VtsWebService;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
{
    public class VehicleCharacteristicsAssembler
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
                target.ItemsGroups.Add(VehicleItemsGroupAssembler.
                    FromDtoToDomainObject(group));
            }
            return target;
        }
    }
}

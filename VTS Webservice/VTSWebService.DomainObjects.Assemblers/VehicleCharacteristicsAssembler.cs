using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using VehicleCharacteristicsEntity = 
    VTSWebService.DataAccess.VehicleCharacteristics;
using VehicleCharacteristicsItemsGroupEntity = 
    VTSWebService.DataAccess.VehicleCharacteristicsItemGroup;

namespace VTSWebService.DomainObjects.Assemblers
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

        public static VehicleCharacteristicsDto FromDomainObjectToDto(VehicleCharacteristics source)
        {
            VehicleCharacteristicsDto target = new VehicleCharacteristicsDto();
            target.ItemGroups = new List<VehicleCharacteristicsItemsGroupDto>();
            target.Id = source.Id;
            target.GeneralVehicleInfo = source.GeneralVehicleInfo;
            target.Vin = source.Vin;
            target.Language = source.Language;
            foreach (VehicleCharacteristicsItemsGroup group in source.ItemsGroups)
            {
                target.ItemGroups.Add(VehicleItemsGroupAssembler.
                    FromDomainObjectToDto(group));
            }
            return target;
        }

        public static VehicleCharacteristics FromEntityToDomainObject(
            VehicleCharacteristicsEntity source)
        {
            VehicleCharacteristics target = new VehicleCharacteristics();
            target.Id = source.Id;
            target.GeneralVehicleInfo = source.GeneralVehicleInfo;
            target.Vin = source.Vin;
            target.Language = source.Language;
            foreach (VehicleCharacteristicsItemsGroupEntity group in
                source.VehicleCharacteristicsItemGroup)
            {
                target.ItemsGroups.Add(VehicleItemsGroupAssembler.
                    FromEntityToDomainObject(group));
            }
            return target;
        }

        public static VehicleCharacteristicsEntity FromDomainObjectToEntity(
            VehicleCharacteristics source)
        {
            VehicleCharacteristicsEntity target = new VehicleCharacteristicsEntity();
            target.Id = source.Id;
            target.GeneralVehicleInfo = source.GeneralVehicleInfo;
            target.Vin = source.Vin;
            target.Language = source.Language;
            foreach (VehicleCharacteristicsItemsGroup group in
                source.ItemsGroups)
            {
                target.VehicleCharacteristicsItemGroup.Add(
                    VehicleItemsGroupAssembler.FromDomainObjectToEntity(group));
            }
            return target;
        }
    }
}

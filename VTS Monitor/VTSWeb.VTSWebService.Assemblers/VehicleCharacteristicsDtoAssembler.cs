using VTSWeb.VTSWebService.VTSWebService;
using VTSWeb.VendorData;

namespace VTSWeb.VTSWebService.Assemblers
{
    public class VehicleCharacteristicsDtoAssembler
    {
        public static VehicleCharacteristics Assemble(VehicleCharacteristicsDto dto)
        {
            VehicleCharacteristics result = new VehicleCharacteristics();

            result.GeneralVehicleInfo = dto.GeneralVehicleInfo;
            result.Language = dto.Language;
            foreach (VehicleCharacteristicsItemsGroupDto group in dto.ItemGroups)
            {
                result.ItemsGroups.Add(VehicleItemsGroupDtoAssembler.Assemble(group));
            }
            return result;
        }
    }
}

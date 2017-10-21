using VTSWeb.VTSWebService.VTSWebService;
using VTSWeb.VendorData;

namespace VTSWeb.VTSWebService.Assemblers
{
    internal static class VehicleItemsGroupDtoAssembler
    {
        public static VehicleCharacteristicsItemsGroup Assemble(
            VehicleCharacteristicsItemsGroupDto dto)
        {
            VehicleCharacteristicsItemsGroup result = 
                new VehicleCharacteristicsItemsGroup();
            result.Name = dto.Name;
            foreach (VehicleCharacteristicsItemDto item in dto.Items)
            {
                result.Items.Add(VehicleCharacteristicsItemDtoAssembler.
                    Assemble(item));
            }
            return result;
        }
    }
}

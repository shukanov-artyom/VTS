using VTSWeb.VTSWebService.VTSWebService;
using VTSWeb.VendorData;

namespace VTSWeb.VTSWebService.Assemblers
{
    internal static class VehicleCharacteristicsItemDtoAssembler
    {
        public static VehicleCharacteristicsItem Assemble(
            VehicleCharacteristicsItemDto dto)
        {
            VehicleCharacteristicsItem item = new VehicleCharacteristicsItem();
            item.Name = dto.Name;
            item.Value = dto.Value;
            return item;
        }
    }
}

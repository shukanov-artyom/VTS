using System;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using VehicleEntity = VTSWebService.DataAccess.Vehicle;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class VehicleAssembler
    {
        public static VehicleDto FromEntityToDto(VehicleEntity source)
        {
            VehicleDto target = new VehicleDto();
            target.Id = source.Id;
            target.Manufacturer = source.Manufacturer;
            target.Mileage = source.Mileage;
            target.Model = source.ModelName;
            target.ProductionYear = source.ProductionYear;
            target.RegisteredDate = source.Registered;
            target.Vin = source.Vin;
            return target;
        }

        public static VehicleEntity FromDomainObjectToEntity(Vehicle source)
        {
            VehicleEntity target = new VehicleEntity();
            target.Id = source.Id;
            target.Manufacturer = (int)source.Manufacturer;
            target.Mileage = source.Mileage;
            target.ModelName = source.Model;
            target.ProductionYear = source.ProductionYear;
            target.Registered = source.RegisteredDate;
            target.Vin = source.Vin;
            return target;
        }
    }
}

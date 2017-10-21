using System;
using Portal.Service.Service;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Portal.Assemblers
{
    public class VehicleAssembler
    {
        public static Vehicle FromDtoToDomainObject(VehicleDto source)
        {
            var target = new Vehicle();
            target.Id = source.Id;
            target.Manufacturer = (Manufacturer)source.Manufacturer;
            target.Mileage = source.Mileage;
            target.Model = source.Model;
            target.ProductionYear = source.ProductionYear;
            target.RegisteredDate = source.RegisteredDate;
            target.Vin = source.Vin;
            return target;
        }
    }
}

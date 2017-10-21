using System;
using Agent.Network.Monitor.VtsWebService;
using VTS.Agent.BusinessObjects;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
{
    public static class VehicleAssembler
    {
        public static Vehicle FromDtoToDomainObject(VehicleDto source)
        {
            Vehicle target = new Vehicle();
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

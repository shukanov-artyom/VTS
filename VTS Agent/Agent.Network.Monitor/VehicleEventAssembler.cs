using System;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
{
    public static class VehicleEventAssembler
    {
        public static VehicleEvent FromDtoToDomainObject(VehicleEventDto source)
        {
            var target = new VehicleEvent();
            target.Comment = source.Comment;
            target.Date = source.Date;
            target.Id = source.Id;
            target.Mileage = source.Mileage;
            target.NextReplacementPeriod = source.NextReplacementPeriod;
            target.RedMileage = source.RedMileage;
            target.Type = (VehicleEventType)source.Type;
            target.VehicleId = source.VehicleId;
            target.YellowMileage = source.YellowMileage;
            return target;
        }

        public static VehicleEventDto FromDomainObjectToDto(VehicleEvent source)
        {
            var target = new VehicleEventDto();
            target.Comment = source.Comment;
            target.Date = source.Date;
            target.Id = source.Id;
            target.Mileage = source.Mileage;
            target.NextReplacementPeriod = source.NextReplacementPeriod;
            target.RedMileage = source.RedMileage;
            target.Type = (int)source.Type;
            target.VehicleId = source.VehicleId;
            target.YellowMileage = source.YellowMileage;
            return target;
        }
    }
}

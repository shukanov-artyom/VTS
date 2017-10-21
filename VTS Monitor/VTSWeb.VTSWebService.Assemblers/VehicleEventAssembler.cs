using System;
using VTS.Shared.DomainObjects;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public class VehicleEventAssembler
    {
        public static VehicleEvent FromDtoToDomainObject(VehicleEventDto source)
        {
            VehicleEvent target = new VehicleEvent();
            target.Id = source.Id;
            target.Comment = source.Comment;
            target.Date = source.Date;
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
            VehicleEventDto target = new VehicleEventDto();
            target.Id = source.Id;
            target.Comment = source.Comment;
            target.Date = source.Date;
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

using System;
using VTSWebService.DataAccess;
using VTSWebService.DataContracts;

namespace VTSWebService.DomainObjects.Assemblers
{
    public class VehicleEventAssembler
    {
        public static VehicleEventDto FromEntityToDto(VehicleEvent source)
        {
            VehicleEventDto target = new VehicleEventDto();
            target.Id = source.Id;
            target.Comment = source.Comment;
            target.Date = source.Date;
            target.Mileage = source.Mileage;
            target.NextReplacementPeriod = source.NextReplacementPeriod;
            target.RedMileage = source.RedMileage;
            target.Type = source.Type;
            target.VehicleId = source.VehicleEntityId;
            target.YellowMileage = source.YellowMileage;
            return target;
        }

        public static VehicleEvent FromDtoToEntity(VehicleEventDto source)
        {
            VehicleEvent target = new VehicleEvent();
            target.Id = source.Id;
            target.Comment = source.Comment;
            target.Date = source.Date;
            target.Mileage = source.Mileage;
            target.NextReplacementPeriod = source.NextReplacementPeriod;
            target.RedMileage = source.RedMileage;
            target.Type = source.Type;
            target.VehicleEntityId = source.VehicleId;
            target.YellowMileage = source.YellowMileage;
            return target;
        }

        public static void CopyEntityProperties(VehicleEvent source, VehicleEvent target)
        {
            target.Id = source.Id;
            target.Comment = source.Comment;
            target.Date = source.Date;
            target.Mileage = source.Mileage;
            target.NextReplacementPeriod = source.NextReplacementPeriod;
            target.RedMileage = source.RedMileage;
            target.Type = source.Type;
            target.VehicleEntityId = source.VehicleEntityId;
            target.YellowMileage = source.YellowMileage;
        }
    }
}

using System;
using VTS.Site.AnalysisCore.Recognition;
using VTS.Site.WebService.VtsWebService;

namespace VTS.Site.WebService.Assemblers
{
    public class VehicleInformationAssembler
    {
        public static VehicleInformation FromDtoToDomainObject(
            VehicleInformationDto source)
        {
            VehicleInformation target = new VehicleInformation(source.Vin);
            target.Engine = EngineAssembler.FromDtoToObject(source.Engine);
            target.VehicleModel = source.VehicleModel;
            return target;
        }

        public static VehicleInformationDto FromDomainObjectToDto(
            VehicleInformation source)
        {
            VehicleInformationDto target = new VehicleInformationDto();
            target.Engine = EngineAssembler.FromObjectToDto(source.Engine);
            target.VehicleModel = source.VehicleModel;
            target.Vin = source.Vin;
            return target;
        }
    }
}

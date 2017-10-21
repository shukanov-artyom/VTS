using System;
using VTS.AnalysisCore.Common;
using VTSWebService.DataContracts;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class VehicleInformationAssembler
    {
        public static VehicleInformationDto FromDomainObjectToDto(
            VehicleInformation source)
        {
            VehicleInformationDto target = new VehicleInformationDto();
            target.Engine = EngineAssembler.FromObjectToDto(source.Engine);
            target.VehicleModel = source.VehicleModel;
            target.Vin = source.Vin;
            return target;
        }

        public static VehicleInformation FromDtoToDomainObject(
            VehicleInformationDto source)
        {
            VehicleInformation target = new VehicleInformation(source.Vin);
            target.Engine = EngineAssembler.FromDtoToObject(source.Engine);
            target.VehicleModel = source.VehicleModel;
            return target;
        }
    }
}

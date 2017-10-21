using System;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public class VehicleInformationAssembler
    {
        public static VehicleInformation FromDtoToDomainObject(VehicleInformationDto source)
        {
            VehicleInformation target = new VehicleInformation(source.Vin);
            target.VehicleModel = source.VehicleModel;
            target.ProductionYear = source.ProductionYear;
            target.Engine = EngineAssembler.FromDtoToDomainObject(source.Engine);
            return target;
        }
    }
}

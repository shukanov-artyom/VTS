using System;
using VTS.Shared;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public class EngineAssembler
    {
        public static Engine FromDtoToDomainObject(EngineDto source)
        {
            Engine target = new Engine();
            target.DisplayName = source.DisplayName;
            target.Family = EngineFamilyAssembler.FromDtoToObject(source.Family);
            target.FuelType = (FuelType)source.FuelType;
            target.InjectionType = (InjectionType) source.InjectionType;
            target.Type = (EngineType) source.Type;
            return target;
        }
    }
}

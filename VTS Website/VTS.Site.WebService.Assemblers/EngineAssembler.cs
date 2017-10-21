using System;
using VTS.Shared;
using VTS.Site.AnalysisCore;
using VTS.Site.DomainObjects;
using VTS.Site.WebService.VtsWebService;

namespace VTS.Site.WebService.Assemblers
{
    public static class EngineAssembler
    {
        public static EngineDto FromObjectToDto(Engine source)
        {
            EngineDto target = new EngineDto();
            target.DisplayName = source.DisplayName;
            target.Family = EngineFamilyAssembler.FromObjectToDto(source.Family);
            target.FuelType = (int)source.FuelType;
            target.InjectionType = (int)source.InjectionType;
            target.Type = (int)source.Type;
            return target;
        }

        public static Engine FromDtoToObject(EngineDto source)
        {
            Engine target = new Engine();
            target.DisplayName = source.DisplayName;
            target.Family = EngineFamilyAssembler.FromDtoToObject(source.Family);
            target.FuelType = (FuelType)source.FuelType;
            target.InjectionType = (InjectionType)source.InjectionType;
            target.Type = (EngineType)source.Type;
            return target;
        }
    }
}

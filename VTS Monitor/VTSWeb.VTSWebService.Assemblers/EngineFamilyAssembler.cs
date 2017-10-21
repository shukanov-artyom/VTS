using System;
using VTS.Shared;
using VTSWeb.AnalysisCore.Recognition.Engines;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public static class EngineFamilyAssembler
    {
        public static EngineFamily FromDtoToObject(EngineFamilyDto source)
        {
            EngineFamily target = new EngineFamily();
            target.DisplayName = source.DisplayName;
            target.Link = source.Link;
            target.Type = (EngineFamilyType)source.Type;
            return target;
        }
    }
}

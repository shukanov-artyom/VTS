using System;
using VTS.Shared;
using VTS.Site.DomainObjects;
using VTS.Site.WebService.VtsWebService;

namespace VTS.Site.WebService.Assemblers
{
    public static class EngineFamilyAssembler
    {
        public static EngineFamilyDto FromObjectToDto(EngineFamily source)
        {
            EngineFamilyDto target = new EngineFamilyDto();
            target.DisplayName = source.DisplayName;
            target.Link = source.Link;
            target.Type = (int)source.Type;
            return target;
        }

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

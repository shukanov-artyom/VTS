using System;
using VTS.Site.DomainObjects;
using VTS.Site.WebService.VtsWebService;

namespace VTS.Site.WebService.Assemblers
{
    public static class SystemNewsLocalizedAssembler
    {
        public static SystemNewsLocalizedDto FromDomainObjectToDto(SystemNewsLocalized source)
        {
            SystemNewsLocalizedDto target = new SystemNewsLocalizedDto();
            target.Header = source.Header;
            target.Id = source.Id;
            target.Language = source.Language;
            target.NewsContentText = source.NewsContentText;
            target.SystemNewsId = source.SystemNewsId;
            return target;
        }

        public static SystemNewsLocalized FromDtoToDomainObject(SystemNewsLocalizedDto source)
        {
            SystemNewsLocalized target = new SystemNewsLocalized();
            target.Header = source.Header;
            target.Id = source.Id;
            target.Language = source.Language;
            target.NewsContentText = source.NewsContentText;
            target.SystemNewsId = source.SystemNewsId;
            return target;
        }
    }
}

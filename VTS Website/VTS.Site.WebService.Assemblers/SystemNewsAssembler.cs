using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Site.DomainObjects;
using VTS.Site.WebService.VtsWebService;

namespace VTS.Site.WebService.Assemblers
{
    public class SystemNewsAssembler
    {
        public static SystemNews FromDtoToDomainObject(SystemNewsDto source)
        {
            SystemNews target = new SystemNews();
            target.Id = source.Id;
            target.DatePublished = source.DatePublished;
            target.IsBlocked = source.IsBlocked;
            foreach (SystemNewsLocalizedDto newsLocalized in source.SystemNewsLocalized)
            {
                SystemNewsLocalized snld =
                    SystemNewsLocalizedAssembler.FromDtoToDomainObject(newsLocalized);
                target.SystemNewsLocalized.Add(snld);
            }
            return target;
        }

        public static SystemNewsDto FromDomainObjectToDto(SystemNews source)
        {
            SystemNewsDto target = new SystemNewsDto();
            target.Id = source.Id;
            target.DatePublished = source.DatePublished;
            target.IsBlocked = source.IsBlocked;
            IList<SystemNewsLocalizedDto> list = new List<SystemNewsLocalizedDto>();
            foreach (SystemNewsLocalized newsLocalized in source.SystemNewsLocalized)
            {
                SystemNewsLocalizedDto snld =
                    SystemNewsLocalizedAssembler.FromDomainObjectToDto(newsLocalized);
                list.Add(snld);
            }
            target.SystemNewsLocalized = list.ToArray();
            return target;
        }
    }
}

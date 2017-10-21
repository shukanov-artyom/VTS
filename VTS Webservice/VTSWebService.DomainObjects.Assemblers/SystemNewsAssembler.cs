using System;
using System.Collections.Generic;
using VTSWebService.DataAccess;
using VTSWebService.DataContracts;
using SystemNewsEntity = VTSWebService.DataAccess.SystemNews;
using SystemNewsLocalizedEntity =
    VTSWebService.DataAccess.SystemNewsLocalized;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class SystemNewsAssembler
    {
        public static SystemNewsDto FromEntityToDto(SystemNewsEntity source)
        {
            SystemNewsDto target = new SystemNewsDto();
            target.SystemNewsLocalized = new List<SystemNewsLocalizedDto>();
            target.Id = source.Id;
            target.DatePublished = source.DatePublished;
            target.IsBlocked = source.IsBlocked;
            foreach (SystemNewsLocalized newsLocalized in source.SystemNewsLocalized)
            {
                SystemNewsLocalizedDto snld = 
                    SystemNewsLocalizedAssembler.FromEntityToDto(newsLocalized);
                target.SystemNewsLocalized.Add(snld);
            }
            return target;
        }

        public static SystemNewsEntity FromDtoToEntity(SystemNewsDto source)
        {
            SystemNewsEntity target = new SystemNewsEntity();
            target.Id = source.Id;
            target.DatePublished = source.DatePublished;
            target.IsBlocked = source.IsBlocked;
            foreach (SystemNewsLocalizedDto newsLocalized in source.SystemNewsLocalized)
            {
                SystemNewsLocalizedEntity snld =
                    SystemNewsLocalizedAssembler.FromDtoToEntity(newsLocalized);
                target.SystemNewsLocalized.Add(snld);
            }
            return target;
        }

        public static void CopyEntityProperties(
            SystemNewsEntity source, SystemNewsEntity target)
        {
            target.DatePublished = source.DatePublished;
            target.Id = source.Id;
            target.IsBlocked = source.IsBlocked;
            target.SystemNewsLocalized.Clear();
            foreach (SystemNewsLocalized newsLocalized in source.SystemNewsLocalized)
            {
                SystemNewsLocalized loc = new SystemNewsLocalized();
                SystemNewsLocalizedAssembler.CopyEntityProperties(newsLocalized, loc);
                target.SystemNewsLocalized.Add(loc);
            }
        }
    }
}

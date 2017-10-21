using System;
using VTSWebService.DataContracts;
using SystemNewsLocalizedEntity = VTSWebService.DataAccess.SystemNewsLocalized;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class SystemNewsLocalizedAssembler
    {
        public static SystemNewsLocalizedDto FromEntityToDto(SystemNewsLocalizedEntity source)
        {
            SystemNewsLocalizedDto target = new SystemNewsLocalizedDto();
            target.Header = source.Header;
            target.Id = source.Id;
            target.Language = source.Language;
            target.NewsContentText = source.NewsContentText;
            target.SystemNewsId = source.SystemNewsId;
            return target;
        }

        public static SystemNewsLocalizedEntity FromDtoToEntity(SystemNewsLocalizedDto source)
        {
            SystemNewsLocalizedEntity target = new SystemNewsLocalizedEntity();
            target.Header = source.Header;
            target.Id = source.Id;
            target.Language = source.Language;
            target.NewsContentText = source.NewsContentText;
            target.SystemNewsId = source.SystemNewsId;
            return target;
        }

        public static void CopyEntityProperties(SystemNewsLocalizedEntity source,
            SystemNewsLocalizedEntity target)
        {
            target.Header = source.Header;
            target.Id = source.Id;
            target.Language = source.Language;
            target.NewsContentText = source.NewsContentText;
            target.SystemNewsId = source.SystemNewsId;
        }
    }
}

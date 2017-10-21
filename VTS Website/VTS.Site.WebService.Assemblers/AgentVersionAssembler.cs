using System;
using VTS.Site.DomainObjects;
using VTS.Site.WebService.VtsWebService;

namespace VTS.Site.WebService.Assemblers
{
    public class AgentVersionAssembler
    {
        public static AvailableAgentVersion FromDtoToDomainObject(AgentVersionDto source)
        {
            AvailableAgentVersion target = new AvailableAgentVersion();
            target.DownloadLink = source.DownloadLink;
            target.Id = source.Id;
            target.ReleasedDate = source.ReleasedDate;
            target.VersionString = source.VersionString;
            return target;
        }
    }
}

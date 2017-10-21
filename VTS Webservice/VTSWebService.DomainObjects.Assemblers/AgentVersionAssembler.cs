 
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using AgentVersionEntity = VTSWebService.DataAccess.AgentVersion;

namespace VTSWebService.DomainObjects.Assemblers
{
    public class AgentVersionAssembler
    {
        public static AgentVersionDto FromDomainObjectToDto(AgentVersion source)
        {
            AgentVersionDto target = new AgentVersionDto();
            target.DownloadLink = source.DownloadLink;
            target.Id = source.Id;
            target.ReleasedDate = source.ReleasedDate;
            target.VersionString = source.VersionString;
            return target;
        }

        public static AgentVersion FromEntityToDomainObject(DataAccess.AgentVersion source)
        {
            AgentVersion target = new AgentVersion();
            target.Id = source.Id;
            target.DownloadLink = source.DownloadLink;
            target.ReleasedDate = source.ReleasedDate;
            target.VersionString = source.VersionString;
            return target;
        }

        public static AgentVersionDto FromEntityToDto(AgentVersionEntity source)
        {
            AgentVersionDto target = new AgentVersionDto();
            target.DownloadLink = source.DownloadLink;
            target.Id = source.Id;
            target.ReleasedDate = source.ReleasedDate;
            target.VersionString = source.VersionString;
            return target;
        }
    }
}
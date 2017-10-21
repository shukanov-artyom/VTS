using Agent.Network.Monitor.VtsWebService;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
{
    public static class AgentVersionAssembler
    {
        public static AgentVersion ToDomainObjectFromDto(AgentVersionDto dto)
        {
            AgentVersion result = new AgentVersion(dto.VersionString);
            result.DownloadLink = dto.DownloadLink;
            result.Id = dto.Id;
            result.ReleasedDate = dto.ReleasedDate;
            return result;
        }
    }
}

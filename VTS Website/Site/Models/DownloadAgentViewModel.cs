using System.Collections.Generic;
using VTS.Site.DomainObjects;
using VTS.Site.WebService.Assemblers;
using VTS.Site.WebService.VtsWebService;

namespace Html.Models
{
    public class DownloadAgentViewModel
    {
        public IList<AvailableAgentVersion> AvailableAgentVersions
        {
            get
            {
                IList<AvailableAgentVersion> result = new List<AvailableAgentVersion>();
                VtsWebServiceClient client = new VtsWebServiceClient();
                foreach (AgentVersionDto dto in client.GetAgentVersions())
                {
                    result.Add(AgentVersionAssembler.FromDtoToDomainObject(dto));
                }
                client.Close();
                return result;
            }
        }
    }
}
using System.Reflection;
using VTS.Shared.DomainObjects;

namespace Agent.Common
{
    public class ApplicationVersion
    {
        public static AgentVersion Current
        {
            get
            {
                AgentVersion version = new AgentVersion(
                    Assembly.GetEntryAssembly().GetName().Version.ToString());
                return version;
            }
        }
    }
}

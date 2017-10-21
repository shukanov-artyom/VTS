using System;

namespace VTS.Shared.DomainObjects
{
    public class AgentVersion : DomainObject
    {
        public AgentVersion()
            : this(String.Empty)
        {
        }

        public AgentVersion(string versionString)
        {
            VersionString = versionString;
        }
        public string VersionString
        {
            get;
            set;
        }

        public string DownloadLink
        {
            get;
            set;
        }

        public DateTime ReleasedDate
        {
            get;
            set;
        }

        public bool IsOlderThan(AgentVersion version)
        {
            Version v = new Version(version.VersionString);
            Version ths = new Version(VersionString);
            int relation = ths.CompareTo(v);
            return relation < 0;
        }
    }
}

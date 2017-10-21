using System;
using VTS.Shared.DomainObjects;

namespace VTS.Site.DomainObjects
{
    public class AvailableAgentVersion : DomainObject
    {
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
    }
}

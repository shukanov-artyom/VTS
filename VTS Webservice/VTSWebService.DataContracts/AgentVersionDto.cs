using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class AgentVersionDto : DomainObjectDto
    {
        [DataMember]
        public string VersionString
        {
            get;
            set;
        }

        [DataMember]
        public string DownloadLink
        {
            get;
            set;
        }

        [DataMember]
        public DateTime ReleasedDate
        {
            get;
            set;
        }
    }
}
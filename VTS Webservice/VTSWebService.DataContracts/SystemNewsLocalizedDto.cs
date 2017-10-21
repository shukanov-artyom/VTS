using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class SystemNewsLocalizedDto : DomainObjectDto
    {
        [DataMember]
        public string Header { get; set; }

        [DataMember]
        public string NewsContentText { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public long SystemNewsId { get; set; }
    }
}
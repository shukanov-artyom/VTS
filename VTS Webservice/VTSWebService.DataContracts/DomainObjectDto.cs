using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class DomainObjectDto
    {
        [DataMember]
        public long Id { get; set; }
    }
}
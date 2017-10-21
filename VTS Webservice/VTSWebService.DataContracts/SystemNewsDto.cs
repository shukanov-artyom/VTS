using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class SystemNewsDto : DomainObjectDto
    {
        [DataMember]
        public bool IsBlocked
        {
            get;
            set;
        }

        [DataMember]
        public List<SystemNewsLocalizedDto> SystemNewsLocalized { get; set; }

        [DataMember]
        public DateTime DatePublished 
        {
            get;
            set;
        }
    }
}
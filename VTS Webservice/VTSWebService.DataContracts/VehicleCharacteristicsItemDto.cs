using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class VehicleCharacteristicsItemDto : DomainObjectDto
    {
        [DataMember]
        public long GroupId
        {
            get;
            set;
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public string Value
        {
            get;
            set;
        }
    }
}
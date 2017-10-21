using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class VehicleCharacteristicsDto : DomainObjectDto
    {
        [DataMember]
        public string Vin { get; set; }

        [DataMember]
        public string GeneralVehicleInfo { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public List<VehicleCharacteristicsItemsGroupDto> ItemGroups { get; set; }
    }
}
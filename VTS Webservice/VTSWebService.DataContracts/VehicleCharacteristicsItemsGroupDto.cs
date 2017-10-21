using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class VehicleCharacteristicsItemsGroupDto : DomainObjectDto
    {
        private List<VehicleCharacteristicsItemDto> items =
            new List<VehicleCharacteristicsItemDto>();

        [DataMember]
        public long CharacteristicsId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<VehicleCharacteristicsItemDto> Items
        {
            get
            {
                return items;
            }
        }
    }
}
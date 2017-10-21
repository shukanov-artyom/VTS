using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class VehicleEventDto : DomainObjectDto
    {
        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public int Mileage { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public int? YellowMileage { get; set; }

        [DataMember]
        public int? RedMileage { get; set; }

        [DataMember]
        public long VehicleId { get; set; }

        [DataMember]
        public int NextReplacementPeriod { get; set; }
    }
}
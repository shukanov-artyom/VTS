using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class VehicleDto : DomainObjectDto
    {
        [DataMember]
        public string Vin
        {
            get;
            set;
        }

        [DataMember]
        public int ProductionYear
        {
            get;
            set;
        }

        [DataMember]
        public int Manufacturer
        {
            get;
            set;
        }

        [DataMember]
        public string Model
        {
            get;
            set;
        }

        [DataMember]
        public System.DateTime RegisteredDate
        {
            get;
            set;
        }

        [DataMember]
        public int Mileage
        {
            get;
            set;
        }
    }
}
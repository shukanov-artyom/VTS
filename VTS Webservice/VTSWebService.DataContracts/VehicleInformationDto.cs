using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class VehicleInformationDto
    {
        [DataMember]
        public string Vin { get; set; }

        [DataMember]
        public EngineDto Engine
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
        public string VehicleModel
        {
            get;
            set;
        }
    }
}
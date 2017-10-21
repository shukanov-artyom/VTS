using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class PsaTraceDto : DomainObjectDto
    {
        public PsaTraceDto()
        {
            ParametersSets = new List<PsaParametersSetDto>();
        }

        [DataMember]
        public long PsaDatasetId
        {
            get;
            set;
        }

        [DataMember]
        public DateTime Date
        {
            get;
            set;
        }

        [DataMember]
        public string Application
        {
            get;
            set;
        }

        [DataMember]
        public string Format
        {
            get;
            set;
        }

        [DataMember]
        public string Phone
        {
            get;
            set;
        }

        [DataMember]
        public string PhoneChannel
        {
            get;
            set;
        }

        [DataMember]
        public string Vin
        {
            get;
            set;
        }

        [DataMember]
        public string Address
        {
            get;
            set;
        }

        [DataMember]
        public string ToolSerialNumber
        {
            get;
            set;
        }

        [DataMember]
        public string ToolName
        {
            get;
            set;
        }

        [DataMember]
        public string SavesetId
        {
            get;
            set;
        }

        [DataMember]
        public string VehicleModelName
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

        [DataMember]
        public int Manufacturer
        {
            get;
            set;
        }

        [DataMember]
        public List<PsaParametersSetDto> ParametersSets
        {
            get;
            set;
        }
    }
}
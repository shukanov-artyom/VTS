using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    public class PsaParametersSetDto : DomainObjectDto
    {
        public PsaParametersSetDto()
        {
            Parameters = new List<PsaParameterDataDto>();
        }

        [DataMember]
        public long PsaTraceId
        {
            get;
            set;
        }

        [DataMember]
        public int Type
        {
            get;
            set;
        }

        [DataMember]
        public string EcuName
        {
            get;
            set;
        }

        [DataMember]
        public string EcuLabel
        {
            get;
            set;
        }

        [DataMember]
        public string OriginalTypeId
        {
            get;
            set;
        }

        [DataMember]
        public string AdditionalSourceInfo
        {
            get;
            set;
        }

        [DataMember]
        public IList<PsaParameterDataDto> Parameters { get; set; }
    }
}
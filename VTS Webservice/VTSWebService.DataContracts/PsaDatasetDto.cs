using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class PsaDatasetDto : DomainObjectDto
    {
        public PsaDatasetDto()
        {
            Traces = new List<PsaTraceDto>();
        }

        [DataMember]
        public DateTime SavedDate
        {
            get;
            set;
        }

        [DataMember]
        public DateTime ExportedDate
        {
            get;
            set;
        }

        [DataMember]
        public Guid Guid
        {
            get;
            set;
        }

        [DataMember]
        public long PsaVehicleId
        {
            get;
            set;
        }

        [DataMember]
        public List<PsaTraceDto> Traces { get; set; }
    }
}
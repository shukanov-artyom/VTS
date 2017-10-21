using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class AnalyticStatisticsItemDto : DomainObjectDto
    {
        [DataMember]
        public IList<AnalyticStatisticsValueDto> Values { get; set; }

        [DataMember]
        public int Type
        {
            get;
            set;
        }

        [DataMember]
        public int TargetEngineType
        {
            get;
            set;
        }

        [DataMember]
        public int TargetEngineFamilyType
        {
            get;
            set;
        }

        [DataMember]
        public string VersionGenerated
        {
            get;
            set;
        }

        [DataMember]
        public DateTime DateGenerated
        {
            get;
            set;
        }
    }
}
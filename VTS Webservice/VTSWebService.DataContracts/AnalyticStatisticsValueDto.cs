using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class AnalyticStatisticsValueDto : DomainObjectDto
    {
        [DataMember]
        public long AnalyticStatisticsItemId
        {
            get;
            set;
        }

        [DataMember]
        public DateTime SourceDataCaptureDateTime 
        {
            get;
            set;
        }

        [DataMember]
        public double Value
        {
            get;
            set;
        }

        [DataMember]
        public string SourceVin
        {
            get;
            set;
        }

        [DataMember]
        public long SourcePsaParametersSetId
        {
            get;
            set;
        }
    }
}
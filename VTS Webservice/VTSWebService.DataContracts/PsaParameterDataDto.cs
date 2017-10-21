using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class PsaParameterDataDto : DomainObjectDto
    {
        public PsaParameterDataDto()
        {
            Values = new List<string>();
            Timestamps = new List<int>();
        }

        [DataMember]
        public long PsaParametersSetId
        {
            get;
            set;
        }

        [DataMember]
        public bool HasTimestamps
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
        public int Type
        {
            get;
            set;
        }

        [DataMember]
        public int Units
        {
            get;
            set;
        }

        [DataMember]
        public List<string> Values { get; set; }

        [DataMember]
        public List<int> Timestamps { get; set; }
    }
}
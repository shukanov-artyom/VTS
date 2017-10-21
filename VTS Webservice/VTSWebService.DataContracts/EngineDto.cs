using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class EngineDto
    {
        [DataMember]
        public int Type
        {
            get;
            set;
        }

        [DataMember]
        public string DisplayName
        {
            get;
            set;
        }

        [DataMember]
        public EngineFamilyDto Family
        {
            get;
            set;
        }

        [DataMember]
        public int FuelType
        {
            get;
            set;
        }

        [DataMember]
        public int InjectionType
        {
            get;
            set;
        }
    }
}
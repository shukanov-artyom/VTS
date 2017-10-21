using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class EngineFamilyDto
    {
        [DataMember]
        public int Type
        {
            get;
            set;
        }

        [DataMember]
        public string Link
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
    }
}
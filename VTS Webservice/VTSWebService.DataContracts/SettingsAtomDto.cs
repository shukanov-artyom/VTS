using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class SettingsAtomDto : DomainObjectDto
    {
        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public double MinOptimal { get; set; }

        [DataMember]
        public double MaxOptimal { get; set; }

        [DataMember]
        public double MinAcceptable { get; set; }

        [DataMember]
        public double MaxAcceptable { get; set; }

        [DataMember]
        public long MoleculeId { get; set; }
    }
}
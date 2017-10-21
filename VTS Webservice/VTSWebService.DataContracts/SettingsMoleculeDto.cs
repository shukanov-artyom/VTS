using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class SettingsMoleculeDto : DomainObjectDto
    {
        [DataMember]
        public SettingsAtomDto StatisticalAtom { get; set; }

        [DataMember]
        public SettingsAtomDto PredefinedAtom { get; set; }

        [DataMember]
        public bool OverrideOptimal { get; set; }

        [DataMember]
        public bool OverrideAcceptable { get; set; }

        [DataMember]
        public long AnalyticRuleSettingsId { get; set; }
    }
}
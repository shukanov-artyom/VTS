using System;
using System.Runtime.Serialization;

namespace VTSWebService.DataContracts
{
    [DataContract]
    public class AnalyticRuleSettingsDto : DomainObjectDto
    {
        [DataMember]
        public int RuleType { get; set; }

        [DataMember]
        public int? EngineFamilyType { get; set; }

        [DataMember]
        public int? EngineType { get; set; }

        [DataMember]
        public SettingsMoleculeDto SettingsMolecule { get; set; }

        [DataMember]
        public int Reliability { get; set; }
    }
}
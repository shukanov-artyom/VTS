using System;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace VTS.AnalysisCore.Common
{
    public class AnalyticRuleSettings : DomainObject
    {
        public AnalyticRuleSettings(AnalyticRuleType type,
            AnalyticItemSettingsReliability reliability)
        {
            SettingsMolecule = new SettingsMolecule();
            RuleType = type;
            Reliability = reliability;
        }

        public AnalyticRuleType RuleType
        {
            get;
            set;
        }

        public EngineFamilyType? EngineFamilyType
        {
            get;
            set;
        }

        public EngineType? EngineType
        {
            get;
            set;
        }

        public SettingsMolecule SettingsMolecule
        {
            get;
            set;
        }

        public AnalyticItemSettingsReliability Reliability
        {
            get;
            set;
        }
    }
}

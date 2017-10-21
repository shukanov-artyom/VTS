using System;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Interfaces;

namespace VTSWeb.AnalysisCore.Models.Settings
{
    public class AnalyticRuleSettings : DomainObject
    {
        private AnalyticRuleType type;
        private AnalyticItemSettingsReliability reliability;
        private SettingsMolecule molecule = new SettingsMolecule();

        public AnalyticRuleSettings(AnalyticRuleType type, 
            AnalyticItemSettingsReliability reliability)
        {
            this.type = type;
            this.reliability = reliability;
        }

        public AnalyticRuleType RuleType
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
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
            get
            {
                return molecule;
            }
            set
            {
                molecule = value;
            }
        }

        public AnalyticItemSettingsReliability Reliability
        {
            get
            {
                return reliability;
            }
            set
            {
                reliability = value;
            }
        }
    }
}

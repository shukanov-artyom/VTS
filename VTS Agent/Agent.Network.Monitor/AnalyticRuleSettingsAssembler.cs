using System;
using Agent.Network.Monitor.VtsWebService;
using VTS.Agent.BusinessObjects;
using VTS.Agent.BusinessObjects.Enums;
using VTS.Shared;

namespace Agent.Network.Monitor
{
    public static class AnalyticRuleSettingsAssembler
    {
        public static AnalyticRuleSettings FromDtoToDomainObject(AnalyticRuleSettingsDto source)
        {
            AnalyticRuleSettings target = new AnalyticRuleSettings(
                (AnalyticRuleType)source.RuleType, 
                (AnalyticItemSettingsReliability)source.Reliability);
            target.Id = source.Id;
            target.EngineFamilyType = (EngineFamilyType)source.EngineFamilyType;
            target.EngineType = (EngineType)source.EngineType;
            target.Reliability = (AnalyticItemSettingsReliability)source.Reliability;
            target.RuleType = (AnalyticRuleType)source.RuleType;
            target.SettingsMolecule = SettingsMoleculeAssembler.
                FromDtoToDomainObject(source.SettingsMolecule);
            return target;
        }
    }
}

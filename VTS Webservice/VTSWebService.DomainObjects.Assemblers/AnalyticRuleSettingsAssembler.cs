using System;
using System.Linq;
using VTS.AnalysisCore.Common;
using VTSWebService.DataContracts;
using AnalyticRuleSettingsEntity = VTSWebService.DataAccess.AnalyticRuleSettings;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class AnalyticRuleSettingsAssembler
    {
        public static AnalyticRuleSettingsEntity FromDtoToEntity(AnalyticRuleSettingsDto source)
        {
            AnalyticRuleSettingsEntity target = new AnalyticRuleSettingsEntity();
            target.Id = source.Id;
            target.EngineFamilyType = source.EngineFamilyType;
            target.EngineType = source.EngineType;
            target.Reliability = source.Reliability;
            target.RuleType = source.RuleType;
            target.SettingsMolecule.Add(SettingsMoleculeAssembler.
                FromDtoToEntity(source.SettingsMolecule));
            return target;
        }

        public static AnalyticRuleSettingsDto FromEntityToDto(AnalyticRuleSettingsEntity source)
        {
            AnalyticRuleSettingsDto target = new AnalyticRuleSettingsDto();
            target.Id = source.Id;
            target.EngineFamilyType = source.EngineFamilyType;
            target.EngineType = source.EngineType;
            target.Reliability = source.Reliability;
            target.RuleType = source.RuleType;
            target.SettingsMolecule = SettingsMoleculeAssembler.FromEntityToDto(source.SettingsMolecule.First());
            return target;
        }

        public static AnalyticRuleSettingsEntity FromDomainObjectToEntity(AnalyticRuleSettings source)
        {
            AnalyticRuleSettingsEntity target = new AnalyticRuleSettingsEntity();
            target.Id = source.Id;
            target.EngineFamilyType = source.EngineFamilyType == null ? new int?() : (int)source.EngineFamilyType;
            target.EngineType = source.EngineType == null ? new int?() : (int)source.EngineType;
            target.Reliability = (int)source.Reliability;
            target.RuleType = (int)source.RuleType;
            target.SettingsMolecule.Add(SettingsMoleculeAssembler.FromDomainObjectToEntity(source.SettingsMolecule));
            return target;
        }

        public static void CopyEntityProperties(
            AnalyticRuleSettingsEntity source,
            AnalyticRuleSettingsEntity target)
        {
            target.Id = source.Id;
            target.RuleType = source.RuleType;
            target.EngineFamilyType = source.EngineFamilyType;
            target.EngineType = source.EngineType;
            target.Reliability = source.Reliability;
            SettingsMoleculeAssembler.CopyEntityProperties(
                source.SettingsMolecule.First(),
                target.SettingsMolecule.First());
        }
    }
}

using System;
using VTS.Shared;
using VTSWeb.AnalysisCore.Interfaces;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public class AnalyticRuleSettingsAssembler
    {
        public static AnalyticRuleSettingsDto FromDomainObjectToDto(AnalyticRuleSettings source)
        {
            AnalyticRuleSettingsDto target = new AnalyticRuleSettingsDto();
            target.Id = source.Id;
            target.EngineFamilyType = source.EngineFamilyType == null ? new int?(): (int) source.EngineFamilyType;
            target.EngineType = source.EngineType == null ? new int?() : (int) source.EngineType;
            target.Reliability = (int) source.Reliability;
            target.RuleType = (int) source.RuleType;
            target.SettingsMolecule = SettingsMoleculeAssembler.
                FromDomainObjectToDto(source.SettingsMolecule);
            return target;
        }

        public static AnalyticRuleSettings FromDtoToDomainObject(AnalyticRuleSettingsDto source)
        {
            AnalyticRuleSettings target = new AnalyticRuleSettings(
                (AnalyticRuleType)source.RuleType, 
                (AnalyticItemSettingsReliability)source.Reliability);
            target.Id = source.Id;
            target.EngineFamilyType = source.EngineFamilyType == null ? new Nullable<EngineFamilyType>() : (EngineFamilyType) source.EngineFamilyType;
            target.EngineType = source.EngineType == null ? new Nullable<EngineType>() : (EngineType) source.EngineType;
            target.SettingsMolecule = SettingsMoleculeAssembler.FromDtoToDomainObject(source.SettingsMolecule);
            return target;
        }
    }
}

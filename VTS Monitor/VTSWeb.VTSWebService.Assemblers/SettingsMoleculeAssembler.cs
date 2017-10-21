using System;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public static class SettingsMoleculeAssembler
    {
        public static SettingsMoleculeDto FromDomainObjectToDto(SettingsMolecule source)
        {
            SettingsMoleculeDto target = new SettingsMoleculeDto();
            target.Id = source.Id;
            target.AnalyticRuleSettingsId = source.AnalyticRuleSettingsId;
            target.OverrideAcceptable = source.OverrideAcceptable;
            target.OverrideOptimal = source.OverrideOptimal;
            target.StatisticalAtom = SettingsAtomAssembler.FromDomainObjectToDto(source.StatisticalAtom);
            target.PredefinedAtom = SettingsAtomAssembler.FromDomainObjectToDto(source.PredefinedAtom);
            return target;
        }

        public static SettingsMolecule FromDtoToDomainObject(SettingsMoleculeDto source)
        {
            SettingsMolecule target = new SettingsMolecule();
            target.Id = source.Id;
            target.AnalyticRuleSettingsId = source.AnalyticRuleSettingsId;
            target.OverrideAcceptable = source.OverrideAcceptable;
            target.OverrideOptimal = source.OverrideOptimal;
            target.StatisticalAtom = SettingsAtomAssembler.FromDtoToDomainObject(source.StatisticalAtom);
            target.PredefinedAtom = SettingsAtomAssembler.FromDtoToDomainObject(source.PredefinedAtom);
            return target;
        }
    }
}

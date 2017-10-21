using System;
using System.Linq;
using VTS.AnalysisCore.Common;
using VTSWebService.AnalysisCore.Enums;
using VTSWebService.DataContracts;
using SettingsMoleculeEntity = VTSWebService.DataAccess.SettingsMolecule;
using SettingsAtomEntity = VTSWebService.DataAccess.SettingsAtom;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class SettingsMoleculeAssembler
    {
        public static SettingsMoleculeEntity FromDtoToEntity(SettingsMoleculeDto source)
        {
            SettingsMoleculeEntity target = new SettingsMoleculeEntity();
            target.Id = source.Id;
            target.OverrideAcceptable = source.OverrideAcceptable;
            target.OverrideOptimal = source.OverrideOptimal;
            target.SettingsAtom.Add(SettingsAtomAssembler.FromDtoToEntity(source.PredefinedAtom));
            target.SettingsAtom.Add(SettingsAtomAssembler.FromDtoToEntity(source.StatisticalAtom));
            target.AnalyticRuleSettingsEntityId = source.AnalyticRuleSettingsId;
            return target;
        }

        public static SettingsMoleculeDto FromEntityToDto(SettingsMoleculeEntity source)
        {
            SettingsMoleculeDto target = new SettingsMoleculeDto();
            target.Id = source.Id;
            target.OverrideAcceptable = source.OverrideAcceptable;
            target.OverrideOptimal = source.OverrideOptimal;
            target.PredefinedAtom = SettingsAtomAssembler.FromEntityToDto(source.SettingsAtom.First(a => a.Type == (int)SettingsAtomType.Predefined));
            target.StatisticalAtom = SettingsAtomAssembler.FromEntityToDto(source.SettingsAtom.First(a => a.Type == (int)SettingsAtomType.Statistical));
            target.AnalyticRuleSettingsId = source.AnalyticRuleSettingsEntityId;
            return target;
        }

        public static void CopyEntityProperties(
            SettingsMoleculeEntity source, SettingsMoleculeEntity target)
        {
            target.Id = source.Id;
            target.AnalyticRuleSettingsEntityId =
                source.AnalyticRuleSettingsEntityId;
            target.OverrideAcceptable = source.OverrideAcceptable;
            target.OverrideOptimal = source.OverrideOptimal;
            foreach (SettingsAtomEntity atom in source.SettingsAtom)
            {
                SettingsAtomAssembler.CopyEntityProperties(atom,
                    target.SettingsAtom.First(a => a.Type == atom.Type));
            }
        }

        public static SettingsMoleculeEntity FromDomainObjectToEntity(SettingsMolecule source)
        {
            SettingsMoleculeEntity target = new SettingsMoleculeEntity();
            target.Id = source.Id;
            target.OverrideAcceptable = source.OverrideAcceptable;
            target.OverrideOptimal = source.OverrideOptimal;
            target.SettingsAtom.Add(SettingsAtomAssembler.FromDomainObjectToEntity(source.PredefinedAtom));
            target.SettingsAtom.Add(SettingsAtomAssembler.FromDomainObjectToEntity(source.StatisticalAtom));
            target.AnalyticRuleSettingsEntityId = source.AnalyticRuleSettingsId;
            return target;
        }
    }
}

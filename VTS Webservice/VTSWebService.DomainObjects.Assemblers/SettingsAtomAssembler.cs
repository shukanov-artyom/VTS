using System;
using VTS.AnalysisCore.Common;
using VTSWebService.DataContracts;
using SettingsAtomEntity = VTSWebService.DataAccess.SettingsAtom;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class SettingsAtomAssembler
    {
        public static SettingsAtomEntity FromDtoToEntity(SettingsAtomDto source)
        {
            SettingsAtomEntity target = new SettingsAtomEntity();
            target.Id = source.Id;
            target.MaxAcceptable = source.MaxAcceptable;
            target.MaxOptimal = source.MaxOptimal;
            target.MinAcceptable = source.MinAcceptable;
            target.MinOptimal = source.MinOptimal;
            target.Type = source.Type;
            target.SettingsMoleculeEntityId = source.MoleculeId;
            return target;
        }

        public static SettingsAtomDto FromEntityToDto(SettingsAtomEntity source)
        {
            SettingsAtomDto target = new SettingsAtomDto();
            target.Id = source.Id;
            target.MaxAcceptable = source.MaxAcceptable;
            target.MaxOptimal = source.MaxOptimal;
            target.MinAcceptable = source.MinAcceptable;
            target.MinOptimal = source.MinOptimal;
            target.Type = source.Type;
            target.MoleculeId = source.SettingsMoleculeEntityId;
            return target;
        }

        public static void CopyEntityProperties(SettingsAtomEntity source,
            SettingsAtomEntity target)
        {
            target.Id = source.Id;
            target.MaxAcceptable = source.MaxAcceptable;
            target.MaxOptimal = source.MaxOptimal;
            target.MinAcceptable = source.MinAcceptable;
            target.MinOptimal = source.MinOptimal;
            target.Type = source.Type;
            target.SettingsMoleculeEntityId = source.SettingsMoleculeEntityId;
        }

        public static SettingsAtomEntity FromDomainObjectToEntity(SettingsAtom source)
        {
            SettingsAtomEntity target = new SettingsAtomEntity();
            target.Id = source.Id;
            target.MaxAcceptable = source.MaxAcceptable;
            target.MaxOptimal = source.MaxOptimal;
            target.MinAcceptable = source.MinAcceptable;
            target.MinOptimal = source.MinOptimal;
            target.Type = (int)source.Type;
            target.SettingsMoleculeEntityId = source.SettingsMoleculeId;
            return target;
        }
    }
}

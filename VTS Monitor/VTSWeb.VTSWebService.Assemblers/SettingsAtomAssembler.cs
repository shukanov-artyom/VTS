using System;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public static class SettingsAtomAssembler
    {
        public static SettingsAtomDto FromDomainObjectToDto(SettingsAtom source)
        {
            SettingsAtomDto target = new SettingsAtomDto();
            target.Id = source.Id;
            target.MoleculeId = source.SettingsMoleculeId;
            target.MaxAcceptable = source.MaxAcceptable;
            target.MaxOptimal = source.MaxOptimal;
            target.MinAcceptable = source.MinAcceptable;
            target.MinOptimal = source.MinOptimal;
            target.Type = (int)source.Type;
            return target;
        }

        public static SettingsAtom FromDtoToDomainObject(SettingsAtomDto source)
        {
            SettingsAtom target = new SettingsAtom();
            target.Id = source.Id;
            target.SettingsMoleculeId = source.MoleculeId;
            target.MaxAcceptable = source.MaxAcceptable;
            target.MaxOptimal = source.MaxOptimal;
            target.MinAcceptable = source.MinAcceptable;
            target.MinOptimal = source.MinOptimal;
            target.Type = (Common.Enums.SettingsAtomType)source.Type;
            return target;
        }
    }
}

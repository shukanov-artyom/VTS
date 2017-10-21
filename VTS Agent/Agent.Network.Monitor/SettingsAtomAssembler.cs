using System;
using Agent.Network.Monitor.VtsWebService;
using VTS.Agent.BusinessObjects;

namespace Agent.Network.Monitor
{
    public static class SettingsAtomAssembler
    {
        public static SettingsAtom FromDtoToDomainObject(SettingsAtomDto source)
        {
            SettingsAtom target = new SettingsAtom();
            target.Id = source.Id;
            target.MaxAcceptable = source.MaxAcceptable;
            target.MaxOptimal = source.MaxOptimal;
            target.MinAcceptable = source.MinAcceptable;
            target.MinOptimal = source.MinOptimal;
            target.Type = (SettingsAtomType)source.Type;
            target.SettingsMoleculeId = source.MoleculeId;
            return target;
        }
    }
}

using System;
using Agent.Network.Monitor.VtsWebService;
using VTS.Agent.BusinessObjects;

namespace Agent.Network.Monitor
{
    public static class SettingsMoleculeAssembler
    {
        public static SettingsMolecule FromDtoToDomainObject(SettingsMoleculeDto source)
        {
            SettingsMolecule target = new SettingsMolecule();
            target.Id = source.Id;
            target.OverrideAcceptable = source.OverrideAcceptable;
            target.OverrideOptimal = source.OverrideOptimal;
            target.PredefinedAtom = SettingsAtomAssembler.FromDtoToDomainObject(source.PredefinedAtom);
            target.StatisticalAtom = SettingsAtomAssembler.FromDtoToDomainObject(source.StatisticalAtom);
            target.AnalyticRuleSettingsId = source.AnalyticRuleSettingsId;
            return target;
        }
    }
}

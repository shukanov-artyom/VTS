using System;
using VTS.Shared.DomainObjects;

namespace VTS.Agent.BusinessObjects
{
    public class SettingsAtom : DomainObject
    {
        public SettingsAtom()
        {

        }

        public SettingsAtom(SettingsAtomType type)
        {
            Type = type;
        }

        public SettingsAtomType Type { get; set; }

        public double MinOptimal { get; set; }

        public double MaxOptimal { get; set; }

        public double MinAcceptable { get; set; }

        public double MaxAcceptable { get; set; }

        public long SettingsMoleculeId { get; set; }

        public void PickFrom(SettingsAtom source)
        {
            MinOptimal = source.MinOptimal;
            MaxOptimal = source.MaxOptimal;
            MinAcceptable = source.MinAcceptable;
            MaxAcceptable = source.MaxAcceptable;
        }

        public void Copy(SettingsAtom source)
        {
            Id = source.Id;
            Type = source.Type;
            PickFrom(source);
        }
    }
}

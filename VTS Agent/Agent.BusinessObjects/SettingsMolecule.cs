﻿using System;
using VTS.Shared.DomainObjects;

namespace VTS.Agent.BusinessObjects
{
    public class SettingsMolecule : DomainObject
    {
        private SettingsAtom statisticalAtom =
            new SettingsAtom(SettingsAtomType.Statistical);
        private SettingsAtom predefinedAtom =
            new SettingsAtom(SettingsAtomType.Predefined);

        public SettingsAtom StatisticalAtom
        {
            get
            {
                return statisticalAtom;
            }
            set
            {
                if (value.Type != SettingsAtomType.Statistical)
                {
                    throw new Exception();
                }
                statisticalAtom = value;
            }
        }

        public SettingsAtom PredefinedAtom
        {
            get
            {
                return predefinedAtom;
            }
            set
            {
                if (value.Type != SettingsAtomType.Predefined)
                {
                    throw new Exception();
                }
                predefinedAtom = value;
            }
        }

        public bool OverrideOptimal
        {
            get;
            set;
        }

        public bool OverrideAcceptable
        {
            get;
            set;
        }

        public long AnalyticRuleSettingsId { get; set; }

        public SettingsAtom GetPriorityAtom()
        {
            SettingsAtom result = new SettingsAtom();
            result.MinOptimal = OverrideOptimal ?
                PredefinedAtom.MinOptimal : StatisticalAtom.MinOptimal;
            result.MaxOptimal = OverrideOptimal ?
                PredefinedAtom.MaxOptimal : StatisticalAtom.MaxOptimal;
            result.MinAcceptable = OverrideAcceptable ?
                PredefinedAtom.MinAcceptable : StatisticalAtom.MinAcceptable;
            result.MaxAcceptable = OverrideAcceptable ?
                PredefinedAtom.MaxAcceptable : StatisticalAtom.MaxAcceptable;
            return result;
        }
    }
}

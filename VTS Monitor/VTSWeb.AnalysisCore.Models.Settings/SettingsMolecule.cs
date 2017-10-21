using System;
using VTS.Shared.DomainObjects;
using VTSWeb.Common.Enums;
using VTSWeb.DomainObjects;

namespace VTSWeb.AnalysisCore.Models.Settings
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

        public long AnalyticRuleSettingsId
        {
            get;
            set;
        }

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

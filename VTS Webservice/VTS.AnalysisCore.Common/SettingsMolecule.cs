using System;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTS.AnalysisCore.Common
{
    public class SettingsMolecule : DomainObject
    {
        private readonly SettingsAtom statisticalAtom = 
            new SettingsAtom(SettingsAtomType.Statistical);
        private readonly SettingsAtom predefinedAtom = 
            new SettingsAtom(SettingsAtomType.Predefined);

        public SettingsAtom StatisticalAtom
        {
            get
            {
                return statisticalAtom;
            }
        }

        public SettingsAtom PredefinedAtom
        {
            get
            {
                return predefinedAtom;
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

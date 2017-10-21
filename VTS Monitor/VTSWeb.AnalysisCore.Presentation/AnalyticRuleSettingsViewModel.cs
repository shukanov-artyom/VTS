using System;
using VTSWeb.AnalysisCore.Interfaces;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.Presentation
{
    public class AnalyticRuleSettingsViewModel : ViewModelBase
    {
        private AnalyticRuleSettings model;
        private AnalyticRuleTypeViewModel type;
        private EngineFamilyTypeViewModel engineFamilyType;
        private EngineTypeViewModel engineType;

        public AnalyticRuleSettingsViewModel(AnalyticRuleSettings model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            type = new AnalyticRuleTypeViewModel(model.RuleType);
            if (model.EngineFamilyType != null)
            {
                engineFamilyType = 
                    new EngineFamilyTypeViewModel(model.EngineFamilyType.Value);
            }
            if (model.EngineType != null)
            {
                engineType = new EngineTypeViewModel(model.EngineType.Value);
            }
        }

        public string TypeName
        {
            get
            {
                return Type.DisplayName;
            }
        }

        public string EngineFamilyTypeName
        {
            get
            {
                if (engineFamilyType == null)
                {
                    return CodeBehindStringResolver.
                        Resolve("AnalyticRuleTypeStubAny");
                }
                return engineFamilyType.DisplayName;
            }
        }

        public string EngineTypeName
        {
            get
            {
                if (engineType == null)
                {
                    return CodeBehindStringResolver.
                        Resolve("AnalyticRuleTypeStubAny");
                }
                return engineType.DisplayName;
            }
        }

        public AnalyticItemSettingsReliability Reliability
        {
            get
            {
                return model.Reliability;
            }
        }

        public AnalyticRuleTypeViewModel Type
        {
            get
            {
                return type;
            }
        }

        public AnalyticRuleSettings Model
        {
            get
            {
                return model;
            }
        }

        public double MinOptimalStat
        {
            get
            {
                return model.SettingsMolecule.StatisticalAtom.MinOptimal;
            }
        }

        public double MaxOptimalStat
        {
            get
            {
                return model.SettingsMolecule.StatisticalAtom.MaxOptimal;
            }
        }

        public double MinOptimalPred
        {
            get
            {
                return model.SettingsMolecule.PredefinedAtom.MinOptimal;
            }
            set
            {
                model.SettingsMolecule.PredefinedAtom.MinOptimal = value;
                OnPropertyChanged("MinOptimalPred");
            }
        }

        public double MaxOptimalPred
        {
            get
            {
                return model.SettingsMolecule.PredefinedAtom.MaxOptimal;
            }
            set
            {
                model.SettingsMolecule.PredefinedAtom.MaxOptimal = value;
                OnPropertyChanged("MaxOptimalPred");
            }
        }

        public double MinAcceptableStat
        {
            get
            {
                return model.SettingsMolecule.StatisticalAtom.MinAcceptable;
            }
        }

        public double MaxAcceptableStat
        {
            get
            {
                return model.SettingsMolecule.StatisticalAtom.MaxAcceptable;
            }
        }

        public double MinAcceptablePred
        {
            get
            {
                return model.SettingsMolecule.PredefinedAtom.MinAcceptable;
            }
            set
            {
                model.SettingsMolecule.PredefinedAtom.MinAcceptable = value;
            }
        }

        public double MaxAcceptablePred
        {
            get
            {
                return model.SettingsMolecule.PredefinedAtom.MaxAcceptable;
            }
            set
            {
                model.SettingsMolecule.PredefinedAtom.MaxAcceptable = value;
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("TypeName");
            OnPropertyChanged("EngineFamilyTypeName");
            OnPropertyChanged("EngineTypeName");
            base.ChangeLanguage();
        }
    }
}

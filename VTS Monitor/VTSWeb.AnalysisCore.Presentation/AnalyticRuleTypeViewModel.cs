using System;
using VTS.Shared;

using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.Presentation
{
    public class AnalyticRuleTypeViewModel : ViewModelBase
    {
        private const string NameResolveFormat = "AnalyticItemDisplayName{0}";
        private AnalyticRuleType model;

        public AnalyticRuleTypeViewModel(AnalyticRuleType model)
        {
            this.model = model;
        }

        public string DisplayName
        {
            get
            {
                return CodeBehindStringResolver.Resolve(String.Format(
                    NameResolveFormat, model.ToString()));
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("DisplayName");
            base.ChangeLanguage();
        }

        public AnalyticRuleType Model
        {
            get
            {
                return model;
            }
        }
    }
}

using System;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.Presentation
{
    public class ViewModelStubAny : ViewModelBase
    {
        private string key = "AnalyticRuleTypeStubAny";

        public string DisplayName
        {
            get
            {
                return CodeBehindStringResolver.Resolve(key);
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("DisplayName");
            base.ChangeLanguage();
        }
    }
}

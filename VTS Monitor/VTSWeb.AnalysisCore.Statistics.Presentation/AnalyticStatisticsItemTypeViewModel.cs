using System;
using VTS.Shared;

using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.Statistics.Presentation
{
    public class AnalyticStatisticsItemTypeViewModel : ViewModelBase
    {
        private const string KeyFormat = "AnalyticItemDisplayName{0}";

        private AnalyticRuleType model;

        public AnalyticStatisticsItemTypeViewModel(
            AnalyticRuleType model)
        {
            this.model = model;
        }

        public string DisplayName
        {
            get
            {
                string key = String.Format(KeyFormat, model.ToString());
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

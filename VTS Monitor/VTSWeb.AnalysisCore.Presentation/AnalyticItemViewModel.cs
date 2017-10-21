using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTSWeb.AnalysisCore.Interfaces;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.Presentation
{
    public class AnalyticItemViewModel : ViewModelBase
    {
        private const string Format = "AnalyticItemDisplayName{0}{1}";

        private IAnalyticItem analyticItem;
        private ObservableCollection<AnalyticItemViewModel> subitems = 
            new ObservableCollection<AnalyticItemViewModel>();
        private ObservableCollection<KeyValuePair<DateTime, double>> history = 
            new ObservableCollection<KeyValuePair<DateTime,double>>();

        public AnalyticItemViewModel(IAnalyticItem analyticItem)
        {
            if (analyticItem == null)
            {
                throw new ArgumentNullException("analyticItem");
            }
            this.analyticItem = analyticItem;
            IAnalyticModel analyticModel = analyticItem as IAnalyticModel;
            if (analyticModel != null)
            {
                foreach (IAnalyticModel submodel in analyticModel.Models)
                {
                    Subitems.Add(new AnalyticItemViewModel(submodel));
                }
                foreach (IAnalyticRule subrule in analyticModel.Rules)
                {
                    Subitems.Add(new AnalyticItemViewModel(subrule));
                }
            }
            InitializeHistory(analyticItem.MarksHistory);
        }

        public ObservableCollection<KeyValuePair<DateTime, double>> History
        {
            get
            {
                return history;
            }
        }

        public ObservableCollection<AnalyticItemViewModel> Subitems
        {
            get
            {
                return subitems;
            }
        }

        public double Mark
        {
            get
            {
                if (analyticItem.MarksHistory.Count == 0)
                {
                    return 0;
                }
                DateTime last = analyticItem.MarksHistory.Max(m => m.Key);
                return analyticItem.MarksHistory.LastOrDefault(
                    m => m.Key == last).Value;
            }
        }

        public string DisplayName
        {
            get
            {
                if (analyticItem != null)
                {
                    return CodeBehindStringResolver.Resolve(
                        String.Format(Format, analyticItem.GetType().Name, 
                        analyticItem.AdditionalInfo));
                }
                throw new Exception();
            }
        }

        public IAnalyticItem Model
        {
            get
            {
                return analyticItem;
            }
        }

        private void InitializeHistory(IDictionary<DateTime, double> dict)
        {
            foreach (KeyValuePair<DateTime, double> pair in dict)
            {
                history.Add(new KeyValuePair<DateTime, double>(
                    pair.Key, Math.Round(pair.Value, 1)));
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("DisplayName");
            base.ChangeLanguage();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Agent.Common.Presentation;
using Agent.Evaluation;
using Agent.Localization;

namespace Agent.Workspace.ViewModels.Evaluation
{
    public class RuleEvaluationChronologyViewModel : ViewModelBase
    {
        private readonly RuleEvaluationChronology model;
        private readonly ObservableCollection<KeyValuePair<DateTime, double>> dataForGraph = 
            new ObservableCollection<KeyValuePair<DateTime, double>>();

        private readonly double currentMark;

        public RuleEvaluationChronologyViewModel(RuleEvaluationChronology model)
        {
            this.model = model;
            currentMark = model.GetMarkForRevision(DateTime.Now).Value;
            foreach (KeyValuePair<DateTime, EvaluationMark> pair in model.MarkChronology)
            {
                dataForGraph.Add(new KeyValuePair<DateTime, double>(pair.Key, pair.Value.Value));
            }
        }

        public string Reliability
        {
            get
            {
                string result = CodeBehindStringResolver.Resolve(
                    String.Format("ReliabilityDisplayName{0}", 
                    model.RuleSettings.Reliability));
                return result;
            }
        }

        public double CurrentMark
        {
            get
            {
                return currentMark;
            }
        }

        public double MinYValue
        {
            get
            {
                return 0;
            }
        }

        public double MaxYValue
        {
            get
            {
                return 10.5;
            }
        }

        public DateTime FirstDatasetDate
        {
            get
            {
                return DataForGraph.Min(p => p.Key);
            }
        }

        public DateTime LastDatasetDate
        {
            get
            {
                return DataForGraph.Max(p => p.Key);
            }
        }

        public string LastDatasetDateString
        {
            get
            {
                return LastDatasetDate.ToLongDateString();
            }
        }

        public ObservableCollection<KeyValuePair<DateTime, double>> DataForGraph
        {
            get
            {
                return dataForGraph;
            }
        }

        public string Summary
        {
            get
            {
                return CodeBehindStringResolver.Resolve(
                    String.Format("AnalyticItemDisplayName{0}", 
                    model.RuleSettings.RuleType));
            }
        }

        protected override void ChangeLanguage()
        {
            base.ChangeLanguage();
            OnPropertyChanged("LastDatasetDateString");
            OnPropertyChanged("Reliability");
        }
    }
}

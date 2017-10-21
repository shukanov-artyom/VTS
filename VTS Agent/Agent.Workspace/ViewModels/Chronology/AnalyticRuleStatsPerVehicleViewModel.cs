using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using Agent.Common.Instance;
using Agent.Common.Presentation;
using Agent.Localization;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.ViewModels.Chronology
{
    public class AnalyticRuleStatsPerVehicleViewModel : ViewModelBase, IDisposable
    {
        private readonly AnalyticRuleType type;
        private readonly string vin;
        private readonly ObservableCollection<AnalyticStatisticsValueViewModel> values = 
            new ObservableCollection<AnalyticStatisticsValueViewModel>();
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private const double Multiplier = 3.0;

        private bool isWaitingMode;

        public AnalyticRuleStatsPerVehicleViewModel(AnalyticRuleType type,
            string vin)
        {
            this.type = type;
            this.vin = vin;
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = false;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.DoWork += RetrieveStatisticsAsync;
        }

        public bool IsWaitingMode
        {
            get
            {
                return isWaitingMode;
            }
            set
            {
                isWaitingMode = value;
                OnPropertyChanged("IsWaitingMode");
            }
        }

        public string Summary
        {
            get
            {
                return CodeBehindStringResolver.Resolve(
                    String.Format("AnalyticItemDisplayName{0}", type)); 
            }
        }

        public ObservableCollection<AnalyticStatisticsValueViewModel> DataForGraph
        {
            get
            {
                if (values.Count == 0)
                {
                    if (worker.IsBusy)
                    {
                        worker.CancelAsync();
                    }
                    else
                    {
                        worker.RunWorkerAsync();
                    }
                }
                return values;
            }
        }

        public double MinYValue
        {
            get
            {
                if (DataForGraph.Count == 0)
                {
                    return 0;
                }
                double max = DataForGraph.Max(v => v.Value);
                double min = DataForGraph.Min(v => v.Value);
                return min - Math.Abs((max - min) / Multiplier);
            }
        }

        public double MaxYValue
        {
            get
            {
                if (DataForGraph.Count == 0)
                {
                    return 0;
                }
                double max = DataForGraph.Max(v => v.Value);
                double min = DataForGraph.Min(v => v.Value);
                return max + Math.Abs((max - min) / Multiplier);
            }
        }

        protected override void ChangeLanguage()
        {
            base.ChangeLanguage();
            OnPropertyChanged("Summary");
        }

        private void RetrieveStatisticsAsync(object sender, DoWorkEventArgs e)
        {
            SetWaitingModeFromAnotherThread(true);
            List<AnalyticStatisticsValue> valuesGot =
                StatisticsCache.GetTypedForVehicle(type, vin);
            List<AnalyticStatisticsValueViewModel> result =
                new List<AnalyticStatisticsValueViewModel>();
            foreach (AnalyticStatisticsValue statisticsValue in valuesGot)
            {
                result.Add(new AnalyticStatisticsValueViewModel(statisticsValue));
            }
            e.Result = result;
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                return;
            }
            List<AnalyticStatisticsValueViewModel> result = e.Result
                as List<AnalyticStatisticsValueViewModel>;
            foreach (AnalyticStatisticsValueViewModel valueViewModel in result)
            {
                values.Add(valueViewModel);
            }
            SetWaitingModeFromAnotherThread(false);
        }

        private void SetWaitingModeFromAnotherThread(bool wait)
        {
            Dispatcher.CurrentDispatcher.Invoke(new ThreadStart(() => IsWaitingMode = wait));
            Dispatcher.CurrentDispatcher.Invoke(new ThreadStart(() => OnPropertyChanged("MinYValue")));
            Dispatcher.CurrentDispatcher.Invoke(new ThreadStart(() => OnPropertyChanged("MaxYValue")));
        }

        public void Dispose()
        {
            worker.Dispose();
        }
    }
}

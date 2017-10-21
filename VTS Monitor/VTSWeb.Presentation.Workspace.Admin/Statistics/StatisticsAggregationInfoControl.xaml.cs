using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.Presentation.Workspace.Admin.Statistics
{
    public partial class StatisticsAggregationInfoControl : UserControl
    {
        private readonly List<AnalyticRuleSettings> result = 
            new List<AnalyticRuleSettings>();

        public StatisticsAggregationInfoControl()
        {
            InitializeComponent();
        }

        public event EventHandler Started;
        public event EventHandler Finished;

        public List<AnalyticRuleSettings> Result
        {
            get
            {
                return result;
            }
        }

        private void StartStatisticsAggregation(
            object sender, RoutedEventArgs e)
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.AggregateStatisticsCompleted += ServiceOnAggregateStatisticsCompleted;
            service.AggregateStatisticsAsync();
        }

        private void ServiceOnAggregateStatisticsCompleted(object sender, 
            AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ErrorCallback(e.Error, e.Error.Message);
            }
            else
            {
                RetrieveGeneratedSettings();
            }
        }

        private void RetrieveGeneratedSettings()
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.GetAnalyticRuleSettingsCompleted += ServiceOnGetAnalyticRuleSettingsCompleted;
            service.GetAnalyticRuleSettingsAsync();
        }

        private void ServiceOnGetAnalyticRuleSettingsCompleted(object sender, 
            GetAnalyticRuleSettingsCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ErrorCallback(e.Error, e.Error.Message);
            }
            else
            {
                result.Clear();
                foreach (AnalyticRuleSettingsDto dto in e.Result)
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromDtoToDomainObject(dto));
                }
                SuccessCallback();
            }
        }

        private void ErrorCallback(Exception e, string msg)
        {
            ErrorWindow w = new ErrorWindow(e, msg);
            w.Closed += DialogWindowStatus.OnDialogClosed;
            w.Show();
        }

        private void SuccessCallback()
        {
            buttonTriggerAggregation.IsEnabled = true;
            if (Finished != null)
            {
                Finished.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

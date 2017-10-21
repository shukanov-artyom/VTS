using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.AnalysisCore.Statistics;
using VTSWeb.AnalysisCore.Statistics.Generation;
using VTSWeb.AnalysisCore.Statistics.Generation.PagedRetrievers;

namespace VTSWeb.Presentation.Workspace.Admin.Statistics
{
    public partial class StatisticsGenerationInfoControl : UserControl
    {
        public event EventHandler StatisticsGenerationStarted;
        public event EventHandler StatisticsGenerated;

        private StatisticsGenerationEngine engine;

        public StatisticsGenerationInfoControl()
        {
            InitializeComponent();
        }

        public AnalyticStatistics Statistics
        {
            get
            {
                return engine.Result;
            }
        }

        private void StartStatisticsGeneration(
            object sender, RoutedEventArgs e)
        {
            buttonTriggerGeneration.IsEnabled = false;
            engine = new StatisticsGenerationEngine(
                StatisticsGenerationUpdate, 
                StatisticsGenerationComplete, 
                ErrorCallback,
                new AllDatasetsPagedRetriever());
            engine.StartGeneration();
            if (StatisticsGenerationStarted != null)
            {
                StatisticsGenerationStarted.Invoke(this, EventArgs.Empty);
            }
        }

        private void StatisticsGenerationUpdate(int percentage)
        {
            string percentageString = percentage.ToString(
                CultureInfo.InvariantCulture);
            textBlockPercentage.Text = 
                String.Format("{0} %", percentageString);
            progressBarStatisticsGeneration.Value = percentage;
        }

        private void StatisticsGenerationComplete()
        {
            buttonTriggerGeneration.IsEnabled = true;
            AnalyticStatistics s = engine.Result;
            if (StatisticsGenerated != null)
            {
                StatisticsGenerated.Invoke(this, EventArgs.Empty);
            }
        }

        private void ErrorCallback(Exception e, string msg)
        {
            buttonTriggerGeneration.IsEnabled = true;
        }
    }
}

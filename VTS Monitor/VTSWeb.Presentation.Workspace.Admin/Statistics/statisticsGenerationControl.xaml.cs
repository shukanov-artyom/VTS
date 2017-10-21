using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.AnalysisCore.Statistics;
using VTSWeb.AnalysisCore.Statistics.Presentation;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.Presentation.Workspace.Admin.Statistics
{
    public partial class StatisticsGenerationControl : NavigatablePage
    {
        private AnalyticStatisticsItemViewModel selectedItem;

        public StatisticsGenerationControl()
        {
            InitializeComponent();
            InitializeHeader("StatisticsGeneration");
            controlStatisticsGeneration.StatisticsGenerationStarted +=
                OnStatisticsGenerationStarted;
            controlStatisticsGeneration.StatisticsGenerated +=
                OnStatisticsGenerated;
            buttonShowItemDetails.IsEnabled = false;
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void OnStatisticsGenerationStarted(object sender, EventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            dataGridStatistics.Height = ApplicationSizeKeeper.Height - 230;
            buttonShowItemDetails.IsEnabled = false;
        }

        private void OnStatisticsGenerated(object sender, EventArgs e)
        {
            AnalyticStatistics statistics =
                controlStatisticsGeneration.Statistics;
            ObservableCollection<AnalyticStatisticsItemViewModel> source = 
                new ObservableCollection<AnalyticStatisticsItemViewModel>();
            foreach (AnalyticStatisticsItem item in statistics.Items)
            {
                source.Add(new AnalyticStatisticsItemViewModel(item));
            }
            dataGridStatistics.ItemsSource = source;
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void ShowStatisticsItemDetailsClicked(
            object sender, RoutedEventArgs e)
        {
            AnalyticStatisticsItemViewModel item = selectedItem;
            AnalyticStatisticsItemDetailsWindow window = 
                new AnalyticStatisticsItemDetailsWindow(item);
            window.Closed += DialogWindowStatus.OnDialogClosed;
            window.Show();
        }

        private void OnGridSelectionChanged(object sender, 
            SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            AnalyticStatisticsItemViewModel selected = e.AddedItems[0]
                as AnalyticStatisticsItemViewModel;
            if (selected == null)
            {
                throw new Exception("Unexpected item type!");
            }
            selectedItem = selected;
            buttonShowItemDetails.IsEnabled = true;
        }

        private void PersistAllClicked(object sender, RoutedEventArgs e)
        {
            buttonShowItemDetails.IsEnabled = false;
            controlStatisticsGeneration.
                buttonTriggerGeneration.IsEnabled = false;
            progressBar.Visibility = Visibility.Visible;
            AnalyticStatistics result = controlStatisticsGeneration.Statistics;
            AnalyticStatisticsDto dto = AnalyticStatisticsAssembler.FromObjectToDto(result);
            VtsWebServiceClient client = new VtsWebServiceClient();
            client.SubmitAnalyticStatisticsCompleted += ClientOnSubmitAnalyticStatisticsCompleted;
            client.SubmitAnalyticStatisticsAsync(dto);
        }

        private void ClientOnSubmitAnalyticStatisticsCompleted(object sender,
            AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(e.Error, e.Error.Message);
                return;
            }
            progressBar.Visibility = Visibility.Collapsed;
            controlStatisticsGeneration.
                buttonTriggerGeneration.IsEnabled = true;
        }

        private void OnError(Exception e, string msg)
        {
            progressBar.Visibility = Visibility.Collapsed;
            ErrorWindow errorWindow = new ErrorWindow(e, msg);
            errorWindow.Closed += DialogWindowStatus.OnDialogClosed;
            errorWindow.Show();
        }
    }
}

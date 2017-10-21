using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Models.Settings.Persistency;
using VTSWeb.AnalysisCore.Models.Settings.Presentation;
using VTSWeb.AnalysisCore.Presentation;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;

namespace VTSWeb.Presentation.Workspace.Admin.Statistics
{
    public partial class StatisticsAggregationControl : NavigatablePage
    {
        private readonly ObservableCollection<AnalyticRuleSettingsViewModel> items =
            new ObservableCollection<AnalyticRuleSettingsViewModel>();

        private AnalyticRuleSettingsViewModel selectedItem;

        public StatisticsAggregationControl()
        {
            InitializeComponent();
            InitializeHeader("StatisticsAggregation");
            controlInfo.Finished += OnFinished;
            controlInfo.Started += OnStarted;
            ApplicationSizeKeeper.AppResized += OnAppResized;
            OnAppResized(null, null);
            progressBar.Visibility = Visibility.Collapsed;
            dataGridRulesSettings.ItemsSource = items;
            buttonShowItemDetails.IsEnabled = false;
        }

        private void OnAppResized(object sender, EventArgs eventArgs)
        {
            dataGridRulesSettings.Height = ApplicationSizeKeeper.Height - 200;
        }

        private void OnStarted(object sender, EventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
        }

        private void OnFinished(object sender, EventArgs e)
        {
            items.Clear();
            foreach (AnalyticRuleSettings s in controlInfo.Result)
            {
                items.Add(new AnalyticRuleSettingsViewModel(s));
            }
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void OnGridSelectionChanged(object sender, 
            SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            selectedItem = e.AddedItems[0] as AnalyticRuleSettingsViewModel;
            buttonShowItemDetails.IsEnabled = true;
        }

        private void ShowAgregatedItemDetailsClicked(
            object sender, RoutedEventArgs e)
        {
            AnalyticRuleSettingsPropertiesWindow window = 
                new AnalyticRuleSettingsPropertiesWindow(selectedItem);
            window.Closed += DialogWindowStatus.OnDialogClosed;
            window.Show();
        }

        /*private void PersistAllClicked(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;
            List<AnalyticRuleSettings> settingsToPersist = 
                new List<AnalyticRuleSettings>();
            foreach (AnalyticRuleSettingsViewModel viewModel in items)
            {
                settingsToPersist.Add(viewModel.Model);
            }
            AnalyticRuleSettingsPersistency persistency = 
                new AnalyticRuleSettingsPersistency(OnPersisted, OnError);
            persistency.PersistOrUpdateStatisticsAndOverrides(settingsToPersist);

        }*/

        /*private void OnError(Exception e, string msg)
        {
            ErrorWindow wnd = new ErrorWindow(e, msg);
            wnd.Closed += DialogWindowStatus.OnDialogClosed;
            wnd.Show();
        }

        private void OnPersisted()
        {
            progressBar.Visibility = Visibility.Collapsed;
        }*/
    }
}

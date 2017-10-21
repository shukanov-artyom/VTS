using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VTS.Shared;
using VTSWeb.AnalysisCore.Interfaces;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Models.Settings.Persistency;
using VTSWeb.AnalysisCore.Models.Settings.Presentation;
using VTSWeb.AnalysisCore.Presentation;

using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Workspace.Admin.AnalyticRulesSettings
{
    public partial class AnalyticRulesSettingsControl : NavigatablePage
    {
        private ObservableCollection<AnalyticRuleSettingsViewModel> collection = 
            new ObservableCollection<AnalyticRuleSettingsViewModel>();

        private AnalyticRuleSettingsViewModel selectedItem;

        public AnalyticRulesSettingsControl()
        {
            InitializeComponent();
            InitializeHeader("AnalyticRulesSettings");
            controlFiltering.CriteriaHasChanged += OnFiltersChanged;
            dataGridRulesSettings.ItemsSource = collection;
            ApplicationSizeKeeper.AppResized += OnResize;
            OnResize(this, EventArgs.Empty);
        }

        private void OnFiltersChanged(object sender, EventArgs e)
        {
            AnalyticRuleSettingsPersistency persistency = 
                new AnalyticRuleSettingsPersistency(Retrieved, OnError);
            if (controlFiltering.RuleType == null && 
                controlFiltering.Family == null && 
                controlFiltering.Engine == null)
            {
                persistency.FetchAll();
            }
            else if (controlFiltering.RuleType != null &&
                controlFiltering.Family != null &&
                controlFiltering.Engine != null)
            {
                AnalyticRuleType ruleType = controlFiltering.RuleType.Model;
                EngineFamilyType familyType = controlFiltering.Family.Model;
                EngineType engineType = controlFiltering.Engine.Model;
                persistency.FetchAllForEngine(ruleType, familyType, engineType);
            }
            else if (controlFiltering.RuleType != null &&
                controlFiltering.Family != null &&
                controlFiltering.Engine == null)
            {
                AnalyticRuleType ruleType = controlFiltering.RuleType.Model;
                EngineFamilyType familyType = controlFiltering.Family.Model;
                persistency.FetchAllForEngineFamily(ruleType, familyType);
            }
            else if (controlFiltering.RuleType != null &&
                controlFiltering.Family == null &&
                controlFiltering.Engine == null)
            {
                AnalyticRuleType ruleType = controlFiltering.RuleType.Model;
                persistency.FetchAllForRuleType(ruleType);
            }
            else if (controlFiltering.RuleType == null &&
                controlFiltering.Family != null &&
                controlFiltering.Engine != null)
            {
                EngineFamilyType familyType = controlFiltering.Family.Model;
                EngineType engineType = controlFiltering.Engine.Model;
                persistency.FetchAllRulesForEngine(familyType, engineType);
            }
            else if (controlFiltering.RuleType == null &&
            controlFiltering.Family != null &&
            controlFiltering.Engine == null)
            {
                EngineFamilyType familyType = controlFiltering.Family.Model;
                persistency.FetchAllRulesForEngineFamily(familyType);
            }
            else
            {
                collection.Clear();
            }
        }

        private void Retrieved(IList<AnalyticRuleSettings> settings)
        {
            collection.Clear();
            foreach (AnalyticRuleSettings setting in settings)
            {
                collection.Add(new AnalyticRuleSettingsViewModel(setting));
            }
            textBlockItemsCount.Text = collection.Count.ToString();
        }

        private void OnError(Exception e, string msg)
        {
            throw e;
        }

        private void ButtonDetailsClick(object sender, RoutedEventArgs e)
        {
            AnalyticRuleSettingsPropertiesWindow propertiesWindow = 
                new AnalyticRuleSettingsPropertiesWindow(selectedItem);
            propertiesWindow.CanSave = true;
            propertiesWindow.Closed += DialogWindowStatus.OnDialogClosed;
            propertiesWindow.Show();
        }

        private void ButtonCreateClick(object sender, RoutedEventArgs e)
        {
            AnalyticRuleSettings settings = new AnalyticRuleSettings(
                AnalyticRuleType.EngineStartUndervoltage, 
                AnalyticItemSettingsReliability.Unknown);
            AnalyticRuleSettingsViewModel viewModel = 
                new AnalyticRuleSettingsViewModel(settings);
            AnalyticRuleSettingsCreationWindow creationWindow = 
                new AnalyticRuleSettingsCreationWindow(viewModel);
            creationWindow.Closed += DialogWindowStatus.OnDialogClosed;
            creationWindow.Show();
        }

        private void OnGridSelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                buttonDetails.IsEnabled = false;
                return;
            }
            selectedItem = e.AddedItems[0] as AnalyticRuleSettingsViewModel;
            buttonDetails.IsEnabled = true;
        }

        private void OnResize(object sender, EventArgs e)
        {
            dataGridRulesSettings.
                Height = ApplicationSizeKeeper.Height - 240;
            dataGridRulesSettings.
                Width = ApplicationSizeKeeper.Width - 150;
        }
    }
}

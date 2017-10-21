using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VTS.Shared;
using VTSWeb.AnalysisCore.Models.Settings.Persistency;
using VTSWeb.AnalysisCore.Presentation;
using VTSWeb.AnalysisCore.Recognition.Engines.Mappings;

using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;

namespace VTSWeb.AnalysisCore.Models.Settings.Presentation
{
    public partial class AnalyticRuleSettingsCreationWindow : ChildWindow
    {
        private ObservableCollection<AnalyticRuleTypeViewModel> ruleTypes = 
            new ObservableCollection<AnalyticRuleTypeViewModel>();
        private ObservableCollection<ViewModelBase> engineFamilyTypes = 
            new ObservableCollection<ViewModelBase>();
        private ObservableCollection<ViewModelBase> engines =
            new ObservableCollection<ViewModelBase>();

        private AnalyticRuleType ruleType;
        private EngineFamilyType? familyType;
        private EngineType? engineType;

        public AnalyticRuleSettingsCreationWindow()
        {
            InitializeComponent();
        }

        public AnalyticRuleSettingsCreationWindow(
            AnalyticRuleSettingsViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
            controlProperties.Initialize(viewModel);
            comboBoxRuleType.ItemsSource = ruleTypes;
            comboBoxEngineFamilyType.ItemsSource = engineFamilyTypes;
            comboBoxEngineType.ItemsSource = engines;
            // Initialize rule Types
            foreach (AnalyticRuleType type in
                Enum.GetValues(typeof(AnalyticRuleType)))
            {
                ruleTypes.Add(
                    new AnalyticRuleTypeViewModel(type));
            }
            // Init engine families
            engineFamilyTypes.Add(new ViewModelStubAny());
            foreach (EngineFamilyType engineFamilyType
                in Enum.GetValues(typeof(EngineFamilyType)))
            {
                engineFamilyTypes.Add(
                    new EngineFamilyTypeViewModel(engineFamilyType));
            }
        }

        private void EngineFamilyTypeSelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            engines.Clear();
            if (e.AddedItems[0] is ViewModelStubAny)
            {
                familyType = null;
                engineType = null;
                return;
            }
            engines.Add(new ViewModelStubAny());
            familyType = ((EngineFamilyTypeViewModel)e.AddedItems[0]).Model;
            foreach (EngineType engineTypeItem in EngineToFamilyMapping.
                GetFamilyMembers(familyType.Value))
            {
                engines.Add(new EngineTypeViewModel(engineTypeItem));
            }
        }

        private void OkClicked(object sender, RoutedEventArgs e)
        {
            textBlockAlreadyExists.Visibility = Visibility.Collapsed;
            progressBarCircular.Visibility = Visibility.Visible;
            AnalyticRuleSettingsCreator creator = 
                new AnalyticRuleSettingsCreator(OnSuccessfullyCreated,
                    OnError, OnAlreadyExistingDetected);
            creator.Create(AssembleObject());
        }

        private void OnAlreadyExistingDetected()
        {
            textBlockAlreadyExists.Visibility = Visibility.Visible;
            progressBarCircular.Visibility = Visibility.Collapsed;
        }

        private void OnSuccessfullyCreated()
        {
            DialogResult = true;
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow errorWindow = new ErrorWindow(e, msg);
            errorWindow.Closed += DialogWindowStatus.OnDialogClosed;
            errorWindow.Show();
        }

        private void CancelClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SelectedRuleTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            else
            {
                buttonOk.IsEnabled = true;
            }
            AnalyticRuleTypeViewModel ruleTypeViewModel =
                e.AddedItems[0] as AnalyticRuleTypeViewModel;
            ruleType = ruleTypeViewModel.Model;
        }

        private AnalyticRuleSettings AssembleObject()
        {
            AnalyticRuleSettings result = 
                ((AnalyticRuleSettingsViewModel) DataContext).Model;
            result.RuleType = ruleType;
            result.EngineFamilyType = familyType;
            result.EngineType = engineType;
            return result;
        }

        private void OnEngineSelected(object sender, 
            SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                engineType = null;
                return;
            }
            engineType = ((EngineTypeViewModel) e.AddedItems[0]).Model;
        }
    }
}


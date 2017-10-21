using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using VTS.Shared;
using VTSWeb.AnalysisCore.Presentation;
using VTSWeb.AnalysisCore.Recognition.Engines.Mappings;

using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Workspace.Admin.AnalyticRulesSettings
{
    public partial class AnalyticRulesSettingsFilteringControl : UserControl
    {
        public event EventHandler CriteriaHasChanged;

        private ObservableCollection<ViewModelBase> ruleTypes =
            new ObservableCollection<ViewModelBase>();
        private ObservableCollection<ViewModelBase> engineFamilyTypes =
            new ObservableCollection<ViewModelBase>();
        private ObservableCollection<ViewModelBase> engines =
            new ObservableCollection<ViewModelBase>();

        private AnalyticRuleTypeViewModel selectedRuleType;
        private EngineFamilyTypeViewModel selectedFamilyType;
        private EngineTypeViewModel selectedEngine;

        public AnalyticRulesSettingsFilteringControl()
        {
            InitializeComponent();
            comboBoxRuleType.ItemsSource = ruleTypes;
            ruleTypes.Add(new ViewModelStubAny());
            foreach (AnalyticRuleType type in 
                Enum.GetValues(typeof(AnalyticRuleType)))
            {
                ruleTypes.Add(
                    new AnalyticRuleTypeViewModel(type));
            }
            comboBoxRuleType.SelectedItem = null;

            engineFamilyTypes.Add(new ViewModelStubAny());
            comboBoxEngineFamily.ItemsSource = engineFamilyTypes;
            foreach (EngineFamilyType engineFamilyType
                in Enum.GetValues(typeof(EngineFamilyType)))
            {
                engineFamilyTypes.Add(
                    new EngineFamilyTypeViewModel(engineFamilyType));
            }

            comboBoxEngineType.ItemsSource = engines;
        }

        public AnalyticRuleTypeViewModel RuleType
        {
            get
            {
                return selectedRuleType;
            }
        }

        public EngineFamilyTypeViewModel Family
        {
            get
            {
                return selectedFamilyType;
            }
        }

        public EngineTypeViewModel Engine
        {
           get
           {
               return selectedEngine;
           }
        }

        private void RuleTypeSelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            selectedRuleType = e.AddedItems[0] as AnalyticRuleTypeViewModel;
            YieldCriteriaHasChanged();
        }

        private void EngineFamilyTypeSelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            engines.Clear();
            selectedEngine = null;
            if (e.AddedItems[0] is ViewModelStubAny)
            {
                selectedFamilyType = null;
                YieldCriteriaHasChanged();
                return;
            }
            engines.Add(new ViewModelStubAny());
            selectedFamilyType = e.AddedItems[0] as EngineFamilyTypeViewModel;
            foreach (EngineType engineType in EngineToFamilyMapping.
                GetFamilyMembers(selectedFamilyType.Model))
            {
                engines.Add(new EngineTypeViewModel(engineType));
            }
            YieldCriteriaHasChanged();
        }

        private void EngineModelSelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            selectedEngine = e.AddedItems[0] as EngineTypeViewModel;
            YieldCriteriaHasChanged();
        }

        private void YieldCriteriaHasChanged()
        {
            if (CriteriaHasChanged != null)
            {
                CriteriaHasChanged.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

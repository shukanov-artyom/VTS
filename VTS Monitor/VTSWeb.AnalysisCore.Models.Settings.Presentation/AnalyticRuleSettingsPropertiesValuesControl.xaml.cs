using System;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.AnalysisCore.Presentation;

namespace VTSWeb.AnalysisCore.Models.Settings.Presentation
{
    public partial class AnalyticRuleSettingsPropertiesValuesControl : UserControl
    {
        public AnalyticRuleSettingsPropertiesValuesControl()
        {
            InitializeComponent();
        }

        public void Initialize(AnalyticRuleSettingsViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentException("Wrong object type");
            }
            DataContext = viewModel;
            bool overrideOptimal =
                viewModel.Model.SettingsMolecule.OverrideOptimal;
            bool overrideAcceptable =
                viewModel.Model.SettingsMolecule.OverrideAcceptable;
            radioButtonAcceptableUsePredefined.IsChecked = overrideAcceptable;
            radioButtonAcceptableUseStatistical.IsChecked = !overrideAcceptable;
            radioButtonOptimalUseStatistical.IsChecked = !overrideOptimal;
            radioButtonOptimalUsePredefined.IsChecked = overrideOptimal;
        }

        private void RadioButtonChecked(
            object sender, RoutedEventArgs e)
        {
            RadioButton senderButton = sender as RadioButton;
            if (senderButton == null)
            {
                throw new Exception("Wrong control!");
            }
            switch (senderButton.Name)
            {
                case "radioButtonOptimalUseStatistical":
                    SetOverrideOptimalBool(false);
                    break;
                case "radioButtonOptimalUsePredefined":
                    SetOverrideOptimalBool(true);
                    break;
                case "radioButtonAcceptableUseStatistical":
                    SetOverrideAcceptableBool(false);
                    break;
                case "radioButtonAcceptableUsePredefined":
                    SetOverrideAcceptableBool(true);
                    break;
                default:
                    throw new Exception("Unknown RadioButton");
            }
        }

        private void SetOverrideOptimalBool(bool setting)
        {
            AnalyticRuleSettingsViewModel viewModel =
                        DataContext as AnalyticRuleSettingsViewModel;
            if (viewModel == null)
            {
                throw new ArgumentException("Wrong object type");
            }
            viewModel.Model.SettingsMolecule.OverrideOptimal = setting;
        }

        private void SetOverrideAcceptableBool(bool setting)
        {
            AnalyticRuleSettingsViewModel viewModel =
                        DataContext as AnalyticRuleSettingsViewModel;
            if (viewModel == null)
            {
                throw new ArgumentException("Wrong object type");
            }
            viewModel.Model.SettingsMolecule.OverrideAcceptable = setting;
        }
    }
}

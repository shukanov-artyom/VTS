using System;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.AnalysisCore.Models.Settings.Persistency;
using VTSWeb.AnalysisCore.Presentation;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;

namespace VTSWeb.AnalysisCore.Models.Settings.Presentation
{
    public partial class AnalyticRuleSettingsPropertiesWindow : ChildWindow
    {
        public AnalyticRuleSettingsPropertiesWindow()
        {
            InitializeComponent();
        }

        public AnalyticRuleSettingsPropertiesWindow(
            AnalyticRuleSettingsViewModel viewModel)
            : this()
        {
            textBlockEngineType.Text = viewModel.EngineTypeName;
            textBlockEngineFamilyType.Text = viewModel.EngineFamilyTypeName;
            textBlockRuleType.Text = viewModel.TypeName;
            DataContext = viewModel;
            controlProperties.Initialize(viewModel);
        }

        public bool CanSave
        {
            get
            {
                return buttonSave.Visibility == Visibility.Visible;
            }
            set
            {
                if (value)
                {
                    buttonSave.Visibility = Visibility.Visible;
                }
                else
                {
                    buttonSave.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void OkClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            progressBarCircular.Visibility = Visibility.Visible;
            AnalyticRuleSettingsPersistency persistency =
                new AnalyticRuleSettingsPersistency(OnPersisted, OnError);
            persistency.Update((
                (AnalyticRuleSettingsViewModel) DataContext).Model);
        }

        private void OnPersisted()
        {
            progressBarCircular.Visibility = Visibility.Collapsed;
        }

        private void OnError(Exception e, string msg)
        {
            progressBarCircular.Visibility = Visibility.Collapsed;
            ErrorWindow wnd = new ErrorWindow(e, msg);
            wnd.Closed += DialogWindowStatus.OnDialogClosed;
            wnd.Show();
        }
    }
}


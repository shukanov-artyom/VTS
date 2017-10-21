using System;
using System.Windows;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Workspace.Admin
{
    public partial class AdministratorDesktopControl : NavigatablePage
    {
        public event EventHandler PartnersClicked;
        public event EventHandler RegKeysClicked;
        public event EventHandler LicensesClicked;
        public event EventHandler AnalyticRulesSettingsClicked;
        public event EventHandler StatisticsGenerationClicked;
        public event EventHandler StatisticsAggregationClicked;
        public event EventHandler SystemNewsClicked;

        public AdministratorDesktopControl()
        {
            InitializeComponent();
            InitializeHeader("Desktop");
        }

        private void PartnersButtonClick(object sender, RoutedEventArgs e)
        {
            if (PartnersClicked != null)
            {
                PartnersClicked.Invoke(this,new EventArgs());
            }
        }

        private void RegistrationKeysButtonClick(
            object sender, RoutedEventArgs e)
        {
            if (RegKeysClicked != null)
            {
                RegKeysClicked.Invoke(this, new EventArgs());
            }
        }

        private void LicensesButtonClicked(object sender, RoutedEventArgs e)
        {
            if (LicensesClicked != null)
            {
                LicensesClicked.Invoke(this, EventArgs.Empty);
            }
        }

        private void AnalyticRulesSettingsButtonClick(
            object sender, RoutedEventArgs e)
        {
            if (AnalyticRulesSettingsClicked != null)
            {
                AnalyticRulesSettingsClicked.Invoke(sender, e);
            }
        }

        private void ButtonClickStatisticsGeneration(
            object sender, RoutedEventArgs e)
        {
            if (StatisticsGenerationClicked != null)
            {
                StatisticsGenerationClicked.Invoke(sender, e);
            }
        }

        private void ButtonClickStatisticsAggregation(
            object sender, RoutedEventArgs e)
        {
            if (StatisticsAggregationClicked != null)
            {
                StatisticsAggregationClicked.Invoke(sender, e);
            }
        }

        private void ButtonClickSystemNews(object sender, RoutedEventArgs e)
        {
            if (SystemNewsClicked != null)
            {
                SystemNewsClicked.Invoke(sender, e);
            }
        }
    }
}

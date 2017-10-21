using System;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.Presentation.Workspace.Admin.AnalyticRulesSettings;
using VTSWeb.Presentation.Workspace.Admin.Statistics;
using VTSWeb.Presentation.Workspace.Common;
using VTSWeb.Presentation.Workspace.Partner.EndUsers;
using VTSWeb.SystemNews;

namespace VTSWeb.Presentation.Workspace.Admin
{
    public partial class AdministratorWorkspaceControl : UserControl
    {
        protected WorkspaceController controller;
        private AdministratorDesktopControl desktop =
            new AdministratorDesktopControl();
        private ClientsControl usersControl;
        private AnalyticRulesSettingsControl analyticRulesSettingsControl;
        private StatisticsGenerationControl statisticsGenerationControl;
        private StatisticsAggregationControl statisticsAggregationControl;
        private SystemNewsControl systemNewsControl;

        public AdministratorWorkspaceControl()
        {
            InitializeComponent();

            desktop.PartnersClicked += ShowPartners;
            desktop.AnalyticRulesSettingsClicked += ShowAnalyticRulesSettings;
            desktop.StatisticsGenerationClicked += ShowStatisticsGeneration;
            desktop.StatisticsAggregationClicked += ShowStatisticsAggregation;
            desktop.SystemNewsClicked += ShowSystemNews;

            controller = new WorkspaceController(
                ContentControlWorkspace, ButtonForward, ButtonBackwards,
                TextBlockForward, TextBlockBackwards, 
                textBlockCurrentPageHeader);
            controller.NavigateToPage(desktop);
        }

        private void ButtonBackwardsClick(object sender, RoutedEventArgs e)
        {
            controller.NavigateBack();
        }

        private void ButtonForwardClick(object sender, RoutedEventArgs e)
        {
            controller.NavigateForward();
        }

        private void ShowPartners(object sender, EventArgs e)
        {
            if (usersControl == null)
            {
                usersControl = new ClientsControl();
            }
            controller.NavigateToPage(usersControl);
        }

        private void ShowAnalyticRulesSettings(
            object sender, EventArgs eventArgs)
        {
            if (analyticRulesSettingsControl == null)
            {
                analyticRulesSettingsControl = 
                    new AnalyticRulesSettingsControl();
            }
            controller.NavigateToPage(analyticRulesSettingsControl);
        }

        private void ShowStatisticsGeneration(
            object sender, EventArgs eventArgs)
        {
            if (statisticsGenerationControl == null)
            {
                statisticsGenerationControl =
                    new StatisticsGenerationControl();
            }
            controller.NavigateToPage(statisticsGenerationControl);
        }

        private void ShowStatisticsAggregation(
            object sender, EventArgs eventArgs)
        {
            if (statisticsAggregationControl == null)
            {
                statisticsAggregationControl =
                    new StatisticsAggregationControl();
            }
            controller.NavigateToPage(statisticsAggregationControl);
        }

        private void ShowSystemNews(object sender, EventArgs eventArgs)
        {
            if (systemNewsControl == null)
            {
                systemNewsControl = new SystemNewsControl();
            }
            controller.NavigateToPage(systemNewsControl);
        }

        protected ContentControl ContentControlWorkspace
        {
            get
            {
                return contentControlWorkspace;
            }
        }
    
        protected Button ButtonForward
        {
            get
            {
                return buttonForward;
            }
        }

        protected Button ButtonBackwards
        {
            get
            {
                return buttonBackward;
            }
        }

        protected TextBlock TextBlockForward
        {
            get
            {
                return textBlockForward;
            }
        }
        
        protected TextBlock TextBlockBackwards
        {
            get
            {
                return textBlockBackwards;
            }
        }
    }
}

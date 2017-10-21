using System;
using System.Windows;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Workspace.Common.Analytic
{
    public partial class DataAnalysisTypeSelectionControl : NavigatablePage
    {
        public event EventHandler ManualParametersMonitoringSelected;
        public event EventHandler AutomaticParametersMonitoringSelected;

        public DataAnalysisTypeSelectionControl()
        {
            InitializeComponent();
            InitializeHeader("DataAnalysis");
        }

        private void ManualParametersControlSelected(object sender, 
            RoutedEventArgs e)
        {
            if (ManualParametersMonitoringSelected != null)
            {
                ManualParametersMonitoringSelected.Invoke(
                    this, EventArgs.Empty);
            }
        }

        private void AutomaticParametersControlSelected(object sender,
            RoutedEventArgs e)
        {
            if (AutomaticParametersMonitoringSelected != null)
            {
                AutomaticParametersMonitoringSelected.Invoke(
                    this, EventArgs.Empty);
            }
        }
    }
}

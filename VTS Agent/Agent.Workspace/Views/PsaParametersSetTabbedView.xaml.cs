using System;
using System.Windows.Controls;
using Agent.Common.Presentation.Controls;
using Agent.Common.Presentation.Data;

namespace Agent.Workspace.Views
{
    /// <summary>
    /// Interaction logic for PsaParametersSetTabbedView.xaml
    /// </summary>
    public partial class PsaParametersSetTabbedView : UserControl
    {
        private readonly PsaParametersSetGraphControl graphControl;
        private readonly PsaParametersSetAnalysisControl analysisControl;

        public PsaParametersSetTabbedView(PsaParametersSetDetailsControl details)
        {
            InitializeComponent();
            itemG.Content = graphControl = new PsaParametersSetGraphControl(details);
            itemA.Content = analysisControl = new PsaParametersSetAnalysisControl(details);
        }

        public PsaParametersSetGraphControl GraphControl
        {
            get
            {
                return graphControl;
            }
        }

        public PsaParametersSetAnalysisControl AnalysisControl
        {
            get
            {
                return analysisControl;
            }
        }
    }
}

using System;
using System.Windows.Controls;

namespace VTSWeb.AnalysisCore.Presentation
{
    public partial class MarksHistoryControl : UserControl
    {
        public MarksHistoryControl()
        {
            InitializeComponent();
        }

        public MarksHistoryControl(AnalyticItemViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}

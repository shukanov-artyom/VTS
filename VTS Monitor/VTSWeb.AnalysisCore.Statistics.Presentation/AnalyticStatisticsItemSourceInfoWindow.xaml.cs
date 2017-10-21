using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.AnalysisCore.Statistics.Presentation
{
    public partial class AnalyticStatisticsItemSourceInfoWindow : ChildWindow
    {
        public AnalyticStatisticsItemSourceInfoWindow()
        {
            InitializeComponent();
        }

        public AnalyticStatisticsItemSourceInfoWindow(
            AnalyticStatisticsItemViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Windows.Controls;
using DevExpress.Xpf.Charts;

namespace VTSWeb.AnalysisCore.Statistics.Presentation
{
    public partial class AnalyticStatisticsItemBarsControl : UserControl
    {
        public AnalyticStatisticsItemBarsControl()
        {
            InitializeComponent();
        }

        public void DisplayDistribution(IDictionary<string, long> source)
        {
            pointsCollectionSeries.Points.Clear();
            foreach (KeyValuePair<string, long> pair in source)
            {
                pointsCollectionSeries.Points.Add(
                    new SeriesPoint(pair.Key, pair.Value));
            }
        }
    }
}

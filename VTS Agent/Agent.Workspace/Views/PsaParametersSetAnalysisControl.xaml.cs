using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Agent.Common.Presentation.Controls;
using Agent.Common.Presentation.Controls.ScaleControllers;
using Agent.Common.Presentation.Data;
using Agent.Common.Presentation.Lexia;
using DevExpress.Xpf.Charts;

namespace Agent.Workspace.Views
{
    /// <summary>
    /// Interaction logic for PsaParameterDataAnalyseControl.xaml
    /// </summary>
    public partial class PsaParametersSetAnalysisControl : UserControl, IParametersSetGraphView
    {
        private readonly ChartController chartController;

        private double[] baseParameter;
        private Series baseSeries;

        public PsaParametersSetAnalysisControl(
            IParametersSetSettingsSource settingsSource)
        {
            InitializeComponent();
            chartController = new ParametersSetAnalysisController(settingsSource, this);
        }

        public double[] BaseParameter
        {
            get
            {
                return baseParameter;
            }
        }

        public Series CreateSeries(PsaParameterDataViewModel vm, Color color, ChartScaleViewModel scale)
        {
            LineSeries2D series = new LineSeries2D();
            series.DataContext = vm;
            series.Brush = new SolidColorBrush(color);
            series.ValueScaleType = ScaleType.Numerical;
            series.ArgumentScaleType = ScaleType.Numerical;
            series.MarkerVisible = false;
            series.ShowInLegend = false;
            series.Label = new SeriesLabel();
            series.Label.Visible = false;
            if (baseParameter != null)
            {
                for (int i = 0; i < vm.Values.Count; i++)
                {
                    series.Points.Add(new SeriesPoint(baseParameter[i], vm.Values[i]));
                }
            }
            else
            {
                for (int i = 0; i < vm.Values.Count; i++)
                {
                    series.Points.Add(new SeriesPoint(i, vm.Values[i]));
                }
            }
            return series;
        }

        public void AddSeries(Series series)
        {
            if (baseParameter == null)
            {
                AcceptBaseParameter(series);
            }
            else
            {
                diagram.Series.Add(series);
            }
        }

        public Series FindSeries(PsaParameterDataViewModel vm)
        {
            if (baseSeries != null && ReferenceEquals(baseSeries.DataContext, vm))
            {
                return baseSeries;
            }
            return diagram.Series.FirstOrDefault(s => ReferenceEquals(((LineSeries2D)s).DataContext, vm));
        }

        public void RemoveSeries(Series s)
        {
            if (ReferenceEquals(baseSeries, s))
            {
                baseSeries = null;
                baseParameter = null;
            }
            else
            {
                diagram.Series.Remove(s);
            }
        }

        private void AcceptBaseParameter(Series series)
        {
            List<SeriesPoint> points = new List<SeriesPoint>();
            foreach (SeriesPoint point in series.Points)
            {
                points.Add(point);
            }
            baseParameter = points.Select(p => p.Value).ToArray();
            baseSeries = series;
        }

        public SeriesCollection SeriesCollection
        {
            get
            {
                return diagram.Series;
            }
        }
    }
}

using System;
using System.Windows.Controls;
using System.Windows.Media;
using Agent.Common.Instance;
using Agent.Common.Presentation.Controls.ScaleControllers;
using Agent.Common.Presentation.Data;
using Agent.Common.Presentation.Lexia;
using DevExpress.Xpf.Charts;

namespace Agent.Common.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for PsaParametersSetGraphControl.xaml
    /// </summary>
    public partial class PsaParametersSetGraphControl : UserControl, IParametersSetGraphView
    {
        private ParametersSetScaleController scaler;

        public PsaParametersSetGraphControl()
        {
            InitializeComponent();
        }

        public PsaParametersSetGraphControl(PsaParametersSetDetailsControl control)
            : this()
        {
            scaler = new ParametersSetScaleController(this, control);
        }

        public double OriginalMinXRange { get; set; }

        public double OriginalMaxXRange { get; set; }

        public SeriesCollection SeriesCollection
        {
            get
            {
                return graphDiagram.Series;
            }
        } 

        public Series FindSeries(PsaParameterDataViewModel vm)
        {
            foreach (Series s in graphDiagram.Series)
            {
                PsaParameterDataViewModel param =
                        s.DataContext as PsaParameterDataViewModel;
                if (param != null)
                {
                    if (param == vm) // the same object
                    {
                        return s as LineSeries2D;
                    }
                }
            }
            return null;
        }

        public bool IsDisplayed(PsaParameterDataViewModel vm)
        {
            return FindSeries(vm) != null;
        }

        private void SliderRangeChanged(object sender, EventArgs e)
        {
            graphDiagram.AxisX.ActualRange.MinValue =
                GetRelative(OriginalMinXRange, OriginalMaxXRange, slider.LowerValue);
            graphDiagram.AxisX.ActualRange.MaxValue =
                GetRelative(OriginalMinXRange, OriginalMaxXRange, slider.UpperValue);
        }

        private void VerticalSliderRangeChanged(object sender, EventArgs e)
        {
            /*graphDiagram.AxisY.ActualRange.MinValue = originalMaxYRange * (slider.LowerValue / 100);
            graphDiagram.AxisY.ActualRange.MaxValue = originalMaxYRange * (slider.UpperValue / 100);*/
        }

        public void Redundant()
        {
            if (MainWindowKeeper.MainWindowInstance == null)
            {
                SliderRangeChanged(null, null);
                VerticalSliderRangeChanged(null, null);
            }
        }

        private double GetRelative(double originalMin, double originalMax, double percentage)
        {
            return originalMin + (originalMax - originalMin) * percentage / 100;
        }

        public void RemoveSeries(Series toDelete)
        {
            SeriesCollection.Remove(toDelete);
        }

        public Series CreateSeries(PsaParameterDataViewModel vm, Color color, ChartScaleViewModel scale)
        {
            LineSeries2D series = new LineSeries2D();
            series.DataContext = vm;
            series.Brush = vm.Color = new SolidColorBrush(color);
            series.ValueScaleType = ScaleType.Numerical;
            series.ArgumentScaleType = ScaleType.Numerical;
            series.MarkerVisible = false;
            series.ShowInLegend = false;
            series.Label = new SeriesLabel();
            series.Label.Visible = false;

            if (!vm.HasTimestamps)
            {
                for (int i = 0; i < vm.Values.Count; i++)
                {
                    series.Points.Add(new SeriesPoint(i, vm.Values[i]));
                }
            }
            else
            {
                for (int i = 0; i < vm.Values.Count; i++)
                {
                    series.Points.Add(new SeriesPoint(vm.Timestamps[i], vm.Values[i]));
                }
            }
            return series;
        }

        public void AddSeries(Series series)
        {
            SeriesCollection.Add(series);
        }
    }
}

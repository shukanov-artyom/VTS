using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Agent.Common.Presentation.Crosshair;
using Agent.Common.Presentation.Data;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;

namespace Agent.Common.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for PsaParameterDataGraphControl.xaml
    /// </summary>
    public partial class PsaParameterDataGraphControl : UserControl
    {
        private LineSeries2D series;
        private double originalMinXRange = double.NaN;
        private double originalMaxXRange = double.NaN;
        private double originalMinYRange = double.NaN;
        private double originalMaxYRange = double.NaN;
        private DoubleDoubleMouseCrosshairCursorProvider doubleDoubleCrosshair;

        private double gridSpacingQuantum = double.NaN;

        public PsaParameterDataGraphControl()
        {
            InitializeComponent();
        }

        public new object DataContext
        {
            get
            {
                return base.DataContext;
            }
            set
            {
                var vm = value as PsaParameterDataViewModel;
                if (vm != null)
                {
                    AddGraph(vm, Colors.Blue);
                }
                base.DataContext = value;
            }
        }

        public void AddGraph(PsaParameterDataViewModel vm, Color strokeColor)
        {
            Clear();
            series = GenerateSeries();
            series.DataContext = vm;
            series.Brush = new SolidColorBrush(strokeColor);
            IList<SeriesPoint> pointsRange = new List<SeriesPoint>();
            if (!vm.HasTimestamps)
            {
                for (int i = 0; i < vm.Values.Count; i++)
                {
                    pointsRange.Add(new SeriesPoint(i, vm.Values[i]));
                }
            }
            else
            {
                for (int i = 0; i < vm.Values.Count; i++)
                {
                    pointsRange.Add(new SeriesPoint(vm.Timestamps[i], vm.Values[i]));
                }
            }
            series.Points.AddRange(pointsRange);
            doubleDoubleCrosshair = new DoubleDoubleMouseCrosshairCursorProvider(
                chart,
                diagram,
                series,
                crosshairCursorCanvas,
                valueX,
                valueY,
                verticalLine,
                horizontalLine,
                diagram.AxisX,
                diagram.AxisY);
            diagram.Series.Add(series);

            originalMinXRange = (double)diagram.AxisX.ActualRange.ActualMinValue;
            originalMaxXRange = (double)diagram.AxisX.ActualRange.ActualMaxValue;
            originalMaxYRange = (double)diagram.AxisY.ActualRange.ActualMaxValue;
            originalMinYRange = (double)diagram.AxisY.ActualRange.ActualMinValue;

            diagram.AxisY.GridSpacing = (originalMaxYRange - originalMinYRange)/4;
            gridSpacingQuantum = diagram.AxisY.GridSpacing/20;
        }

        private void Clear()
        {
            diagram = new XYDiagram2D();
            diagram.AxisX = new AxisX2D();
            diagram.AxisX.Label = new AxisLabel();
            diagram.AxisX.Label.Staggered = false;
            diagram.AxisX.Name = "axisX";
            diagram.AxisX.TickmarksMinorVisible = false;
            diagram.AxisX.GridLinesMinorVisible = true;
            diagram.AxisX.GridLinesVisible = true;
            diagram.AxisY = new AxisY2D();
            diagram.AxisY.Name = "axisY";
            diagram.AxisY.GridLinesMinorVisible = true;
            diagram.AxisY.GridLinesVisible = true;
            chart.Diagram = diagram;
            ResetSliders();
        }

        private void ResetSliders()
        {
            sliderVertical.LowerValue = 0;
            sliderVertical.UpperValue = 100;
            slider.LowerValue = 0;
            slider.UpperValue = 100;
        }

        private LineSeries2D GenerateSeries()
        {
            series = new LineSeries2D();
            series.ValueScaleType = ScaleType.Numerical;
            series.ArgumentScaleType = ScaleType.Numerical;
            series.MarkerVisible = false;
            series.ShowInLegend = false;
            series.Label = new SeriesLabel();
            series.Label.Visible = false;
            return series;
        }

        private void SliderRangeChanged(object sender, EventArgs e)
        {
            diagram.AxisX.ActualRange.MinValue = 
                GetRelative(originalMinXRange, originalMaxXRange, slider.LowerValue); 
            //originalMaxXRange * (slider.LowerValue / 100);
            diagram.AxisX.ActualRange.MaxValue =
                GetRelative(originalMinXRange, originalMaxXRange, slider.UpperValue);
            //originalMaxXRange * (slider.UpperValue / 100);
        }

        private void VerticalRangeChanged(object sender, EventArgs e)
        {
            /*double A = originalMinYRange;
            double B = originalMaxYRange;

            double C1 = sliderVertical.LowerValue;
            double C2 = sliderVertical.UpperValue;

            double X1 = (C1/100)*(B - A);
            double X2 = ((100-C2)/100)*(B - A);

            double P1 = A + X1;
            double P2 = B - X2;

            diagram.AxisY.ActualRange.MinValue = P1;
            diagram.AxisY.ActualRange.MaxValue = P2;*/

            diagram.AxisY.ActualRange.MinValue = 
                //GetRelative(originalMinYRange, originalMaxYRange, sliderVertical.LowerValue); 
            originalMinYRange + (sliderVertical.LowerValue / 100) * (originalMaxYRange - originalMinYRange);
            diagram.AxisY.ActualRange.MaxValue =
                //GetRelative(originalMinYRange, originalMaxYRange, sliderVertical.UpperValue);
            originalMaxYRange - ((100 - sliderVertical.UpperValue) / 100) * (originalMaxYRange - originalMinYRange);
        }

        private double GetRelative(double originalMin, double originalMax, double percentage)
        {
            return originalMin + (originalMax - originalMin)*percentage/100;
        }

        public void Redundant()
        {
            SliderRangeChanged(null, null);
            VerticalRangeChanged(null, null);
            GetRelative(0, 0, 0);
        }

        private void ScaleDensityPlus(object sender, RoutedEventArgs e)
        {
            double newValue = diagram.AxisY.GridSpacing + gridSpacingQuantum;
            if (newValue > 0 && newValue.IsNumber())
            {
                diagram.AxisY.GridSpacing = Math.Round(newValue, 2);
            }
        }

        private void ScaleDensityMinus(object sender, RoutedEventArgs e)
        {
            double newValue = diagram.AxisY.GridSpacing - gridSpacingQuantum;
            if (newValue > 0 && newValue.IsNumber())
            {
                diagram.AxisY.GridSpacing = Math.Round(newValue, 2);
            }
        }
    }
}

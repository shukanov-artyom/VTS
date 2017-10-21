using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Agent.Common.Presentation.Crosshair;
using Agent.Common.Presentation.Data;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;

namespace Agent.Common.Presentation.Controls.Cascade
{
    /// <summary>
    /// Interaction logic for CascadeChartControl.xaml
    /// </summary>
    public partial class CascadeChartControl : UserControl
    {
        private LineSeries2D series;
        public DoubleDoubleMovingCrosshairCursorProvider cursorProvider;
        private double gridSpacingQuantum = double.NaN;

        public CascadeChartControl()
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
                    AddGraph(vm);
                }
                base.DataContext = value;
            }
        }

        public void AddGraph(PsaParameterDataViewModel vm)
        {
            series = GenerateSeries();
            series.DataContext = vm;
            series.Brush = vm.Color;
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
            cursorProvider = 
                new DoubleDoubleMovingCrosshairCursorProvider(diagram, 
                    axisX, 
                    axisY, 
                    crosshairCursorCanvas, 
                    series, 
                    verticalLine, 
                    horizontalLine, 
                    valueX, 
                    valueY);
            series.Points.AddRange(pointsRange);
            diagram.Series.Add(series);

            verticalLine.Visibility = Visibility.Hidden;
            horizontalLine.Visibility = Visibility.Hidden;

            double originalMaxYRange = (double)diagram.AxisY.ActualRange.ActualMaxValue;
            double originalMinYRange = (double)diagram.AxisY.ActualRange.ActualMinValue;
            diagram.AxisY.GridSpacing = (originalMaxYRange - originalMinYRange) / 4;
            gridSpacingQuantum = diagram.AxisY.GridSpacing / 20;
        }

        public void MoveCursorRight(object sender, EventArgs e)
        {
            cursorProvider.MoveCursorRight();
        }

        public void MoveCursorLeft(object sender, EventArgs e)
        {
            cursorProvider.MoveCursorLeft();
        }

        public void SliderMoved(double newValue)
        {
            cursorProvider.SetValue(newValue);
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

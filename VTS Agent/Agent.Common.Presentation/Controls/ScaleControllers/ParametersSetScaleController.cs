using System;
using System.Linq;
using System.Windows.Media;
using Agent.Common.Presentation.Data;
using Agent.Common.Presentation.Lexia;
using DevExpress.Xpf.Charts;
using Color = System.Windows.Media.Color;

namespace Agent.Common.Presentation.Controls.ScaleControllers
{
    public class ParametersSetScaleController : ChartController
    {
        private const double graphOverhead = 0.3;
        private const double ratioLimit = 6.0;

        private readonly PsaParametersSetGraphControl graph;
        private readonly PsaParametersSetDetailsControl details;
        private readonly ScalesSetViewModel scalesSet;

        public ParametersSetScaleController(
            PsaParametersSetGraphControl graph,
            PsaParametersSetDetailsControl details)
            : base(details, graph)
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }
            if (details == null)
            {
                throw new ArgumentNullException("details");
            }
            this.graph = graph;
            this.details = details;
            scalesSet = new ScalesSetViewModel(graph.graphDiagram);
            details.gridScales.DataContext = scalesSet;
        }

        protected override void SettingsOnGraphColorChanged(object sender, EventArgs eventArgs)
        {
            OnColorChanged(sender, eventArgs);
        }

        protected override void SettingsOnGraphAdded(object sender, EventArgs eventArgs)
        {
            ControlOnCheckBoxChecked(sender, eventArgs);
        }

        protected override void SettingsOnGraphRemoved(object sender, EventArgs eventArgs)
        {
            ControlOnCheckBoxUnchecked(sender, eventArgs);
        }

        private void OnColorChanged(object sender, EventArgs eventArgs)
        {
            ParameterDisplaySettingsViewModel cbvm =
                sender as ParameterDisplaySettingsViewModel;
            if (cbvm == null)
            {
                throw new ArgumentException("Wrong sender!");
            }
            PsaParameterDataViewModel vm = cbvm.ParamData as PsaParameterDataViewModel;
            if (graph.IsDisplayed(vm))
            {
                LineSeries2D series = graph.FindSeries(vm) as LineSeries2D;
                vm.Color = new SolidColorBrush(cbvm.StrokeColor);
                series.Brush = new SolidColorBrush(cbvm.StrokeColor);
                SecondaryAxisY2D secAxs = XYDiagram2D.GetSeriesAxisY(series);
                if (secAxs != null)
                {
                    XYDiagram2D.GetSeriesAxisY(series).Brush = new SolidColorBrush(cbvm.StrokeColor);
                }
            }
            UpdateScalesSetView();
            UpdateScalesComboboxesView();
        }

        private void ControlOnCheckBoxUnchecked(object sender, EventArgs eventArgs)
        {
            ParameterDisplaySettingsViewModel cbvm =
                sender as ParameterDisplaySettingsViewModel;
            if (cbvm == null)
            {
                throw new ArgumentException("Wrong sender!");
            }
            RemoveGraph(cbvm.ParamData as PsaParameterDataViewModel);
            UpdateScalesSetView();
            UpdateScalesComboboxesView();
        }

        private void RemoveGraph(PsaParameterDataViewModel vm)
        {
            Series toDelete = null;
            foreach (Series s in graph.SeriesCollection)
            {
                PsaParameterDataViewModel param =
                        s.DataContext as PsaParameterDataViewModel;
                if (param != null)
                {
                    if (param == vm) // the same object
                    {
                        toDelete = s;
                        break;
                    }
                }
            }
            // determine axes to delete
            AxisY2D axsToDelete = XYDiagram2D.GetSeriesAxisY((XYSeries)toDelete);
            bool axsUsedSomewhereElse = false;
            foreach (XYSeries s in graph.SeriesCollection)
            {
                if (s != null && s == toDelete)
                {
                    continue;
                }
                AxisY2D axs = XYDiagram2D.GetSeriesAxisY(s);
                if (axs != null && axs == axsToDelete)
                {
                    axsUsedSomewhereElse = true;
                }
            }
            graph.RemoveSeries(toDelete);
            SecondaryAxisY2D axsToDeleteCast = axsToDelete as SecondaryAxisY2D;
            if (!axsUsedSomewhereElse && axsToDeleteCast != null)
            {
                graph.graphDiagram.SecondaryAxesY.Remove(axsToDeleteCast);
            }
        }

        private void ControlOnCheckBoxChecked(object sender, EventArgs eventArgs)
        {
            var cbvm = sender as ParameterDisplaySettingsViewModel;
            if (cbvm == null)
            {
                throw new ArgumentException("Wrong sender!");
            }
            AddGraph(cbvm.ParamData as PsaParameterDataViewModel, cbvm.StrokeColor, cbvm.SelectedScale);
        }

        private void AddGraph(PsaParameterDataViewModel vm, Color color, ChartScaleViewModel scale)
        {
            LineSeries2D series = View.CreateSeries(vm, color, scale) as LineSeries2D;
            if (graph.graphDiagram.Series.Count > 0)
            {
                if (scale.Model.Equals(ChartScale.Auto))
                {
                    ProcessAutoscale(series, color);
                }
                else
                {
                    ProcessSelectedScale(series, scale.Model);
                }
            }
            graph.graphDiagram.Series.Add(series);

            if (graph.graphDiagram.Series.Count == 1)
            {
                graph.OriginalMaxXRange = (double)graph.graphDiagram.AxisX.ActualRange.ActualMaxValue;
                graph.OriginalMinXRange = (double)graph.graphDiagram.AxisX.ActualRange.ActualMinValue;
            }
        }

        private void ProcessSelectedScale(LineSeries2D series, ChartScale scale)
        {
            var axs = scale.Axis as SecondaryAxisY2D;
            if (axs == null)
            {
                throw new ArgumentException();
            }
            UpdateAxisRangeToNewSeries(axs, series);
            XYDiagram2D.SetSeriesAxisY(series, axs);
        }

        private void ProcessAutoscale(LineSeries2D series, Color color)
        {
            AxisY2D compatibleAxis = FindCompatibleAxis(graph.graphDiagram, series);
            if (compatibleAxis == null) // no compatible axis
            {
                SecondaryAxisY2D newAxis = GenerateNewAxis(series);
                newAxis.Brush = new SolidColorBrush(color);
                graph.graphDiagram.SecondaryAxesY.Add(newAxis);
                XYDiagram2D.SetSeriesAxisY(series, newAxis);
            }
            else // there is a compatible axis
            {
                SecondaryAxisY2D axs = compatibleAxis as SecondaryAxisY2D;
                if (axs != null)
                {
                    UpdateAxisRangeToNewSeries(axs, series);
                    XYDiagram2D.SetSeriesAxisY(series,
                    (SecondaryAxisY2D)compatibleAxis);
                }
            }
            UpdateScalesSetView();
            UpdateScalesComboboxesView();
        }

        private void UpdateScalesSetView()
        {
            scalesSet.Refresh();
        }

        private AxisY2D FindCompatibleAxis(XYDiagram2D diagram,
            LineSeries2D series)
        {
            double minSeriesValue = GetSeriesMinValue(series);
            double maxSeriesValue = GetSeriesMaxValue(series);

            // check primary axis
            bool primaryAxisFits = false;
            primaryAxisFits = WhetherRangesAreCompatible(
                (double)diagram.AxisY.ActualRange.ActualMinValue,
                (double)diagram.AxisY.ActualRange.ActualMaxValue,
                minSeriesValue,
                maxSeriesValue);
            if (primaryAxisFits)
            {
                return diagram.AxisY;
            }

            // Check all availabe secondary axes
            foreach (SecondaryAxisY2D axs in diagram.SecondaryAxesY)
            {
                double axsMin = (double)axs.ActualRange.MinValue;
                double axsMax = (double)axs.ActualRange.MaxValue;
                if (WhetherRangesAreCompatible(axsMin, axsMax,
                    minSeriesValue, maxSeriesValue))
                {
                    return axs;
                }
            }

            // return null is no compatible axis found.
            return null;
        }

        private SecondaryAxisY2D GenerateNewAxis(LineSeries2D series)
        {
            double seriesMin = GetSeriesMinValue(series);
            double seriesMax = GetSeriesMaxValue(series);

            SecondaryAxisY2D result = new SecondaryAxisY2D();

            result.ActualRange.MinValue = seriesMin -
                Math.Abs(graphOverhead * seriesMin);
            result.ActualRange.MaxValue = seriesMax +
                Math.Abs(graphOverhead * seriesMax);
            result.Range = new AxisRange();
            result.Range.MinValue = seriesMin -
                Math.Abs(graphOverhead * seriesMin);
            result.Range.MaxValue = seriesMax +
                Math.Abs(graphOverhead * seriesMax);
            return result;
        }

        private bool WhetherRangesAreCompatible(double r1Min, double r1Max,
            double r2Min, double r2Max)
        {
            double minRatio = Math.Abs((Math.Min(r1Min, r2Min) -
                Math.Max(r1Min, r2Min)) / Math.Min(r1Min, r2Min));
            if (minRatio > ratioLimit)
            {
                return false;
            }
            double maxRatio = Math.Abs((Math.Min(r1Max, r2Max) -
                Math.Max(r1Max, r2Max)) / Math.Min(r1Max, r2Max));
            if (maxRatio > ratioLimit)
            {
                return false;
            }
            return true;
        }

        private double GetSeriesMinValue(LineSeries2D series)
        {
            double minSeriesValue = series.Points[0].Value;
            foreach (SeriesPoint p in series.Points)
            {
                if (p.Value < minSeriesValue)
                {
                    minSeriesValue = p.Value;
                }
            }
            return minSeriesValue;
        }

        private double GetSeriesMaxValue(LineSeries2D series)
        {
            double maxSeriesValue = series.Points[0].Value;
            foreach (SeriesPoint p in series.Points)
            {
                if (p.Value > maxSeriesValue)
                {
                    maxSeriesValue = p.Value;
                }
            }
            return maxSeriesValue;
        }

        private void UpdateAxisRangeToNewSeries(SecondaryAxisY2D axis,
            LineSeries2D series)
        {
            double seriesMax = GetSeriesMaxValue(series);
            double seriesMin = GetSeriesMinValue(series);
            double max = Math.Max((double)axis.ActualRange.ActualMaxValue, seriesMax);
            double min = Math.Min((double)axis.ActualRange.ActualMinValue, seriesMin);
            axis.ActualRange.MinValue = min - Math.Abs(graphOverhead * min);
            axis.ActualRange.MaxValue = max + Math.Abs(graphOverhead * max);
            if (axis.Range != null)
            {
                axis.Range.MinValue = min - Math.Abs(graphOverhead * min);
                axis.Range.MaxValue = max + Math.Abs(graphOverhead * max);
            }
        }

        private void UpdateScalesComboboxesView()
        {
            foreach (ParameterDisplaySettingsControl cb in details.checkboxesStackPanel.Children)
            {
                var vm = cb.DataContext as ParameterDisplaySettingsViewModel;
                vm.Scales.Clear();
                var auto = new ChartScaleViewModel(ChartScale.Auto);
                vm.Scales.Add(auto);
                foreach (SecondaryAxisY2D axis in graph.graphDiagram.SecondaryAxesY)
                {
                    if (!vm.Scales.Any(s => s.Model.Equals(new ChartScale(axis))))
                    {
                        vm.Scales.Add(new ChartScaleViewModel(new ChartScale(axis)));
                    }
                }
                vm.SelectedScale = auto;
            }
        }
    }
}

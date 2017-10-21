using System;
using System.Windows.Controls;
using DevExpress.Xpf.Charts;

namespace VTSWeb.Presentation.Graph
{
    public class MultipleGraphControl : UserControl
    {
        private const double graphOverhead = 0.3;
        private const double ratioLimit = 6.0;

        protected SecondaryAxisY2D GenerateNewAxis(LineSeries2D series)
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

        protected bool WhetherRangesAreCompatible(double r1min, double r1max,
            double r2min, double r2max)
        {
            double minRatio = Math.Abs((Math.Min(r1min, r2min) -
                Math.Max(r1min, r2min)) / Math.Min(r1min, r2min));
            if (minRatio > ratioLimit)
            {
                return false;
            }
            double maxRatio = Math.Abs((Math.Min(r1max, r2max) -
                Math.Max(r1max, r2max)) / Math.Min(r1max, r2max));
            if (maxRatio > ratioLimit)
            {
                return false;
            }
            return true;
        }

        protected double GetSeriesMinValue(LineSeries2D series)
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

        protected double GetSeriesMaxValue(LineSeries2D series)
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

        protected void UpdateAxisByNewSeries(SecondaryAxisY2D axis,
            LineSeries2D series)
        {
            double seriesMax = GetSeriesMaxValue(series);
            double seriesMin = GetSeriesMinValue(series);
            double max = Math.Max((double)axis.ActualRange.ActualMaxValue,
                seriesMax);
            double min = Math.Min((double)axis.ActualRange.ActualMinValue,
                                  seriesMin);
            axis.ActualRange.MinValue = min - Math.Abs(graphOverhead * min);
            axis.ActualRange.MaxValue = max + Math.Abs(graphOverhead * max);
            if (axis.Range != null)
            {
                axis.Range.MinValue = min - Math.Abs(graphOverhead * min);
                axis.Range.MaxValue = max + Math.Abs(graphOverhead * max);
            }
        }

        protected AxisY2D FindCompatibleAxis(XYDiagram2D diagram,
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
    }
}

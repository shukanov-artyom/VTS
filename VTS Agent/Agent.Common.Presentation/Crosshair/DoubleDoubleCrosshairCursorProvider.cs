using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Charts;

namespace Agent.Common.Presentation.Crosshair
{
    public abstract class DoubleDoubleCrosshairCursorProvider : CrosshairCursorProvider<double>
    {
        private readonly XYDiagram2D diagram;
        private readonly AxisX2D axisX;
        private readonly AxisY2D axisY;
        private Border verticalLine;
        private Border horizontalLine;
        private ValueItem valueX;
        private ValueItem valueY;

        public DoubleDoubleCrosshairCursorProvider(XYDiagram2D diagram, 
            AxisX2D axisX,
            AxisY2D axisY,
            Border verticalLine,
            Border horizontalLine,
            ValueItem valueX,
            ValueItem valueY)
        {
            this.diagram = diagram;
            this.axisX = axisX;
            this.axisY = axisY;
            this.verticalLine = verticalLine;
            this.horizontalLine = horizontalLine;
            this.valueX = valueX;
            this.valueY = valueY;
        }

        protected ValueItem ValueX
        {
            get
            {
                return valueX;
            }
        }

        protected ValueItem ValueY
        {
            get
            {
                return valueY;
            }
        }

        // Find a series point that is closest to an argument from 
        // chart coordinates.
        protected override double GetSeriesValue(Series series, double argument)
        {
            for (int i = 0; i < series.Points.Count - 1; i++)
            {
                int pointArg1 = Int32.Parse(series.Points[i].Argument);
                int pointArg2 = Int32.Parse(series.Points[i + 1].Argument);
                if (pointArg1 == (int)argument)
                {
                    return series.Points[i].Value;
                }
                if (pointArg1 < argument && pointArg2 > argument)
                {
                    double interval1 = argument - pointArg1;
                    double interval2 = pointArg2 - argument;
                    // move to right to make a correction
                    return interval1 - 0.3 <= interval2 ? series.Points[i].Value :
                        series.Points[i + 1].Value;
                }
            }
            return double.NaN;
        }

        protected override ControlCoordinates GetTopLeftCoordinates()
        {
            return diagram.DiagramToPoint(
                (double)axisX.ActualRange.ActualMinValue,
                (double)axisY.ActualRange.ActualMaxValue);
        }

        protected override ControlCoordinates GetBottomRightCoordinates()
        {
            return diagram.DiagramToPoint(
                (double)axisX.ActualRange.ActualMaxValue,
                (double)axisY.ActualRange.ActualMinValue);
        }

        // Set crosshair cursor visibility.
        protected virtual void SetCrosshairVisibility(Visibility newValue)
        {
            verticalLine.Visibility = newValue;
            horizontalLine.Visibility = newValue;
            valueX.Visibility = newValue;
            valueY.Visibility = newValue;
        }

        protected void PlaceValuesOnAxis(double x, double y)
        {
            ControlCoordinates coordinatesTopLeft = GetTopLeftCoordinates();
            ControlCoordinates coordinatesBottomRight = GetBottomRightCoordinates();
            Canvas.SetLeft(valueX, x);
            Canvas.SetTop(valueX, coordinatesBottomRight.Point.Y);
            Canvas.SetLeft(valueY, coordinatesTopLeft.Point.X);
            Canvas.SetTop(valueY, y);
        }
    }
}

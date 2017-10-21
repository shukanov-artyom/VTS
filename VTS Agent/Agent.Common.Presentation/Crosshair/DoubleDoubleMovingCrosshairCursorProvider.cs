using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.Charts;

namespace Agent.Common.Presentation.Crosshair
{
    public class DoubleDoubleMovingCrosshairCursorProvider : DoubleDoubleCrosshairCursorProvider
    {
        private double currentXPosition = 0;
        private Canvas crosshairCursorCanvas;
        private XYDiagram2D diagram;
        private Series series;
        private Border verticalLine;
        private Border horizontalLine;
        private int currentPointNumber = 1;

        public DoubleDoubleMovingCrosshairCursorProvider(XYDiagram2D diagram,
            AxisX2D axisX,
            AxisY2D axisY,
            Canvas crosshairCursorCanvas, 
            Series series,
            Border verticalLine,
            Border horizontalLine,
            ValueItem valueX,
            ValueItem valueY)
            :base(diagram, axisX, axisY, verticalLine, horizontalLine, valueX, valueY)
        {
            this.crosshairCursorCanvas = crosshairCursorCanvas;
            this.diagram = diagram;
            this.series = series;
            this.verticalLine = verticalLine;
            this.horizontalLine = horizontalLine;
        }

        public void MoveCursorRight()
        {
            currentPointNumber++;
            UpdateCrosshairPosition();
        }

        public void MoveCursorLeft()
        {
            if (currentPointNumber != 0)
            {
                currentPointNumber--;
            }
            UpdateCrosshairPosition();
        }

        private void UpdateCrosshairPosition()
        {
            ClipCrosshairLines();
            currentXPosition = GetXPosition();
            double seriesValue = GetSeriesValue(series, currentXPosition);
            ControlCoordinates controlCoordinates = diagram.DiagramToPoint(currentXPosition, seriesValue);
            SetNumbersToValuePads(seriesValue.ToString("F2"));
            PlaceValuesOnAxis(controlCoordinates.Point.X, controlCoordinates.Point.Y);
            SetCrosshairVisibility(Visibility.Visible);
            Canvas.SetLeft(verticalLine, controlCoordinates.Point.X);
            Canvas.SetTop(horizontalLine, controlCoordinates.Point.Y);
        }

        private void SetNumbersToValuePads(string y)
        {
            ValueY.Text = y;
        }

        private int GetXPosition()
        {
            if (series.Points.Count < 2)
            {
                return 0;
            }
            return Int32.Parse(series.Points[currentPointNumber].Argument);
        }

        public void SetValue(double newValue)
        {
            ClipCrosshairLines();
            currentPointNumber = GetNearestPointNumber(newValue);
            UpdateCrosshairPosition();
        }

        private int GetNearestPointNumber(double newValue)
        {
            double firstPointArgument;
            double lastPointArgument;
            double.TryParse(series.Points[0].Argument, out firstPointArgument);
            double.TryParse(series.Points[series.Points.Count - 1].Argument, out lastPointArgument);
            double roughValue = firstPointArgument + (lastPointArgument - firstPointArgument)*newValue/100;
            return LocateNearestPoint(roughValue);
        }

        private int LocateNearestPoint(double roughValue)
        {
            int result = 1;
            double lastDelta = Math.Abs(roughValue - double.Parse(series.Points[0].Argument));
            for (int i = 1; i < series.Points.Count; i++)
            {
                double newDelta = Math.Abs(roughValue - double.Parse(series.Points[i].Argument));
                if (newDelta < lastDelta)
                {
                    lastDelta = newDelta;
                    result = i;
                }
            }
            return result;
        }

        private void ClipCrosshairLines()
        {
            ControlCoordinates coordinatesTopLeft = GetTopLeftCoordinates();
            ControlCoordinates coordinatesBottomRight = GetBottomRightCoordinates();
            RectangleGeometry geometry = new RectangleGeometry();
            geometry.Rect = new Rect(coordinatesTopLeft.Point,
                coordinatesBottomRight.Point);
            crosshairCursorCanvas.Clip = geometry;
        }

        protected override double GetSeriesValue(Series series, double argument)
        {
            for (int i = 0; i < series.Points.Count - 1; i++)
            {
                int pointArg1 = Int32.Parse(series.Points[i].Argument);
                if (pointArg1 == (int)argument)
                {
                    return series.Points[i].Value;
                }
            }
            return double.NaN;
        }

        protected override void SetCrosshairVisibility(Visibility newValue)
        {
            verticalLine.Visibility = newValue;
            horizontalLine.Visibility = newValue;
            ValueY.Visibility = newValue;
        }
    }
}

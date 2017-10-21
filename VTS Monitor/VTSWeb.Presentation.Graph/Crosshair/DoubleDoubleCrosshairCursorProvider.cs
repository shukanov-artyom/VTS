using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Xpf.Charts;

namespace VTSWeb.Presentation.Graph.Crosshair
{
    public class DoubleDoubleCrosshairCursorProvider
    {
        public const double ToolTipOffset = 5;

        private ChartControl chart;
        private XYDiagram2D diagram;
        private LineSeries2D series;
        private Canvas crosshairCursorCanvas;
        private ValueItem valueX;
        private ValueItem valueY;
        private Border verticalLine;
        private Border horizontalLine;
        private AxisX2D axisX;
        private AxisY2D axisY;

        public DoubleDoubleCrosshairCursorProvider(ChartControl chart,
            XYDiagram2D diagram,
            LineSeries2D series,
            Canvas crosshairCursorCanvas,
            ValueItem valueX,
            ValueItem valueY,
            Border verticalLine,
            Border horizontalLine,
            AxisX2D axisX,
            AxisY2D axisY)
        {
            this.chart = chart;
            this.diagram = diagram;
            this.series = series;
            this.crosshairCursorCanvas = crosshairCursorCanvas;
            this.valueX = valueX;
            this.valueY = valueY;
            this.verticalLine = verticalLine;
            this.horizontalLine = horizontalLine;
            this.axisX = axisX;
            this.axisY = axisY;

            chart.MouseMove += ChartMouseMove;
            valueX.SizeChanged += ValueXSizeChanged;
            valueY.SizeChanged += ValueYSizeChanged;
        }

        private void ChartMouseMove(object sender, MouseEventArgs e)
        {
            // Clip crosshair lines within a diagram bounds.
            ClipCrosshairLines();

            // Get the current cursor position and transform it to the diagram coordinates.  
            Point position = e.GetPosition(chart);
            DiagramCoordinates diagramCoordinates =
                diagram.PointToDiagram(position);

            if (!diagramCoordinates.IsEmpty)
            {
                // Get a value of a series point that is nearest to the current cursor position.
                double seriesValue = GetSeriesValue(series,
                    (double)diagramCoordinates.NumericalArgument);

                if (!double.IsNaN(seriesValue))
                {
                    // Specify the text for X and Y crosshair labels.
                    valueX.Text = ((int)diagramCoordinates.
                        NumericalArgument).
                        ToString(CultureInfo.InvariantCulture);
                    valueY.Text = Math.Round(seriesValue, 2).
                        ToString(CultureInfo.InvariantCulture);

                    // Show the crosshair cursor elements.
                    SetCrosshairVisibility(Visibility.Visible);

                    // Convert chart coordinates to screen coordinates to 
                    // place crosshair labels along the scale.
                    ControlCoordinates controlCoordinates =
                        diagram.DiagramToPoint(diagramCoordinates.
                        NumericalArgument, seriesValue);
                    PlaceValuesOnAxis(position.X, controlCoordinates.Point.Y);

                    // Draw the crosshair cursor.
                    Canvas.SetLeft(verticalLine, position.X);
                    Canvas.SetTop(horizontalLine, controlCoordinates.Point.Y);
                }
            }
            else
            {
                // Hide the crosshair cursor elements.
                SetCrosshairVisibility(Visibility.Collapsed);
            }
        }

        private void ClipCrosshairLines()
        {
            ControlCoordinates coordinatesTopLeft = GetTopLeftCoordinates();
            ControlCoordinates coordinatesBottomRight =
                GetBottomRightCoordinates();
            RectangleGeometry geometry = new RectangleGeometry();
            geometry.Rect = new Rect(coordinatesTopLeft.Point,
                coordinatesBottomRight.Point);
            crosshairCursorCanvas.Clip = geometry;
        }

        private ControlCoordinates GetTopLeftCoordinates()
        {
            return diagram.DiagramToPoint(
                (double)axisX.ActualRange.ActualMinValue,
                (double)axisY.ActualRange.ActualMaxValue);
        }

        private ControlCoordinates GetBottomRightCoordinates()
        {
            return diagram.DiagramToPoint((double)
                axisX.ActualRange.ActualMaxValue,
                (double)axisY.ActualRange.ActualMinValue);
        }

        private void PlaceValuesOnAxis(double x, double y)
        {
            ControlCoordinates coordinatesTopLeft = GetTopLeftCoordinates();
            ControlCoordinates coordinatesBottomRight = GetBottomRightCoordinates();
            Canvas.SetLeft(valueX, x);
            Canvas.SetTop(valueX, coordinatesBottomRight.Point.Y);
            Canvas.SetLeft(valueY, coordinatesTopLeft.Point.X);
            Canvas.SetTop(valueY, y);
        }

        // Set crosshair cursor visibility.
        void SetCrosshairVisibility(Visibility newValue)
        {
            verticalLine.Visibility = newValue;
            horizontalLine.Visibility = newValue;
            valueX.Visibility = newValue;
            valueY.Visibility = newValue;
        }

        // Move the X and Y labels along the axes.
        private void ValueXSizeChanged(object sender, SizeChangedEventArgs e)
        {
            TranslateTransform transform = valueX.RenderTransform as
                TranslateTransform;
            if (transform != null)
            {
                transform.X = -Math.Round(e.NewSize.Width / 2);
                transform.Y = ToolTipOffset;
            }
        }

        private void ValueYSizeChanged(object sender, SizeChangedEventArgs e)
        {
            TranslateTransform transform = valueY.RenderTransform as
                TranslateTransform;
            if (transform != null)
            {
                transform.X = -ToolTipOffset - e.NewSize.Width;
                transform.Y = -Math.Round(e.NewSize.Height / 2);
            }
        }

        // Find a series point that is closest to an argument from 
        // chart coordinates.
        private double GetSeriesValue(Series series, double argument)
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
    }
}

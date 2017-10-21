using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Xpf.Charts;

namespace Agent.Common.Presentation.Crosshair
{
    public class DateTimeDoubleCrosshairCursorProvider : CrosshairCursorProvider<DateTime>
    {
        private const double ToolTipOffset = 5;

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

        public DateTimeDoubleCrosshairCursorProvider(ChartControl chart,
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

            IsEnabled = true;
        }

        private bool IsEnabled
        {
            get;
            set;
        }

        private void ChartMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsEnabled || !series.Visible)
            {
                SetCrosshairVisibility(Visibility.Collapsed);
                return;
            }
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
                    diagramCoordinates.DateTimeArgument);

                if (!double.IsNaN(seriesValue))
                {
                    // Specify the text for X and Y crosshair labels.
                    valueX.Text = (diagramCoordinates.DateTimeArgument).
                        ToString("D", CultureInfo.CurrentCulture);
                    valueY.Text = Math.Round(seriesValue, 2).
                        ToString(CultureInfo.CurrentCulture);

                    // Show the crosshair cursor elements.
                    SetCrosshairVisibility(Visibility.Visible);

                    // Convert chart coordinates to screen coordinates to 
                    // place crosshair labels along the scale.
                    ControlCoordinates controlCoordinates =
                        diagram.DiagramToPoint(diagramCoordinates.
                        DateTimeArgument, seriesValue);
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
            if (!IsEnabled || !series.Visible)
            {
                return;
            }
            ControlCoordinates coordinatesTopLeft = GetTopLeftCoordinates();
            ControlCoordinates coordinatesBottomRight =
                GetBottomRightCoordinates();
            RectangleGeometry geometry = new RectangleGeometry();
            geometry.Rect = new Rect(coordinatesTopLeft.Point,
                coordinatesBottomRight.Point);
            crosshairCursorCanvas.Clip = geometry;
        }

        private void PlaceValuesOnAxis(double x, double y)
        {
            if (!IsEnabled || !series.Visible)
            {
                return;
            }
            ControlCoordinates coordinatesTopLeft = GetTopLeftCoordinates();
            ControlCoordinates coordinatesBottomRight = GetBottomRightCoordinates();
            Canvas.SetLeft(valueX, x);
            Canvas.SetTop(valueX, coordinatesBottomRight.Point.Y);
            Canvas.SetLeft(valueY, coordinatesTopLeft.Point.X);
            Canvas.SetTop(valueY, y);
        }

        // Set crosshair cursor visibility.
        private void SetCrosshairVisibility(Visibility newValue)
        {
            verticalLine.Visibility = newValue;
            horizontalLine.Visibility = newValue;
            valueX.Visibility = newValue;
            valueY.Visibility = newValue;
        }

        // Move the X and Y labels along the axes.
        private void ValueXSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!IsEnabled || !series.Visible)
            {
                return;
            }
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
            if (!IsEnabled || !series.Visible)
            {
                return;
            }
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
        protected override double GetSeriesValue(Series series, DateTime argument)
        {
            for (int i = 0; i < series.Points.Count - 1; i++)
            {
                /*int pointArg1 = Int32.Parse(series.Points[i].Argument);
                int pointArg2 = Int32.Parse(series.Points[i + 1].Argument);*/
                if (series.Points[i].DateTimeArgument == argument)
                {
                    return series.Points[i].Value;
                }
                if (series.Points[i].DateTimeArgument < argument && 
                    series.Points[i + 1].DateTimeArgument > argument)
                {
                    TimeSpan interval1 = argument - series.Points[i].DateTimeArgument;
                    TimeSpan interval2 = series.Points[i + 1].DateTimeArgument
                        - argument;
                    // move to right to make a correction
                    return interval1 <= interval2 ? series.Points[i].Value :
                        series.Points[i + 1].Value;
                }
            }
            return double.NaN;
        }

        protected override ControlCoordinates GetTopLeftCoordinates()
        {
            return diagram.DiagramToPoint(
                (DateTime)axisX.ActualRange.ActualMinValue,
                (double)axisY.ActualRange.ActualMaxValue);
        }

        protected override ControlCoordinates GetBottomRightCoordinates()
        {
            return diagram.DiagramToPoint(
                (DateTime)axisX.ActualRange.ActualMaxValue,
                (double)axisY.ActualRange.ActualMinValue);
        }
    }
}

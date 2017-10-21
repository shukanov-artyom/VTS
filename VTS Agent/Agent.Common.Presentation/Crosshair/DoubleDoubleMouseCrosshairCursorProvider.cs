using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Xpf.Charts;

namespace Agent.Common.Presentation.Crosshair
{
    public class DoubleDoubleMouseCrosshairCursorProvider : DoubleDoubleCrosshairCursorProvider
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

        public DoubleDoubleMouseCrosshairCursorProvider(ChartControl chart,
            XYDiagram2D diagram, 
            LineSeries2D series,
            Canvas crosshairCursorCanvas,
            ValueItem valueX,
            ValueItem valueY,
            Border verticalLine,
            Border horizontalLine,
            AxisX2D axisX,
            AxisY2D axisY)
            : base(diagram, axisX, axisY,verticalLine, horizontalLine, valueX, valueY)
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
            DiagramCoordinates diagramCoordinates = diagram.PointToDiagram(position);
            if (!diagramCoordinates.IsEmpty)
            {
                // Get a value of a series point that is nearest to the current cursor position.
                double seriesValue = GetSeriesValue(series, diagramCoordinates.NumericalArgument);
                if (!double.IsNaN(seriesValue))
                {
                    // Specify the text for X and Y crosshair labels.
                    valueX.Text = ((int)diagramCoordinates.NumericalArgument).
                        ToString(CultureInfo.InvariantCulture);
                    valueY.Text = Math.Round(seriesValue, 2).ToString(CultureInfo.InvariantCulture);

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

        // Move the X and Y labels along the axes.
        private void ValueXSizeChanged(object sender, SizeChangedEventArgs e)
        {
            TranslateTransform transform = valueX.RenderTransform as TranslateTransform;
            if (transform != null)
            {
                transform.X = -Math.Round(e.NewSize.Width / 2);
                transform.Y = ToolTipOffset;
            }
        }

        private void ValueYSizeChanged(object sender, SizeChangedEventArgs e)
        {
            TranslateTransform transform = valueY.RenderTransform as TranslateTransform;
            if (transform != null)
            {
                transform.X = -ToolTipOffset - e.NewSize.Width;
                transform.Y = -Math.Round(e.NewSize.Height / 2);
            }
        }
    }
}

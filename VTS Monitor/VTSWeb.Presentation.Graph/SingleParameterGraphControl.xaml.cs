using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.Charts;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Graph.Crosshair;

namespace VTSWeb.Presentation.Graph
{
    public partial class SingleParameterGraphControl : UserControl
    {
        private IList<int> timestamps;
        private IList<double> values;
        private Color color; 

        private DoubleDoubleCrosshairCursorProvider doubleDoubleCrosshair;

        public SingleParameterGraphControl()
        {
            InitializeComponent();

            series.ValueScaleType = ScaleType.Numerical;
            series.ArgumentScaleType = ScaleType.Numerical;
            series.MarkerVisible = false;
            series.ShowInLegend = false;
            series.Label = new SeriesLabel();
            series.LabelsVisibility = false;

            doubleDoubleCrosshair = new DoubleDoubleCrosshairCursorProvider(
                chart,
                diagram,
                series,
                canvas.crosshairCursorCanvas,
                canvas.valueX,
                canvas.valueY,
                canvas.verticalLine,
                canvas.horizontalLine,
                axisX,
                axisY);
        }

        public bool Zoomed
        {
            get;
            set;
        }

        public void Clear()
        {
            series.Points.Clear();
            timestamps = null;
            values = null;
        }

        public void DisplayGraph(IList<double> data, Color strokeColor)
        {
            Clear();
            values = data;
            color = strokeColor;

            series.Brush = new SolidColorBrush(strokeColor);
            for (int i=0; i<data.Count; i++)
            {
                SeriesPoint pt = new SeriesPoint(i, data[i]);
                series.Points.Add(pt);
            }
        }

        public void DisplayGraph(IList<int> timestamps, 
            IList<double> data, Color strokeColor)
        {
            Clear();
            this.timestamps = timestamps;
            values = data;
            color = strokeColor;
            series.Brush = new SolidColorBrush(strokeColor);
            for (int i = 0; i < data.Count; i++)
            {
                SeriesPoint pt = new SeriesPoint(timestamps[i], data[i]);
                series.Points.Add(pt);
            }
        }

        private void OnGridMouseClick(object sender, EventArgs e)
        {
            if (Zoomed)
            {
                return;
            }
            GraphZoomWindow w = new GraphZoomWindow();
            SingleParameterGraphControl control = 
                new SingleParameterGraphControl();
            control.Zoomed = true;
            if (timestamps != null && values != null)
            {
                control.DisplayGraph(timestamps, values, color);
            }
            else if (timestamps == null && values != null)
            {
                control.DisplayGraph(values, color);
            }
            w.PutControl(control);
            w.Closed += DialogWindowStatus.OnDialogClosed;
            w.Show();
        }
    }
}

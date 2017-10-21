using System;
using System.Windows.Controls;
using Agent.Common.Presentation.Crosshair;

namespace Agent.Workspace.Views.Chrono
{
    /// <summary>
    /// Interaction logic for ChronologicalItemControl.xaml
    /// </summary>
    public partial class ChronologicalItemControl : UserControl, IChronologicalDataItemView
    {
        private DateTimeDoubleCrosshairCursorProvider doubleDoubleCrosshair;

        public ChronologicalItemControl()
        {
            InitializeComponent();
            doubleDoubleCrosshair = new DateTimeDoubleCrosshairCursorProvider(
                chart,
                diagram,
                series,
                crosshairCursorCanvas,
                valueX,
                valueY,
                verticalLine,
                horizontalLine,
                axisX,
                axisY);
        }
    }
}

using System;
using Agent.Common.Presentation.Crosshair;

namespace Agent.Workspace.Views.Evaluation
{
    /// <summary>
    /// Interaction logic for VehicleEvaluationDataItemControl.xaml
    /// </summary>
    public partial class VehicleEvaluationDataItemControl : IEvaluationDataItemView
    {
        private readonly DateTimeDoubleCrosshairCursorProvider crosshair;

        public VehicleEvaluationDataItemControl()
        {
            InitializeComponent();
            crosshair = new DateTimeDoubleCrosshairCursorProvider(
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

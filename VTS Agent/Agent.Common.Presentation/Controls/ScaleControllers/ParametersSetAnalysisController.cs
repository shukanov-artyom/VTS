using System;
using System.Windows.Media;
using Agent.Common.Presentation.Data;
using Agent.Common.Presentation.Lexia;
using DevExpress.Xpf.Charts;

namespace Agent.Common.Presentation.Controls.ScaleControllers
{
    public class ParametersSetAnalysisController : ChartController
    {
        public ParametersSetAnalysisController(IParametersSetSettingsSource settings,
            IParametersSetGraphView view)
             : base(settings, view)
        {
        }

        protected override void SettingsOnGraphColorChanged(object sender, EventArgs eventArgs)
        {
            ParameterDisplaySettingsViewModel vm = sender as ParameterDisplaySettingsViewModel;
            LineSeries2D series = View.FindSeries(vm.ParameterDataViewModel) as LineSeries2D;
            if (series != null)
            {
                series.Brush = new SolidColorBrush(vm.StrokeColor);
            }
        }

        protected override void SettingsOnGraphRemoved(object sender, EventArgs eventArgs)
        {
            ParameterDisplaySettingsViewModel vm = sender as ParameterDisplaySettingsViewModel;
            LineSeries2D series = View.FindSeries(vm.ParameterDataViewModel) as LineSeries2D;
            if (series != null)
            {
                View.RemoveSeries(series);
            }
        }

        protected override void SettingsOnGraphAdded(object sender, EventArgs eventArgs)
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
            Series series = View.CreateSeries(vm, color, scale);
            View.AddSeries(series);
        }
    }
}

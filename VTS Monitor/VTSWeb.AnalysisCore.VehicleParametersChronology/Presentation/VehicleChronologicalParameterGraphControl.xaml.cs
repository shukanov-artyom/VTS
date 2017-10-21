using System;
using System.Windows.Controls;

namespace VTSWeb.AnalysisCore.VehicleParametersChronology.Presentation
{
    public partial class VehicleChronologicalParameterGraphControl : UserControl
    {
        public VehicleChronologicalParameterGraphControl()
        {
            InitializeComponent();
        }

        public VehicleChronologicalParameterGraphControl(
            VehicleChronologicalParameterViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }
    }
}

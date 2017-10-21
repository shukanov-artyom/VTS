using System;
using System.Windows.Media;
using Agent.Common.Presentation.Data;

namespace Agent.Workspace.Views
{
    /// <summary>
    /// Interaction logic for ParameterView.xaml
    /// </summary>
    public partial class ParameterView : IDataItemView
    {
        public ParameterView()
        {
            InitializeComponent();
        }

        public ParameterView(PsaParameterDataViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
            controlGraph.AddGraph(viewModel, Colors.Blue);
        }
    }
}

using System;
using System.Windows.Controls;
using Agent.Common.Presentation.Data;

namespace Agent.Workspace.Views
{
    /// <summary>
    /// Interaction logic for ParameterDetailsControl.xaml
    /// </summary>
    public partial class ParameterDetailsControl : UserControl
    {
        public ParameterDetailsControl()
        {
            InitializeComponent();
        }

        public ParameterDetailsControl(PsaParameterDataViewModel vm)
            : this()
        {
            DataContext = vm;
        }
    }
}

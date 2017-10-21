using System;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.VehicleParametersChronology.Presentation
{
    public partial class VehicleParametersChronologyTreeControl : UserControl
    {
        private HeaderedBorderControl borderControl;

        public event RoutedPropertyChangedEventHandler<object> SelectedTreeItemChanged;

        public VehicleParametersChronologyTreeControl()
        {
            InitializeComponent();
        }

        public VehicleParametersChronologyTreeControl(
            VehicleParametersChronologyViewModel viewModel, 
            HeaderedBorderControl borderControl)
            : this()
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }
            DataContext = viewModel;
            this.borderControl = borderControl;
        }

        private void OnTreeSelectedItemChanged(object sender, 
            RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectedTreeItemChanged != null)
            {
                SelectedTreeItemChanged.Invoke(sender, e);
            }
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Presentation.Workspace.Common.TreeItems
{
    public partial class VehicleTreeItemControl : UserControl
    {
        public VehicleTreeItemControl()
        {
            InitializeComponent();
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            textBlockVin.Visibility = Visibility.Visible;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            textBlockVin.Visibility = Visibility.Collapsed;
        }
    }
}

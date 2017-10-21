using System;
using System.Windows;
using Agent.Common.Instance;

namespace Agent.Workspace.Views
{
    /// <summary>
    /// Interaction logic for VehicleEventWindow.xaml
    /// </summary>
    public partial class VehicleEventWindow : Window
    {
        public VehicleEventWindow()
        {
            InitializeComponent();
            Owner = MainWindowKeeper.MainWindowInstance as Window;
        }
    }
}

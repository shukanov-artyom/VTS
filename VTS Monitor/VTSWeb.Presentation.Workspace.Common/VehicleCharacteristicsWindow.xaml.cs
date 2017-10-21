using System;
using System.Windows.Controls;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Workspace.Common
{
    public partial class VehicleCharacteristicsWindow : ChildWindow
    {
        public VehicleCharacteristicsWindow()
        {
            InitializeComponent();
            Height = ApplicationSizeKeeper.Height - 200;
            Width = ApplicationSizeKeeper.Width - 150;
        }
    }
}


using System;
using System.Windows;
using System.Windows.Controls;

namespace VTSWeb.Presentation.Common
{
    public partial class GraphZoomWindow : ChildWindow
    {
        public GraphZoomWindow()
        {
            InitializeComponent();
            LayoutRoot.Width = ApplicationSizeKeeper.Width - 80;
            LayoutRoot.Height = ApplicationSizeKeeper.Height - 250;
        }

        public void PutControl(UIElement control)
        {
            contentControlPlaceholder.Content = control;
        }
    }
}


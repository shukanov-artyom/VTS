using System;
using System.Windows;

namespace VTSWeb.Presentation.Common
{
    public static class ApplicationSizeKeeper
    {
        public static event EventHandler AppResized;

        public static void OnApplicationResize(object sender, EventArgs e)
        {
            Width = ((FrameworkElement)sender).ActualWidth;
            Height = ((FrameworkElement)sender).ActualHeight;
            if (AppResized != null)
            {
                AppResized.Invoke(sender, EventArgs.Empty);
            }
        }

        public static double Height;
        public static double Width;
    }
}

using System;
using System.Windows;
using Agent.Common.Instance;
using Agent.Common.Presentation.Error;
using Agent.Logging;

namespace Agent.About
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void OnVisitWebsiteButtonClicked(object sender, RoutedEventArgs e)
        {
            string link = "http://vtsautomonitoring.com";
            try
            {
                System.Diagnostics.Process.Start(link);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                {
                    Log.Error(noBrowser.Message);
                    ErrorWindow wnd = new ErrorWindow(noBrowser.Message);
                    wnd.Owner = MainWindowKeeper.MainWindowInstance as Window;
                    wnd.ShowDialog();
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

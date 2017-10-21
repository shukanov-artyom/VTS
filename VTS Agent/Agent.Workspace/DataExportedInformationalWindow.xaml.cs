using System;
using System.Windows;
using Agent.Common.Instance;
using Agent.Common.Presentation.Error;
using Agent.Localization;
using Agent.Logging;
using VTS.Agent.Host;

namespace Agent.Workspace
{
    /// <summary>
    /// Interaction logic for DataExportedInformationalWindow.xaml
    /// </summary>
    public partial class DataExportedInformationalWindow : Window
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataExportedInformationalWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// private void GoToHelp(object sender, RouteEventArgs e)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToHelp(object sender, RoutedEventArgs e)
        {
            string link = CodeBehindStringResolver.Resolve("WikiLinkDataUpload");
            string error = BrowserCaller.GoToLink(link);
            if (!String.IsNullOrEmpty(error))
            {
                Log.Error(error);
                ErrorWindow wnd = new ErrorWindow(error);
                wnd.Owner = MainWindowKeeper.MainWindowInstance as Window;
                wnd.ShowDialog();
            }
        }

        private void GoToMonitor(object sender, RoutedEventArgs e)
        {
            string linkToVts = CodeBehindStringResolver.Resolve("MonitorLink");
            string error = BrowserCaller.GoToLink(linkToVts);
            if (!String.IsNullOrEmpty(error))
            {
                Log.Error(error);
                ErrorWindow wnd = new ErrorWindow(error);
                wnd.Owner = MainWindowKeeper.MainWindowInstance as Window;
                wnd.ShowDialog();
            }
        }
    }
}

using System;
using System.Windows;
using Agent.Common.Instance;
using Agent.Common.Presentation.Error;
using Agent.Localization;
using Agent.Logging;
using VTS.Agent.Host;

namespace Agent.DataAnalysis
{
    /// <summary>
    /// Interaction logic for DataAnalysisWindow.xaml
    /// </summary>
    public partial class DataAnalysisWindow : Window
    {
        public DataAnalysisWindow()
        {
            InitializeComponent();
        }

        private void AnalyseButtonClick(object sender, RoutedEventArgs e)
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

        private void WikiButtonClick(object sender, RoutedEventArgs e)
        {
            string linkToWiki = CodeBehindStringResolver.Resolve("WikiLinkDataUpload");
            string error = BrowserCaller.GoToLink(linkToWiki);
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

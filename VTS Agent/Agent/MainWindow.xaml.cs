using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Agent.About;
using Agent.Common;
using Agent.Common.Instance;
using Agent.Common.Presentation.Network;
using Agent.Common.Presentation.RegistryUser;
using Agent.Common.Presentation.Users;
using Agent.Connector;
using Agent.Connector.PSA;
using Agent.Logging;
using Agent.Network.Monitor;
using Agent.Network.Monitor.VtsWebService;
using Agent.Version;
using Agent.Workspace.ViewModels;
using VTS.Shared.DomainObjects;

namespace Agent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private delegate void ShowNewVersionWindowDelegate(string downloadLink);

        public MainWindow()
        {
            InitializeComponent();
            MainWindowKeeper.MainWindowInstance = this;
            LogonLocallyStoredUser();
            IList<DiagnosticSystemConnector> connectors = new List<DiagnosticSystemConnector>();
            foreach (DiagnosticSystemConnector connector in DiagnosticSystemConnectorsFactory.GetApplicableConnectors())
            {
                connectors.Add(connector);
            }
            psaDataControl.DataContext = new PsaDataViewModel(connectors.ToArray());
            textBlockVersion.Text = ApplicationVersion.Current.VersionString;
            LastVersionChecker lastVersionChecker = 
                new LastVersionChecker(OnNewVersionAvailable, 
                    OnNewVersionCheckError);
            string networkForbidden = ConfigurationManager.AppSettings["NetworkOperationsForbidden"];
            if (!networkForbidden.Equals("True", StringComparison.InvariantCultureIgnoreCase))
            {
                lastVersionChecker.CheckAsync();
            }
            controlLoggeUser.LogonClicked += OnLogonClicked;
            controlLoggeUser.LogoutClicked += OnLogoutClicked;
            controlLoggeUser.ViewProfileClicked += ViewProfileClicked;
        }

        private void LogonLocallyStoredUser()
        {
            LoggedUserContext.LoggedUser = StoredSettings.Current;
        }

        private void ViewProfileClicked(object sender, EventArgs eventArgs)
        {
            //throw new NotImplementedException();
        }

        private void OnLogoutClicked(object sender, EventArgs eventArgs)
        {
            LoggedUserContext.LoggedUser = null;
        }

        private void ShowAboutWindow(object sender, RoutedEventArgs e)
        {
            AboutWindow wnd = new AboutWindow();
            wnd.Owner = MainWindowKeeper.MainWindowInstance as Window;
            wnd.ShowDialog();
        }

        private void OnNewVersionAvailable(AgentVersion newVersion)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, 
                new ShowNewVersionWindowDelegate(ShowNewVersionWindow), 
                newVersion.DownloadLink);
        }

        private void OnNewVersionCheckError(Exception e, string msg)
        {
            Log.Error(e, msg);
        }

        private void ShowNewVersionWindow(string newVersionDownloadLink)
        {
            NewVersionAvailableWindow window = 
                new NewVersionAvailableWindow(newVersionDownloadLink);
            window.Owner = MainWindowKeeper.MainWindowInstance as Window;
            window.ShowDialog();
        }

        private void OnLogonClicked(object sender, EventArgs e)
        {
            Window connectionCheckWindow = new ConnectionCheckWindow();
            connectionCheckWindow.Owner = MainWindowKeeper.
                MainWindowInstance as Window;
            bool? connectionCheckResult = connectionCheckWindow.ShowDialog();
            if (connectionCheckResult.Value)
            {
                LogonAndRegistrationViewModel viewModel = 
                    new LogonAndRegistrationViewModel();
                Window window = new LogonAndRegistrationWindow();
                window.DataContext = viewModel;
                window.Owner = MainWindowKeeper.MainWindowInstance as Window;
                window.ShowDialog();
            }
        }
    }
}

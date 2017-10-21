using System;
using System.Windows;
using System.Windows.Controls;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.Common;
using VTSWeb.Presentation;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.UserRegistration;
using VTSWeb.UserRegistration.ClientRegistration;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTS
{
    public partial class LoggedUserBarControl : UserControl
    {
        private UserRegistrationProgressWindow progressWindow;
        private const string noData = "—";

        private User registeredClient;

        public LoggedUserBarControl()
        {
            InitializeComponent();
            LoggedUserContext.UserLoggedIn += OnUserLoggedOn;
            hyperlinkButtonLoggedUserName.Visibility = Visibility.Collapsed;
            hyperlinkButtonLogout.Visibility = Visibility.Collapsed;
        }

        private void LogonButtonClick(object sender, RoutedEventArgs e)
        {
            UserCredentials creds = new UserCredentials();
            UserCredentialsViewModel vm = new UserCredentialsViewModel(creds);
            LogonWindow win = new LogonWindow(vm);
            win.Show();
        }

        private void OnUserLoggedOn(object sender, EventArgs e)
        {
            UserLoggedOnEventArgs ea = e as UserLoggedOnEventArgs;
            if (ea == null)
            {
                throw new ArgumentException("e");
            }
            UpdateUiForLoggedUser();
        }

        private void ButtonLogoutClick(object sender, RoutedEventArgs e)
        {
            LoggedUserContext.LogOff();
            UpdateUiForLoggedOutUser();
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            RegisterUser();
        }

        private void RegisterUser()
        {
            User client = new User();
            UserRegistrationPropertiesViewModel vm =
                new UserRegistrationPropertiesViewModel(client);
            ClientRegistrationWindow wnd = new ClientRegistrationWindow();
            wnd.DataContext = vm;
            wnd.Closed += OnClientRegWndClosed;
            wnd.Show();
        }

        private void OnClientRegWndClosed(object sender, EventArgs e)
        {
            ClientRegistrationWindow window = sender as ClientRegistrationWindow;
            window.Closed -= OnClientRegWndClosed;
            if (window.DialogResult != null && window.DialogResult.Value)
            {
                UserRegistrationPropertiesViewModel vm = window.DataContext
                    as UserRegistrationPropertiesViewModel;
                if (vm == null)
                {
                    throw new ArgumentException("Wrong object type");
                }
                User client = vm.Model;
                string password = vm.Password;
                client.PasswordHash = Sha256Hash.Calculate(password);
                registeredClient = client;
                ProcessMissingFields(client);
                UserRegistrator registrator = new UserRegistrator(client, 
                    OnClientRegistered, OnError);
                progressWindow = new UserRegistrationProgressWindow(registrator);
                progressWindow.Closed += OnRegistrationWindowClosed;
                progressWindow.Show();
            }
        }

        private void OnRegistrationWindowClosed(object sender, EventArgs e)
        {
            if (progressWindow.DialogResult != null &&
                progressWindow.DialogResult.Value)
            {
                if (progressWindow.LoginAfterRegistration)
                {
                    VtsWebServiceClient client = new VtsWebServiceClient();
                    client.AuthenticateUserCompleted += OnUserAuthenticated;
                    client.AuthenticateUserAsync(registeredClient.Login,
                        registeredClient.PasswordHash);
                }
            }
        }

        private void OnClientRegistered()
        {
            // nothing
        }

        private void ProcessMissingFields(User user)
        {
            if (String.IsNullOrEmpty(user.Name))
            {
                user.Name = noData;
            }
            if (String.IsNullOrEmpty(user.Surname))
            {
                user.Surname = noData;
            }
            if (String.IsNullOrEmpty(user.Phone))
            {
                user.Phone = noData;
            }
        }

        private void UpdateUiForLoggedUser()
        {
            hyperlinkButtonLoggedUserName.Text =
                (String.IsNullOrEmpty(LoggedUserContext.LoggedUser.Name) ||
                LoggedUserContext.LoggedUser.Name == noData)
                    ? LoggedUserContext.LoggedUser.Login
                    : LoggedUserContext.LoggedUser.Name;
            hyperlinkButtonLogon.Visibility = Visibility.Collapsed;
            hyperlinkButtonLoggedUserName.Visibility = Visibility.Visible;

            hyperlinkButtonRegister.Visibility = Visibility.Collapsed;
            hyperlinkButtonLogout.Visibility = Visibility.Visible;
            hyperlinkButtonLogout.IsEnabled = true;
        }

        private void UpdateUiForLoggedOutUser()
        {
            hyperlinkButtonLoggedUserName.Text = String.Empty;
            hyperlinkButtonLogon.Visibility = Visibility.Visible;
            hyperlinkButtonLoggedUserName.Visibility = Visibility.Collapsed;

            hyperlinkButtonRegister.Visibility = Visibility.Visible;
            hyperlinkButtonLogout.Visibility = Visibility.Collapsed;
            hyperlinkButtonLogout.IsEnabled = false;
        }

        private void OnUserAuthenticated(object s, 
            AuthenticateUserCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(e.Error, e.Error.Message);
            }
            else
            {
                LoggedUserContext.LogonPasswordHash(
                    e.Result.Login, e.Result.PasswordHash);
            }
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow wnd  = new ErrorWindow(e, msg);
            wnd.Show();
        }
    }
}

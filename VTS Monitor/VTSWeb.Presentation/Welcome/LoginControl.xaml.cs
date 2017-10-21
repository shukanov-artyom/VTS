
using System;
using System.Windows;
using System.Windows.Controls;
using VTSWeb.Common;

namespace VTSWeb.Presentation.Welcome
{
    public partial class LoginControl : UserControl
    {
        public event EventHandler UserAttemptingToLogin;
        public event EventHandler UnsuccessfulLoginAttempt;

        public LoginControl()
        {
            InitializeComponent();
            LoggedUserContext.IncorrectCredentialsAttempt += OnIncorrectCredentialsAttempt;
            LoggedUserContext.UserLoggedIn += OnUserLoggedIn;
            UserCredentials creds = new UserCredentials();
            DataContext = new UserCredentialsViewModel(creds);
        }

        private void OkClicked(object sender, RoutedEventArgs e)
        {
            if (UserAttemptingToLogin != null)
            {
                UserAttemptingToLogin.Invoke(this, EventArgs.Empty);
            }
            UserCredentialsViewModel vm = 
                DataContext as UserCredentialsViewModel;
            string login = vm.Model.Username;
            string password = vm.Model.Password;
            LoggedUserContext.Logon(login, password);
        }

        private void OnIncorrectCredentialsAttempt(object s, EventArgs e)
        {
            textBlockIncorrectCredentials.Visibility = Visibility.Visible;
            if (UnsuccessfulLoginAttempt != null)
            {
                UnsuccessfulLoginAttempt.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnUserLoggedIn(object s, EventArgs e)
        {
            // do nothing
        }
    }
}

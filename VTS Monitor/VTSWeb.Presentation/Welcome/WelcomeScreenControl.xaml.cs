using System;
using System.Windows.Controls;
using VTSWeb.Common;

namespace VTSWeb.Presentation.Welcome
{
    public partial class WelcomeScreenControl : UserControl
    {
        public WelcomeScreenControl()
        {
            InitializeComponent();

            UserCredentials creds = new UserCredentials();
            UserCredentialsViewModel vm = new UserCredentialsViewModel(creds);
            LoginControl.DataContext = vm;
            LoginControl.UnsuccessfulLoginAttempt += OnUnsuccessfulLoginAttempt;
            LoginControl.UserAttemptingToLogin += OnUserAttemptingToLogin;
        }

        private LoginControl LoginControl
        {
            get
            {
                return (LoginControl)controlLogin.InnerContent;
            }
        }

        private void OnUnsuccessfulLoginAttempt(object sender, EventArgs e)
        {
            controlLogin.SetWaitingMode(false);
        }

        private void OnUserAttemptingToLogin(object sender, EventArgs e)
        {
            controlLogin.SetWaitingMode(true);
        }
    }
}
using System;
using System.Windows;
using System.Windows.Controls;
using Agent.Common.Instance;

namespace Agent.Common.Presentation.Users
{
    /// <summary>
    /// Interaction logic for LoggedUserToolbarControl.xaml
    /// </summary>
    public partial class LoggedUserToolbarItemControl : UserControl
    {
        public event EventHandler LogonClicked;
        public event EventHandler LogoutClicked;
        public event EventHandler ViewProfileClicked;

        public LoggedUserToolbarItemControl()
        {
            InitializeComponent();
            LoggedUserContext.UserLoggedOff += OnUserLoggedOff;
            LoggedUserContext.UserLoggedOn += OnUserLoggedOn;
            controlHyperlinkButton.HyperlinkClicked += ViewUserProfile;
        }

        private void OnUserLoggedOff(object sender, EventArgs eventArgs)
        {
            buttonLogOn.Visibility = Visibility.Visible;
            buttonLogOff.Visibility = Visibility.Collapsed;
            controlHyperlinkButton.SetHyperlinkText(String.Empty);
        }

        private void OnUserLoggedOn(object sender, EventArgs eventArgs)
        {
            buttonLogOn.Visibility = Visibility.Collapsed;
            buttonLogOff.Visibility = Visibility.Visible;
            controlHyperlinkButton.SetHyperlinkText(
                LoggedUserContext.LoggedUser.Login);
        }

        private void OnLogonClicked(object sender, RoutedEventArgs e)
        {
            if (LogonClicked != null)
            {
                LogonClicked.Invoke(sender, e);
            }
        }

        private void OnLogoffClicked(object sender, RoutedEventArgs e)
        {
            if (LogoutClicked != null)
            {
                LogoutClicked.Invoke(sender, e);
            }
        }

        private void ViewUserProfile(object sender, EventArgs e)
        {
            if (ViewProfileClicked != null)
            {
                ViewProfileClicked.Invoke(sender, e);
            }
        }
    }
}

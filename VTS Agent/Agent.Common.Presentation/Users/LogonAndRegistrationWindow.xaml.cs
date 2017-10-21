using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Agent.Common.Instance;

namespace Agent.Common.Presentation.Users
{
    /// <summary>
    /// Interaction logic for LogonAndRegistrationWindow.xaml
    /// </summary>
    public partial class LogonAndRegistrationWindow : Window
    {
        public LogonAndRegistrationWindow()
        {
            InitializeComponent();
            LoggedUserContext.UserLoggedOn += OnUserLogged;
        }

        private void OnUserLogged(object sender, EventArgs eventArgs)
        {
            LoggedUserContext.UserLoggedOn -= OnUserLogged;
            DialogResult = true;
        }

        private void OnPasswordChangedOnPasswordBox(object sender, RoutedEventArgs e)
        {
            ((LogonAndRegistrationViewModel) DataContext).PasswordText = passwordBox.Password;
        }

        private void UsernameFieldLostFocus(object sender, RoutedEventArgs e)
        {
            BindingExpression be = textBoxUsername.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            controlUsernameValidation.TriggerUsernameValidation();
        }

        private void OnRegisterSelected(object sender, RoutedEventArgs e)
        {
            controlUsernameValidation.TriggerUsernameValidation();
        }
    }
}

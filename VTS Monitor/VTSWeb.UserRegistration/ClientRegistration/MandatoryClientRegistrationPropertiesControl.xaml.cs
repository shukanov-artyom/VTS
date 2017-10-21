using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VTSWeb.Storage.Retrievers.Users;

namespace VTSWeb.UserRegistration.ClientRegistration
{
    public partial class MandatoryClientRegistrationPropertiesControl : UserControl
    {
        public MandatoryClientRegistrationPropertiesControl()
        {
            InitializeComponent();
        }

        private void OnCheckLoginClicked(object sender, EventArgs e)
        {
            hyperLinkButtonCheckLogin.IsEnabled = false;
            textBoxLogin.IsEnabled = false;

            if (String.IsNullOrWhiteSpace(textBoxLogin.Text))
            {
                UpdateUiForInvalidLogin();
                hyperLinkButtonCheckLogin.IsEnabled = true;
                textBoxLogin.IsEnabled = true;
            }
            else
            {
                circularProgressBar.Visibility = Visibility.Visible;
                UserLoginAvailabilityChecker checker =
                    new UserLoginAvailabilityChecker(OnLoginChecked);
                checker.CheckLoginAvailability(textBoxLogin.Text);
            }
        }

        private void OnLoginChecked(bool isAccessible)
        {
            if (isAccessible)
            {
                textBlockLoginValidationMessage.Visibility =
                   Visibility.Collapsed;
                textBlockLoginCheckResult.Text = " OK";
                textBlockLoginCheckResult.Foreground =
                    new SolidColorBrush(Colors.Green);
            }
            else
            {
                UpdateUiForInvalidLogin();
            }
            circularProgressBar.Visibility = Visibility.Collapsed;
            hyperLinkButtonCheckLogin.IsEnabled = true;
            textBoxLogin.IsEnabled = true;
        }

        private void OnLoginTextInput(object sender, TextCompositionEventArgs e)
        {
            if (textBlockLoginCheckResult.Text != "  ?")
            {
                textBlockLoginCheckResult.Text = "  ?";
            }
            if (textBlockLoginValidationMessage.Visibility !=
                    Visibility.Collapsed)
            {
                textBlockLoginValidationMessage.Visibility =
                    Visibility.Collapsed;
            }
        }

        private void UpdateUiForInvalidLogin()
        {
            textBlockLoginValidationMessage.Visibility =
                    Visibility.Visible;
            textBlockLoginCheckResult.Text = "  !";
            textBlockLoginCheckResult.Foreground =
                new SolidColorBrush(Colors.Red);
        }
    }
}

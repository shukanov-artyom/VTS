using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Agent.Common.Instance;
using Agent.Common.Presentation.Error;
using Agent.Logging;
using Agent.Network.Monitor.VtsWebService;

namespace Agent.Common.Presentation.Users
{
    /// <summary>
    /// Interaction logic for LoginPreRegistrationCheckControl.xaml
    /// </summary>
    public partial class LoginPreRegistrationCheckControl : UserControl
    {
        public static readonly DependencyProperty IsUsernameValidatedProperty =
            DependencyProperty.Register("IsUsernameValidated",
            typeof(bool),
            typeof(LoginPreRegistrationCheckControl),
            new UIPropertyMetadata(OnValidationStateChanged));

        public static readonly DependencyProperty UsernameInputProperty =
            DependencyProperty.Register("UsernameInput",
            typeof(string),
            typeof(LoginPreRegistrationCheckControl),
            new UIPropertyMetadata(OnUsernameInputChanged));

        public LoginPreRegistrationCheckControl()
        {
            InitializeComponent();
        }

        public void TriggerUsernameValidation()
        {
            textBlock.Text = String.Empty;
            progressBarCircular.Visibility = Visibility.Visible;
            IVtsWebService client = Infrastructure.Container.GetInstance<IVtsWebService>();
            try
            {
                string username = UsernameInput;
                if (String.IsNullOrEmpty(username))
                {
                    progressBarCircular.Visibility = Visibility.Collapsed;
                    textBlock.Text = String.Empty;
                    IsUsernameValidated = false;
                    return;
                }
                bool occupied = client.CheckUsernameAvailability(username);
                if (!occupied)
                {
                    progressBarCircular.Visibility = Visibility.Collapsed;
                    textBlock.Text = "Available";
                    textBlock.Foreground = new SolidColorBrush(Colors.Green);
                    IsUsernameValidated = true;
                }
                else
                {
                    progressBarCircular.Visibility = Visibility.Collapsed;
                    textBlock.Text = "Occupied";
                    textBlock.Foreground = new SolidColorBrush(Colors.Red);
                    IsUsernameValidated = false;
                }
            }
            catch (Exception e)
            {
                progressBarCircular.Visibility = Visibility.Collapsed;
                Log.Error(e, e.Message);
                ErrorWindow wnd = new ErrorWindow(e, e.Message);
                wnd.Owner = MainWindowKeeper.MainWindowInstance as Window;
                wnd.ShowDialog();
            }
        }

        public bool IsUsernameValidated
        {
            get
            {
                return (bool)GetValue(IsUsernameValidatedProperty);
            }
            set
            {
                SetValue(IsUsernameValidatedProperty, value);
                ((LogonAndRegistrationViewModel) DataContext).IsUsernameValidated = value;
            }
        }

        public string UsernameInput
        {
            get
            {
                return (string)GetValue(UsernameInputProperty);
            }
            set
            {
                SetValue(UsernameInputProperty, value);
            }
        }

        private static void OnValidationStateChanged(DependencyObject dob,
            DependencyPropertyChangedEventArgs e)
        {
            // nothing here yet
        }

        private static void OnUsernameInputChanged(DependencyObject dop,
            DependencyPropertyChangedEventArgs e)
        {
            // nothing yet
        }
    }
}

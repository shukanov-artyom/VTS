using System;
using System.Windows;
using System.Windows.Controls;
using VTS.Shared.DomainObjects;
using VTSWeb.Common;
using VTSWeb.DomainObjects;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.Storage.Retrievers.Users;

namespace VTSWeb.Presentation
{
    public partial class LogonWindow : ChildWindow
    {
        public LogonWindow()
        {
            InitializeComponent();
        }

        public LogonWindow(UserCredentialsViewModel viewModel)
            : this()
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }
            DataContext = viewModel;
            userLogonDataControl.DataContext = viewModel;
            LoggedUserContext.UserLoggedIn += OnUserLoggedIn;
            LoggedUserContext.IncorrectCredentialsAttempt += OnIncorrectCredentaials;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            SetWaitingState();
            UserCredentialsViewModel vm = 
                DataContext as UserCredentialsViewModel;
            // TODO: add some basic validation here
            if (vm == null)
            {
                throw new Exception("Wrong viewmodel type!");
            }
            string login = vm.Model.Username;
            string password = vm.Model.Password;
            LoggedUserContext.Logon(login, password);
        }

        private void OnUserLoggedIn(object sender, EventArgs e)
        {
            UserCredentialsViewModel vm =
                        DataContext as UserCredentialsViewModel;
            UsersRetriever retriever = new UsersRetriever(
                 GetUserCallback, ErrorCallback);
            retriever.GetUser(vm.Model.Username, vm.Model.Password);
        }

        private void OnIncorrectCredentaials(object o, EventArgs e)
        {
            textBlockIncorrectCredentials.Visibility = Visibility.Visible;
            SetNormalState();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ErrorCallback(Exception e, string msg)
        {
            ErrorWindow w = new ErrorWindow(e, msg);
            w.Show();
            SetNormalState();
        }

        private void GetUserCallback(User user)
        {
            if (user == null)
            {
                textBlockIncorrectCredentials.Visibility = Visibility.Visible;
                SetNormalState();
            }
            else
            {
                textBlockIncorrectCredentials.Visibility = 
                    Visibility.Collapsed;
                DialogResult = true;
            }
        }

        private void SetWaitingState()
        {
            userLogonDataControl.Visibility = Visibility.Collapsed;
            circularPogressBar.Visibility = Visibility.Visible;
            okButton.IsEnabled = false;
            cancelButton.IsEnabled = false;
        }

        private void SetNormalState()
        {
            userLogonDataControl.Visibility = Visibility.Visible;
            circularPogressBar.Visibility = Visibility.Collapsed;
            okButton.IsEnabled = true;
            cancelButton.IsEnabled = true;
        }
    }
}


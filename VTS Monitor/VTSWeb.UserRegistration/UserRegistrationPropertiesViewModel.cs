using System;
using System.Windows.Input;
using VTS.Shared.DomainObjects;
using VTSWeb.Presentation.Common;

namespace VTSWeb.UserRegistration
{
    public class UserRegistrationPropertiesViewModel : ViewModelBase
    {
        private User model;
        private string password;
        private DelegateCommand furtherProcessDataCommand;
        private string loginCheckState;

        public UserRegistrationPropertiesViewModel(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            loginCheckState = "  ?";
            furtherProcessDataCommand = new DelegateCommand(
                FurtherProcessData, CanFurtherProcessData);
        }

        public ICommand FurtherProcessDataCommand
        {
            get
            {
                return furtherProcessDataCommand;
            }
        }

        public string Login
        {
            get
            {
                return model.Login;
            }
            set
            {
                model.Login = value;
                LoginCheckState = "  ?";
                OnPropertyChanged("Login");
                furtherProcessDataCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
                furtherProcessDataCommand.RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get
            {
                return model.Email;
            }
            set
            {
                model.Email = value;
                OnPropertyChanged("Email");
                furtherProcessDataCommand.RaiseCanExecuteChanged();
            }
        }

        public string Name
        {
            get
            {
                return model.Name;
            }
            set
            {
                model.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Surname
        {
            get
            {
                return model.Surname;
            }
            set
            {
                model.Surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public string Phone
        {
            get
            {
                return model.Phone;
            }
            set
            {
                model.Phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public User Model
        {
            get
            {
                return model;
            }
        }

        private bool CanFurtherProcessData(object arg)
        {
            return ValidateData();
        }

        private void FurtherProcessData(object arg)
        {

        }

        private bool ValidateData()
        {
            if (!String.IsNullOrEmpty(Login) &&
                !String.IsNullOrEmpty(Password) &&
                !String.IsNullOrEmpty(Email))
            {
                if (LoginCheckState == " OK")
                {
                    return true;
                }
            }
            return false;
        }

        public string LoginCheckState
        {
            get
            {
                return loginCheckState;
            }
            set
            {
                loginCheckState = value;
                OnPropertyChanged("LoginCheckState");
                furtherProcessDataCommand.RaiseCanExecuteChanged();
            }
        }
    }
}

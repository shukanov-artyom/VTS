using System;
using System.Windows;
using System.Windows.Input;
using Agent.Common.Instance;
using Agent.Common.Presentation.Error;
using Agent.Common.Presentation.RegistryUser;
using Agent.Localization;
using Agent.Logging;
using Agent.Network.Monitor;
using Agent.Network.Monitor.VtsWebService;
using VTS.Agent.BusinessObjects.Enums;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Common.Presentation.Users
{
    public class LogonAndRegistrationViewModel : ViewModelBase
    {
        private string username;
        private string password;
        private string email;
        private bool isLogon = true;
        private bool isRegister = false;
        private bool isWindowUiEnabled = true;
        private string messageText = String.Empty;
        private bool isUsernameValidated = false;

        public LogonAndRegistrationViewModel()
        {
            CommandForward = new DelegateCommand(GoForward, CanGoForward);
        }

        public ICommand CommandForward
        {
            get; 
            set;
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                OnPropertyChanged("Username");
                IsUsernameValidated = false;
                username = value;
                HideIncorrectCredentialsMessage();
            }
        }

        public string PasswordText
        {
            get
            {
                return password;
            }
            set
            {
                OnPropertyChanged("PasswordText");
                password = value;
                HideIncorrectCredentialsMessage();
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                OnPropertyChanged("Email");
                email = value;
            }
        }

        public bool IsUsernameValidated
        {
            get
            {
                return isUsernameValidated;
            }
            set
            {
                isUsernameValidated = value;
                OnPropertyChanged("IsUsernameValidated");
            }
        }

        public bool IsLogon
        {
            get
            {
                return isLogon;
            }
            set
            {
                isLogon = value;
                OnPropertyChanged("IsLogon");
            }
        }

        public bool IsRegister
        {
            get
            {
                return isRegister;
            }
            set
            {
                isRegister = value;
                OnPropertyChanged("IsRegister");
                OnPropertyChanged("Username");
            }
        }

        public bool IsWindowUiEnabled
        {
            get
            {
                return isWindowUiEnabled;
            }
            set
            {
                isWindowUiEnabled = value;
                OnPropertyChanged("IsWindowUiEnabled");
            }
        }

        public string MessageText
        {
            get
            {
                return messageText;
            }
            set
            {
                messageText = value;
                OnPropertyChanged("MessageText");
            }
        }

        private bool CanGoForward()
        {
            if (IsLogon)
            {
                return !String.IsNullOrEmpty(Username) && 
                    !String.IsNullOrEmpty(PasswordText);
            }
            if (IsRegister)
            {
                return !String.IsNullOrEmpty(Username) &&
                       !String.IsNullOrEmpty(PasswordText) &&
                       !String.IsNullOrEmpty(Email) &&
                       IsUsernameValidated;
            }
            throw new NotSupportedException();
        }

        private void GoForward()
        {
            if (IsRegister)
            {
                User user = new User();
                user.Login = Username;
                user.PasswordHash = Sha256Hash.Calculate(PasswordText);
                user.Role = UserRole.Partner;
                user.RegisteredDate = DateTime.Now;
                user.Email = Email;

                user.Name = "???";
                user.Phone = "???";
                user.Profile = null;
                user.Surname = "???";

                UserDto userDto = UserAssembler.FromDomainObjectToDto(user);
                IVtsWebService service = Infrastructure.Container.GetInstance<IVtsWebService>();
                try
                {
                    service.RegisterUser(userDto);
                    UserDto userDtoNew = service.AuthenticateUser(Username,
                        Sha256Hash.Calculate(PasswordText));
                    User userNew = UserAssembler.FromDtoToDomainObject(userDtoNew);
                    LoggedUserContext.LoggedUser = userNew;
                    StoredSettings.Current = userNew;
                }
                catch (Exception e)
                {
                    Log.Error(e, e.Message);
                    ErrorWindow wnd = new ErrorWindow(e.Message);
                    wnd.Owner = MainWindowKeeper.MainWindowInstance as Window;
                    wnd.ShowDialog();
                }
            }
            else if (IsLogon)
            {
                VtsWebServiceClient service = new VtsWebServiceClient();
                try
                {
                    UserDto userDto = service.AuthenticateUser(Username,
                        Sha256Hash.Calculate(PasswordText));
                    if (userDto != null)
                    {
                        User user = UserAssembler.FromDtoToDomainObject(userDto);
                        LoggedUserContext.LoggedUser = user;
                        StoredSettings.Current = user;
                    }
                    else
                    {
                        ShowIncorrectCredentialsText();
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, e.Message);
                    ErrorWindow wnd = new ErrorWindow(e.Message);
                    wnd.Owner = MainWindowKeeper.MainWindowInstance as Window;
                    wnd.ShowDialog();
                }
            }
        }

        private void ShowIncorrectCredentialsText()
        {
            MessageText = CodeBehindStringResolver.Resolve(
                "IncorrectUserCredentialsMessage");
        }

        private void HideIncorrectCredentialsMessage()
        {
            MessageText = String.Empty;
        }
    }
}

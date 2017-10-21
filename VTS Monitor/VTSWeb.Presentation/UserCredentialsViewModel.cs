using System;
using VTSWeb.Common;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation
{
    public class UserCredentialsViewModel : ViewModelBase
    {
        private UserCredentials model;

        public UserCredentialsViewModel(UserCredentials model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
        }

        public string Username
        {
            get
            {
                return model.Username;
            }
            set
            {
                model.Username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get
            {
                return model.Password;
            }
            set
            {
                model.Password = value;
                OnPropertyChanged("Password");
            }
        }

        public UserCredentials Model
        {
            get
            {
                return model;
            }
        }
    }
}

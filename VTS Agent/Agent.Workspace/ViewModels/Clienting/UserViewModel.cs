using System;
using Agent.Common.Presentation;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.ViewModels.Clienting
{
    public class UserViewModel : ViewModelBase
    {
        private readonly User model;

        public UserViewModel(User model)
        {
            this.model = model;
        }

        public string Username
        {
            get
            {
                return model.Login;
            }
        }

        public string Email
        {
            get
            {
                return model.Email;
            }
        }

        public string RegisteredDate
        {
            get
            {
                return model.RegisteredDate.ToLongDateString();
            }
        }

        public User Model
        {
            get
            {
                return model;
            }
        }
    }
}

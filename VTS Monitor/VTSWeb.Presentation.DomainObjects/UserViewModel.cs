using System;
using VTS.Shared.DomainObjects;

using VTSWeb.DomainObjects;

namespace VTSWeb.Presentation.DomainObjects
{
    public class UserViewModel : DomainObjectViewModel
    {
        private User model;

        public UserViewModel(User model)
            : base(model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
        }

        public string NameSurname
        {
            get
            {
                return String.Format("{0} {1}", model.Name, model.Surname);
            }
        }

        public string Name
        {
            get
            {
                return model.Name;
            }
        }

        public string Surname
        {
            get
            {
                return model.Surname;
            }
        }

        public string Email
        {
            get
            {
                return model.Email;
            }
        }

        public string Login
        {
            get
            {
                return model.Login;
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

        public DateTime RegisteredDate
        {
            get
            {
                return model.RegisteredDate;
            }
        }

        public UserRole Role
        {
            get
            {
                return model.Role;
            }
        }
    }
}

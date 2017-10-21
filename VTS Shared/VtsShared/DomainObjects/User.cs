using System;

namespace VTS.Shared.DomainObjects
{
    public class User : DomainObject
    {
        public User()
        {
            RegisteredDate = DateTime.Now;
        }

        public string Name
        {
            get;
            set;
        }

        public string Surname
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Login
        {
            get;
            set;
        }

        public string PasswordHash
        {
            get;
            set;
        }

        public DateTime RegisteredDate
        {
            get;
            set;
        }

        public UserRole Role
        {
            get;
            set;
        }

        public UserProfile Profile
        {
            get;
            set;
        }

        public override void CopyTo(DomainObject target)
        {
            User tgt = target as User;
            if (tgt != null)
            {
                tgt.Name = Name;
                tgt.Login = Login;
                tgt.PasswordHash = PasswordHash;
                tgt.Phone = Phone;
                tgt.Role = Role;
                tgt.Surname = Surname;
                tgt.Email = Email;
                tgt.RegisteredDate = RegisteredDate;
            }
            base.CopyTo(target);
        }
    }
}

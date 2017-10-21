using System;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects;

namespace VTSWeb.Common
{
    public class UserLoggedOnEventArgs : EventArgs
    {
        private User user;

        public UserLoggedOnEventArgs(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            this.user = user;
        }

        public User User
        {
            get
            {
                return user;
            }
        }
    }
}

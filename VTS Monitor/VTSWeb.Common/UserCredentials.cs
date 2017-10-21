using System;

namespace VTSWeb.Common
{
    public class UserCredentials
    {
        public UserCredentials()
        {
            Username = String.Empty;
            Password = String.Empty;
        }

        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }
    }
}

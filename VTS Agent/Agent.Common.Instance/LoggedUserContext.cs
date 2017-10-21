using System;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Common.Instance
{
    public static class LoggedUserContext
    {
        public static event EventHandler UserLoggedOn;
        public static event EventHandler UserLoggedOff;

        private static User loggedUser;

        public static User LoggedUser
        {
            get
            {
                return loggedUser;
            }
            set
            {
                loggedUser = value;
                if (value == null)
                {
                    if (UserLoggedOff != null)
                    {
                        UserLoggedOff.Invoke(new Object(), EventArgs.Empty);
                    }
                }
                else
                {
                    if (UserLoggedOn != null)
                    {
                        UserLoggedOn.Invoke(new Object(), EventArgs.Empty);
                    }
                }
            }
        }
    }
}

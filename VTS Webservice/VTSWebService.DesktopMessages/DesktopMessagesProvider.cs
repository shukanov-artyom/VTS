using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;

namespace VTSWebService.DesktopMessages
{
    public class DesktopMessagesProvider
    {
        private readonly User user;

        public DesktopMessagesProvider(User user)
        {
            this.user = user;
        }

        public List<string> Provide()
        {
            List<string> result = new List<string>();
            if (user.Role == UserRole.Administrator)
            {
                return result;
            }
            return result;
        }
    }
}

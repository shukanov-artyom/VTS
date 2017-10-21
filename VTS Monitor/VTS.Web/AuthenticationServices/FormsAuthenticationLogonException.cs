using System;

namespace VTS.Web.AuthenticationServices
{
    public class FormsAuthenticationLogonException : Exception
    {
        public FormsAuthenticationLogonException(string message) 
            : base(message)
        {
            
        }
    }
}

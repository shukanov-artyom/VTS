using System;
using System.ServiceModel.DomainServices.Server;
using System.Security.Principal;
using System.ServiceModel.DomainServices.Server.ApplicationServices;
using System.Web;
using System.Web.Security;

namespace VTS.Web.AuthenticationServices
{
    public abstract class FormsAuthenticationService<TUser> : DomainService,
        IAuthentication<TUser> where TUser : UserBase
    {
        protected abstract TUser GetCurrentUser(string name, string userData);

        protected virtual TUser GetDefaultUser()
        {
            return null;
        }

        protected abstract TUser ValidateCredentials(string name,
            string password, string customData, out string userData);

        public TUser GetUser()
        {
            IPrincipal currentUser = ServiceContext.User;
            if ((currentUser != null) && currentUser.Identity.IsAuthenticated)
            {
                FormsAuthenticationTicket ticket = null;
                FormsIdentity userIdentity = currentUser.Identity as FormsIdentity;
                if (userIdentity != null)
                {
                    ticket = userIdentity.Ticket;
                    if (ticket != null)
                    {
                        return GetCurrentUser(currentUser.Identity.Name,
                            ticket.UserData);
                    }
                }
            }
            return GetDefaultUser();
        }

        public TUser Login(string userName, string password, bool 
            isPersistent, string customData)
        {
            string userData;
            TUser user = ValidateCredentials(userName, password, 
                customData, out userData);
            if (user != null)
            {
                FormsAuthenticationTicket ticket =
                     new FormsAuthenticationTicket(
                    /* version */ 1, userName,
                        DateTime.Now, DateTime.Now.AddMinutes(30),
                        isPersistent,
                        userData,
                        FormsAuthentication.FormsCookiePath);
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.
                    FormsCookieName, encryptedTicket);
                HttpContextBase httpContext = (HttpContextBase)ServiceContext.
                    GetService(typeof(HttpContextBase));
                httpContext.Response.Cookies.Add(authCookie);
            }
            else
            {
                HttpContextBase httpContext = (HttpContextBase)
                    ServiceContext.GetService(typeof(HttpContextBase));
                httpContext.AddError(new FormsAuthenticationLogonException(
                    "Username or password is not correct."));
            }
            return user;
        }

        public TUser Logout()
        {
            FormsAuthentication.SignOut();
            return GetDefaultUser();
        }

        public void UpdateUser(TUser user)
        {
            throw new NotImplementedException();
        }
    }
}



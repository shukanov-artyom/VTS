using System;
using System.ServiceModel.DomainServices.Hosting;
using VTSWeb.Service;
using VTSWeb.Service.VtsWebService;

namespace VTS.Web.AuthenticationServices
{
    [EnableClientAccess()]
    public class CustomAuthenticationService : FormsAuthenticationService<UserAuthenticationDto>
    {
        protected override UserAuthenticationDto GetCurrentUser(string name, string userData)
        {
            return new UserAuthenticationDto()
            {
                Name = name,
            };
        }

        protected override UserAuthenticationDto GetDefaultUser()
        {
            return new UserAuthenticationDto()
            {
                Name = "NoName",
            };
        }

        protected override UserAuthenticationDto ValidateCredentials(string name,
            string passwordHash, string customData, out string userData)
        {
            UserAuthenticationDto user = null;
            userData = null;
            UserDto authUser = UserAuthenticator.Authenticate(name, passwordHash);
            if (authUser != null)
            {
                user = new UserAuthenticationDto();
                user.Name = authUser.Login;
                userData = authUser.Name;
            }
            return user;
        }
    }
}



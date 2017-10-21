using System;
using VTSWeb.Service.VtsWebService;

namespace VTSWeb.Service
{
    public static class UserAuthenticator
    {
        public static UserDto Authenticate(string username, string passwordHash)
        {
            VtsWebServiceClient client = new VtsWebServiceClient();
            return client.AuthenticateUser(username, passwordHash);
        }
    }
}

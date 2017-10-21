using System;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.Storage.Retrievers.Users
{
    public class UsersRetriever
    {
        public delegate void UserCallback(User user);

        private UserCallback callback;
        private ErrorCallbackDelegate errorCallback;

        public UsersRetriever(
            UserCallback callback,
            ErrorCallbackDelegate errorCallback)
        {
            this.callback = callback;
            this.errorCallback = errorCallback;
        }

        public void GetUser(string login, string password)
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.AuthenticateUserCompleted += delegate(object s, AuthenticateUserCompletedEventArgs e)
            {
                if (e.Error != null)
                {
                    ErrorCallback(e.Error, e.Error.Message);
                }
                else
                {
                    User result = UserAssembler.Assemble(e.Result);
                    GetUserCallback(result);
                }
            };
            service.AuthenticateUserAsync(login, Sha256Hash.Calculate(password));
        }

        private void GetUserCallback(User user)
        {
            if (callback != null)
            {
                callback.Invoke(user);
            }
        }

        private void ErrorCallback(Exception e, string msg)
        {
            if (errorCallback != null)
            {
                errorCallback.Invoke(e, msg);
            }
        }
    }
}

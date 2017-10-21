using System;
using System.ComponentModel;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.Common;
using VTSWeb.DomainObjects;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.UserRegistration
{
    public class UserRegistrator
    {
        public event EventHandler OnSuccess;

        private readonly User user;
        private readonly SuccessCallbackDelegate successCallback;
        private readonly ErrorCallbackDelegate errorCallback;

        public UserRegistrator(User user, SuccessCallbackDelegate successCallback,
            ErrorCallbackDelegate errorCallback)
        {
            this.errorCallback = errorCallback;
            this.successCallback = successCallback;
            this.user = user;
        }

        public void StartRegistration()
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.RegisterUserCompleted += OnUserRegistered;
            service.RegisterUserAsync(UserAssembler.Disassemble(user));
        }

        private void OnUserRegistered(object s, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                if (OnSuccess != null)
                {
                    OnSuccess.Invoke(this, EventArgs.Empty);
                }
                successCallback.Invoke();
            }
        }
    }
}

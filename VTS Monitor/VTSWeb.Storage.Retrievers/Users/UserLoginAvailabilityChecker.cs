using System;
using VTSWeb.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.Storage.Retrievers.Users
{
    public class UserLoginAvailabilityChecker
    {
        private BoolCallbackDelegate callback;

        public UserLoginAvailabilityChecker(
            BoolCallbackDelegate callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }
            this.callback = callback;
        }

        public void CheckLoginAvailability(string login)
        {
            VtsWebServiceClient webservice = new VtsWebServiceClient();
            webservice.CheckUsernameAvailabilityCompleted += OnUserNameChecked;
            webservice.CheckUsernameAvailabilityAsync(login);
        }

        private void OnUserNameChecked(object sender, 
            CheckUsernameAvailabilityCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ErrorWindow wnd = new ErrorWindow(e.Error, e.Error.Message);
                wnd.Show();
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(!e.Result);
                }
            }
        }
    }
}

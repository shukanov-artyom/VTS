using System;

namespace Portal.WebUI.Models
{
    public class LogonPageViewModel
    {
        public string EmailOrLogin { get; set; }

        public string Password { get; set; }

        public string Language { get; set; }

        public bool PreviousLogonAttemptFailed { get; set; }
    }
}
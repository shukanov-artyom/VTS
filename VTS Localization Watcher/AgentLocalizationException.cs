using System;

namespace LocalizationWatcher
{
    public class AgentLocalizationException : Exception
    {
        public AgentLocalizationException(string lang)
        {
            Language = lang;
        }

        public string Language
        {
            get;
            set;
        } 
    }
}

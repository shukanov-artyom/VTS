using System;
using System.ComponentModel;
using System.Configuration;
using Agent.Localization;

namespace Agent.Common.Presentation
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static bool debugMode = !String.IsNullOrEmpty(ConfigurationManager.AppSettings["DebugMode"]) &&
            ConfigurationManager.AppSettings["DebugMode"].Equals("true",
            StringComparison.OrdinalIgnoreCase);

        public bool DebugMode
        {
            get
            {
                return debugMode;
            }
        }

        public static bool AppDebugMode
        {
            get
            {
                return debugMode;
            }
        }

        public ViewModelBase()
        {
            TranslationManager.Instance.LanguageChanged += OnLanguageChanged;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            ChangeLanguage();
        }

        protected virtual void ChangeLanguage()
        {
            
        }
    }
}

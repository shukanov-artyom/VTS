using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using VTSWeb.Localization;

namespace VTSWeb.Presentation.Common
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static Dictionary<string, PropertyChangedEventArgs> 
            argumentInstances = new Dictionary<string,
                PropertyChangedEventArgs>();

        public string CurrentLanguageName
        {
            get
            {
                return TranslationManager.Instance.CurrentLanguage.Name;
            }
        }

        public CultureInfo CurrentLanguage
        {
            get
            {
                return TranslationManager.Instance.CurrentLanguage;
            }
        }

        public CultureInfo Language
        {
            get
            {
                return new CultureInfo("ru-RU");//TranslationManager.Instance.CurrentLanguage;
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

        protected void OnPropertyChangedAsync(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(
                    "PropertyName cannot be empty or null.");
            }

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChangedEventArgs args;
                if (!argumentInstances.TryGetValue(propertyName, out args))
                {
                    args = new PropertyChangedEventArgs(propertyName);
                    argumentInstances[propertyName] = args;
                }

                SmartDispatcher.BeginInvoke(delegate
                {
                    handler(this, args);
                });
            }
        }

        private void OnLanguageChanged(Object sender, EventArgs e)
        {
            ChangeLanguage();
        }

        protected virtual void ChangeLanguage()
        {
            OnPropertyChanged("Language");
        }
    }
}

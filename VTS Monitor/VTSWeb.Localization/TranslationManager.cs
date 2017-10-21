using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace VTSWeb.Localization
{
    public class TranslationManager
    {
        private static TranslationManager translationManager;

        public event EventHandler LanguageChanged;

        public CultureInfo CurrentLanguage
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value != Thread.CurrentThread.CurrentUICulture)
                {
                    Thread.CurrentThread.CurrentUICulture = value;
                    OnLanguageChanged();
                }
            }
        }

        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                if (TranslationProvider != null)
                {
                    return TranslationProvider.Languages;
                }
                return Enumerable.Empty<CultureInfo>();
            }
        }

        public static TranslationManager Instance
        {
            get
            {
                if (translationManager == null)
                    translationManager = new TranslationManager();
                return translationManager;
            }
        }

        public ITranslationProvider TranslationProvider
        {
            get; 
            set;
        }

        private void OnLanguageChanged()
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.
                CurrentUICulture = Instance.CurrentLanguage;
            if (LanguageChanged != null)
            {
                LanguageChanged(this, EventArgs.Empty);
            }
        }

        public object Translate(string key)
        {
            if (TranslationProvider != null)
            {
                object translatedValue = TranslationProvider.Translate(key);
                if (translatedValue != null)
                {
                    return translatedValue;
                }
            }
            return string.Format("!{0}!", key);
        }
    }
}

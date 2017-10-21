using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using VTS.Shared;

namespace Agent.Localization
{
    public class TranslationManager
    {
        private static TranslationManager translationManager;

        public event EventHandler LanguageChanged;

        public static CultureInfo CurrentCulture
        {
            get
            {
                return Instance.CurrentLanguage;
            }
        }

        public CultureInfo CurrentLanguage
        {
            get { return Thread.CurrentThread.CurrentCulture; }
            set
            {
                if (value != Thread.CurrentThread.CurrentUICulture)
                {
                    Thread.CurrentThread.CurrentUICulture = value;
                    Thread.CurrentThread.CurrentCulture = value;
                    OnLanguageChanged();
                }
            }
        }

        public SupportedLanguage CurrentLanguageEnum
        {
            get
            {
                return Convert(CurrentLanguage);
            }
            set
            {
                CurrentLanguage = Convert(value);
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
                {
                    translationManager = new TranslationManager();
                }
                return translationManager;
            }
        }

        public ITranslationProvider TranslationProvider { get; set; }

        private void OnLanguageChanged()
        {
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

        public static CultureInfo Convert(SupportedLanguage lang)
        {
            switch (lang)
            {
                case SupportedLanguage.Belarusian:
                    return new CultureInfo("be-BY");
                case SupportedLanguage.English:
                    return new CultureInfo("en-GB");
                case SupportedLanguage.Russian:
                    return new CultureInfo("ru-RU");
                default:
                    throw new NotSupportedException("Language not supported");
            }
        }

        public static SupportedLanguage Convert(CultureInfo info)
        {
            switch (info.TwoLetterISOLanguageName)
            {
                case "en":
                    return SupportedLanguage.English;
                case "ru":
                    return SupportedLanguage.Russian;
                case "be":
                    return SupportedLanguage.Belarusian;
                default:
                    throw new NotSupportedException("LanguageNotSupported");
            }
        }
    }
}

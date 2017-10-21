using System.Globalization;
using Agent.Common.Presentation;
using VTS.Shared;

namespace Agent.Localization
{
    public class LanguageViewModel : ViewModelBase
    {
        private readonly string locale;
        private readonly CultureInfo loc;

        public LanguageViewModel(CultureInfo loc)
        {
            locale = FixLocaleName(loc.NativeName);
            this.loc = loc;
        }

        public string Locale
        {
            get
            {
                return locale;
            }
        }

        public CultureInfo Model
        {
            get
            {
                return loc;
            }
        }

        public SupportedLanguage ModelEnum
        {
            get
            {
                return TranslationManager.Convert(Model);
            }
        }

        private static string FixLocaleName(string incorrect)
        {
            switch (incorrect)
            {
                case "English":
                    return "English";
                case "Беларускі":
                    return "Беларуская";
                case "русский":
                    return "Русский";
                default:
                    return incorrect;
            }
        }
    }
}

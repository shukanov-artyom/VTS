using System;
using VTS.Shared;

namespace VTSWeb.Common
{
    public class LanguageMapper
    {
        public static SupportedLanguage Map(string langName)
        {
            if (langName == "ru_RU" ||
                langName == "ru" || 
                langName == "ru-RU")
            {
                return SupportedLanguage.Russian;
            }
            if (langName == "en_EN" ||
                langName == "en_GB" ||
                langName == "en-GB" ||
                langName == "en")
            {
                return SupportedLanguage.English;
            }
            if (langName == "be_BY" ||
                langName == "be_BE" ||
                langName == "be-BE" ||
                langName == "be-BY" ||
                langName == "be")
            {
                return SupportedLanguage.Belarusian;
            }
            throw new NotSupportedException();
        }
    }
}

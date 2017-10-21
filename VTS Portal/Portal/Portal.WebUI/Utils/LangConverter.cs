using System;
using VTS.Shared;

namespace Portal.WebUI.Utils
{
    public static class LangConverter
    {
        public static SupportedLanguage Convert(string lang)
        {
            if (lang.Equals("Ru", StringComparison.InvariantCultureIgnoreCase))
            {
                return SupportedLanguage.Russian;
            }
            if (lang.Equals("By", StringComparison.InvariantCultureIgnoreCase) ||
                lang.Equals("Be", StringComparison.InvariantCultureIgnoreCase))
            {
                return SupportedLanguage.Belarusian;
            }
            if (lang.Equals("En", StringComparison.InvariantCultureIgnoreCase))
            {
                return SupportedLanguage.English;
            }
            throw new NotSupportedException("Language not supported.");
        }

        public static SupportedLanguage ConvertForCharacteristics(string lang)
        {
            SupportedLanguage result = Convert(lang);
            if (result == SupportedLanguage.Belarusian)
            {
                return SupportedLanguage.Russian;
            }
            return result;
        }
    }
}
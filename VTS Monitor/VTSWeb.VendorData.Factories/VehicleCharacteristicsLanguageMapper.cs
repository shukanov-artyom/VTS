using VTSWeb.Localization;

namespace VTSWeb.VendorData.Factories
{
    public class VehicleCharacteristicsLanguageMapper
    {
        public static string GetForRequest(string lang)
        {
            return Map(lang);
        }

        private  static string Map(string currentLang)
        {
            if (currentLang == "ru_RU" ||
                currentLang == "ru" ||
                currentLang == "be")
            {
                return "ru_RU";
            }
            if (currentLang == "en_EN" ||
                currentLang == "en_GB" ||
                currentLang == "en")
            {
                return "en_GB";
            }
            return "en_GB";
        }
    }
}

using System;
using VTS.Shared;

namespace VTSWebService.VendorInfo
{
    internal class CharacteristicsLanguageMapper
    {
        private readonly Manufacturer manufacturer;

        public CharacteristicsLanguageMapper(Manufacturer manufacturer)
        {
            this.manufacturer = manufacturer;
        }

        public string GetSpecificCode(SupportedLanguage lang)
        {
            switch (manufacturer)
            {
                case Manufacturer.Citroen:
                case Manufacturer.Peugeot:
                    return GetForPsa(lang);
                case Manufacturer.Opel:
                    return GetForOpel(lang);
                default:
                    throw new NotSupportedException("Manufacturer not supported");
            }
        }

        private string GetForPsa(SupportedLanguage lang)
        {
            if (lang == SupportedLanguage.Russian ||
                lang == SupportedLanguage.Belarusian)
            {
                return "ru_RU";
            }
            if (lang == SupportedLanguage.English)
            {
                return "en_GB";
            }
            throw new NotSupportedException("Language not supported");
        }

        private string GetForOpel(SupportedLanguage lang)
        {
            throw new NotImplementedException();
        }
    }
}

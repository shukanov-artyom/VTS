using System;

namespace VTS.Site.DomainObjects.VendorData
{
    public class LocalizedCharacteristicsStrings
    {
        public const string GeneralInformationEn = "General information";
        public const string GeneralInformationRu = "Общая информация";

        public const string EngineFamilyEn = "ENGINE (TYPE)";
        public const string EngineFamilyRu = "ДВИГАТЕЛЬ (ТИП)";

        private const string EngineModelRu = "ДВИГАТЕЛЬ";
        private const string EngineModelEn = "ENGINE";

        private const string VehicleModelGroupEn = "Vehicle characteristics";
        private const string VehicleModelGroupRu = "Характеристики автомобиля";

        private const string VehicleModelEn = "VEHICLE FAMILY";
        private const string VehicleModelRuCitroen = "Группа товаров";
        private const string VehicleModelRuPeugeot = "Модель";

        public static string ResolveGeneralInformationGroupName(string lang)
        {
            if (lang == "en_GB")
            {
                return GeneralInformationEn;
            }
            else if (lang == "ru_RU")
            {
                return GeneralInformationRu;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static string ResolveEngineFamilyKey(string lang)
        {
            if (lang == "en_GB")
            {
                return EngineFamilyEn;
            }
            else if (lang == "ru_RU")
            {
                return EngineFamilyRu;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static string ResolveEngineModelKey(string lang)
        {
            if (lang == "en_GB")
            {
                return EngineModelEn;
            }
            else if (lang == "ru_RU")
            {
                return EngineModelRu;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static string ResolveVehicleModelKey(VehicleCharacteristics c)
        {
            if (c.Language == "en_GB")
            {
                return VehicleModelEn;
            }
            else if (c.Language == "ru_RU")
            {
                throw new NotSupportedException();
                /*return VehicleModelRuCitroen;
                return VehicleModelRuPeugeot;*/
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static string ResolveVehicleModelGroupKey(string lang)
        {
            if (lang == "en_GB")
            {
                return VehicleModelGroupEn;
            }
            else if (lang == "ru_RU")
            {
                return VehicleModelGroupRu;
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}

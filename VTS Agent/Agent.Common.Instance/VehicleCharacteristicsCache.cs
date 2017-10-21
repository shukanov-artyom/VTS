using System;
using System.Collections.Generic;
using Agent.Localization;
using Agent.Logging;
using Agent.Network.Monitor;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Common.Instance
{
    public static class VehicleCharacteristicsCache
    {
        private static IDictionary<string, IDictionary<SupportedLanguage, VehicleCharacteristics>> cache =
            new Dictionary<string, IDictionary<SupportedLanguage, VehicleCharacteristics>>();

        public static VehicleCharacteristics Get(string vin)
        {
            SupportedLanguage currentLang = TranslationManager.Instance.CurrentLanguageEnum;
            string vinU = vin.ToUpper();
            if (cache.ContainsKey(vinU) &&
                cache[vinU].ContainsKey(currentLang))
            {
                return cache[vinU][currentLang];
            }
            else
            {
                VtsWebServiceClient client = new VtsWebServiceClient();
                try
                {
                    VehicleCharacteristicsDto resultDto = client.GetVehicleCharacteristics(vinU, 
                        (int)TranslationManager.Instance.CurrentLanguageEnum);
                    VehicleCharacteristics result = VehicleCharacteristicsAssembler.FromDtoToDomainObject(resultDto);
                    if (result == null)
                    {
                        return null;
                    }

                    if (!cache.ContainsKey(vinU))
                    {
                        cache[vinU] = new Dictionary<SupportedLanguage, VehicleCharacteristics>();
                    }
                    cache[vinU][currentLang] = result;
                    return cache[vinU][currentLang];
                }
                catch (Exception e)
                {
                    Log.Error(e, CodeBehindStringResolver.Resolve("CouldNotGetVehicleCharacteristicsMessage"));
                    return null;
                }
            }
        }
    }
}

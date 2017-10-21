using System;
using System.Collections.Generic;
using Agent.Logging;
using Agent.Network.Monitor;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Common.Instance
{
    public static class StatisticsCache
    {
        private static readonly Dictionary<string, StatisticsPerVehicleSubCache> Cache = 
            new Dictionary<string, StatisticsPerVehicleSubCache>();

        public static List<AnalyticStatisticsValue> GetTypedForVehicle(
            AnalyticRuleType type, string vin)
        {
            string vinU = vin.ToUpper();
            CheckCacheForVehicleAndTypeFillIfRequired(vinU, type);
            return Cache[vin].Get(type);
        }

        public static StatisticsPerVehicleSubCache GetSubCacheForVehicle(string vin)
        {
            string vinU = vin.ToUpper();
            CheckCacheForVehicleFillIfRequired(vinU);
            return Cache[vinU];
        }

        private static void CheckCacheForVehicleFillIfRequired(string vinU)
        {
            if (!Cache.ContainsKey(vinU))
            {
                Cache[vinU] = new StatisticsPerVehicleSubCache();
            }
            try
            {
                foreach (AnalyticRuleType type in AnalyticRuleSettingsCache.GetAvailableTypes(vinU))
                {
                    CheckCacheForVehicleAndTypeFillIfRequired(vinU, type);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Can not get available analytic types for vehicle.");
                throw;
            }
        }

        private static void CheckCacheForVehicleAndTypeFillIfRequired(
            string vinU, AnalyticRuleType type)
        {
            if (!Cache.ContainsKey(vinU) ||
                (Cache.ContainsKey(vinU) && Cache[vinU].Get(type) == null))
            {
                lock (Cache)
                {
                    if (!Cache.ContainsKey(vinU))
                    {
                        Cache[vinU] = new StatisticsPerVehicleSubCache();
                    }
                    Cache[vinU].Set(GetFromService(type, vinU), type);
                }
            }
        }

        private static List<AnalyticStatisticsValue> GetFromService(
            AnalyticRuleType type, string vin)
        {
            try
            {
                List<AnalyticStatisticsValue> result = new List<AnalyticStatisticsValue>();
                VtsWebServiceClient service = new VtsWebServiceClient();
                foreach (AnalyticStatisticsValueDto dto in 
                    service.GetAnalyticStatisticsPerVehicle(vin, (int)type))
                {
                    result.Add(AnalyticStatisticsValueAssembler.FromDtoToDomainObject(dto));
                }
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e, "Could not fill Analytic statistics cache");
                return new List<AnalyticStatisticsValue>();
            }
        }
    }
}

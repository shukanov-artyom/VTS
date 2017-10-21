using System;
using System.Collections.Generic;
using Agent.Logging;
using Agent.Network.Monitor;
using Agent.Network.Monitor.VtsWebService;
using VTS.Agent.BusinessObjects;
using VTS.Agent.BusinessObjects.Enums;
using VTS.Shared;

namespace Agent.Common.Instance
{
    public static class AnalyticRuleSettingsCache
    {
        private static readonly Dictionary<string, List<AnalyticRuleType>> RuleTypesCache = 
            new Dictionary<string, List<AnalyticRuleType>>();
        private static readonly Dictionary<string, List<AnalyticRuleSettings>> RuleSettingsForVinCache =
            new Dictionary<string, List<AnalyticRuleSettings>>();

        public static List<AnalyticRuleType> GetAvailableTypes(string vin)
        {
            string vinU = vin.ToUpper();
            if (!RuleTypesCache.ContainsKey(vinU))
            {
                try
                {
                    RuleTypesCache[vinU] = new List<AnalyticRuleType>();
                    VtsWebServiceClient service = new VtsWebServiceClient();
                    foreach (int ruleTypeInt in 
                        service.GetAvailableAnalyticStatisticsTypesForVehicle(vinU))
                    {
                        RuleTypesCache[vinU].Add((AnalyticRuleType)ruleTypeInt);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Can not fillavailable analytic rules cache for vehicle.");
                }
            }
            return RuleTypesCache[vinU];
        }

        public static List<AnalyticRuleSettings> GetSettings(string vin)
        {
            string vinU = vin.ToUpper();

            if (!RuleSettingsForVinCache.ContainsKey(vinU))
            {
                try
                {
                    VtsWebServiceClient service = new VtsWebServiceClient();
                    List<AnalyticRuleSettings> result = new List<AnalyticRuleSettings>();
                    foreach (AnalyticRuleSettingsDto dto in service.GetAllSettingsForVehicle(vinU))
                    {
                        result.Add(AnalyticRuleSettingsAssembler.FromDtoToDomainObject(dto));
                    }
                    RuleSettingsForVinCache[vinU] = result;
                }
                catch (Exception e)
                {
                    Log.Error(e, "Can not get settings.");
                    return new List<AnalyticRuleSettings>(); // empty
                }
            }
            return RuleSettingsForVinCache[vinU];
        }
    }
}

using System;
using System.Collections.Generic;
using VTS.Agent.BusinessObjects;
using VTS.Agent.BusinessObjects.Enums;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Common.Instance
{
    public class StatisticsPerVehicleSubCache
    {
        private readonly Dictionary<AnalyticRuleType, List<AnalyticStatisticsValue>> cacheInternal =
            new Dictionary<AnalyticRuleType, List<AnalyticStatisticsValue>>();

        public List<DateTime> GetDatesOfDataUnits()
        {
            List<DateTime> result = new List<DateTime>();
            foreach (KeyValuePair<AnalyticRuleType, List<AnalyticStatisticsValue>> pair in cacheInternal)
            {
                foreach(AnalyticStatisticsValue v in pair.Value)
                {
                    if (!result.Contains(v.SourceDataCaptureDateTime))
                    {
                        result.Add(v.SourceDataCaptureDateTime);
                    }
                }
            }
            result.Sort();
            return result;
        }

        public IEnumerable<AnalyticRuleType> GetAvailableTypes()
        {
            foreach (KeyValuePair<AnalyticRuleType, List<AnalyticStatisticsValue>> pair in cacheInternal)
            {
                yield return pair.Key;
            }
        }

        public List<AnalyticStatisticsValue> Get(AnalyticRuleType type)
        {
            if (cacheInternal.ContainsKey(type))
            {
                return cacheInternal[type];
            }
            return null;
        }

        public void Set(List<AnalyticStatisticsValue> values, AnalyticRuleType targetType)
        {
            cacheInternal[targetType] = values;
        }
    }
}

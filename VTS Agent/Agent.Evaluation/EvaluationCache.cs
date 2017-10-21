using System;
using System.Collections.Generic;
using Agent.Common.Instance;

namespace Agent.Evaluation
{
    public static class EvaluationCache
    {
        private static readonly Dictionary<string, VehicleEvaluationGrid> Cache = 
            new Dictionary<string, VehicleEvaluationGrid>();

        public static VehicleEvaluationGrid Get(string vin)
        {
            string vinU = vin.ToUpper();
            if (!Cache.ContainsKey(vinU))
            {
                Cache[vinU] = new VehicleEvaluationGrid(vinU,
                    StatisticsCache.GetSubCacheForVehicle(vinU),
                    AnalyticRuleSettingsCache.GetSettings(vinU));
            }
            return Cache[vinU];
        }
    }
}

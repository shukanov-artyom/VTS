using System;
using System.Collections.Generic;
using VTS.Shared;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Statistics.Tools
{
    public static class CylinderNumberToCoilRuleTypeMapper
    {
        private static IDictionary<AnalyticRuleType, int> mapping =
            new Dictionary<AnalyticRuleType, int>();

        static CylinderNumberToCoilRuleTypeMapper()
        {
            mapping.Add(AnalyticRuleType.Cylinder1CoilChargeTime1000Rpm, 1);
            mapping.Add(AnalyticRuleType.Cylinder1CoilChargeTime2000Rpm, 1);
            mapping.Add(AnalyticRuleType.Cylinder1CoilChargeTime3000Rpm, 1);

            mapping.Add(AnalyticRuleType.Cylinder2CoilChargeTime1000Rpm, 2);
            mapping.Add(AnalyticRuleType.Cylinder2CoilChargeTime2000Rpm, 2);
            mapping.Add(AnalyticRuleType.Cylinder2CoilChargeTime3000Rpm, 2);

            mapping.Add(AnalyticRuleType.Cylinder3CoilChargeTime1000Rpm, 3);
            mapping.Add(AnalyticRuleType.Cylinder3CoilChargeTime2000Rpm, 3);
            mapping.Add(AnalyticRuleType.Cylinder3CoilChargeTime3000Rpm, 3);

            mapping.Add(AnalyticRuleType.Cylinder4CoilChargeTime1000Rpm, 4);
            mapping.Add(AnalyticRuleType.Cylinder4CoilChargeTime2000Rpm, 4);
            mapping.Add(AnalyticRuleType.Cylinder4CoilChargeTime3000Rpm, 4);
        }

        public static int Map(AnalyticRuleType type)
        {
            return mapping[type];
        }
    }
}

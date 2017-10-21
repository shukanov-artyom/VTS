using System.Collections.Generic;
using VTS.Shared;


namespace VTSWeb.AnalysisCore.Common.PetrolEngineIgnition
{
    public static class RpmToCoilRuleTypeMapper
    {
        private static IDictionary<AnalyticRuleType, CheckpointRpm> mapping =
            new Dictionary<AnalyticRuleType, CheckpointRpm>();

        static RpmToCoilRuleTypeMapper()
        {
            mapping.Add(AnalyticRuleType.Cylinder1CoilChargeTime1000Rpm, CheckpointRpm.Rpm1000);
            mapping.Add(AnalyticRuleType.Cylinder1CoilChargeTime2000Rpm, CheckpointRpm.Rpm2000);
            mapping.Add(AnalyticRuleType.Cylinder1CoilChargeTime3000Rpm, CheckpointRpm.Rpm3000);

            mapping.Add(AnalyticRuleType.Cylinder2CoilChargeTime1000Rpm, CheckpointRpm.Rpm1000);
            mapping.Add(AnalyticRuleType.Cylinder2CoilChargeTime2000Rpm, CheckpointRpm.Rpm2000);
            mapping.Add(AnalyticRuleType.Cylinder2CoilChargeTime3000Rpm, CheckpointRpm.Rpm3000);

            mapping.Add(AnalyticRuleType.Cylinder3CoilChargeTime1000Rpm, CheckpointRpm.Rpm1000);
            mapping.Add(AnalyticRuleType.Cylinder3CoilChargeTime2000Rpm, CheckpointRpm.Rpm2000);
            mapping.Add(AnalyticRuleType.Cylinder3CoilChargeTime3000Rpm, CheckpointRpm.Rpm3000);

            mapping.Add(AnalyticRuleType.Cylinder4CoilChargeTime1000Rpm, CheckpointRpm.Rpm1000);
            mapping.Add(AnalyticRuleType.Cylinder4CoilChargeTime2000Rpm, CheckpointRpm.Rpm2000);
            mapping.Add(AnalyticRuleType.Cylinder4CoilChargeTime3000Rpm, CheckpointRpm.Rpm3000);
        }

        public static CheckpointRpm Map(AnalyticRuleType type)
        {
            return mapping[type];
        }
    }
}

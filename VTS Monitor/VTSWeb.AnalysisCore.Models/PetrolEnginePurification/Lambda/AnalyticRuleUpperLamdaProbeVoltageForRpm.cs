using System;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Models.Settings;


namespace VTSWeb.AnalysisCore.Models.PetrolEnginePurification.Lambda
{
    public class AnalyticRuleUpperLamdaProbeVoltageForRpm : AnalyticRuleLambdaProbeVoltageForRpmBase
    {
        public AnalyticRuleUpperLamdaProbeVoltageForRpm(
            AnalyticRuleSettings settings)
            : base(settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            RegisterRequiredParameter(PsaParameterType.EngineRpm);
            RegisterRequiredParameter(PsaParameterType.UpperOxygenSensorVoltage);
        }

        protected override PsaParameterType GetOxygenSensorParameter()
        {
            return PsaParameterType.UpperOxygenSensorVoltage;
        }

        protected override CheckpointRpm GetTargetRpm()
        {
            return RuleTypeToRpm.Map(Type);
        }
    }
}

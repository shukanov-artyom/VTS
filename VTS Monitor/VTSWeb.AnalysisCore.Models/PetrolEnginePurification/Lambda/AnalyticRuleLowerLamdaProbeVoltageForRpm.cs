using System;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Models.Settings;


namespace VTSWeb.AnalysisCore.Models.PetrolEnginePurification.Lambda
{
    public class AnalyticRuleLowerLamdaProbeVoltageForRpm :
        AnalyticRuleLambdaProbeVoltageForRpmBase
    {
        private AnalyticRuleSettings settings;

        public  AnalyticRuleLowerLamdaProbeVoltageForRpm(
            AnalyticRuleSettings settings)
            : base(settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            this.settings = settings;
            RegisterRequiredParameter(PsaParameterType.EngineRpm);
            RegisterRequiredParameter(PsaParameterType.LowerOxygenVoltage);
        }

        protected override PsaParameterType GetOxygenSensorParameter()
        {
            return PsaParameterType.LowerOxygenVoltage;
        }

        protected override CheckpointRpm GetTargetRpm()
        {
            return RuleTypeToRpm.Map(Type);
        }
    }
}

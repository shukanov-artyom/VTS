using System;
using System.Linq;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Recognition;

namespace VTSWeb.AnalysisCore.Statistics.Generation.PetrolEnginePurification.LambdaProbes
{
    public class FitterUpperLambdaVoltage : FitterLambdaVoltageBase
    {
        public FitterUpperLambdaVoltage(VehicleInformation info, AnalyticRuleType type)
            : base(info, RuleTypeToRpm.Map(type), type)
        {
        }

        public override bool Fits(PsaParametersSet set)
        {
            bool hasRpm = set.Parameters.Any(p =>
                p.Type == PsaParameterType.EngineRpm);
            bool hasHigherLambsaVoltageData = set.Parameters.Any(
                p => p.Type == PsaParameterType.UpperOxygenSensorVoltage);
            return hasRpm && hasHigherLambsaVoltageData;
        }

        public override PsaParameterData GetRequiredDependentData(PsaParametersSet set)
        {
            return set.GetParameterOfType(PsaParameterType.UpperOxygenSensorVoltage);
        }
    }
}

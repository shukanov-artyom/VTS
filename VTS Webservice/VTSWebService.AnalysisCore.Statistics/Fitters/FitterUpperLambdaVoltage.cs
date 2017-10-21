using System;
using System.Linq;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Statistics.Fitters
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

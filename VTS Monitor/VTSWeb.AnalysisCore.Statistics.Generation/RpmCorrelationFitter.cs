using System;
using System.Collections.Generic;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.AnalysisCore.Tools;

using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.AnalysisCore.Statistics.Generation
{
    public class RpmCorrelationFitter : IFitter
    {
        private readonly PsaParameterType baseParameter;
        private readonly PsaParameterType dependentParameter;
        private readonly int baseParamDiffTresholdPercentage;
        private readonly VehicleInformation info;
        private readonly AnalyticRuleType ruleType;

        public RpmCorrelationFitter(
            PsaParameterType dependentParameter, 
            int baseParamDiffTresholdPercentage,
            VehicleInformation info,
            AnalyticRuleType ruleType)
        {
            this.baseParameter = PsaParameterType.EngineRpm;
            this.dependentParameter = dependentParameter;
            this.baseParamDiffTresholdPercentage = baseParamDiffTresholdPercentage;
            this.info = info;
            this.ruleType = ruleType;
        }

        public bool Fits(PsaParametersSet set)
        {
            return set.HasParameterOfType(baseParameter) &&
                set.HasParameterOfType(dependentParameter);
        }

        public AnalyticStatisticsItem Get(PsaParametersSet set,
            DateTime sourceDataCaptureDateTime)
        {
            AnalyticStatisticsItem result = new AnalyticStatisticsItem(ruleType,
                info.Engine.Family.Type, info.Engine.Type);
            IList<double> baseValues = 
                set.GetParameterOfType(baseParameter).GetDoubles();
            IList<double> dependantValues =
                set.GetParameterOfType(dependentParameter).GetDoubles();
            CorrelatedMedianExtractor extractor = 
                new CorrelatedMedianExtractor(baseValues, 
                    dependantValues, baseParamDiffTresholdPercentage);
            double targetRpm = (int)RuleTypeToRpm.Map(ruleType);
            double value = extractor.GetForBaseValue(targetRpm);
            if (!double.IsNaN(value))
            {
                result.Values.Add(new AnalyticStatisticsValue(
                    value, info.Vin, set.Id, sourceDataCaptureDateTime));
            }
            return result;
        }
    }
}

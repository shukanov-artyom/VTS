using System;
using System.Collections.Generic;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Common;
using VTSWebService.AnalysisCore.Enums;
using VTSWebService.AnalysisCore.Statistics.Tools;

namespace VTSWebService.AnalysisCore.Statistics.Fitters
{
    public class FitterCylinderCoilChargeTimeForRpm : IFitter
    {
        private readonly VehicleInformation info;
        private readonly AnalyticRuleType ruleType;

        public FitterCylinderCoilChargeTimeForRpm(
            VehicleInformation info, AnalyticRuleType ruleType)
        {
            this.info = info;
            this.ruleType = ruleType;
        }

        public bool Fits(PsaParametersSet set)
        {
            PsaParameterType requiredType = GetRequiredType();
            bool hasRequiredType = set.HasParameterOfType(requiredType);
            bool hasRpmData = set.HasParameterOfType(PsaParameterType.EngineRpm);
            return hasRequiredType && hasRpmData;
        }

        public AnalyticStatisticsItem Get(
            PsaParametersSet set, DateTime sourceCaptureTime)
        {
            AnalyticStatisticsItem result = new AnalyticStatisticsItem(
                ruleType, info.Engine.Family.Type, info.Engine.Type);
            IList<double> rpmData = set.GetParameterOfType(PsaParameterType.EngineRpm).GetDoubles();
            IList<double> chargeTimeData = set.GetParameterOfType(GetRequiredType()).GetDoubles();
            CorrelatedMedianExtractor extractor = new CorrelatedMedianExtractor(rpmData, chargeTimeData, 5);
            double value = extractor.GetForBaseValue(Rpm);
            if (!double.IsNaN(value))
            {
                result.Values.Add(new AnalyticStatisticsValue(
                    value, info.Vin, set.Id, sourceCaptureTime));
            }
            return result;
        }

        private int Rpm
        {
            get
            {
                return (int)RpmToCoilRuleTypeMapper.Map(ruleType);
            }
        }

        private PsaParameterType GetRequiredType()
        {
            int cyl = CylinderNumberToCoilRuleTypeMapper.Map(ruleType);
            return ModelRelatedPsaParameters.Get(cyl);
        }
    }
}

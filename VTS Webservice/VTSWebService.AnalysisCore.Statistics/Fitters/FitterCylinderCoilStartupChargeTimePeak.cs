using System;
using System.Collections.Generic;
using System.Linq;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;
using VTSWebService.AnalysisCore.Statistics.Tools;

namespace VTSWebService.AnalysisCore.Statistics.Fitters
{
    public class FitterCylinderCoilStartupChargeTimePeak : IFitter
    {
        private readonly VehicleInformation info;
        private readonly AnalyticRuleType ruleType;

        public FitterCylinderCoilStartupChargeTimePeak(
            VehicleInformation info, AnalyticRuleType ruleType)
        {
            this.info = info;
            this.ruleType = ruleType;
        }

        private PsaParameterType RequiredType
        {
            get
            {
                if (ruleType == AnalyticRuleType.Cylinder1CoilStartupChargeTimePeak)
                {
                    return PsaParameterType.CylinderCoilChargeTime1;
                }
                if (ruleType == AnalyticRuleType.Cylinder2CoilStartupChargeTimePeak)
                {
                    return PsaParameterType.CylinderCoilChargeTime2;
                }
                if (ruleType == AnalyticRuleType.Cylinder3CoilStartupChargeTimePeak)
                {
                    return PsaParameterType.CylinderCoilChargeTime3;
                }
                if (ruleType == AnalyticRuleType.Cylinder4CoilStartupChargeTimePeak)
                {
                    return PsaParameterType.CylinderCoilChargeTime4;
                }
                throw new NotSupportedException();
            }
        }

        public bool Fits(PsaParametersSet set)
        {
            bool hasRpm = set.HasParameterOfType(PsaParameterType.EngineRpm);
            bool hasRequiredType = set.HasParameterOfType(RequiredType);
            return hasRpm && hasRequiredType;
        }

        public AnalyticStatisticsItem Get(PsaParametersSet set,
            DateTime sourceDataCaptureTime)
        {
            AnalyticStatisticsItem result = new AnalyticStatisticsItem(
                ruleType, info.Engine.Family.Type, info.Engine.Type);
            IList<double> rpmData = set.GetParameterOfType(PsaParameterType.EngineRpm).GetDoubles();
            IList<double> requiredData = set.GetParameterOfType(RequiredType).GetDoubles();
            EngineStartupDetector detector = new EngineStartupDetector(rpmData);
            if (!detector.EngineStartupDetected())
            {
                return result;
            }
            IList<int> startupIndexes = detector.GetEngineStartupPointIndexes();
            foreach (int startupIndex in startupIndexes)
            {
                double res = StartupRegionExtractor.Extract(startupIndex, requiredData).Max();
                result.Values.Add(new AnalyticStatisticsValue(
                    res, info.Vin, set.Id, sourceDataCaptureTime));
            }
            return result;
        }
    }
}

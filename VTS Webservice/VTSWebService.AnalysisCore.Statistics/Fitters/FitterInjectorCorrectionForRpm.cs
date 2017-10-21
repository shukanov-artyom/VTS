using System;
using System.Linq;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Common;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Statistics.Fitters
{
    public class FitterInjectorCorrectionForRpm : IFitter
    {
        private const int RpmCorrelationThresholdPercentage = 5;

        private readonly VehicleInformation info;
        private readonly AnalyticRuleType type;

        public FitterInjectorCorrectionForRpm(
            VehicleInformation info, AnalyticRuleType type)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            this.info = info;
            this.type = type;
        }

        protected VehicleInformation VehicleInfo
        {
            get
            {
                return info;
            }
        }

        protected AnalyticRuleType Type
        {
            get
            {
                return type;
            }
        }

        public bool Fits(PsaParametersSet set)
        {
            bool hasRpm = set.Parameters.Any(p =>
                p.Type == PsaParameterType.EngineRpm);
            bool hasInjectorData = set.Parameters.Any(
                p =>
                p.Type == PsaParameterType.Injector1Correction ||
                p.Type == PsaParameterType.Injector2Correction ||
                p.Type == PsaParameterType.Injector3Correction ||
                p.Type == PsaParameterType.Injector4Correction);
            return hasRpm && hasInjectorData;
        }

        public AnalyticStatisticsItem Get(
            PsaParametersSet set, DateTime sourceDataCaptureDateTime)
        {
            AnalyticStatisticsItem result = new AnalyticStatisticsItem(Type,
                VehicleInfo.Engine.Family.Type,
                VehicleInfo.Engine.Type);
            int injectorNumber = GetInjectorNumber(Type);
            ExtractForInjector(set, result,
                injectorNumber, sourceDataCaptureDateTime);
            return result;
        }

        private void ExtractForInjector(PsaParametersSet set,
            AnalyticStatisticsItem result, int injectorNumber,
            DateTime sourceDataCaptureDateTime)
        {
            PsaParameterData forThisInjector =
                set.GetCertainInjectorCorrections(injectorNumber);
            if (forThisInjector == null)
            {
                return;
            }
            PsaParameterData rpmData =
                set.GetParameterOfType(PsaParameterType.EngineRpm);
            CorrelatedMedianExtractor extractor =
                new CorrelatedMedianExtractor(
                    rpmData.GetDoubles(), forThisInjector.GetDoubles(),
                    RpmCorrelationThresholdPercentage);
            double res = extractor.GetForBaseValue((int)RuleTypeToRpm.Map(type));
            if (double.IsNaN(res))
            {
                return;
            }
            // becuse we need correction's absolute value, it can be negative
            double abs = Math.Abs(res);
            result.Values.Add(new AnalyticStatisticsValue(
                abs, info.Vin, set.Id, sourceDataCaptureDateTime));
        }

        private int GetInjectorNumber(AnalyticRuleType ruleType)
        {
            switch (ruleType)
            {
                case AnalyticRuleType.Injector1CorrectionAt1000Rpm:
                case AnalyticRuleType.Injector1CorrectionAt2000Rpm:
                case AnalyticRuleType.Injector1CorrectionAt3000Rpm:
                    return 1;
                case AnalyticRuleType.Injector2CorrectionAt1000Rpm:
                case AnalyticRuleType.Injector2CorrectionAt2000Rpm:
                case AnalyticRuleType.Injector2CorrectionAt3000Rpm:
                    return 2;
                case AnalyticRuleType.Injector3CorrectionAt1000Rpm:
                case AnalyticRuleType.Injector3CorrectionAt2000Rpm:
                case AnalyticRuleType.Injector3CorrectionAt3000Rpm:
                    return 3;
                case AnalyticRuleType.Injector4CorrectionAt1000Rpm:
                case AnalyticRuleType.Injector4CorrectionAt2000Rpm:
                case AnalyticRuleType.Injector4CorrectionAt3000Rpm:
                    return 4;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}

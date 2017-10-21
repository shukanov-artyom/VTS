using System;
using System.Linq;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.AnalysisCore.Tools;

using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.AnalysisCore.Statistics.Generation.CommonRail.Injectors
{
    public class FitterInjectorsCorrectionForRpm : IFitter
    {
        private int rpmCorrelationThresholdPercentage = 5;

        private VehicleInformation info;
        private AnalyticRuleType type;

        public FitterInjectorsCorrectionForRpm(
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

        public AnalyticStatisticsItem Get(PsaParametersSet set, 
            DateTime sourceDataCaptureDateTime)
        {
            AnalyticStatisticsItem result = new AnalyticStatisticsItem(Type,
                VehicleInfo.Engine.Family.Type, 
                VehicleInfo.Engine.Type);
            ExtractValues(set, result, sourceDataCaptureDateTime);
            return result;
        }

        private void ExtractValues(
            PsaParametersSet set, AnalyticStatisticsItem result, 
            DateTime sourceDataCaptureDateTime)
        {
            ExtractForInjector(set, result, 1, sourceDataCaptureDateTime);
            ExtractForInjector(set, result, 2, sourceDataCaptureDateTime);
            ExtractForInjector(set, result, 3, sourceDataCaptureDateTime);
            ExtractForInjector(set, result, 4, sourceDataCaptureDateTime);
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
                    rpmCorrelationThresholdPercentage);
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
    }
}

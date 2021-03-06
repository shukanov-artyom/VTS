﻿using System;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Common;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Statistics.Fitters
{
    public abstract class FitterLambdaVoltageBase : IFitter
    {
        protected const int RpmCorrelationThresholdPercentage = 5;
        private readonly VehicleInformation info;
        private readonly CheckpointRpm rpm;
        private readonly AnalyticRuleType type;

        protected FitterLambdaVoltageBase(VehicleInformation info,
            CheckpointRpm rpm, AnalyticRuleType type)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            this.info = info;
            this.rpm = rpm;
            this.type = type;
        }

        protected VehicleInformation VehicleInfo
        {
            get
            {
                return info;
            }
        }

        protected CheckpointRpm TargetRpm
        {
            get
            {
                return rpm;
            }
        }

        protected AnalyticRuleType Type
        {
            get
            {
                return type;
            }
        }

        public AnalyticStatisticsItem Get(PsaParametersSet set,
            DateTime sourceDataCaptureDateTime)
        {
            AnalyticStatisticsItem result = new AnalyticStatisticsItem(Type,
                VehicleInfo.Engine.Family.Type, VehicleInfo.Engine.Type);
            PsaParameterData rpmData = set.GetParameterOfType(PsaParameterType.EngineRpm);
            PsaParameterData voltageData = GetRequiredDependentData(set);
            if (rpmData == null || voltageData == null)
            {
                throw new Exception("set does not fit.");
            }
            CorrelatedMedianExtractor extractor = new CorrelatedMedianExtractor(
                rpmData.GetDoubles(), voltageData.GetDoubles(),
                RpmCorrelationThresholdPercentage);
            double doubleValue = extractor.GetForBaseValue(Convert.ToDouble(TargetRpm));
            if (!double.IsNaN(doubleValue))
            {
                AnalyticStatisticsValue value =
                    new AnalyticStatisticsValue(doubleValue, info.Vin, set.Id,
                        sourceDataCaptureDateTime);
                result.Values.Add(value);
            }
            return result;
        }

        public abstract bool Fits(PsaParametersSet set);

        public abstract PsaParameterData GetRequiredDependentData(PsaParametersSet set);
    }
}

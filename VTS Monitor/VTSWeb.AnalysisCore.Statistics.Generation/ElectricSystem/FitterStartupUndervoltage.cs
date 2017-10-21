using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.AnalysisCore.Tools;
using VTSWeb.AnalysisCore.Tools.StartupUndervoltage;

using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.AnalysisCore.Statistics.Generation.ElectricSystem
{
    public class FitterStartupUndervoltage : IFitter
    {
        private VehicleInformation info;

        public FitterStartupUndervoltage(VehicleInformation info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            this.info = info;
        }

        public bool Fits(PsaParametersSet set)
        {
            bool hasRpm = set.Parameters.Any(p =>
                p.Type == PsaParameterType.EngineRpm);
            bool hasBatteryVoltageData = set.Parameters.Any(
                p => p.Type == PsaParameterType.BatteryVoltage);
            return hasRpm && hasBatteryVoltageData;
        }

        public AnalyticStatisticsItem Get(PsaParametersSet set,
            DateTime sourceDataCaptureDateTime)
        {
            AnalyticStatisticsItem result = new AnalyticStatisticsItem(
                AnalyticRuleType.EngineStartUndervoltage,
                info.Engine.Family.Type, info.Engine.Type);
            IList<double> rpmLine =
                set.GetParameterOfType(PsaParameterType.EngineRpm).GetDoubles();
            IList<double> voltagesLine =
                set.GetParameterOfType(PsaParameterType.BatteryVoltage).GetDoubles();
            EngineStartupDetector detector = new EngineStartupDetector(rpmLine);
            if (!detector.EngineStartupDetected())
            {
                return result; // empty, will be assimilated and disappear
            }
            IList<int> startupPoints = detector.GetEngineStartupPointIndexes();
            // we will have as many statistical values as there is startup points
            foreach (int startupPointIndex in startupPoints)
            {
                StartupUndervoltageExtractor extractor =
                new StartupUndervoltageExtractor(startupPointIndex, voltagesLine);
                double startupUndervoltage = extractor.Extract();
                if (!double.IsNaN(startupUndervoltage))
                {
                    result.Values.Add(new AnalyticStatisticsValue(
                        startupUndervoltage, info.Vin, set.Id, 
                        sourceDataCaptureDateTime));
                }
            }
            return result;
        }
    }
}

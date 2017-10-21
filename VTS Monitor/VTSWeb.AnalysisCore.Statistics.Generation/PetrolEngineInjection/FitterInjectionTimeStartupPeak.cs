using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.AnalysisCore.Tools;

using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.AnalysisCore.Statistics.Generation.PetrolEngineInjection
{
    public class FitterInjectionTimeStartupPeak : IFitter
    {
        private readonly VehicleInformation info;

        public FitterInjectionTimeStartupPeak(VehicleInformation info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            this.info = info;
        }

        public bool Fits(PsaParametersSet set)
        {
            bool hasRpm = set.HasParameterOfType(PsaParameterType.EngineRpm);
            bool hasInjectionTimeData = set.HasParameterOfType(PsaParameterType.InjectionTime);
            return hasRpm && hasInjectionTimeData;
        }

        public AnalyticStatisticsItem Get(PsaParametersSet set, DateTime sourceDataCapturetime)
        {
            AnalyticStatisticsItem result = new AnalyticStatisticsItem(
                AnalyticRuleType.InjectionTimeStartupPeak, 
                info.Engine.Family.Type, info.Engine.Type);
            IList<double> rpmData = set.GetParameterOfType(PsaParameterType.EngineRpm).GetDoubles();
            IList<double> reqData = set.GetParameterOfType(PsaParameterType.InjectionTime).GetDoubles();
            EngineStartupDetector detectro = new EngineStartupDetector(rpmData);
            if (detectro.EngineStartupDetected())
            {
                IList<int> startupIndexes = detectro.GetEngineStartupPointIndexes();
                foreach (int startupIndex in startupIndexes)
                {
                    AnalyticStatisticsValue value = 
                        new AnalyticStatisticsValue(
                            StartupRegionExtractor.Extract(startupIndex, reqData).Max(),
                            info.Vin, set.Id, sourceDataCapturetime);
                    result.Values.Add(value);
                }
            }
            return result;
        }
    }
}

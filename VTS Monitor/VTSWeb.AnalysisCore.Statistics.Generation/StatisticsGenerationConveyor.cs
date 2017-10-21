using System;
using System.Collections.Generic;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Recognition;

using VTSWeb.DomainObjects.Psa;

namespace VTSWeb.AnalysisCore.Statistics.Generation
{
    public class StatisticsGenerationConveyor
    {
        private PsaTrace trace;
        private VehicleInformation info;

        private IList<IFitter> fitters = 
            new List<IFitter>();

        public StatisticsGenerationConveyor(
            PsaTrace trace, VehicleInformation info)
        {
            if (trace == null)
            {
                throw new ArgumentNullException("trace");
            }
            this.info = info;
            this.trace = trace;
            InitializeFitters();
        }

        private void InitializeFitters()
        {
            foreach (AnalyticRuleType ruleType in 
                Enum.GetValues(typeof(AnalyticRuleType)))
            {
                FitterFactory factory = new FitterFactory(info);
                IFitter fitter = factory.Create(ruleType);
                fitters.Add(fitter);
            }
        }

        public IList<AnalyticStatisticsItem> RollAlong()
        {
            IList<AnalyticStatisticsItem> result = 
                new List<AnalyticStatisticsItem>();
            foreach (PsaParametersSet set in trace.ParametersSets)
            {
                foreach (IFitter fitter in fitters)
                {
                    if (fitter.Fits(set))
                    {
                        AnalyticStatisticsItem item = fitter.Get(set, trace.Date);
                        if (item.Values.Count != 0)
                        {
                            result.Add(item);
                        }
                    }
                }
            }
            return result;
        }
    }
}

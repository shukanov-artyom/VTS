using System;
using System.Collections.Generic;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;

namespace VTSWebService.AnalysisCore.Statistics
{
    internal class StatisticsGenerationConveyour
    {
        private readonly VehicleInformation vehicleInformation;
        private readonly List<IFitter> fitters = new List<IFitter>();

        public StatisticsGenerationConveyour(
            VehicleInformation vehicleInformation)
        {
            this.vehicleInformation = vehicleInformation;
            InitializeFitters();
        }

        public List<AnalyticStatisticsItem> RollAlong(PsaTrace trace)
        {
            List<AnalyticStatisticsItem> result =
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

        private void InitializeFitters()
        {
            foreach (AnalyticRuleType ruleType in
                Enum.GetValues(typeof(AnalyticRuleType)))
            {
                FitterFactory factory = new FitterFactory(vehicleInformation);
                IFitter fitter = factory.Create(ruleType);
                fitters.Add(fitter);
            }
        }
    }
}

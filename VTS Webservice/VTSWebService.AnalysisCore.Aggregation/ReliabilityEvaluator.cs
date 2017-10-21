using System;
using System.Collections.Generic;
using System.Linq;
using VTS.AnalysisCore.Common;
using VTS.Shared.DomainObjects;


namespace VTSWebService.AnalysisCore.Aggregation
{
    public class ReliabilityEvaluator
    {
        private AnalyticStatisticsItem item;

        public ReliabilityEvaluator(AnalyticStatisticsItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            this.item = item;
        }

        public AnalyticItemSettingsReliability Evaluate()
        {
            /*
 * > 10 vehicles with > 4 datasets for each => High
 * 6-10 vehicles with > 4 datasets for each => MediumHigh
 * 3-5 vehicles with > 4 datasets for each => Medium 
 * 2 vehicles with > 4 datasets for each => MediumLow
 * <2 vehicles with > 4 datasets for each => Low
 */
            IDictionary<string, int> distr =
                GetSourceDatasetsCountDistributionByVin();
            // >10 vehicles and > 4 datasets for each is reliable
            if (distr.Count(d => d.Value > 4) > 10)
            {
                return AnalyticItemSettingsReliability.High;
            }
            if (distr.Count(d => d.Value > 4) > 5 &&
                distr.Count(d => d.Value > 4) < 11)
            {
                return AnalyticItemSettingsReliability.MediumHigh;
            }
            if (distr.Count(d => d.Value > 4) > 2 &&
                distr.Count(d => d.Value > 4) < 6)
            {
                return AnalyticItemSettingsReliability.Medium;
            }
            if (distr.Count(d => d.Value > 4) == 2)
            {
                return AnalyticItemSettingsReliability.MediumLow;
            }
            if (distr.Count(d => d.Value > 4) < 2)
            {
                return AnalyticItemSettingsReliability.Low;
            }
            throw new NotSupportedException();
        }

        private IDictionary<string, int> GetSourceDatasetsCountDistributionByVin()
        {
            IDictionary<string, int> distr = new Dictionary<string, int>();
            foreach (AnalyticStatisticsValue v in item.Values)
            {
                if (distr.ContainsKey(v.SourceVin))
                {
                    distr[v.SourceVin]++;
                }
                else
                {
                    distr[v.SourceVin] = 1;
                }
            }
            return distr;
        }
    }
}

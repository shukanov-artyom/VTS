using System;
using System.Collections.Generic;
using System.Linq;

namespace VTSWeb.AnalysisCore.Tools.Statistics
{
    public class SigmaDistributor
    {
        private IList<double> source;
        private float sampling;
        private double median;
        private double sigma;

        public SigmaDistributor(IList<double> source, float sampling)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            this.source = source;
            this.sampling = sampling;
            median = source.Average();
            sigma = Sigma.Get(source);
        }

        public IDictionary<string, long> Distribute()
        {
            IList<SigmaInterval> intervals = new List<SigmaInterval>();
            foreach (double value in source)
            {
                if (!intervals.Any(i => i.Belongs(value)))
                {
                    SigmaInterval interval = GenerateInterval(intervals, value);
                    interval.ValuesCount++;
                    intervals.Add(interval);
                }
                else
                {
                    intervals.First(i => i.Belongs(value)).ValuesCount++;
                }
            }
            return GetDictionary(intervals.
                OrderBy(i=>(i.Bound1+i.Bound2)/2).ToList());
        }

        private SigmaInterval GenerateInterval(
            IList<SigmaInterval> intervals, double value)
        {
            if (value >= median)
            {
                SigmaInterval interval = new SigmaInterval();
                interval.Bound2 = median;
                interval.Bound1 = median + sampling*sigma;
                interval.Name = String.Format("+{0}σ", sampling);
                int samplingCounter = 1;
                while (!interval.Belongs(value))
                {
                    if (!intervals.Any(i => i.Name == interval.Name))
                    {
                        intervals.Add(new SigmaInterval(interval));
                    }
                    interval.Bound1 += sampling*sigma;
                    interval.Bound2 += sampling*sigma;
                    samplingCounter++;
                    interval.Name = String.Format("+{0}σ", 
                        samplingCounter*sampling);
                }
                return interval;
            }
            else if (value < median)
            {
                SigmaInterval interval = new SigmaInterval();
                interval.Bound1 = median;
                interval.Bound2 = median - sampling * sigma;
                interval.Name = String.Format("-{0}σ", sampling);
                int samplingCounter = 1;
                while (!interval.Belongs(value))
                {
                    if (!intervals.Any(i => i.Name == interval.Name))
                    {
                        intervals.Add(new SigmaInterval(interval));
                    }
                    interval.Bound1 -= sampling * sigma;
                    interval.Bound2 -= sampling * sigma;
                    samplingCounter++;
                    interval.Name = String.Format("-{0}σ",
                        samplingCounter*sampling);
                }
                return interval;
            }
            throw new Exception("Something is wrong");
        }

        private IDictionary<string, long> GetDictionary(
            IList<SigmaInterval> src)
        {
            IDictionary<string, long> result = new Dictionary<string, long>();
            foreach (SigmaInterval interval in src)
            {
                result[interval.Name] = interval.ValuesCount;
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace VTSWeb.AnalysisCore.Tools.Statistics
{
    public static class Sigma
    {
        public static double Get(IList<double> source)
        {
            double median = source.Average();
            double sum = 0;
            foreach (double d in source)
            {
                sum += Math.Pow(d - median, 2);
            }
            return Math.Sqrt(sum/source.Count);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace VTSWebService.AnalysisCore.Statistics.Tools
{
    public static class StartupRegionExtractor
    {
        public static IList<double> Extract(
            int startupIndex, IList<double> values)
        {
            IList<int> regionIndexes = new List<int>();
            // 1. Take an index +/- 10 (if have enough space)
            int leftBorder = startupIndex < 11 ? 0 : startupIndex - 10;
            int rightBorder = startupIndex > values.Count - 11 ? values.Count : startupIndex + 10;
            for (int j = leftBorder; j < rightBorder; j++)
            {
                if (!regionIndexes.Contains(j))
                {
                    regionIndexes.Add(j);
                }
            }

            // 3. summarize the range
            IOrderedEnumerable<int> ordered = regionIndexes.OrderBy(i => i);
            IList<double> result = new List<double>();
            foreach (int ind in ordered)
            {
                result.Add(values[ind]);
            }

            // 4. Return result
            return result;
        }
    }
}

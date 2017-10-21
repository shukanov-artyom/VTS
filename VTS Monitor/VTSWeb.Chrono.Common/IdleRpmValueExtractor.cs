using System;
using System.Collections.Generic;
using System.Linq;

namespace VTSWeb.Chrono.Common
{
    /// <summary>
    /// Extracts Idle rpm without acceleration right after the engine start.
    /// Consumes Double RPM values beginning from 0 RPM (engine not started)
    /// </summary>
    public class IdleRpmValueExtractor
    {
        private const int UpMovementsCountTreshhold = 7;
        private const int MinAppropriateValuesCount = 10;
        private const double PostAnalyseMedianFilteringThreshold = 5.0;

        public static double ExtractIdleMedian(IList<double> values)
        {
            return ExtractInitialIdleLine(values).Average();
        }

        public static IList<int> ExtractIdleIndexes(IList<double> values)
        {
            IList<int> result = new List<int>();
            for (int i = 0; i < ExtractInitialIdleLine(values).Count; i++)
            {
                result.Add(i);
            }
            return result;
        }

        /// <summary>
        /// Finds initial rpm line and filters it of peak values.
        /// </summary>
        public static IList<double>
            ExtractInitialIdleLine(IList<double> values)
        {
            IList<double> intermediate;
            IList<double> results = new List<double>();

            intermediate = ExtractIntermediate(values);

            if (intermediate.Count < MinAppropriateValuesCount ||
                intermediate.Count < UpMovementsCountTreshhold)
            {
                // not enough data, return empty array
                return results;
            }
            for (int i = 0; i < UpMovementsCountTreshhold; i++)
            {
                intermediate.RemoveAt(intermediate.Count - 1);
            }
            double finalMedian = intermediate.Average();
            // filter initial line data according to the new median
            for (int i = 0; i < intermediate.Count; i++)
            {
                if ((intermediate[i] - finalMedian) * 100 / finalMedian <=
                    PostAnalyseMedianFilteringThreshold)
                {
                    results.Add(intermediate[i]);
                }
            }
            return results;
        }

        private static IList<double> ExtractIntermediate(IList<double> values)
        {
            double prev = 0;
            double median = 0;
            double prevMedian = 0;
            int upMovementCounter = 0;
            IList<double> intermediate = new List<double>();
            foreach (double d in values)
            {
                if (d != 0)
                {
                    if (prev == 0)
                    {
                        prev = d;
                        median = d;
                        intermediate.Add(d);
                        continue;
                    }
                    prevMedian = median;
                    median = intermediate.Average();
                    //double medianDelta=(median - prevMedian)*100/prevMedian;
                    if (median > prevMedian)
                    {
                        upMovementCounter++;
                    }
                    else
                    {
                        upMovementCounter = upMovementCounter > 0 ?
                            upMovementCounter-- : upMovementCounter;
                    }
                    intermediate.Add(d);
                    if (upMovementCounter == UpMovementsCountTreshhold)
                    {
                        break;
                    }
                }
            }
            return intermediate;
        }
    }
}

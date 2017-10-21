using System;
using System.Collections.Generic;
using System.Linq;

namespace VTSWeb.AnalysisCore.Tools
{
    public class CorrelatedMedianExtractor
    {
        private IList<double> baseValues;
        private IList<double> dependantValues;
        private int baseValuesDifferencePercentageThreshold;

        public CorrelatedMedianExtractor(
            IList<double> baseValues,
            IList<double> dependantValues,
            int baseValuesDifferencePercentageThreshold)
        {
            this.baseValues = baseValues;
            this.dependantValues = dependantValues;
            this.baseValuesDifferencePercentageThreshold =
                baseValuesDifferencePercentageThreshold;
        }

        public double GetForBaseValue(double baseValue)
        {
            // 1. Extract suitable base indexes
            // 2. Calculate median dependtan value for these indexes
            IList<double> values = new List<double>();
            foreach (int i in GetSuitableBaseIndexes(baseValues, baseValue))
            {
                values.Add(dependantValues[i]);
            }
            if (values.Count == 0)
            {
                return double.NaN;
            }
            return values.Average();
        }

        private IEnumerable<int> GetSuitableBaseIndexes(IList<double> baseArray,
            double baseValue)
        {
            for (int i = 0; i < baseArray.Count; i++)
            {
                if (DeltaHelper.GetDeltaPercentage(baseArray[i],
                    baseValue) <= baseValuesDifferencePercentageThreshold)
                {
                    yield return i;
                }
            }
        }
    }
}

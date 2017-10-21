using System;

namespace VTSWebService.AnalysisCore.Common
{
    public static class DeltaHelper
    {
        public static double GetDeltaPercentage(double value, double baseValue)
        {
            return (Math.Max(value, baseValue) -
                Math.Min(value, baseValue)) * 100
                / baseValue;
        }
    }
}

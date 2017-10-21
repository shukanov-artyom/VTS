using System;

namespace VTSWebService.AnalysisCore.Aggregation
{
    public class StandardAggregatorSettings
    {
        /// <summary>
        /// it's (M +/- optimalBound*sigma) wil be taken as an optimal range
        /// </summary>
        private static float optimalBound = (float)0.5;

        /// <summary>
        /// Max acceptable will be taken as (M +/- maxAcceptableBound*sigma)
        /// </summary>
        private static float maxAcceptableBound = (float)2.0;

        public static float OptimalBound
        {
            get
            {
                return optimalBound;
            }
        }

        public static float MaxAcceptableBound
        {
            get
            {
                return maxAcceptableBound;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace VTSWeb.AnalysisCore.Tools
{
    public class EngineStartupDetector
    {
        private IList<double> rpmValues;

        public EngineStartupDetector(IList<double> rpmValues)
        {
            this.rpmValues = rpmValues;
        }

        public bool EngineStartupDetected()
        {
            return rpmValues.Any(v => v == 0) && rpmValues.Any(v => v > 0) && rpmValues.Count > 10;
        }

        /// <summary>
        /// Gets Values indexes corresponding to engine start process
        /// </summary>
        /// <returns></returns>
        public IList<int> GetEngineStartupPointIndexes()
        {
            // startup Indexes detection (keeping in mind there possibly will be several startup points)
            IList<int> startupPoints = GetStartupPoints(rpmValues);
            return startupPoints;
        }

        private IList<int> GetStartupPoints(IList<double> rpm)
        {
            IList<int> result = new List<int>();
            for (int i = 0; i < rpm.Count; i++ )
            {
                if (i == 0)
                {
                    continue;
                }
                if (rpm[i] > 0 && rpm[i - 1] == 0)
                {
                    // this is the start point
                    // take the last 0 rpm point for the start point
                    result.Add(i-1);
                }
            }
            return result;
        }
    }
}

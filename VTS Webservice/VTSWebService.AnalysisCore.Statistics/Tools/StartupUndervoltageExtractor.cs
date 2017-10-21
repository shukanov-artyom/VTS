using System;
using System.Collections.Generic;
using System.Linq;

namespace VTSWebService.AnalysisCore.Statistics.Tools
{
    public class StartupUndervoltageExtractor
    {
        private int startupMomentIndex;
        private IList<double> voltageValues;

        public StartupUndervoltageExtractor(
            int startupMomentIndex, IList<double> voltageValues)
        {
            this.startupMomentIndex = startupMomentIndex;
            this.voltageValues = voltageValues;
        }

        public double Extract()
        {
            // 3. Get engine startup voltages region for provided index
            IList<double> startupRegionVoltages =
                StartupRegionExtractor.Extract(startupMomentIndex, voltageValues);

            // 4. If was not able to detect it - return NAN
            if (startupRegionVoltages.Count == 0)
            {
                return double.NaN;
            }

            // 5. Get minimal value of region voltages
            return startupRegionVoltages.Min();
        }
    }
}

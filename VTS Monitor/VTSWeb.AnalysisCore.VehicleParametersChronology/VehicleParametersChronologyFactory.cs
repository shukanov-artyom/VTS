using System;
using VTSWeb.AnalysisCore.Statistics;

namespace VTSWeb.AnalysisCore.VehicleParametersChronology
{
    public class VehicleParametersChronologyFactory
    {
        private AnalyticStatistics statistics;

        public VehicleParametersChronologyFactory(AnalyticStatistics statistics)
        {
            this.statistics = statistics;
        }

        public VehicleParametersChronology Create()
        {
            VehicleParametersChronology result = new VehicleParametersChronology();
            foreach (AnalyticStatisticsItem item in statistics.Items)
            {
                foreach (AnalyticStatisticsValue value in item.Values)
                {
                    result.AddValue(item.Type, 
                        value.SourceDataCaptureDateTime, value.Value);
                }
            }
            return result;
        }
    }
}

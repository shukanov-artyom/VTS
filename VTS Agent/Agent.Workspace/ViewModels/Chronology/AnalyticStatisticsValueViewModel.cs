using System;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.ViewModels.Chronology
{
    public class AnalyticStatisticsValueViewModel
    {
        private readonly AnalyticStatisticsValue model;

        public AnalyticStatisticsValueViewModel(AnalyticStatisticsValue model)
        {
            this.model = model;
        }

        public DateTime Date
        {
            get
            {
                return model.SourceDataCaptureDateTime;
            }
        }

        public double Value
        {
            get
            {
                return model.Value;
            }
        }
    }
}

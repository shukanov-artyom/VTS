using System;
using System.Collections.ObjectModel;
using VTSWeb.AnalysisCore.Statistics;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public class AnalyticStatisticsAssembler
    {
        public static AnalyticStatisticsDto FromObjectToDto(AnalyticStatistics source)
        {
            AnalyticStatisticsDto target = new AnalyticStatisticsDto();
            target.Items = new ObservableCollection<AnalyticStatisticsItemDto>();
            foreach (AnalyticStatisticsItem item in source.Items)
            {
                target.Items.Add(AnalyticStatisticsItemAssembler.FromDomainObjectToDto(item));
            }
            return target;
        }
    }
}

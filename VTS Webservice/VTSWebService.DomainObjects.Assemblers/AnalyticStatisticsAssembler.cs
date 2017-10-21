using System;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class AnalyticStatisticsAssembler
    {
        public static AnalyticStatistics FromDtoToObject(AnalyticStatisticsDto source)
        {
            AnalyticStatistics target = new AnalyticStatistics();
            foreach (AnalyticStatisticsItemDto item in source.Items)
            {
                target.Items.Add(AnalyticStatisticsItemAssembler.FromDtoToDomainObject(item));
            }
            return target;
        }

        public static AnalyticStatisticsDto FromObjectToDto(AnalyticStatistics source)
        {
            AnalyticStatisticsDto target = new AnalyticStatisticsDto();
            foreach (AnalyticStatisticsItem item in source.Items)
            {
                target.Items.Add(AnalyticStatisticsItemAssembler.FromDomainObjectToDto(item));
            }
            return target;
        }
    }
}

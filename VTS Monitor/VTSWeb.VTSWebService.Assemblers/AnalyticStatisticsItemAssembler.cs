using System;
using System.Collections.ObjectModel;
using VTSWeb.AnalysisCore.Statistics;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public class AnalyticStatisticsItemAssembler
    {
        public static AnalyticStatisticsItemDto FromDomainObjectToDto(AnalyticStatisticsItem source)
        {
            AnalyticStatisticsItemDto target = new AnalyticStatisticsItemDto();
            target.Values = new ObservableCollection<AnalyticStatisticsValueDto>();
            target.Id = source.Id;
            target.DateGenerated = source.DateGenerated;
            target.TargetEngineFamilyType = (int)source.TargetEngineFamilyType;
            target.TargetEngineType = (int) source.TargetEngineType;
            target.Type = (int) source.Type;
            target.VersionGenerated = source.VersionGenerated.ToString();
            foreach (AnalyticStatisticsValue value in source.Values)
            {
                target.Values.Add(AnalyticStatisticsValueAssembler.FromDomainObjectToDto(value));
            }
            return target;
        }
    }
}

using System;
using VTSWeb.AnalysisCore.Statistics;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VTSWebService.Assemblers
{
    public  static class AnalyticStatisticsValueAssembler
    {
        public static AnalyticStatisticsValue FromDtoToDomainObject(
            AnalyticStatisticsValueDto source)
        {
            AnalyticStatisticsValue target = new AnalyticStatisticsValue(
                source.Value, source.SourceVin, source.SourcePsaParametersSetId,
                source.SourceDataCaptureDateTime);
            target.Id = source.Id;
            target.AnalyticStatisticsItemId = source.AnalyticStatisticsItemId;
            return target;
        }

        public static AnalyticStatisticsValueDto FromDomainObjectToDto(
            AnalyticStatisticsValue source)
        {
            AnalyticStatisticsValueDto target = new AnalyticStatisticsValueDto();
            target.Id = source.Id;
            target.AnalyticStatisticsItemId = source.AnalyticStatisticsItemId;
            target.Value = source.Value;
            target.SourceVin = source.SourceVin;
            target.SourcePsaParametersSetId = source.SourcePsaParametersSetId;
            target.SourceDataCaptureDateTime = source.SourceDataCaptureDateTime;
            return target;
        }
    }
}

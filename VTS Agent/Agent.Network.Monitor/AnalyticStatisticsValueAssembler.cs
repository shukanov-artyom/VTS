using System;
using Agent.Network.Monitor.VtsWebService;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Network.Monitor
{
    public static class AnalyticStatisticsValueAssembler
    {
        public static AnalyticStatisticsValue FromDtoToDomainObject(
            AnalyticStatisticsValueDto source)
        {
            AnalyticStatisticsValue target = new AnalyticStatisticsValue(
                source.Value, 
                source.SourceVin, 
                source.SourcePsaParametersSetId,
                source.SourceDataCaptureDateTime);
            target.AnalyticStatisticsItemId = source.AnalyticStatisticsItemId;
            target.Id = source.Id;
            return target;
        }
    }
}

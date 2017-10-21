using System;
using VTS.Shared.DomainObjects;
using VTSWebService.DataContracts;
using AnalyticStatisticsValueEntity = VTSWebService.DataAccess.AnalyticStatisticsValue;

namespace VTSWebService.DomainObjects.Assemblers
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

        public static AnalyticStatisticsValueEntity FromDtoToEntity(AnalyticStatisticsValueDto source)
        {
            AnalyticStatisticsValueEntity target = new AnalyticStatisticsValueEntity();
            target.Id = source.Id;
            target.AnalyticStatisticsItemId = source.AnalyticStatisticsItemId;
            target.SourceDataCaptureDateTime = source.SourceDataCaptureDateTime;
            target.SourcePsaParametersSetId = source.SourcePsaParametersSetId;
            target.SourceVehicleVin = source.SourceVin;
            target.Value = source.Value;
            return target;
        }

        public static AnalyticStatisticsValueDto FromDomainObjectToDto(AnalyticStatisticsValue source)
        {
            AnalyticStatisticsValueDto target = new AnalyticStatisticsValueDto();
            target.Id = source.Id;
            target.AnalyticStatisticsItemId = source.AnalyticStatisticsItemId;
            target.SourceDataCaptureDateTime = source.SourceDataCaptureDateTime;
            target.SourcePsaParametersSetId = source.SourcePsaParametersSetId;
            target.SourceVin = source.SourceVin;
            target.Value = source.Value;
            return target;
        }

        public static AnalyticStatisticsValueEntity FromDomainObjectToEntity(
            AnalyticStatisticsValue source)
        {
            AnalyticStatisticsValueEntity target = new AnalyticStatisticsValueEntity();
            target.Id = source.Id;
            target.AnalyticStatisticsItemId = source.AnalyticStatisticsItemId;
            target.SourceDataCaptureDateTime = source.SourceDataCaptureDateTime;
            target.SourcePsaParametersSetId = source.SourcePsaParametersSetId;
            target.SourceVehicleVin = source.SourceVin;
            target.Value = source.Value;
            return target;
        }

        public static AnalyticStatisticsValue FromEntityToDomainObject(AnalyticStatisticsValueEntity source)
        {
            AnalyticStatisticsValue target = new AnalyticStatisticsValue(
                source.Value,
                source.SourceVehicleVin,
                source.SourcePsaParametersSetId,
                source.SourceDataCaptureDateTime);
            target.Id = source.Id;
            target.AnalyticStatisticsItemId = source.AnalyticStatisticsItemId;
            return target;
        }

        public static AnalyticStatisticsValueDto FromEntityToDto(AnalyticStatisticsValueEntity source)
        {
            AnalyticStatisticsValueDto target = new AnalyticStatisticsValueDto();
            target.Id = source.Id;
            target.AnalyticStatisticsItemId = source.AnalyticStatisticsItemId;
            target.SourceDataCaptureDateTime = source.SourceDataCaptureDateTime;
            target.SourcePsaParametersSetId = source.SourcePsaParametersSetId;
            target.SourceVin = source.SourceVehicleVin;
            target.Value = source.Value;
            return target;
        }
    }
}

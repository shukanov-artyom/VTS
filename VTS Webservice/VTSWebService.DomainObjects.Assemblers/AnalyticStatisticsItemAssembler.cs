using System;
using System.Collections.Generic;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Enums;
using VTSWebService.DataContracts;
using AnalyticStatisticsItemEntity = 
    VTSWebService.DataAccess.AnalyticStatisticsItem;
using AnalyticStatisticsValueEntity = VTSWebService.DataAccess.AnalyticStatisticsValue;

namespace VTSWebService.DomainObjects.Assemblers
{
    public static class AnalyticStatisticsItemAssembler
    {
        public static AnalyticStatisticsItem FromDtoToDomainObject(AnalyticStatisticsItemDto source)
        {
            AnalyticStatisticsItem target = new AnalyticStatisticsItem(
                (AnalyticRuleType) source.Type,
                (EngineFamilyType) source.TargetEngineFamilyType,
                (EngineType) source.TargetEngineType);
            target.DateGenerated = source.DateGenerated;
            target.Id = source.Id;
            target.VersionGenerated = new Version(source.VersionGenerated);
            foreach (AnalyticStatisticsValueDto value in source.Values)
            {
                target.Values.Add(AnalyticStatisticsValueAssembler.FromDtoToDomainObject(value));
            }
            return target;
        }

        public static AnalyticStatisticsItemEntity FromDtoToEntity(AnalyticStatisticsItemDto source)
        {
            AnalyticStatisticsItemEntity target = new AnalyticStatisticsItemEntity();
            target.Id = source.Id;
            target.DateGenerated = source.DateGenerated;
            target.TargetEngineFamilyType = source.TargetEngineFamilyType;
            target.TargetEngineType = source.TargetEngineType;
            target.Type = source.Type;
            target.VersionGenerated = source.VersionGenerated;
            foreach (AnalyticStatisticsValueDto value in source.Values)
            {
                target.AnalyticStatisticsValue.Add(AnalyticStatisticsValueAssembler.FromDtoToEntity(value));
            }
            return target;
        }

        public static AnalyticStatisticsItem FromEntityToDomainObject(AnalyticStatisticsItemEntity source)
        {
            AnalyticStatisticsItem target = new AnalyticStatisticsItem(
                (AnalyticRuleType)source.Type,
                (EngineFamilyType)source.TargetEngineFamilyType,
                (EngineType)source.TargetEngineType);
            target.Id = source.Id;
            target.DateGenerated = source.DateGenerated;
            target.TargetEngineFamilyType = (EngineFamilyType)source.TargetEngineFamilyType;
            target.TargetEngineType = (EngineType)source.TargetEngineType;
            target.Type = (AnalyticRuleType)source.Type;
            target.VersionGenerated = new Version(source.VersionGenerated);
            foreach (AnalyticStatisticsValueEntity value in source.AnalyticStatisticsValue)
            {
                target.Values.Add(AnalyticStatisticsValueAssembler.FromEntityToDomainObject(value));
            }
            return target;
        }

        public static AnalyticStatisticsItemDto FromEntityToDto(AnalyticStatisticsItemEntity source)
        {
            AnalyticStatisticsItemDto target = new AnalyticStatisticsItemDto();
            target.Id = source.Id;
            target.DateGenerated = source.DateGenerated;
            target.TargetEngineFamilyType = source.TargetEngineFamilyType;
            target.TargetEngineType = source.TargetEngineType;
            target.Type = source.Type;
            target.VersionGenerated = source.VersionGenerated;
            foreach (AnalyticStatisticsValueEntity value in source.AnalyticStatisticsValue)
            {
                target.Values.Add(AnalyticStatisticsValueAssembler.FromEntityToDto(value));
            }
            return target;
        }

        public static AnalyticStatisticsItemDto FromDomainObjectToDto(AnalyticStatisticsItem source)
        {
            AnalyticStatisticsItemDto target = new AnalyticStatisticsItemDto();
            target.Values = new List<AnalyticStatisticsValueDto>();
            target.Id = source.Id;
            target.DateGenerated = source.DateGenerated;
            target.TargetEngineFamilyType = (int)source.TargetEngineFamilyType;
            target.TargetEngineType = (int)source.TargetEngineType;
            target.Type = (int) source.Type;
            target.VersionGenerated = source.VersionGenerated.ToString();
            foreach (AnalyticStatisticsValue value in source.Values)
            {
                target.Values.Add(AnalyticStatisticsValueAssembler.FromDomainObjectToDto(value));
            }
            return target;
        }

        public static AnalyticStatisticsItemEntity FromDomainObjectToEntity(AnalyticStatisticsItem source)
        {
            AnalyticStatisticsItemEntity target = new AnalyticStatisticsItemEntity();
            target.Id = source.Id;
            target.DateGenerated = source.DateGenerated;
            target.TargetEngineFamilyType = (int)source.TargetEngineFamilyType;
            target.TargetEngineType = (int)source.TargetEngineType;
            target.Type = (int)source.Type;
            target.VersionGenerated = source.VersionGenerated.ToString();
            foreach (AnalyticStatisticsValue value in source.Values)
            {
                target.AnalyticStatisticsValue.Add(
                    AnalyticStatisticsValueAssembler.FromDomainObjectToEntity(value));
            }
            return target;
        }
    }
}

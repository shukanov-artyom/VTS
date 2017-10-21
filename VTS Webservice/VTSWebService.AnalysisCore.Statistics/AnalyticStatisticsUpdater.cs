using System;
using System.Collections.Generic;
using System.Linq;
using VTSWebService.DataAccess;
using VTSWebService.DomainObjects.Assemblers;
using AnalyticStatisticsItem = VTS.Shared.DomainObjects.AnalyticStatisticsItem;
using AnalyticStatisticsValue = VTS.Shared.DomainObjects.AnalyticStatisticsValue;
using AnalyticStatisticsItemEntity = VTSWebService.DataAccess.AnalyticStatisticsItem;
using AnalyticStatisticsValueEntity = VTSWebService.DataAccess.AnalyticStatisticsValue;

namespace VTSWebService.AnalysisCore.Statistics
{
    public class AnalyticStatisticsUpdater
    {
        private readonly List<AnalyticStatisticsItem> items;
        private readonly List<long> affectedItemsIds = new List<long>();

        public List<long> UpdatedItemsIds
        {
            get
            {
                return affectedItemsIds;
            }
        }

        public AnalyticStatisticsUpdater(List<AnalyticStatisticsItem> items)
        {
            this.items = items;
        }

        public void Update()
        {
            foreach (AnalyticStatisticsItem item in items)
            {
                UpdateDatabaseWithItem(item);
            }
        }

        private void UpdateDatabaseWithItem(AnalyticStatisticsItem item)
        {
            if (AlreadyHaveItemWithSignature(item))
            {
                foreach (AnalyticStatisticsValue value in item.Values)
                {
                    if (AlreadyHaveThisValueForThisItem(item, value))
                    {
                        UpdateValue(item, value);
                    }
                    else
                    {
                        AddNewValueToExistingItem(item, value);
                    }
                }
            }
            else
            {
                PersistNewAnalyticStatisticsItem(item);
            }
        }

        private bool AlreadyHaveItemWithSignature(AnalyticStatisticsItem item)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                return database.AnalyticStatisticsItem.Any(
                    i => i.Type == (int) item.Type &&
                         i.TargetEngineFamilyType == (int) item.TargetEngineFamilyType &&
                         i.TargetEngineType == (int) item.TargetEngineType);
            }
        }

        private void PersistNewAnalyticStatisticsItem(AnalyticStatisticsItem item)
        {
            AnalyticStatisticsItemEntity entity =
                AnalyticStatisticsItemAssembler.FromDomainObjectToEntity(item);
            using (VTSDatabase database = new VTSDatabase())
            {
                database.AnalyticStatisticsItem.Add(entity);
                database.SaveChanges();
                RememberUpdatedItem(entity);
            }
        }

        private bool AlreadyHaveThisValueForThisItem(
            AnalyticStatisticsItem item,
            AnalyticStatisticsValue value)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                AnalyticStatisticsItemEntity ourItem = database.AnalyticStatisticsItem.First(
                    i => i.Type == (int)item.Type &&
                         i.TargetEngineFamilyType == (int)item.TargetEngineFamilyType &&
                         i.TargetEngineType == (int)item.TargetEngineType);
                return ourItem.AnalyticStatisticsValue.Any(v => 
                    v.SourcePsaParametersSetId == value.SourcePsaParametersSetId);
            }
        }

        private void AddNewValueToExistingItem(AnalyticStatisticsItem item,
            AnalyticStatisticsValue value)
        {
            AnalyticStatisticsValueEntity entityToAdd =
                AnalyticStatisticsValueAssembler.FromDomainObjectToEntity(value);
            using (VTSDatabase database = new VTSDatabase())
            {
                AnalyticStatisticsItemEntity existingItem = database.AnalyticStatisticsItem.First(
                    i => i.Type == (int)item.Type &&
                         i.TargetEngineFamilyType == (int)item.TargetEngineFamilyType &&
                         i.TargetEngineType == (int)item.TargetEngineType);
                existingItem.AnalyticStatisticsValue.Add(entityToAdd);
                database.SaveChanges();
                RememberUpdatedItem(existingItem);
            }
        }

        private void UpdateValue(AnalyticStatisticsItem item, 
            AnalyticStatisticsValue value)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                AnalyticStatisticsItemEntity existingItem = database.AnalyticStatisticsItem.First(
                    i => i.Type == (int)item.Type &&
                         i.TargetEngineFamilyType == (int)item.TargetEngineFamilyType &&
                         i.TargetEngineType == (int)item.TargetEngineType);
                AnalyticStatisticsValueEntity entityToUpdate = existingItem.AnalyticStatisticsValue.First(
                    v => v.SourcePsaParametersSetId == value.SourcePsaParametersSetId);
                if (!entityToUpdate.Value.Equals(value.Value))
                {
                    entityToUpdate.Value = value.Value;
                    database.SaveChanges();
                    RememberUpdatedItem(existingItem);
                }
            }
        }

        private void RememberUpdatedItem(AnalyticStatisticsItemEntity item)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                AnalyticStatisticsItemEntity itemEntity = database.
                    AnalyticStatisticsItem.FirstOrDefault(e =>
                    e.Type == item.Type &&
                    e.TargetEngineFamilyType == item.TargetEngineFamilyType &&
                    e.TargetEngineType == item.TargetEngineType);
                if (itemEntity == null)
                {
                    return;
                }
                affectedItemsIds.Add(itemEntity.Id);
            }
        }
    }
}

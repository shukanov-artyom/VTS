using System;
using System.Collections.Generic;
using System.Linq;
using VTSWebService.AnalysisCore.Enums;
using VTSWebService.DataAccess;
using VTSWebService.DomainObjects.Assemblers;
using AnalyticStatisticsItemEntity = VTSWebService.DataAccess.AnalyticStatisticsItem;
using AnalyticRuleSettingsEntity = VTSWebService.DataAccess.AnalyticRuleSettings;

namespace VTSWebService.AnalysisCore.Aggregation
{
    public class StatisticsAggregationEngine
    {
        public void Aggregate(List<long> statisticsItemIds)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                List<AnalyticStatisticsItemEntity> items = 
                    new List<AnalyticStatisticsItemEntity>();
                foreach (long id in statisticsItemIds)
                {
                    AnalyticStatisticsItemEntity entity = database.
                        AnalyticStatisticsItem.First(item => item.Id == id);
                    items.Add(entity);
                }
                foreach (AnalyticStatisticsItem item in items)
                {
                    AggregateAnalyticStatisticsItemEntity(item, database);
                }
                database.SaveChanges();
            }
        }

        public void Aggregate()
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (AnalyticStatisticsItemEntity item in database.AnalyticStatisticsItem)
                {
                    AggregateAnalyticStatisticsItemEntity(item, database);
                }
                database.SaveChanges();
            }
        }

        private void AggregateAnalyticStatisticsItemEntity(
            AnalyticStatisticsItemEntity item,
            VTSDatabase database)
        {
            VTS.Shared.DomainObjects.AnalyticStatisticsItem asi =
                        AnalyticStatisticsItemAssembler.FromEntityToDomainObject(item);
            VTS.AnalysisCore.Common.AnalyticRuleSettings settings = AggregatorFactory.Create(asi).Aggregate();
            AnalyticRuleSettingsEntity entityToUpdate = database.AnalyticRuleSettings.
                FirstOrDefault(s => s.RuleType == (int)settings.RuleType && s.EngineType == (int)settings.EngineType);
            if (entityToUpdate == null)
            {
                database.AnalyticRuleSettings.Add(
                    AnalyticRuleSettingsAssembler.FromDomainObjectToEntity(settings));
            }
            else
            {
                AnalyticRuleSettingsEntity sourceEntity =
                    AnalyticRuleSettingsAssembler.FromDomainObjectToEntity(settings);
                UpdateAnalyticRuleStatisticalAtomEntity(sourceEntity, entityToUpdate);
                ReliabilitySummarizer summarizer = new ReliabilitySummarizer();
                summarizer.SummarizeFor(entityToUpdate);
            }
        }

        private void UpdateAnalyticRuleStatisticalAtomEntity(
            AnalyticRuleSettings source, AnalyticRuleSettings target)
        {
            SettingsAtom atom = target.SettingsMolecule.First().SettingsAtom.First(a => a.Type == (int) SettingsAtomType.Statistical);
            atom.MaxAcceptable = source.SettingsMolecule.First().SettingsAtom.First(a => a.Type == (int)SettingsAtomType.Statistical).MaxAcceptable;
            atom.MinAcceptable = source.SettingsMolecule.First().SettingsAtom.First(a => a.Type == (int)SettingsAtomType.Statistical).MinAcceptable;
            atom.MaxOptimal = source.SettingsMolecule.First().SettingsAtom.First(a => a.Type == (int)SettingsAtomType.Statistical).MaxOptimal;
            atom.MinOptimal = source.SettingsMolecule.First().SettingsAtom.First(a => a.Type == (int)SettingsAtomType.Statistical).MinOptimal;
            target.Reliability = source.Reliability;
        }
    }
}

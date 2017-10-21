using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using VTS.Shared;
using VTSWeb.Common;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.AnalysisCore.Models.Settings.Persistency
{
    public class AnalyticRuleSettingsPersistency
    {
        public delegate void AnalyticRuleSettingsRetrieved(
            IList<AnalyticRuleSettings> result);
        public delegate void AnalyticRulePrioritySettingsRetrieved(
            AnalyticRuleSettings result);
        private readonly VtsWebServiceClient service = new VtsWebServiceClient();
        private AnalyticRuleSettingsRetrieved collectionFetchedCallback;
        private readonly AnalyticRulePrioritySettingsRetrieved singleFetchedCallback;
        private readonly ErrorCallbackDelegate errorCallback;

        public AnalyticRuleSettingsPersistency(
            AnalyticRuleSettingsRetrieved collectionFetchedCallback,
            ErrorCallbackDelegate errorCallback)
        {
            this.errorCallback = errorCallback;
            this.collectionFetchedCallback = collectionFetchedCallback;
        }

        public AnalyticRuleSettingsPersistency(
            AnalyticRulePrioritySettingsRetrieved singleFetchedCallback,
            ErrorCallbackDelegate errorCallback)
        {
            this.errorCallback = errorCallback;
            this.singleFetchedCallback = singleFetchedCallback;
        }

        ~AnalyticRuleSettingsPersistency()
        {
            service.CloseAsync();
        }

        public void FetchAll()
        {
            service.GetAnalyticRuleSettingsCompleted += ServiceOnGetAnalyticRuleSettingsCompleted;
            service.GetAnalyticRuleSettingsAsync();
        }

        private void ServiceOnGetAnalyticRuleSettingsCompleted(object sender, 
            GetAnalyticRuleSettingsCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                List<AnalyticRuleSettings> result = new List<AnalyticRuleSettings>();
                foreach (AnalyticRuleSettingsDto ruleSettingsDto in e.Result)
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromDtoToDomainObject(ruleSettingsDto));
                }
                collectionFetchedCallback.Invoke(result);
            }
        }

        public void FetchAllForRuleType(AnalyticRuleType ruleType)
        {
            service.GetAnalyticRuleSettingsByTypeCompleted += ServiceOnGetAnalyticRuleSettingsByTypeCompleted;
            service.GetAnalyticRuleSettingsByTypeAsync((int)ruleType);
        }

        private void ServiceOnGetAnalyticRuleSettingsByTypeCompleted(object sender, 
            GetAnalyticRuleSettingsByTypeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                List<AnalyticRuleSettings> result = new List<AnalyticRuleSettings>();
                foreach (AnalyticRuleSettingsDto ruleSettingsDto in e.Result)
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromDtoToDomainObject(ruleSettingsDto));
                }
                collectionFetchedCallback.Invoke(result);
            }
        }

        public void FetchAllForEngineFamily(AnalyticRuleType ruleType, 
            EngineFamilyType engineFamily)
        {
            service.GetAnalyticRuleSettingsByTypeAndEngineFamilyCompleted += ServiceOnGetAnalyticRuleSettingsByTypeAndEngineFamilyCompleted;
            service.GetAnalyticRuleSettingsByTypeAndEngineFamilyAsync((int)ruleType, (int)engineFamily);
        }

        private void ServiceOnGetAnalyticRuleSettingsByTypeAndEngineFamilyCompleted(object sender,
            GetAnalyticRuleSettingsByTypeAndEngineFamilyCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                List<AnalyticRuleSettings> result = new List<AnalyticRuleSettings>();
                foreach (AnalyticRuleSettingsDto ruleSettingsDto in e.Result)
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromDtoToDomainObject(ruleSettingsDto));
                }
                collectionFetchedCallback.Invoke(result);
            }
        }

        public void FetchAllForEngine(AnalyticRuleType ruleType,
            EngineFamilyType engineFamily, 
            EngineType engineType)
        {
            service.GetAnalyticRuleSettingsByTypeEngineFamilyAndEngineTypeCompleted += ServiceOnGetAnalyticRuleSettingsByTypeEngineFamilyAndEngineTypeCompleted;
            service.GetAnalyticRuleSettingsByTypeEngineFamilyAndEngineTypeAsync(
                (int)ruleType, (int)engineFamily, (int)engineType);
        }

        private void ServiceOnGetAnalyticRuleSettingsByTypeEngineFamilyAndEngineTypeCompleted(object sender,
            GetAnalyticRuleSettingsByTypeEngineFamilyAndEngineTypeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                if (e.Result.Count == 0)
                {
                    singleFetchedCallback.Invoke(null);
                    return;
                }
                singleFetchedCallback.Invoke(AnalyticRuleSettingsAssembler.FromDtoToDomainObject(e.Result.First()));
            }
        }

        public void FetchAllRulesForEngine(
            EngineFamilyType engineFamily, EngineType engineType)
        {
            service.GetAnalyticRuleSettingsByEngineFamilyAndEngineTypeCompleted += ServiceOnGetAnalyticRuleSettingsByEngineFamilyAndEngineTypeCompleted;
            service.GetAnalyticRuleSettingsByEngineFamilyAndEngineTypeAsync(
                (int)engineFamily, (int)engineType);
        }

        private void ServiceOnGetAnalyticRuleSettingsByEngineFamilyAndEngineTypeCompleted(object sender, 
            GetAnalyticRuleSettingsByEngineFamilyAndEngineTypeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                List<AnalyticRuleSettings> result = new List<AnalyticRuleSettings>();
                foreach (AnalyticRuleSettingsDto ruleSettingsDto in e.Result)
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromDtoToDomainObject(ruleSettingsDto));
                }
                collectionFetchedCallback.Invoke(result);
            }
        }

        public void FetchAllRulesForEngineFamily(EngineFamilyType engineFamily)
        {
            service.GetAnalyticRuleSettingsByEngineFamilyCompleted += ServiceOnGetAnalyticRuleSettingsByEngineFamilyCompleted;
            service.GetAnalyticRuleSettingsByEngineFamilyAsync((int)engineFamily);
        }

        private void ServiceOnGetAnalyticRuleSettingsByEngineFamilyCompleted(object sender, 
            GetAnalyticRuleSettingsByEngineFamilyCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                List<AnalyticRuleSettings> result = new List<AnalyticRuleSettings>();
                foreach (AnalyticRuleSettingsDto ruleSettingsDto in e.Result)
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromDtoToDomainObject(ruleSettingsDto));
                }
                collectionFetchedCallback.Invoke(result);
            }
        }

        public void FetchRulesRangeByPriorityForEngine(
            IList<AnalyticRuleType> types, EngineFamilyType engineFamily, 
            EngineType engineType)
        {
            List<int> list = new List<int>();
            foreach (AnalyticRuleType ruleType in types)
            {
                list.Add((int)ruleType);
            }
            service.GetAnalyticRuleSettingsDefaultByTypesCompleted +=
                ServiceOnGetAnalyticRuleSettingsDefaultByTypesCompleted;
            service.GetAnalyticRuleSettingsDefaultByTypesAsync(
                new ObservableCollection<int>(list), 
                (int)engineFamily, 
                (int)engineType);
        }

        private void ServiceOnGetAnalyticRuleSettingsDefaultByTypesCompleted(object sender, 
            GetAnalyticRuleSettingsDefaultByTypesCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                List<AnalyticRuleSettings> result = new List<AnalyticRuleSettings>();
                foreach (AnalyticRuleSettingsDto ruleSettingsDto in e.Result)
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromDtoToDomainObject(ruleSettingsDto));
                }
                collectionFetchedCallback.Invoke(result);
            }
        }

        public void CheckNewlyCreatedSignature(AnalyticRuleType type, 
            EngineFamilyType? familyType, EngineType? engineType)
        {
            if (engineType == null && familyType == null)
            {
                service.GetAnalyticRuleSettingsWhereFamilyAndEngineIsNullCompleted += 
                    ServiceOnGetAnalyticRuleSettingsWhereFamilyAndEngineIsNullCompleted;
                service.GetAnalyticRuleSettingsWhereFamilyAndEngineIsNullAsync((int)type);
            }
            else if (engineType == null && familyType != null)
            {
                service.GetAnalyticRuleSettingsWhereEngineIsNullCompleted += 
                    ServiceOnGetAnalyticRuleSettingsWhereEngineIsNullCompleted;
                service.GetAnalyticRuleSettingsWhereEngineIsNullAsync((int)type, (int)familyType);
            }
            else if (engineType != null && familyType != null)
            {
                service.GetAnalyticRuleSettingsByTypeEngineFamilyAndEngineTypeCompleted += 
                    ServiceOnGetAnalyticRuleSettingsByTypeEngineFamilyAndEngineTypeCompleted;

                service.GetAnalyticRuleSettingsByTypeEngineFamilyAndEngineTypeAsync(
                    (int)type, (int)familyType, (int)engineType);
            }
        }

        private void ServiceOnGetAnalyticRuleSettingsWhereEngineIsNullCompleted(object sender, 
            GetAnalyticRuleSettingsWhereEngineIsNullCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                List<AnalyticRuleSettings> result = new List<AnalyticRuleSettings>();
                foreach (AnalyticRuleSettingsDto ruleSettingsDto in e.Result)
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromDtoToDomainObject(ruleSettingsDto));
                }
                collectionFetchedCallback.Invoke(result);
            }
        }

        private void ServiceOnGetAnalyticRuleSettingsWhereFamilyAndEngineIsNullCompleted(object sender, 
            GetAnalyticRuleSettingsWhereFamilyAndEngineIsNullCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                List<AnalyticRuleSettings> result = new List<AnalyticRuleSettings>();
                foreach (AnalyticRuleSettingsDto ruleSettingsDto in e.Result)
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromDtoToDomainObject(ruleSettingsDto));
                }
                collectionFetchedCallback.Invoke(result);
            }
        }

        #region Persist And Update Existing Statistic Data

        private SuccessCallbackDelegate successCallback;
        List<AnalyticRuleSettings> itemsToUpdate =
                new List<AnalyticRuleSettings>();

        public AnalyticRuleSettingsPersistency(
            SuccessCallbackDelegate successCallback,
            ErrorCallbackDelegate errorCallback)
        {
            this.errorCallback = errorCallback;
            this.successCallback = successCallback;
        }

        /*public void PersistOrUpdateStatisticsAndOverrides(
            List<AnalyticRuleSettings> items)
        {
            context = new AnalyticRuleSettingsEntityContext();
            foreach (AnalyticRuleSettings item in items)
            {
                if (item.Id == 0)
                {
                    AnalyticRuleSettingsAssembler asm =
                        new AnalyticRuleSettingsAssembler(item);
                    context.AnalyticRuleSettingsEntities.Add(asm.Disassemble());
                }
                else
                {
                    itemsToUpdate.Add(item);
                }
            }
            context.SubmitChanges(OnPersisted, null);
        }*/

        /*private void OnPersisted(SubmitOperation op)
        {
            if (op.HasError)
            {
                errorCallback.Invoke(op.Error, op.Error.Message);
            }
            else
            {
                List<long> idsToUpdate = new List<long>();
                foreach (AnalyticRuleSettings item in itemsToUpdate)
                {
                    idsToUpdate.Add(item.Id);
                }
                EntityQuery<AnalyticRuleSettingsEntity> query = 
                    context.GetAnalyticRuleSettingsByIdsListQuery(idsToUpdate);
                context.Load(query, OnItemsToUpdateRetrieved, null);
            }
        }*/

        /*private void OnItemsToUpdateRetrieved(
            LoadOperation<AnalyticRuleSettingsEntity> op)
        {
            if (op.HasError)
            {
                errorCallback.Invoke(op.Error, op.Error.Message);
            }
            else
            {
                foreach (AnalyticRuleSettingsEntity attachedEntity in op.Entities)
                {
                    // 1. get source local entity
                    AnalyticRuleSettingsAssembler asm =
                        new AnalyticRuleSettingsAssembler(
                            itemsToUpdate.First(i => i.Id == attachedEntity.Id));
                    AnalyticRuleSettingsEntity localEntity = asm.Disassemble();
                    AnalyticRuleSettingsAssembler.CopyEntityProperties(localEntity, attachedEntity);
                }
                context.SubmitChanges(OnSubmitted, null);
            }
        }*/

        /*private void OnSubmitted(SubmitOperation op)
        {
            if (op.HasError)
            {
                errorCallback.Invoke(op.Error, op.Error.Message);
            }
            else
            {
                successCallback.Invoke();
            }
        }*/

        /*private void CopyAtomEntityData(
            SettingsAtomDto source, SettingsAtomDto target)
        {
            target.MaxAcceptable = source.MaxAcceptable;
            target.MinAcceptable = source.MinAcceptable;
            target.MaxOptimal = source.MaxOptimal;
            target.MinOptimal = source.MinOptimal;
        }*/

        #endregion

        #region Persist

        public void Persist(AnalyticRuleSettings item)
        {
            AnalyticRuleSettingsDto dto = AnalyticRuleSettingsAssembler.FromDomainObjectToDto(item);
            service.SubmitAnalyticRuleSettingsCompleted += ServiceOnSubmitAnalyticRuleSettingsCompleted;
            service.SubmitAnalyticRuleSettingsAsync(dto);
        }

        private void ServiceOnSubmitAnalyticRuleSettingsCompleted(object sender, 
            AsyncCompletedEventArgs e)
        {
            service.SubmitAnalyticRuleSettingsCompleted -= ServiceOnSubmitAnalyticRuleSettingsCompleted;
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                successCallback.Invoke();
            }
        }

        #endregion

        #region Update

        public void Update(AnalyticRuleSettings item)
        {
            AnalyticRuleSettingsDto dto = AnalyticRuleSettingsAssembler.FromDomainObjectToDto(item);
            service.SubmitAnalyticRuleSettingsCompleted += ServiceOnSubmitAnalyticRuleSettingsCompleted;
            service.SubmitAnalyticRuleSettingsAsync(dto);
        }

        #endregion
    }
}

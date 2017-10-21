using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using VTSWebService.DataContracts;

namespace VTSWebService
{
    [ServiceContract]
    public interface IVtsWebService
    {
        [OperationContract]
        string CheckConnection();

        [OperationContract]
        bool CheckUsernameAvailability(string username);

        [OperationContract]
        UserDto AuthenticateUser(string loginOrEmail, string passwordHash);

        [OperationContract]
        AgentVersionDto GetLastAgentVersion();

        [OperationContract]
        void RegisterUser(UserDto user);

        [OperationContract]
        int GetUserRoleByUserId(long userId);

        [OperationContract]
        int GetPartnersVehiclesCount(long partnerId);

        [OperationContract]
        List<UserDto> GetClientsForPartner(string login, string passwordHash);

        [OperationContract]
        List<UserDto> GetAllUsers(string administrativeLogin, string administrativePasswordHash);

        [OperationContract]
        List<UserDto> GetAllPartnerClients(string partnerLogin, string partnerPasswordHash);

        [OperationContract]
        void ProvideAccessToVehicleForClientUsingEmail(long vehicleId, string clientEmail, 
            string partnerLogin, string partnerPasswordHash);

        [OperationContract]
        void RevokeAccessToVehicleForClientUsingEmail(long vehicleId, string clientEmail,
            string partnerLogin, string partnerPasswordHash);

        [OperationContract]
        List<VehicleDto> GetUsersVehicles(string login, string passwordHash);

        [OperationContract]
        VehicleDto GetVehicleByVin(string vin);

        [OperationContract]
        [FaultContract(typeof(VtsWebServiceException))]
        bool CheckWhetherVinIsSupported(string vin);

        [OperationContract]
        List<string> CheckVinsReturnUnsupported(List<string> vins);

        [OperationContract]
        VehicleInformationDto GetVehicleInformationByVin(string vin);

        [OperationContract]
        List<string> ReturnUnsupportedVinsFromTheList(List<string> vins);

        [OperationContract]
        List<AgentVersionDto> GetAgentVersions();

        [OperationContract]
        List<string> CheckVinsReturnUnregistered(List<string> vins);

        [OperationContract]
        void RegisterVehicle(string vin);

        [OperationContract]
        void RegisterVehicles(List<string> vins);

        [OperationContract]
        void AssociateVehicleWithUser(string vin, string userLogin, string userPasswordHash);

        [OperationContract]
        void AssociateVehiclesWithUser(List<string> vin, string userLogin, string userPasswordHash);

        [OperationContract]
        VehicleCharacteristicsDto GetVehicleCharacteristics(string vin, int lang);

        [OperationContract]
        List<VehicleInformationDto> GetVehiclesInformation(List<long> vehicleIds);

        [OperationContract]
        List<VehicleDto> GetVehiclesForUser(string login, string passwordHash);

        [OperationContract]
        List<string> GetDesktopMessages(string login, string passwordHash);

        [OperationContract]
        void UpdateVehicleMileage(string vin, int newMileage);

        #region AnalysisCore

        // deprecated
        [OperationContract]
        void SubmitAnalyticStatistics(AnalyticStatisticsDto statistics);

        [OperationContract]
        List<AnalyticRuleSettingsDto> GetAnalyticRuleSettings();

        [OperationContract]
        List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsByType(int type);

        [OperationContract]
        List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsByTypeAndEngineFamily(int type, int engineFamilyType);

        [OperationContract]
        List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsByTypeEngineFamilyAndEngineType(int type, int engineFamilyType, int engineType);

        [OperationContract]
        List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsByEngineFamilyAndEngineType(int engineFamilyType, int engineType);

        [OperationContract]
        List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsByEngineFamily(int engineFamilyType);

        [OperationContract]
        List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsDefaultByTypes(List<int> types, int engineFamilyType, int engineType);

        [OperationContract]
        List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsBySignature(List<int> signatureRuleTypes, List<int> signatureEngineTypes);

        [OperationContract]
        List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsWhereFamilyAndEngineIsNull(int ruleType);

        [OperationContract]
        List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsWhereEngineIsNull(int ruleType, int engineFamilyType);

        [OperationContract]
        List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsByIds(List<long> ids);

        [OperationContract]
        void SubmitAnalyticRuleSettings(AnalyticRuleSettingsDto settings);

        [OperationContract]
        void AggregateStatistics();

        [OperationContract]
        List<int> GetAvailableAnalyticStatisticsTypesForVehicle(string vin);

        [OperationContract]
        List<AnalyticStatisticsValueDto> GetAnalyticStatisticsPerVehicle(string vin, int analyticRuleType);

        [OperationContract]
        AnalyticRuleSettingsDto GetSettingsForVehicle(string vin, int ruleType, string login, string passwordHash);

        [OperationContract]
        List<AnalyticRuleSettingsDto> GetAllSettingsForVehicle(string vin);

            #endregion

        #region DataOperations

        // deprecated
        [OperationContract]
        List<PsaDatasetDto> GetDatasetsForVehicle(string vin);

        [OperationContract]
        List<long> GetDatasetIdsForVehicle(string vin, string userLogin, string userPasswordHash);

        [OperationContract]
        PsaDatasetDto GetDatasetById(long id, string userLogin, string userPasswordHash);

        [OperationContract]
        void UploadDataset(PsaDatasetDto dataset);

        [OperationContract]
        void UploadDatasets(List<PsaDatasetDto> datasets);

        [OperationContract]
        void DeleteDataset(long datasetId, string login, string passwordHash);

        [OperationContract]
        void DeleteTrace(long traceId, string login, string passwordHash);

        [OperationContract]
        int GetDatasetsCount();

        [OperationContract]
        int GetDatasetsCountForVehicle(long vehicleId);

        [OperationContract]
        List<PsaDatasetDto> GetNextPageForVehicle(long lastId, int pageSize, long vehicleId);

        [OperationContract]
        List<PsaDatasetDto> GetNextPage(long lastId, int pagesize);

        [OperationContract]
        PsaParameterDataDto GetParameterById(long id);

            #endregion

        #region VehicleEvents

        [OperationContract]
        List<VehicleEventDto> GetUpcomingVehicleEvents(string login, string password);

        [OperationContract]
        List<VehicleEventDto> GetVehicleEvents(string vin);

        [OperationContract]
        void SubmitVehicleEvent(VehicleEventDto e);

        [OperationContract]
        void DeleteVehicleEvent(VehicleEventDto e);

        #endregion

        #region NewsManagement

        [OperationContract]
        List<SystemNewsDto> NewsGetLast(int topCount);

        [OperationContract]
        List<SystemNewsDto> NewsGetAll();

        [OperationContract]
        SystemNewsDto NewsGet(long id);

        [OperationContract]
        void NewsUpdate(SystemNewsDto item);

        [OperationContract]
        void NewsPersist(SystemNewsDto item);

        [OperationContract]
        void NewsDelete(long id);

        #endregion

        #region UnrecognizedData

        [OperationContract]
        void ReportUnrecognizedData(Stream dataStream);

        #endregion
    }
}

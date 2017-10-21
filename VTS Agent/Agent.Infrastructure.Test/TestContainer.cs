using System;
using System.Collections.Generic;
using Agent.Network.Monitor.VtsWebService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Agent.Infrastructure.Test
{
    [TestClass]
    public class TestContainer
    {
        Mock<IVtsWebService> svcMock = new Mock<IVtsWebService>();

        [TestMethod]
        public void TestMappingToStatic()
        {
            Container.AddConstant(typeof(IVtsWebService), svcMock.Object);
            var service = Container.GetInstance<IVtsWebService>();
            Assert.AreSame(svcMock.Object, service);
        }

        [TestMethod]
        public void TestMappingToDynamic()
        {
            Container.RemoveConstant(typeof(IVtsWebService));
            var d = new Dictionary<Type, Type>();
            d.Add(typeof (IVtsWebService), typeof (TestWebService));
            Container.Initialize(d);
            var t = Container.GetInstance<IVtsWebService>();
            Assert.IsInstanceOfType(t, typeof(TestWebService));
        }

        #region testclass
        private class TestWebService : IVtsWebService
        {

            public string CheckConnection()
            {
                throw new NotImplementedException();
            }

            public bool CheckUsernameAvailability(string username)
            {
                throw new NotImplementedException();
            }

            public UserDto AuthenticateUser(string login, string passwordHash)
            {
                throw new NotImplementedException();
            }

            public AgentVersionDto GetLastAgentVersion()
            {
                throw new NotImplementedException();
            }

            public void RegisterUser(UserDto user)
            {
                throw new NotImplementedException();
            }

            public int GetUserRoleByUserId(long userId)
            {
                throw new NotImplementedException();
            }

            public int GetPartnersVehiclesCount(long partnerId)
            {
                throw new NotImplementedException();
            }

            public UserDto[] GetClientsForPartner(string login, string passwordHash)
            {
                throw new NotImplementedException();
            }

            public UserDto[] GetAllUsers(string administrativeLogin, string administrativePasswordHash)
            {
                throw new NotImplementedException();
            }

            public UserDto[] GetAllPartnerClients(string partnerLogin, string partnerPasswordHash)
            {
                throw new NotImplementedException();
            }

            public void ProvideAccessToVehicleForClientUsingEmail(long vehicleId, string clientEmail, string partnerLogin, string partnerPasswordHash)
            {
                throw new NotImplementedException();
            }

            public void RevokeAccessToVehicleForClientUsingEmail(long vehicleId, string clientEmail, string partnerLogin, string partnerPasswordHash)
            {
                throw new NotImplementedException();
            }

            public VehicleDto[] GetUsersVehicles(string login, string passwordHash)
            {
                throw new NotImplementedException();
            }

            public VehicleDto GetVehicleByVin(string vin)
            {
                throw new NotImplementedException();
            }

            public bool CheckWhetherVinIsSupported(string vin)
            {
                throw new NotImplementedException();
            }

            public string[] CheckVinsReturnUnsupported(string[] vins)
            {
                throw new NotImplementedException();
            }

            public VehicleInformationDto GetVehicleInformationByVin(string vin)
            {
                throw new NotImplementedException();
            }

            public string[] ReturnUnsupportedVinsFromTheList(string[] vins)
            {
                throw new NotImplementedException();
            }

            public AgentVersionDto[] GetAgentVersions()
            {
                throw new NotImplementedException();
            }

            public string[] CheckVinsReturnUnregistered(string[] vins)
            {
                throw new NotImplementedException();
            }

            public void RegisterVehicle(string vin)
            {
                throw new NotImplementedException();
            }

            public void RegisterVehicles(string[] vins)
            {
                throw new NotImplementedException();
            }

            public void AssociateVehicleWithUser(string vin, string userLogin, string userPasswordHash)
            {
                throw new NotImplementedException();
            }

            public void AssociateVehiclesWithUser(string[] vin, string userLogin, string userPasswordHash)
            {
                throw new NotImplementedException();
            }

            public VehicleCharacteristicsDto GetVehicleCharacteristics(string vin, int lang)
            {
                throw new NotImplementedException();
            }

            public VehicleInformationDto[] GetVehiclesInformation(long[] vehicleIds)
            {
                throw new NotImplementedException();
            }

            public VehicleDto[] GetVehiclesForUser(string login, string passwordHash)
            {
                throw new NotImplementedException();
            }

            public string[] GetDesktopMessages(string login, string passwordHash)
            {
                throw new NotImplementedException();
            }

            public void UpdateVehicleMileage(string vin, int newMileage)
            {
                throw new NotImplementedException();
            }

            public void SubmitAnalyticStatistics(AnalyticStatisticsDto statistics)
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto[] GetAnalyticRuleSettings()
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto[] GetAnalyticRuleSettingsByType(int type)
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto[] GetAnalyticRuleSettingsByTypeAndEngineFamily(int type, int engineFamilyType)
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto[] GetAnalyticRuleSettingsByTypeEngineFamilyAndEngineType(int type, int engineFamilyType, int engineType)
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto[] GetAnalyticRuleSettingsByEngineFamilyAndEngineType(int engineFamilyType, int engineType)
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto[] GetAnalyticRuleSettingsByEngineFamily(int engineFamilyType)
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto[] GetAnalyticRuleSettingsDefaultByTypes(int[] types, int engineFamilyType, int engineType)
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto[] GetAnalyticRuleSettingsBySignature(int[] signatureRuleTypes, int[] signatureEngineTypes)
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto[] GetAnalyticRuleSettingsWhereFamilyAndEngineIsNull(int ruleType)
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto[] GetAnalyticRuleSettingsWhereEngineIsNull(int ruleType, int engineFamilyType)
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto[] GetAnalyticRuleSettingsByIds(long[] ids)
            {
                throw new NotImplementedException();
            }

            public void SubmitAnalyticRuleSettings(AnalyticRuleSettingsDto settings)
            {
                throw new NotImplementedException();
            }

            public void AggregateStatistics()
            {
                throw new NotImplementedException();
            }

            public int[] GetAvailableAnalyticStatisticsTypesForVehicle(string vin)
            {
                throw new NotImplementedException();
            }

            public AnalyticStatisticsValueDto[] GetAnalyticStatisticsPerVehicle(string vin, int analyticRuleType)
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto GetSettingsForVehicle(string vin, int ruleType, string login, string passwordHash)
            {
                throw new NotImplementedException();
            }

            public AnalyticRuleSettingsDto[] GetAllSettingsForVehicle(string vin)
            {
                throw new NotImplementedException();
            }

            public PsaDatasetDto[] GetDatasetsForVehicle(string vin)
            {
                throw new NotImplementedException();
            }

            public long[] GetDatasetIdsForVehicle(string vin, string userLogin, string userPasswordHash)
            {
                throw new NotImplementedException();
            }

            public PsaDatasetDto GetDatasetById(long id, string userLogin, string userPasswordHash)
            {
                throw new NotImplementedException();
            }

            public void UploadDataset(PsaDatasetDto dataset)
            {
                throw new NotImplementedException();
            }

            public void UploadDatasets(PsaDatasetDto[] datasets)
            {
                throw new NotImplementedException();
            }

            public void DeleteDataset(long datasetId, string login, string passwordHash)
            {
                throw new NotImplementedException();
            }

            public void DeleteTrace(long traceId, string login, string passwordHash)
            {
                throw new NotImplementedException();
            }

            public int GetDatasetsCount()
            {
                throw new NotImplementedException();
            }

            public int GetDatasetsCountForVehicle(long vehicleId)
            {
                throw new NotImplementedException();
            }

            public PsaDatasetDto[] GetNextPageForVehicle(long lastId, int pageSize, long vehicleId)
            {
                throw new NotImplementedException();
            }

            public PsaDatasetDto[] GetNextPage(long lastId, int pagesize)
            {
                throw new NotImplementedException();
            }

            public VehicleEventDto[] GetUpcomingVehicleEvents(string login, string password)
            {
                throw new NotImplementedException();
            }

            public VehicleEventDto[] GetVehicleEvents(string vin)
            {
                throw new NotImplementedException();
            }

            public void SubmitVehicleEvent(VehicleEventDto e)
            {
                throw new NotImplementedException();
            }

            public void DeleteVehicleEvent(VehicleEventDto e)
            {
                throw new NotImplementedException();
            }

            public SystemNewsDto[] NewsGetLast(int topCount)
            {
                throw new NotImplementedException();
            }

            public SystemNewsDto[] NewsGetAll()
            {
                throw new NotImplementedException();
            }

            public SystemNewsDto NewsGet(long id)
            {
                throw new NotImplementedException();
            }

            public void NewsUpdate(SystemNewsDto item)
            {
                throw new NotImplementedException();
            }

            public void NewsPersist(SystemNewsDto item)
            {
                throw new NotImplementedException();
            }

            public void NewsDelete(long id)
            {
                throw new NotImplementedException();
            }

            public void ReportUnrecognizedData(System.IO.Stream dataStream)
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}

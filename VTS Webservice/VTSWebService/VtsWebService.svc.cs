using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using VTS.AnalysisCore.Common;
 
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWebService.AnalysisCore.Aggregation;
using VTSWebService.AnalysisCore.Recognition;
using VTSWebService.AnalysisCore.Statistics;
using VTSWebService.DataAccess;
using VTSWebService.DataContracts;
using VTSWebService.DesktopMessages;
using VTSWebService.DomainObjects.Assemblers;
using VTSWebService.Emailing;
using VTSWebService.VendorInfo;
using UserEntity = VTSWebService.DataAccess.User;
using AgentVersionEntity = VTSWebService.DataAccess.AgentVersion;
using VehicleEntity = VTSWebService.DataAccess.Vehicle;
using PsaDatasetEntity = VTSWebService.DataAccess.PsaDataset;
using PsaTraceEntity = VTSWebService.DataAccess.PsaTrace;
using VehicleEventEntity = VTSWebService.DataAccess.VehicleEvent;
using AnalyticRuleSettingsEntity = VTSWebService.DataAccess.AnalyticRuleSettings;
using AnalyticStatisticsValueEntity = VTSWebService.DataAccess.AnalyticStatisticsValue;
using AnalyticStatisticsItemEntity = VTSWebService.DataAccess.AnalyticStatisticsItem;
using SystemNews = VTSWebService.DataAccess.SystemNews;
using UserVehicleAssociationEntity = VTSWebService.DataAccess.UserVehicleAssociation;
using PsaParameterDataEntity = VTSWebService.DataAccess.PsaParameterData;

namespace VTSWebService
{
    public class VtsWebService : IVtsWebService
    {
        public string CheckConnection()
        {
            return "ok";
        }

        public AgentVersionDto GetLastAgentVersion()
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                AgentVersionEntity lastVersion = database.AgentVersion.
                    OrderByDescending(v => v.ReleasedDate).FirstOrDefault();
                VTS.Shared.DomainObjects.AgentVersion version = 
                    AgentVersionAssembler.FromEntityToDomainObject(lastVersion);
                return AgentVersionAssembler.FromDomainObjectToDto(version);
            }
        }

        public bool CheckUsernameAvailability(string username)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                return database.User.Any(u => u.Login == username);
            }
        }

        public int GetPartnersVehiclesCount(long partnerId)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                return database.UserVehicleAssociation.Count(uva => uva.UserEntityId == partnerId);
            }
        }

        public List<VehicleDto> GetUsersVehicles(string login, string passwordHash)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                UserEntity user = database.User.FirstOrDefault(
                    u => u.Login == login && u.PasswordHash == passwordHash);
                if (user == null)
                {
                    throw new Exception("User not found!");
                }
                IList<UserVehicleAssociation> associations =
                    database.UserVehicleAssociation.Where(uva => 
                        uva.UserEntityId == user.Id).ToList();
                IList<long> vehicleIds = new List<long>();
                foreach (UserVehicleAssociation association in associations)
                {
                    if (!vehicleIds.Contains(association.VehicleEntityId))
                    {
                        vehicleIds.Add(association.VehicleEntityId);
                    }
                }
                List<VehicleEntity> entities = database.Vehicle.Where(v => 
                    vehicleIds.Contains(v.Id)).ToList();
                return entities.Select(e => VehicleAssembler.FromEntityToDto(e)).ToList();
            }
        }

        public VehicleDto GetVehicleByVin(string vin)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                string vinU = vin.ToUpper();
                return VehicleAssembler.FromEntityToDto(
                    database.Vehicle.First(v => v.Vin == vinU));
            }
        }

        public bool CheckWhetherVinIsSupported(string vin)
        {
            if (String.IsNullOrWhiteSpace(vin))
            {
                throw new FaultException<VtsWebServiceException>(
                    new VtsWebServiceException("VIN cannot be null."));
            }
            string vinU = vin.ToUpper();
            VTS.Shared.DomainObjects.VehicleCharacteristics ch = 
                GetCharacteristicsByVin(vinU, SupportedLanguage.English);
            if (ch == null)
            {
                RegisterUnsupportedVin(vinU);
                return false;
            }
            try
            {
                RecognizeCharacteristics(vinU, ch);
                return true;
            }
            catch (NotSupportedException)
            {
                RegisterUnsupportedVin(vinU);
                return false;
            }
        }

        public List<string> CheckVinsReturnUnsupported(List<string> vins)
        {
            return ReturnUnsupportedVinsFromTheList(vins);
        }

        public VehicleInformationDto GetVehicleInformationByVin(string vin)
        {
            string vinU = vin.ToUpper();
            VehicleInformation info = RecognizeCharacteristics(vinU, 
                GetCharacteristicsByVin(vinU, SupportedLanguage.English));
            return VehicleInformationAssembler.FromDomainObjectToDto(info);
        }

        public List<string> ReturnUnsupportedVinsFromTheList(List<string> vins)
        {
            List<string> result = new List<string>();
            foreach (string vin in vins)
            {
                string vinu = vin.ToUpper();
                try
                {
                    VehicleInformationDto info = GetVehicleInformationByVin(vinu);
                    if (info == null)
                    {
                        result.Add(vinu);
                        RegisterUnsupportedVin(vinu);
                    }
                }
                catch (NotSupportedException)
                {
                    result.Add(vinu);
                    RegisterUnsupportedVin(vinu);
                }
            }
            return result;
        }

        private void RegisterUnsupportedVin(string vin)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                database.UnrecognizedVin.Add(
                    new UnrecognizedVin()
                    {
                        Date = DateTime.Now, Vin = vin
                    });
                database.SaveChanges();
                Emailer.NotifyAdminAboutNewUnrecognizedVin(vin);
            }
        }

        public List<AgentVersionDto> GetAgentVersions()
        {
            List<AgentVersionDto> result = new List<AgentVersionDto>();
            using (VTSDatabase db = new VTSDatabase())
            {
                foreach (AgentVersionEntity entity in db.AgentVersion)
                {
                    result.Add(AgentVersionAssembler.FromEntityToDto(entity));
                }
            }
            return result;
        }

        public List<string> CheckVinsReturnUnregistered(List<string> vins)
        {
            List<string> result = new List<string>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (string vin in vins)
                {
                    string vinU = vin.ToUpper();
                    if (!database.Vehicle.Any(v => v.Vin == vinU))
                    {
                        result.Add(vinU);
                    }
                }
            }
            return result;
        }

        public void RegisterVehicle(string vin)
        {
            RegisterVehiclePrivate(vin);
        }

        public void RegisterVehicles(List<string> vins)
        {
            foreach (string vin in vins)
            {
                RegisterVehiclePrivate(vin);
            }
        }

        public void AssociateVehicleWithUser(string vin, string userLogin, string userPasswordHash)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                string vinU = vin.ToUpper();
                UserEntity user = database.User.First(u => 
                    u.Login == userLogin && u.PasswordHash == userPasswordHash);
                VehicleEntity veh = database.Vehicle.FirstOrDefault(v => v.Vin == vinU);
                if (veh == null)
                {
                    // This vehicle has not been registered or is not supported
                    return;
                }
                long userId = user.Id;
                long vehicleId = veh.Id;
                if (!database.UserVehicleAssociation.Any(uva =>
                    uva.VehicleEntityId == vehicleId && uva.UserEntityId == userId))
                {
                    UserVehicleAssociation association = new UserVehicleAssociation();
                    association.UserEntityId = userId;
                    association.VehicleEntityId = vehicleId;
                    database.UserVehicleAssociation.Add(association);
                    database.SaveChanges();
                    Emailer.NotifyAdminAboutVehicleAssociationWithUser(veh.ModelName, veh.Vin, user.Login, user.Id);
                }
            }
        }

        public void AssociateVehiclesWithUser(List<string> vins, string userLogin, string userPasswordHash)
        {
            foreach (string vin in vins)
            {
                AssociateVehicleWithUser(vin, userLogin, userPasswordHash);
            }
        }

        private void RegisterVehiclePrivate(string vin)
        {
            string vinU = vin.ToUpper();
            using (VTSDatabase database = new VTSDatabase())
            {
                if (database.Vehicle.Any(v => v.Vin == vinU))
                {
                    return; // already registered
                }
                VTS.Shared.DomainObjects.Vehicle vehicle = new VTS.Shared.DomainObjects.Vehicle();
                VTS.Shared.DomainObjects.VehicleCharacteristics chars =
                    GetCharacteristicsByVin(vinU, SupportedLanguage.English);
                VehicleInformation info = RecognizeCharacteristics(vinU, chars);
                vehicle.Vin = vinU;
                vehicle.Manufacturer = VinChecker.GetManufacturer(vinU);
                vehicle.RegisteredDate = DateTime.Now;
                vehicle.Model = info.VehicleModel;
                vehicle.ProductionYear = info.ProductionYear;
                VehicleEntity entity = VehicleAssembler.FromDomainObjectToEntity(vehicle);
                database.Vehicle.Add(entity);
                database.SaveChanges();
                Emailer.NotifyAdminAboutVehicleRegistration(vehicle.Vin, vehicle.Model);
            }
        }

        public VehicleCharacteristicsDto GetVehicleCharacteristics(string vin, int language)
        {
            string vinU = vin.ToUpper();
            VehicleCharacteristicsManager manager = 
                new VehicleCharacteristicsManager();
            VTS.Shared.DomainObjects.VehicleCharacteristics chars = 
                manager.GetVehicleCharacteristicsForVin(vinU, (SupportedLanguage)language);
            return VehicleCharacteristicsAssembler.FromDomainObjectToDto(chars);
        }

        public List<VehicleInformationDto> GetVehiclesInformation(List<long> vehicleIds)
        {
            List<VehicleInformationDto> result = new List<VehicleInformationDto>();
            List<string> vins= new List<string>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (long vehicleId in vehicleIds)
                {
                    string vin = database.Vehicle.First(v => v.Id == vehicleId).Vin;
                    if (!vins.Contains(vin, StringComparer.InvariantCultureIgnoreCase))
                    {
                        vins.Add(vin);
                    }
                }
            }
            foreach (string vin in vins)
            {
                VehicleInformationDto info = GetVehicleInformationByVin(vin);
                if (info != null)
                {
                    result.Add(info);
                }
            }
            return result;
        }

        public List<VehicleDto> GetVehiclesForUser(string login, string passwordHash)
        {
            List<VehicleDto> result = new List<VehicleDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (UserVehicleAssociation association in database.UserVehicleAssociation.
                    Where(a => a.User.Login == login && a.User.PasswordHash ==passwordHash))
                {
                    result.Add(VehicleAssembler.FromEntityToDto(association.Vehicle));
                }
            }
            return result;
        }

        public List<string> GetDesktopMessages(string login, string passwordHash)
        {
            UserAssembler asm = new UserAssembler();
            VTS.Shared.DomainObjects.User user;
            using (VTSDatabase database = new VTSDatabase())
            {
                UserEntity entity = database.User.First(u => 
                    u.Login == login && u.PasswordHash == passwordHash);
                user = asm.FromEntityToDomainObject(entity);
            }
            DesktopMessagesProvider provider = new DesktopMessagesProvider(user);
            return provider.Provide();
        }

        public void UpdateVehicleMileage(string vin, int newMileage)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                string vinU = vin.ToUpper();
                VehicleEntity veh = database.Vehicle.
                    FirstOrDefault(v => v.Vin == vinU);
                if (veh == null)
                {
                    return;
                }
                veh.Mileage = newMileage;
                database.SaveChanges();
            }
        }

        #region UserOperations

        public List<UserDto> GetClientsForPartner(string login, string passwordHash)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                UserEntity partner = database.User.First(u =>
                    u.Login == login && u.PasswordHash == passwordHash);
                IList<UserEntity> clients = database.User.Where(u =>
                    u.PrincipalId == partner.Id).ToList();
                List<UserDto> result = new List<UserDto>();
                UserAssembler asm = new UserAssembler();
                foreach (UserEntity client in clients)
                {
                    result.Add(asm.FromEntityToDto(client));
                }
                return result;
            }
        }

        public List<UserDto> GetAllUsers(string administrativeLogin, string administrativePasswordHash)
        {
            List<UserDto> result = new List<UserDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                UserEntity admin = database.User.First(u =>
                    u.Login == administrativeLogin && u.PasswordHash == administrativePasswordHash);
                if (admin == null)
                {
                    throw new Exception("Administrative privileges required.");
                }
                UserAssembler asm = new UserAssembler();
                foreach (UserEntity client in database.User)
                {
                    result.Add(asm.FromEntityToDto(client));
                }
                return result;
            }
        }

        public UserDto AuthenticateUser(string loginOrEmail, string passwordHash)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                UserEntity user = database.User.FirstOrDefault(u => 
                    (u.Login.Equals(loginOrEmail, StringComparison.InvariantCulture) 
                        || u.Email.Equals(loginOrEmail, StringComparison.InvariantCultureIgnoreCase))
                        && u.PasswordHash.Equals(passwordHash, StringComparison.OrdinalIgnoreCase));
                if (user == null)
                {
                    return null;
                }
                UserAssembler asm = new UserAssembler();
                return asm.FromEntityToDto(user);
            }
        }

        public int GetUserRoleByUserId(long userId)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                UserEntity user = database.User.FirstOrDefault(
                    u => u.Id == userId);
                return user.Role;
            }
        }

        public void RegisterUser(UserDto user)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                UserAssembler asm = new UserAssembler();
                UserEntity userEntity = asm.FromDtoToEntity(user);
                database.User.Add(userEntity);
                database.SaveChanges();
                Emailer.NotifyAdminAboutUserRegistration(user.Login, user.Email);
            }
        }

        public List<UserDto> GetAllPartnerClients(string partnerLogin, string partnerPasswordHash)
        {
            List<UserDto> result = new List<UserDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                UserEntity partner = database.User.First(u =>
                    u.Login == partnerLogin &&
                    u.PasswordHash == partnerPasswordHash);
                if (partner == null)
                {
                    throw new Exception("Partner privileges required or partner not found.");
                }
                UserAssembler asm = new UserAssembler();
                foreach (UserEntity client in database.User.Where(u => u.PrincipalId == partner.Id))
                {
                    result.Add(asm.FromEntityToDto(client));
                }
                return result;
            }
        }

        public void ProvideAccessToVehicleForClientUsingEmail(long vehicleId, string clientEmail,
            string partnerLogin, string partnerPasswordHash)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                UserEntity partner =
                    database.User.First(u =>
                        u.Login == partnerLogin &&
                        u.PasswordHash == partnerPasswordHash);
                VehicleEntity vehicle = database.Vehicle.
                    First(v => v.Id == vehicleId);
                UserEntity client = database.User.FirstOrDefault(u =>
                    u.Email.Equals(clientEmail,
                    StringComparison.InvariantCultureIgnoreCase));
                if (client == null)
                {
                    RegisterNewClientWithNotification(clientEmail, partner, database);
                    client = database.User.FirstOrDefault(u =>
                        u.Email.Equals(clientEmail,
                        StringComparison.InvariantCultureIgnoreCase));
                }
                ProvideClientAccessToVehicleWithNotification(client, vehicle, database);
            }
        }

        private void RegisterNewClientWithNotification(
            string email,
            UserEntity principalPartner,
            VTSDatabase db)
        {
            string username = Guid.NewGuid().ToString().Substring(0, 6);
            string password = Guid.NewGuid().ToString().Substring(24, 8);
            UserEntity newClient = new UserEntity();
            newClient.Email = email;
            newClient.Login = username;
            newClient.PasswordHash = Sha256Hash.Calculate(password);
            newClient.PrincipalId = principalPartner.Id;
            newClient.Phone = "???";
            newClient.Name = "???";
            newClient.RegisteredDate = DateTime.Now;
            newClient.Role = (int)UserRole.Client;
            newClient.Surname = "???";
            db.User.Add(newClient);
            db.SaveChanges();
            Emailer.NotifyClientAboutRegistration(email, username, password);
        }

        private void ProvideClientAccessToVehicleWithNotification(
            UserEntity client,
            VehicleEntity vehicle,
            VTSDatabase database)
        {
            UserVehicleAssociationEntity association =
                database.UserVehicleAssociation.FirstOrDefault(ass => 
                    ass.UserEntityId == client.Id && 
                    ass.VehicleEntityId == vehicle.Id);
            if (association == null)
            {
                UserVehicleAssociationEntity newAssociation = 
                    new UserVehicleAssociation();
                newAssociation.User = client;
                newAssociation.UserEntityId = client.Id;
                newAssociation.Vehicle = vehicle;
                newAssociation.VehicleEntityId = vehicle.Id;
                newAssociation.License = null;
                database.UserVehicleAssociation.Add(newAssociation);
                database.SaveChanges();
            }
            Emailer.NotifyClientAboutVehicleAssociation(client.Email, 
                ((Manufacturer)vehicle.Manufacturer).ToString(),
                vehicle.ModelName, 
                vehicle.Vin);
        }

        public void RevokeAccessToVehicleForClientUsingEmail(long vehicleId, string clientEmail,
            string partnerLogin, string partnerPasswordHash)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                UserVehicleAssociationEntity ass =
                    database.UserVehicleAssociation.FirstOrDefault(
                        a => a.VehicleEntityId == vehicleId && a.User.Email == clientEmail);
                if (ass != null)
                {
                    database.UserVehicleAssociation.Remove(ass);
                    database.SaveChanges();
                }
            }
        }
        
        #endregion

        #region AnalysisCore

        // deprecated and will be removed later
        public void SubmitAnalyticStatistics(AnalyticStatisticsDto statistics)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (AnalyticStatisticsItemEntity item in database.AnalyticStatisticsItem)
                {
                    database.AnalyticStatisticsItem.Remove(item);
                }
                database.SaveChanges();
                foreach (AnalyticStatisticsItemDto itemDto in statistics.Items)
                {
                    database.AnalyticStatisticsItem.Add(AnalyticStatisticsItemAssembler.FromDtoToEntity(itemDto));
                }
                database.SaveChanges();
            }
        }

        public List<AnalyticRuleSettingsDto> GetAnalyticRuleSettings()
        {
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (DataAccess.AnalyticRuleSettings ruleSettings in database.AnalyticRuleSettings)
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromEntityToDto(ruleSettings));
                }
            }
            return result;
        }

        public List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsByType(int type)
        {
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (DataAccess.AnalyticRuleSettings ruleSettings in 
                    database.AnalyticRuleSettings.Where(rs => rs.RuleType == type))
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromEntityToDto(ruleSettings));
                }
            }
            return result;
        }

        public List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsByTypeAndEngineFamily(int type, int engineFamilyType)
        {
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (DataAccess.AnalyticRuleSettings ruleSettings in
                    database.AnalyticRuleSettings.Where(rs => rs.RuleType == type && rs.EngineFamilyType == engineFamilyType))
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromEntityToDto(ruleSettings));
                }
            }
            return result;
        }

        public List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsByTypeEngineFamilyAndEngineType(int type, int engineFamilyType, int engineType)
        {
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (DataAccess.AnalyticRuleSettings ruleSettings in
                    database.AnalyticRuleSettings.Where(rs => rs.RuleType == type && rs.EngineFamilyType == engineFamilyType && rs.EngineType == engineType))
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromEntityToDto(ruleSettings));
                }
            }
            return result;
        }

        public List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsByEngineFamilyAndEngineType(int engineFamilyType, int engineType)
        {
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (DataAccess.AnalyticRuleSettings ruleSettings in
                    database.AnalyticRuleSettings.Where(rs => rs.EngineFamilyType == engineFamilyType && rs.EngineType == engineType))
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromEntityToDto(ruleSettings));
                }
            }
            return result;
        }

        public List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsByEngineFamily(int engineFamilyType)
        {
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (DataAccess.AnalyticRuleSettings ruleSettings in
                    database.AnalyticRuleSettings.Where(rs => rs.EngineFamilyType == engineFamilyType))
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromEntityToDto(ruleSettings));
                }
            }
            return result;
        }

        public List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsDefaultByTypes(
                    List<int> types, int engineFamilyType, int engineType)
        {
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();

            foreach (int type in types)
            {
                result.Add(GetDefaultSettingsBySignature(type,
                    engineFamilyType, engineType));
            }
            return result;
        }

        private AnalyticRuleSettingsDto GetDefaultSettingsBySignature(
            int ruleType, int engineFamilyType, int engineType)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                AnalyticRuleSettingsEntity exactMatch =
                    database.AnalyticRuleSettings.FirstOrDefault(
                        s => s.RuleType == ruleType &&
                             s.EngineFamilyType == engineFamilyType &&
                             s.EngineType == engineType);
                if (exactMatch != null)
                {
                    return AnalyticRuleSettingsAssembler.
                        FromEntityToDto(exactMatch);
                }
                AnalyticRuleSettingsEntity engineFamilyMatch =
                    database.AnalyticRuleSettings.FirstOrDefault(
                        s => s.RuleType == ruleType &&
                            s.EngineFamilyType == engineFamilyType &&
                            s.EngineType == null);
                if (engineFamilyMatch != null)
                {
                    return AnalyticRuleSettingsAssembler.
                        FromEntityToDto(engineFamilyMatch);
                }
                AnalyticRuleSettingsEntity typeFallbackMatch =
                    database.AnalyticRuleSettings.First(
                    s => s.RuleType == ruleType &&
                    s.EngineFamilyType == null &&
                    s.EngineType == null);
                return AnalyticRuleSettingsAssembler.
                        FromEntityToDto(typeFallbackMatch);
            }
        }

        public List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsBySignature(List<int> signatureRuleTypes, List<int> signatureEngineTypes)
        {
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                for (int i = 0; i < signatureEngineTypes.Count; i++)
                {
                    AnalyticRuleSettingsEntity entity = database.AnalyticRuleSettings.FirstOrDefault(s =>
                        s.RuleType == signatureRuleTypes[i] && s.EngineType == signatureEngineTypes[i]);
                    if (entity != null)
                    {
                        result.Add(AnalyticRuleSettingsAssembler.FromEntityToDto(entity));
                    }
                }
            }
            return result;
        }

        public List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsWhereFamilyAndEngineIsNull(int ruleType)
        {
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (AnalyticRuleSettingsEntity settingsEntity in database.AnalyticRuleSettings.Where(
                    s => s.RuleType == ruleType && s.EngineFamilyType == null && s.EngineType == null))
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromEntityToDto(settingsEntity));
                }
            }
            return result;
        }

        public List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsWhereEngineIsNull(int ruleType, int engineFamilyType)
        {
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (AnalyticRuleSettingsEntity settingsEntity in database.AnalyticRuleSettings.Where(
                    s => s.RuleType == ruleType && s.EngineFamilyType == engineFamilyType && s.EngineType == null))
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromEntityToDto(settingsEntity));
                }
            }
            return result;
        }

        public List<AnalyticRuleSettingsDto> GetAnalyticRuleSettingsByIds(List<long> ids)
        {
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (long id in ids)
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromEntityToDto(
                        database.AnalyticRuleSettings.First(s => s.Id == id)));
                }
            }
            return result;
        }

        public void SubmitAnalyticRuleSettings(AnalyticRuleSettingsDto settings)
        {
            AnalyticRuleSettingsEntity entity = 
                AnalyticRuleSettingsAssembler.FromDtoToEntity(settings);
            using (VTSDatabase database = new VTSDatabase())
            {
                if (entity.Id == 0)
                {
                    database.AnalyticRuleSettings.Add(entity);
                }
                else
                {
                    AnalyticRuleSettingsEntity entityToUpdate =
                        database.AnalyticRuleSettings.First(rs => rs.Id == settings.Id);
                    AnalyticRuleSettingsAssembler.CopyEntityProperties(entity, entityToUpdate);
                }
                database.SaveChanges();
            }
        }

        public void AggregateStatistics()
        {
            StatisticsAggregationEngine aggregationEngine = 
                new StatisticsAggregationEngine();
            aggregationEngine.Aggregate();
        }

        public List<int> GetAvailableAnalyticStatisticsTypesForVehicle(string vin)
        {
            string vinU = vin.ToUpper();
            using (VTSDatabase database = new VTSDatabase())
            {
                List<int> list = database.AnalyticStatisticsItem.
                    Where(item => item.AnalyticStatisticsValue.Any(v => v.SourceVehicleVin.Equals(vinU, StringComparison.InvariantCultureIgnoreCase))).
                    Select(it => it.Type).
                    ToList();
                return list;
            }
        }

        public List<AnalyticStatisticsValueDto> GetAnalyticStatisticsPerVehicle(
            string vin, int analyticRuleType)
        {
            string vinU = vin.ToUpper();
            List<AnalyticStatisticsValueDto> result = 
                new List<AnalyticStatisticsValueDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (AnalyticStatisticsValueEntity value in database.AnalyticStatisticsValue.Where(asv => 
                    asv.SourceVehicleVin == vinU && 
                    asv.AnalyticStatisticsItem.Type == analyticRuleType))
                {
                    result.Add(AnalyticStatisticsValueAssembler.
                        FromEntityToDto(value));
                }
            }
            return result;
        }

        public AnalyticRuleSettingsDto GetSettingsForVehicle(string vin, int ruleType, string login, string passwordHash)
        {
            string vinU = vin.ToUpper();
            using (VTSDatabase database = new VTSDatabase())
            {
                VehicleEntity vehicle = database.Vehicle.FirstOrDefault(v => v.Vin == vinU);
                if (vehicle == null)
                {
                    return null;
                }
                UserVehicleAssociationEntity association = database.UserVehicleAssociation.FirstOrDefault(
                    uva => uva.Vehicle.Vin == vinU && uva.User.Login == login && uva.User.PasswordHash == passwordHash);
                if (association == null)
                {
                    return null;
                }
                VehicleInformationDto info = GetVehicleInformationByVin(vinU);
                AnalyticRuleSettingsEntity resultEntity = database.AnalyticRuleSettings.
                    FirstOrDefault(ars =>
                        ars.RuleType == ruleType && 
                        ars.EngineFamilyType == info.Engine.FuelType &&
                        ars.EngineType == info.Engine.Type);
                if (resultEntity == null)
                {
                    return null;
                }
                return AnalyticRuleSettingsAssembler.FromEntityToDto(resultEntity);
            }
        }

        public List<AnalyticRuleSettingsDto> GetAllSettingsForVehicle(string vin)
        {
            string vinU = vin.ToUpper();
            VehicleInformationDto info = GetVehicleInformationByVin(vinU);
            List<AnalyticRuleSettingsDto> result = new List<AnalyticRuleSettingsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (AnalyticRuleSettingsEntity ruleSettings in 
                    database.AnalyticRuleSettings.Where(rs => 
                        rs.EngineFamilyType == info.Engine.Family.Type && 
                        rs.EngineType == info.Engine.Type))
                {
                    result.Add(AnalyticRuleSettingsAssembler.FromEntityToDto(ruleSettings));
                }
            }
            return result;
        }

        #endregion

        #region DataOperations

        public List<PsaDatasetDto> GetDatasetsForVehicle(string vin)
        {
            string vinU = vin.ToUpper();
            List<PsaDatasetDto> result = new List<PsaDatasetDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (PsaDatasetEntity dse in database.PsaDataset.
                    Where(ds => ds.Vehicle.Vin == vinU))
                {
                    result.Add(PsaDatasetAssembler.FromEntityToDto(dse));
                }
            }
            return result;
        }

        public List<long> GetDatasetIdsForVehicle(string vin, string userLogin, string userPasswordHash)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                string vinU = vin.ToUpper();
                UserEntity user = database.User.FirstOrDefault(u => u.Login == userLogin && u.PasswordHash == userPasswordHash);
                VehicleEntity vehicle = database.Vehicle.FirstOrDefault(v => v.Vin == vinU);
                if (user == null || vehicle == null || 
                    !database.UserVehicleAssociation.Any(uva => uva.UserEntityId == user.Id && uva.VehicleEntityId == vehicle.Id))
                {
                    return null;
                }
                return database.PsaDataset.Where(pd => pd.VehicleEntityId == vehicle.Id).Select(pd => pd.Id).ToList();
            }
        }

        public PsaDatasetDto GetDatasetById(long id, string userLogin, string userPasswordHash)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                UserEntity user = database.User.FirstOrDefault(u => u.Login == userLogin && u.PasswordHash == userPasswordHash);
                PsaDatasetEntity dataset = database.PsaDataset.FirstOrDefault(pd => pd.Id == id);
                if (dataset == null)
                {
                    return null;
                }
                VehicleEntity vehicle = dataset.Vehicle;
                if (!database.UserVehicleAssociation.Any(
                        uv => uv.VehicleEntityId == vehicle.Id && uv.UserEntityId == user.Id))
                {
                    return null;
                }
                return PsaDatasetAssembler.FromEntityToDto(dataset);
            }
        }

        public void UploadDataset(PsaDatasetDto dataset)
        {
            DatasetPersister persister = new DatasetPersister();
            Guid updatedDatasetGuid = persister.Persist(dataset);
            UpdateAnalyticDataBasedOnUpdatedDataset(updatedDatasetGuid);
        }

        private void UpdateAnalyticDataBasedOnUpdatedDataset(Guid updatedDatasetGuid)
        {
            if (updatedDatasetGuid == Guid.Empty)
            {
                return;
            }
            VTS.Shared.DomainObjects.PsaDataset dataset;
            using (VTSDatabase database = new VTSDatabase())
            {
                PsaDatasetEntity datasetEntity = database.PsaDataset.
                    FirstOrDefault(pd => pd.Guid == updatedDatasetGuid);
                if (datasetEntity == null)
                {
                    return;
                }
                dataset = PsaDatasetAssembler.FromEntityToDomainObject(datasetEntity);
            }
            if (dataset == null)
            {
                return;
            }
            StatisticsEngine engine = new StatisticsEngine();
            List<VTS.Shared.DomainObjects.AnalyticStatisticsItem> items = 
                engine.ProcessDataset(dataset);
            AnalyticStatisticsUpdater updater = 
                new AnalyticStatisticsUpdater(items);
            updater.Update();
            StatisticsAggregationEngine aggregationEngine =
                new StatisticsAggregationEngine();
            aggregationEngine.Aggregate(updater.UpdatedItemsIds);
        }

        public void UploadDatasets(List<PsaDatasetDto> datasets)
        {
            foreach (PsaDatasetDto dataset in datasets)
            {
                UploadDataset(dataset);
            }
        }

        public void DeleteDataset(long datasetId, string login, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public void DeleteTrace(long traceId, string login, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public int GetDatasetsCountForVehicle(long vehicleId)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                return database.PsaDataset.Count(d => d.VehicleEntityId == vehicleId);
            }
        }

        public int GetDatasetsCount()
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                return database.PsaDataset.Count();
            }
        }

        public List<PsaDatasetDto> GetNextPageForVehicle(long lastId, int pageSize, long vehicleId)
        {
            List<PsaDatasetDto> result = new List<PsaDatasetDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (PsaDatasetEntity dataset in database.PsaDataset.Where(d => 
                    d.VehicleEntityId == vehicleId && d.Id > lastId).Take(pageSize))
                {
                    result.Add(PsaDatasetAssembler.FromEntityToDto(dataset));
                }
            }
            return result;
        }

        public List<PsaDatasetDto> GetNextPage(long lastId, int pageSize)
        {
            List<PsaDatasetDto> result = new List<PsaDatasetDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (PsaDatasetEntity dataset in database.PsaDataset.Where(d =>
                    d.Id > lastId).Take(pageSize))
                {
                    result.Add(PsaDatasetAssembler.FromEntityToDto(dataset));
                }
            }
            return result;
        }

        public PsaParameterDataDto GetParameterById(long id)
        {
            using (var db = new VTSDatabase())
            {
                PsaParameterDataEntity entity = db.PsaParameterData.FirstOrDefault(pd => pd.Id == id);
                return entity == null ? null : PsaParameterDataAssembler.FromEntityToDto(entity);
            }
        }

        #endregion

        #region VehicleEvents 

        public List<VehicleEventDto> GetUpcomingVehicleEvents(string login, string password)
        {
            var result = new List<VehicleEventDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (UserVehicleAssociation association in database.UserVehicleAssociation.
                    Where(uva => 
                        uva.User.Login == login && 
                        uva.User.PasswordHash == password))
                {
                    long vehicleId = association.VehicleEntityId;
                    foreach (VehicleEventEntity vehicleEvent in database.VehicleEvent.
                        Where(ve => ve.VehicleEntityId == vehicleId))
                    {
                        if (RequiresAttention(vehicleEvent))
                        {
                            result.Add(VehicleEventAssembler.FromEntityToDto(vehicleEvent));
                        }
                    }
                }
            }
            return result;
        }

        private bool RequiresAttention(VehicleEventEntity vehEvent)
        {
            return true;
            /*if (vehEvent.RedMileage == null)
            {
                return false;
            }
            int mileageToReplace = vehEvent.Mileage + vehEvent.MileageUntilChange;
            int currentVehicleMileage = vehEvent.CurrentVehicleMileage;
            TimeSpan leftUntilNextChange = vehEvent.Date + TimeSpan.
                FromDays(vehEvent.NextReplacementPeriod) - DateTime.Now;
            double daysLeftUntilNextChange = leftUntilNextChange.TotalDays;

            if (mileageToReplace - threshold <= currentVehicleMileage ||
                daysLeftUntilNextChange < 30)
            {
                if (!allEvents.Any(e => vehEvent.Type == vehEvent.Type &&
                    e.Date > vehEvent.Date))
                {
                    return true;
                }
            }
            return false;*/
        }

        public List<VehicleEventDto> GetVehicleEvents(string vin)
        {
            string vinU = vin.ToUpper();
            List<VehicleEventDto> result = new List<VehicleEventDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (VehicleEventEntity vehicleEvent in database.
                    VehicleEvent.Where(ve => ve.Vehicle.Vin == vinU))
                {
                    result.Add(VehicleEventAssembler.FromEntityToDto(vehicleEvent));
                }
            }
            return result;
        }

        public void SubmitVehicleEvent(VehicleEventDto e)
        {
            VehicleEventEntity entity = VehicleEventAssembler.FromDtoToEntity(e);
            using (VTSDatabase database = new VTSDatabase())
            {
                if (entity.Id != 0)
                {
                    VehicleEventEntity entityToUpdate = database.VehicleEvent.First(ve => ve.Id == e.Id);
                    VehicleEventAssembler.CopyEntityProperties(entity, entityToUpdate);
                    database.SaveChanges();
                }
                else
                {
                    database.VehicleEvent.Add(entity);
                    database.SaveChanges();
                }
            }
        }

        public void DeleteVehicleEvent(VehicleEventDto ev)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                VehicleEventEntity entityToDelete =
                    database.VehicleEvent.First(e => e.Id == ev.Id);
                database.VehicleEvent.Remove(entityToDelete);
                database.SaveChanges();
            }
        }

        #endregion

        #region NewsManagement

        public List<SystemNewsDto> NewsGetLast(int topCount)
        {
            List<SystemNewsDto> result = new List<SystemNewsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (SystemNews newsEntity in database.SystemNews.
                    OrderByDescending(s => s.DatePublished).Take(topCount))
                {
                    result.Add(SystemNewsAssembler.FromEntityToDto(newsEntity));
                }
            }
            return result;
        }

        public List<SystemNewsDto> NewsGetAll()
        {
             List<SystemNewsDto> result = new List<SystemNewsDto>();
            using (VTSDatabase database = new VTSDatabase())
            {
                foreach (SystemNews newsEntity in database.SystemNews)
                {
                    result.Add(SystemNewsAssembler.FromEntityToDto(newsEntity));
                }
            }
            return result;
        }

        public SystemNewsDto NewsGet(long id)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                SystemNews news = database.SystemNews.FirstOrDefault(n => n.Id == id);
                if (news == null)
                {
                    return null;
                }
                return SystemNewsAssembler.FromEntityToDto(news);
            }
        }

        public void NewsUpdate(SystemNewsDto item)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                SystemNews newsSource = SystemNewsAssembler.FromDtoToEntity(item);
                SystemNews newsToUpdate = database.SystemNews.
                    FirstOrDefault(n => n.Id == item.Id);
                if (newsToUpdate == null)
                {
                    throw new Exception("Entity to update has not been found in the database.");
                }
                SystemNewsAssembler.CopyEntityProperties(newsSource, newsToUpdate);
                database.SaveChanges();
            }
        }

        public void NewsPersist(SystemNewsDto item)
        {
            SystemNews entity = SystemNewsAssembler.FromDtoToEntity(item);
            using (VTSDatabase database = new VTSDatabase())
            {
                database.SystemNews.Add(entity);
                database.SaveChanges();
            }
        }

        public void NewsDelete(long id)
        {
            using (VTSDatabase database = new VTSDatabase())
            {
                var v = database.SystemNews.First(n => n.Id == id);
                if (v == null)
                {
                    throw new Exception("Object not found in database.");
                }
                database.SystemNews.Remove(v);
                database.SaveChanges();
            }
        }

        #endregion

        #region UnrecognizedData

        public void ReportUnrecognizedData(Stream dataStream)
        {
            Emailer.NotifySystemAboutUnrecognizedData(dataStream);
        }

        #endregion

        #region Private

        private VTS.Shared.DomainObjects.VehicleCharacteristics GetCharacteristicsByVin(
            string vin, SupportedLanguage lang)
        {
            string vinU = vin.ToUpper();
            VehicleCharacteristicsManager manager =
                new VehicleCharacteristicsManager();
            VTS.Shared.DomainObjects.VehicleCharacteristics result =
                manager.GetVehicleCharacteristicsForVin(vinU, lang);
            return result;
        }

        private VehicleInformation RecognizeCharacteristics(
            string vin,
            VTS.Shared.DomainObjects.VehicleCharacteristics ch)
        {
            VehicleInfoRecognizer recognizer =
                new VehicleInfoRecognizer(vin.ToUpper(), ch);
            return recognizer.Recognize();
        }

        #endregion
    }
}


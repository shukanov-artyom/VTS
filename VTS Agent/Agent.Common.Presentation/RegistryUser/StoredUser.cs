using System;
using Agent.Localization;
using Agent.Logging;
using Agent.Network.Monitor;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Common.Presentation.RegistryUser
{
    public static class StoredSettings
    {
        private static User storedUser;

        static StoredSettings()
        {
            TranslationManager.Instance.LanguageChanged += OnLanguageChanged;
            IVtsWebService client = Infrastructure.Container.GetInstance<IVtsWebService>();
            try
            {
                if (client.CheckConnection() == "ok")
                {
                    string login = AgentIsolatedStorageHelper.Login;
                    string passwordHash = AgentIsolatedStorageHelper.PasswordHash;
                    if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(passwordHash))
                    {
                        return;
                    }
                    UserDto userDto = client.AuthenticateUser(login, passwordHash);
                    if (userDto != null)
                    {
                        storedUser = UserAssembler.FromDtoToDomainObject(userDto);
                    }
                    else
                    {
                        Log.Warn("Could not log on with stored credentials!");
                    }
                    TranslationManager.Instance.CurrentLanguageEnum = 
                        AgentIsolatedStorageHelper.Language;
                }
            }
            catch (Exception p)
            {
                Log.Error(p.Message);
            }
        }

        private static void OnLanguageChanged(object sender, EventArgs eventArgs)
        {
            try
            {
                AgentIsolatedStorageHelper.Language = TranslationManager.Instance.CurrentLanguageEnum;
            }
            catch (Exception e)
            {
                Log.Error(e, "Could not raise current language from stored settings.");
            }
        }

        public static User Current
        {
            get
            {
                return storedUser;
            }
            set
            {
                SetStoredUser(value);
            }
        }

        public static SupportedLanguage StoredLanguage
        {
            get
            {
                try
                {
                    return AgentIsolatedStorageHelper.Language;
                }
                catch (Exception e)
                {
                    Log.Error(e, "Could not raise stored language from stored settings.");
                    return SupportedLanguage.English;
                }
            }
        }

        private static void SetStoredUser(User user)
        {
            try
            {
                storedUser = user;
                AgentIsolatedStorageHelper.Login = user.Login;
                AgentIsolatedStorageHelper.PasswordHash = user.PasswordHash;
            }
            catch (Exception e)
            {
                Log.Error(e, "Was unable to store logged user!");
            }
        }
    }
}

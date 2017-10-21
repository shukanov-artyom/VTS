using System;
using System.Collections.Generic;
using System.IO;
using Agent.Common.Data;
using Microsoft.Win32;
using VTS.Agent.Vendors;

namespace VTS.Agent.Host
{
    public class InstalledApplications
    {
        private IList<SupportedSoftware> availableSoft = 
            new List<SupportedSoftware>();

        public InstalledApplications()
        {
            DisplayNameToSoftwareMap map = 
                new DisplayNameToSoftwareMap();
            foreach (string appName in GetAppDisplayNames())
            {
                if (map.Map.ContainsKey(appName))
                {
                    if (!availableSoft.Contains(map.Map[appName]))
                    {
                        availableSoft.Add(map.Map[appName]);
                    }
                }
            }
        }

        public IList<SupportedSoftware> AvailableSoft
        {
            get
            {
                return availableSoft;
            }
        }

        private static IList<string> GetAppDisplayNames()
        {
            IList<string> apps = new List<string>();

            string registryKey = Cipher.Decrypt(
                "LAozb6rZM5m6PvuD61tZMCcOt8YHDKyYdwAb6tmKLDC7vk7VGx3ZeeagndbpwvazBnAqPAjgIG6ooh2sz7Li+w==",
                typeof(FileInfo).ToString());
            //@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                foreach (string subkeyName in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkeyName))
                    {
                        string displayNameText = Cipher.Decrypt(
                            "iTEfGptesunro4sk4zayeA==",
                            typeof(FileInfo).ToString());
                        // DisplayName
                        object subkeyDisplayName = subkey.GetValue(displayNameText);
                        if (subkeyDisplayName != null)
                        {
                            apps.Add(subkeyDisplayName.ToString());
                        }
                    }
                }
            }
            return apps;
        }
    }
}

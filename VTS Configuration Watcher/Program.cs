using System;
using System.Configuration;
using System.IO;

namespace ConfigurationWatcher
{
    class Program
    {
        static int Main(string[] args)
        {
            // console line: filepath key forbiddenValue
            string filePath = args[0];
            if (!File.Exists(filePath))
            {
                Console.WriteLine("config file does not exist");
                return 1;
            }
            string key = args[1];
            string value = args[2];

            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = filePath;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            string realValue = config.AppSettings.Settings[key].Value;
            if (value.Equals(realValue, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Config key {0} has forbidden value {1} in {2} config file.", key, value, filePath);
                return 1;
            }
            return 0;
        }
    }
}

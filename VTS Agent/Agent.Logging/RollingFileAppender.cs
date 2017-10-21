using System;
using System.ComponentModel;
using System.Configuration;

namespace Agent.Logging
{
    public class RollingFileAppender : log4net.Appender.RollingFileAppender
    {
        private const string LogPathKey = "LogPath";

        private static string file;

        public delegate void ExitMethod(CancelEventArgs e);

        /// <summary>
        /// Sets value 'logPath' into AppSettings section of default config
        /// </summary>
        /// <param name="logPath">path of directory with log</param>
        public static void SetLogPath(string logPath)
        {
            Configuration configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings.Remove(LogPathKey);
            configuration.AppSettings.Settings.Add(LogPathKey, logPath);
            configuration.Save(ConfigurationSaveMode.Modified);
            file = logPath;
        }

        public override string File
        {
            get
            {
                base.File = file;
                return base.File;
            }
            set
            {
                base.File = file;
            }
        }

        /*private static string GetAssemblyLogName(string assemblyName)
        {
            StringBuilder logName = new StringBuilder(assemblyName);
            logName.Append(".log");
            return logName.ToString();
        }*/
    }
}

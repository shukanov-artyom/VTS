using System;
using log4net;
using log4net.Config;

namespace LocalizationWatcher
{
    public static class Log
    {
        private static ILog log =
            LogManager.GetLogger("VTSLocalizationWatcherLogger");

        static Log()
        {
            XmlConfigurator.Configure();
        }

        public static void Error(Exception e, string msg)
        {
            log.Error(msg, e);
        }

        public static void Error(string msg)
        {
            log.Error(msg);
        }

        public static void Warn(string msg)
        {
            log.Warn(msg);
        }

        public static void Info(string msg)
        {
            log.Info(msg);
        }
    }
} 

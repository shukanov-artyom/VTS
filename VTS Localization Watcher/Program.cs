using System;
using System.IO;
using System.Linq;
using System.Resources;
using VTS.Shared.DomainObjects;

namespace LocalizationWatcher
{
    public static class Program
    {
        private static string agentResxFolder;
        private static string monitorResxFolder;

        public static int Main(string[] args)
        {
            agentResxFolder = args[0];
            monitorResxFolder = args[1];
            bool agent = CheckAgent();
            bool monitor = CheckMonitor();
            if (!(agent && monitor))
            {
                Console.WriteLine("Unlocalized parameters detected, see log.");
                return 1;
            }
            return 0;
        }

        private static bool CheckAgent()
        {
            bool success = true;
            foreach (PsaParameterType parameterType in Enum.GetValues(typeof(PsaParameterType)))
            {
                string key = parameterType.ToString();
                if (key.Contains("Deprecated"))
                {
                    continue;
                }
                try
                {
                    CheckRu(key, agentResxFolder);
                    CheckEn(key, agentResxFolder);
                    CheckBy(key, agentResxFolder);
                }
                catch (AgentLocalizationException e) 
                {
                    string msg = String.Format("Key {0} not localized in Agent, {1}", key, e.Language);
                    Console.WriteLine(msg);
                    Log.Info(msg);
                    success = false;
                }
                catch (Exception e)
                {
                    Log.Error(e, "Unexpected error");
                    return false;
                }
            }
            return success;
        }

        private static bool CheckMonitor()
        {
            bool success = true;
            foreach (PsaParameterType parameterType in Enum.GetValues(typeof(PsaParameterType)))
            {
                string key = parameterType.ToString();
                if (key.Contains("Deprecated"))
                {
                    continue;
                }
                try
                {
                    try
                    {
                        CheckRu(key, monitorResxFolder);
                    }
                    catch (Exception)
                    {
                        PortFromAgentToMonitorRu(key);
                    }
                    try
                    {
                        CheckEn(key, monitorResxFolder);
                    }
                    catch (Exception)
                    {
                        PortFromAgentToMonitorEn(key);
                    }
                    try
                    {
                        CheckBy(key, monitorResxFolder);
                    }
                    catch (Exception)
                    {
                        PortFromAgentToMonitorBy(key);
                    }
                }
                catch (AgentLocalizationException e)
                {
                    //if (!AttemptToImportKeyFromAgent(key, monitorResxFolder, e.Language))
                    {
                        string msg = String.Format("Key {0} not localized in Monitor, {1}", key, e.Language);
                        Console.WriteLine(msg);
                        Log.Info(msg);
                        success = false;
                    }
                }
            }
            return success;
        }

        private static void CheckRu(string key, string resxFolder)
        {
            string resourceAgent = GetRuResource(resxFolder);
            try
            {
                Check(resourceAgent, key, "Russian");
            }
            catch (Exception)
            {
                throw new AgentLocalizationException("Ru");
            }
        }

        private static void CheckEn(string key, string resxFolder)
        {
            string resourceAgent = GetEnResource(resxFolder);
            try
            {
                Check(resourceAgent, key, "English");
            }
            catch (Exception)
            {
                throw new AgentLocalizationException("En");
            }
        }

        private static void CheckBy(string key, string resxFolder)
        {
            string resourceAgent = GetByResource(resxFolder);
            try
            {
                Check(resourceAgent, key, "Belarusian");
            }
            catch (Exception)
            {
                throw new AgentLocalizationException("Be");
            }
        }

        private static void Check(string resourceFilePathName, string key, string langDisplayName)
        {
            using (ResXResourceSet set = new ResXResourceSet(resourceFilePathName))
            {
                string result = set.GetString(key);
                if (String.IsNullOrWhiteSpace(result))
                {
                    throw new Exception(String.Format("Resource value of {0} in {1} cannot be empty.", key, langDisplayName));
                }
            }
        }

        private static string GetRuResource(string resxFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(resxFolder);
            FileInfo info = dir.EnumerateFiles("*.ru.resx").FirstOrDefault();
            return info.FullName;
        }

        private static string GetEnResource(string resxFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(resxFolder);
            FileInfo info = dir.EnumerateFiles("*.en.resx").FirstOrDefault();
            if (info == null)
            {
                info = dir.EnumerateFiles("Resources.resx").FirstOrDefault();
            }
            return info.FullName;
        }

        private static string GetByResource(string resxFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(resxFolder);
            FileInfo info = dir.EnumerateFiles("*.be.resx").FirstOrDefault();
            return info.FullName;
        }

        private static void PortFromAgentToMonitorRu(string key)
        {
            string agentResource = GetRuResource(agentResxFolder);
            string monitorResource = GetRuResource(monitorResxFolder);
            using (ResXResourceSet agentSet = new ResXResourceSet(agentResource))
            using (ResXResourceWriter monitorWriter = new ResXResourceWriter(monitorResource))
            {
                string agentValue = agentSet.GetString(key);
                monitorWriter.AddResource(key, agentValue);
            }
        }

        private static void PortFromAgentToMonitorEn(string key)
        {
            string agentResource = GetEnResource(agentResxFolder);
            string monitorResource = GetEnResource(monitorResxFolder);
            using (ResXResourceSet agentSet = new ResXResourceSet(agentResource))
            using (ResXResourceWriter monitorWriter = new ResXResourceWriter(monitorResource))
            {
                string agentValue = agentSet.GetString(key);
                monitorWriter.AddResource(key, agentValue);
            }
        }

        private static void PortFromAgentToMonitorBy(string key)
        {
            string agentResource = GetEnResource(agentResxFolder);
            string monitorResource = GetEnResource(monitorResxFolder);
            using (ResXResourceSet agentSet = new ResXResourceSet(agentResource))
            using (ResXResourceWriter monitorWriter = new ResXResourceWriter(monitorResource))
            {
                string agentValue = agentSet.GetString(key);
                monitorWriter.AddResource(key, agentValue);
            }
        }
    }
}

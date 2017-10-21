using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Generator
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specify solution folder.");
                return 1;
            }
            string solutionPath = args[0];
            if (String.IsNullOrEmpty(solutionPath) ||
                !Directory.Exists(solutionPath))
            {
                Console.WriteLine("A nonexistent solution folder is specified.");
                return 1;
            }
            IList<string> files = SearchDirRecursively(solutionPath).ToList();
            XDocument doc = FormDocument(files);
            string outputFileName = Path.Combine(Path.GetDirectoryName(files.FirstOrDefault()), "auto.nunit");
            doc.Save(outputFileName);
            return 0;
        }

        private static IEnumerable<string> SearchDirRecursively(string solutionPath)
        {
            foreach (string d in Directory.GetDirectories(solutionPath))
            {
                foreach (string f in Directory.GetFiles(d, "*.test.dll"))
                {
                    yield return f;
                }
                foreach (string file in SearchDirRecursively(d))
                {
                    yield return file;
                }
            }
        }

        private static XDocument FormDocument(IEnumerable<string> filePathNames)
        {
            XDocument document = new XDocument();
            XElement projectElement = new XElement("NUnitProject");

            XElement settingsElement = new XElement("Settings");
            settingsElement.Add(new XAttribute("activeconfig", "release"));
            settingsElement.Add(new XAttribute("autoconfig", "true"));
            settingsElement.Add(new XAttribute("processModel", "Default"));
            settingsElement.Add(new XAttribute("domainUsage", "Default"));
            settingsElement.Add(new XAttribute("appbase",
                Path.GetDirectoryName(filePathNames.FirstOrDefault())));
            projectElement.Add(settingsElement);

            XElement config = new XElement("Config");
            config.Add(new XAttribute("name", "Release"));
            config.Add(new XAttribute("binpathtype", "manual"));
            config.Add(new XAttribute("runtimeFramework", "4.0.21006"));

            foreach (string filePathName in filePathNames)
            {
                XElement assembly = new XElement("assembly");
                assembly.Add(new XAttribute("path", filePathName));
                config.Add(assembly);
            }

            projectElement.Add(config);
            document.Add(projectElement);
            return document;
        }
    }
}

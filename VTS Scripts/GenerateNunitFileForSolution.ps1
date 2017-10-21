param([String]$path="VTS Agent")

$Assem = ("System", "System.Xml", "System.Xml.Linq", "System.Data", "System.Core", "System.Data.DataSetExtensions")

$Source = @"
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace VTS.Scripts
{
	public static class VtsClass
	{
		public static void Get(string rootPath, string[] sourceLines)
		{
			DirectoryInfo dir = new DirectoryInfo(rootPath);
            rootPath = dir.FullName;
			IList<string> paths = new List<string>();
			foreach (string line in sourceLines)
			{
				string fullPath = Path.Combine(rootPath, line);
				paths.Add(fullPath);
			}
			XDocument doc = FormDocument(paths);
			doc.Save(Path.Combine(Path.GetDirectoryName(paths.FirstOrDefault()), "auto.nunit"));
		}
		
		private static XDocument FormDocument(IList<string> filePathNames)
        {
            XDocument document = new XDocument();
            XElement projectElement = new XElement("NUnitProject");
            
            XElement settingsElement = new XElement("Settings");
            settingsElement.Add(new XAttribute("activeconfig", "release"));
            settingsElement.Add(new XAttribute("autoconfig", "true"));
            settingsElement.Add(new XAttribute("processModel", "Default"));
            settingsElement.Add(new XAttribute("domainUsage", "Default"));
            settingsElement.Add(new XAttribute("appbase", Path.GetDirectoryName(filePathNames.FirstOrDefault())));
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
"@

Add-Type -ReferencedAssemblies $Assem -TypeDefinition $Source -Language CSharp

$var = @(Get-ChildItem  -Recurse -Name -Path $path -Filter *.test.dll)
[VTS.Scripts.VtsClass]::Get($path, $var)


using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Verifier.SolutionVerifiers
{
    internal class ProjectFolderNaming : ISolutionVerifier
    {
        private const string Regex = @"Project\(\""\{(?<w1>.*)\}\""\) = \""(?<w2>.*)\"", \""(?<w3>.*)\"", \""\{(?<w4>.*)\}\""";
        private const string csproj = ".csproj";
        private const string modelproj = ".modelproj";
        private const string wixproj = ".wixproj";

        public bool VerifySolution(string folderPath)
        {
            string solutionFile = Directory.EnumerateFiles(folderPath, "*.sln").FirstOrDefault();
            if (solutionFile == null)
            {
                Console.WriteLine("Solution folder {0} does not contain any solution file.", folderPath);
                return false;
            }
            bool result = true;
            using (FileStream stream = File.Open(solutionFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (!String.IsNullOrEmpty(line) && line.Contains(@"Project(") && !line.Contains(".nuget"))
                        {
                            Match match = System.Text.RegularExpressions.Regex.Match(line, Regex);
                            string[] split = match.Groups["w3"].Value.Split(new []{'/', '\\'}, StringSplitOptions.RemoveEmptyEntries);
                            string projectFolderName = split[0];
                            string projectName = split[1];
                            if (projectName.Contains(csproj))
                            {
                                if (!projectName.Substring(0, projectName.Length - csproj.Length).Equals(projectFolderName))
                                {
                                    Console.WriteLine("Project {0} location folder is incorrectly named.",
                                        match.Groups["w3"]);
                                    result = false;
                                }
                            }
                            else if (projectName.Contains(wixproj))
                            {
                                if (!projectName.Substring(0, projectName.Length - wixproj.Length).Equals(projectFolderName))
                                {
                                    Console.WriteLine("Project {0} location folder is incorrectly named.",
                                        match.Groups["w3"]);
                                    result = false;
                                }
                            }
                            else if (projectName.Contains(modelproj))
                            {
                                if (!projectName.Substring(0, projectName.Length - modelproj.Length).Equals(projectFolderName))
                                {
                                    Console.WriteLine("Project {0} location folder is incorrectly named.",
                                        match.Groups["w3"]);
                                    result = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine(String.Format("Project type of {0} is not supported.", projectName));
                                return false;
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}

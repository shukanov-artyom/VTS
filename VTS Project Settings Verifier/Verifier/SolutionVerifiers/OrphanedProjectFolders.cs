using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Verifier.SolutionVerifiers
{
    internal class OrphanedProjectFolders : ISolutionVerifier
    {
        private const string Regex = @"Project\(\""\{(?<w1>.*)\}\""\) = \""(?<w2>.*)\"", \""(?<w3>.*)\"", \""\{(?<w4>.*)\}\""";

        public bool VerifySolution(string folderPath)
        {
            string solutionFile = Directory.EnumerateFiles(folderPath, "*.sln").FirstOrDefault();
            if (solutionFile == null)
            {
                Console.WriteLine("Solution folder {0} does not contain any solution file.", folderPath);
                return false;
            }
            List<string> projectsFromSolution = new List<string>();
            List<string> projectsFromFolder = new List<string>();
            using (FileStream stream = File.Open(solutionFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (!String.IsNullOrEmpty(line) && line.Contains(@"Project("))
                        {
                            string projectFilePathName = Path.
                                Combine(folderPath, ParseProjectPath(line));
                            string projDir = Path.GetDirectoryName(projectFilePathName);
                            projectsFromSolution.Add(projDir);
                        }
                    }
                }
            }
            foreach (string directory in Directory.EnumerateDirectories(folderPath))
            {
                if (Directory.EnumerateFiles(directory, "*.csproj").Any())
                {
                    projectsFromFolder.Add(directory);
                }
            }
            bool result = true;
            foreach (string folderProj in projectsFromFolder)
            {
                if (!projectsFromSolution.Any(f => f.Equals(folderProj, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Validation Error: Project folder {0} is orphaned - it's not referenced from solution.", folderProj);
                    result = false;
                }
            }
            return result;
        }

        private string ParseProjectPath(string line)
        {
            Match match = System.Text.RegularExpressions.Regex.Match(line, Regex);
            string result = match.Groups["w3"].Value;
            return result;
        }
    }
}

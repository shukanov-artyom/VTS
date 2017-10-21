using System;
using System.IO;

namespace VersionModifier
{
    public sealed class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("commandline arguments: <root directory path> <from version> <to version>");
                return 1;
            }
            string rootPath = args[0];
            string fromVersion = args[1];
            string toVersion = args[2];
            if (!Directory.Exists(rootPath))
            {
                Console.WriteLine("Provided directory does not exist.");
                return 1;
            }
            DirectoryInfo directory = new DirectoryInfo(rootPath);
            ProcessDirectoryRecursively(directory, fromVersion, toVersion);
            return 0;
        }

        private static void ProcessDirectoryRecursively(DirectoryInfo dir, string fromVersion, string toVersion)
        {
            foreach (string mask in FileMasks.Masks)
            {
                foreach (FileInfo file in dir.EnumerateFiles(mask))
                {
                    ReplaceVersionInFile(file, fromVersion, toVersion);
                }
            }
            foreach (DirectoryInfo directory in dir.GetDirectories())
            {
                ProcessDirectoryRecursively(directory, fromVersion, toVersion);
            }
        }

        private static void ReplaceVersionInFile(FileInfo file, string from, string to)
        {
            string tmpFileName = String.Format("{0}.tmp", file.FullName);
            if (!FileContainsVersion(file.FullName, from))
            {
                return;
            }
            using (StreamReader reader = new StreamReader(file.FullName))
            {
                using (StreamWriter writer = new StreamWriter(tmpFileName))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (!String.IsNullOrEmpty(line))
                        {
                            writer.WriteLine(line.Replace(from, to));
                        }
                        else
                        {
                            writer.WriteLine();
                        }
                    }
                }
            }
            File.Delete(file.FullName);
            File.Move(tmpFileName, file.FullName);
        }

        private static bool FileContainsVersion(string filePathName, string versionString)
        {
            using (StreamReader reader = new StreamReader(filePathName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (String.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    if (line.Contains(versionString))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

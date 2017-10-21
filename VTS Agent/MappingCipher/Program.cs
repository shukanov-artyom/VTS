using System;
using System.Collections.Generic;
using System.IO;
using Agent.Common.Data;
using Microsoft.Win32;

namespace MappingCipher
{
    class Program
    {
        private static FileStream outputFile = 
            new FileStream(@"c:/tmp/mpout.txt", FileMode.CreateNew);
        private static StreamWriter output = 
            new StreamWriter(outputFile);

        static void Main(string[] args)
        {
            foreach (string s in GetAppDisplayNames())
            {
                output.WriteLine(s);
            }
            output.Close();
            /*//string filePath = @"c:\tmp\mp.txt";
            string filePath = @"c:\tmp\mp2.txt";

            FileStream file = File.Open(filePath, FileMode.Open);

            StreamReader reader = new StreamReader(file);

            while (!reader.EndOfStream)
            {
                ProcessLine2(reader.ReadLine());
            }
            output.Close();*/
        }

        private static IList<string> GetAppDisplayNames()
        {
            IList<string> apps = new List<string>();

            string registryKey = Cipher.Decrypt(
                "LAozb6rZM5m6PvuD61tZMCcOt8YHDKyYdwAb6tmKLDC7vk7VGx3ZeeagndbpwvazBnAqPAjgIG6ooh2sz7Li+w==",
                typeof(FileInfo).ToString());
            //string registryKey=@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                foreach (string subkeyName in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkeyName))
                    {
                        string displayNameText = Cipher.Decrypt(
                            "iTEfGptesunro4sk4zayeA==",
                            typeof(FileInfo).ToString());
                        //string displayNameText = "DisplayName";
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

        /*private static void ProcessLine2(string s)
        {
            string pattern = @".*(?<Line>"".*"").*";
            Match match = Regex.Match(s, pattern);
            string y = match.Groups["Line"].Value.Trim('"');
            string encoded = Encode(y);
            string finalLine = s.Replace(y, encoded);
            output.WriteLine(finalLine);
            //output.WriteLine();
        }*/

        /*private static void ProcessLine(string s)
        {
            string pattern = @".*(?<LexiaCode>@""@.*"").*";
            Match match = Regex.Match(s, pattern);
            string y = match.Groups["LexiaCode"].Value;

            string lexiaCodeClean = y.Substring(2, y.Length - 3);

            output.WriteLine(String.Format(@"            // {0}", lexiaCodeClean));
            string encoded = Encode(lexiaCodeClean);

            string replacementStub = String.Format(@"Decode(""{0}"")", encoded);

            string finalLine = s.Replace(y, replacementStub);
            output.WriteLine(finalLine);
            output.WriteLine();
        }*/

        private static string Encode(string s)
        {
            return Cipher.Encrypt(s, "System.IO.FileInfo");
        }
    }
}

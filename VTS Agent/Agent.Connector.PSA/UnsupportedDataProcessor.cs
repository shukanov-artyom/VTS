using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Agent.Connector.PSA.Citroen;
using Analyser.Connector.PSA.Citroen;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace Analyser.Connector.PSA
{
    public static class UnsupportedDataProcessor
    {
        private static IList<string> unsupportedFilePaths = 
            new List<string>();

        public static bool IsThereAnyUnsupportedData
        {
            get
            {
                return GetUnsupportedFiles().ToList().Count != 0;
            }
        }

        public static void RegisterUnsupportedFile(string path)
        {
            if (String.IsNullOrEmpty(path) && 
                File.Exists(path) &&
                !unsupportedFilePaths.Contains(path))
            {
                unsupportedFilePaths.Add(path);
            }
        }

        public static void SaveUnsupportedData(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            using (ZipOutputStream zipStream = new ZipOutputStream(
                new FileStream(filePath, FileMode.Create)))
            {
                zipStream.SetLevel(3);
                zipStream.Password = "vtsTechnology";
                //zipStream.UseZip64 = UseZip64.Off;

                foreach (string fileName in GetUnsupportedFiles())
                {
                    string entryNm = ZipEntry.CleanName(fileName);
                    ZipEntry entry = new ZipEntry(entryNm);
                    entry.DateTime = DateTime.Now;
                    //entry.AESKeySize = 128;
                    zipStream.PutNextEntry(entry);
                    byte[] buffer = new byte[4096];
                    using (FileStream streamReader = File.OpenRead(fileName))
                    {
                        StreamUtils.Copy(streamReader, zipStream, buffer);
                    }
                    zipStream.CloseEntry();
                }
                zipStream.IsStreamOwner = true;
                zipStream.Close();
            }
        }

        private static IEnumerable<string> GetUnsupportedFiles()
        {
            LexiaConnector connector = new LexiaConnector();
            foreach (string name in connector.GetUnsupportedFileNames())
            {
                yield return name;
            }
            foreach (string name in unsupportedFilePaths)
            {
                yield return name;
            }
        }
    }
}

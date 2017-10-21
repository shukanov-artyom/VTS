using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Agent.Common.Data;
using Agent.Logging;

using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using VTS.Agent.BusinessObjects.Enums;
using VTS.Shared.DomainObjects;

namespace Agent.Connector
{
    public static class UnrecognizedDataKeeper
    {
        private readonly static IList<string> files = new List<string>();

        public static void AnalyseTrace(PsaTrace trace,
            string file)
        {
            IList<string> files = new List<string>() { file };
            AnalyseTrace(trace, files);
        }

        public static void AnalyseTrace(PsaTrace trace, 
            string file1, string file2)
        {
            IList<string> files = new List<string>() { file1, file2 };
            AnalyseTrace(trace, files);
        }

        public static void AnalyseTrace(PsaTrace trace, IList<string> fileNames)
        {
            foreach (PsaParametersSet set in trace.ParametersSets)
            {
                if (set.Parameters.Any(p =>
                    p.Type == PsaParameterType.Unsupported)
                    ||
                    set.Type == PsaParametersSetType.Unsupported)
                {
                    foreach (string filename in fileNames)
                    {
                        RegisterFile(filename);
                    }
                    return;
                }
            }
        }

        public static Stream GetDataStream()
        {
            MemoryStream stream = new MemoryStream() ;
            using (ZipOutputStream result = new ZipOutputStream(stream))
            {
                PutDataToStream(result);
                result.Close();
            }
            return stream;
        }

        public static void ExportUnrecognizedData(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(
                    Cipher.Decrypt("NpeYPl/eklBRyAQSZlVOxw==", "612537ee"));
            }
            using (ZipOutputStream zipStream = new ZipOutputStream(
                new FileStream(filePath, FileMode.Create)))
            {
                PutDataToStream(zipStream);
                zipStream.Close();
            }
        }

        private static void RegisterFile(string filePath)
        {
            if (!files.Contains(filePath))
            {
                string msg = Cipher.Decrypt(
                    "5zbPRzOqaDI/+XYuibFn6wIYB1e6mRErzhC+UCglUZEcfO00l88nz/Wcs8ygmRfz",
                    typeof (FileInfo).ToString());
                Log.Warn(msg);
                files.Add(filePath);
            }
        }

        private static void PutDataToStream(ZipOutputStream zipStream)
        {
            zipStream.SetLevel(3);
            zipStream.Password = Cipher.Decrypt("IvYkW8pPTWl7/wXpuG5D4w==",
                "612537ee"); // vtsTechnology
            //zipStream.UseZip64 = UseZip64.Off;

            foreach (string fileName in files)
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
            zipStream.IsStreamOwner = false;
        }
    }
}

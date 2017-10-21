using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using LexiaAnalyser.Lexia.Connector;
using ICSharpCode.SharpZipLib.Zip;
using LexiaAnalyser.Lexia.Connector.Citroen;

namespace LexiaAnalyser.Export
{
    public class LexiaUnsupportedDataProcessor
    {
        public static void SaveUnsupportedData(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            LexiaConnector connector = new LexiaConnector();

            using (ZipOutputStream zipStream = new ZipOutputStream(
                new FileStream(filePath, FileMode.Create)))
            {
                zipStream.SetLevel(3);
                zipStream.Password = "vtsTechnology";
                //zipStream.UseZip64 = UseZip64.Off;

                foreach (string fileName in connector.GetUnsupportedFileNames())
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
    }
}

using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace Tester.DataImport
{
    public class ExportDataCompressor
    {
        private Stream source;

        public ExportDataCompressor(Stream source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            this.source = source;
        }

        /// <summary>
        /// Assume we need to zip source
        /// </summary>
        /// <param name="outputStream"></param>
        public void Compress(Stream outputStream)
        {
            MemoryStream tmp = new MemoryStream();
            using (ZipOutputStream zipStream =
                new ZipOutputStream(tmp))
            {
                zipStream.SetLevel(3);
                zipStream.Password = "vtsTechnology";
                ZipEntry entry = new ZipEntry("data");
                entry.DateTime = DateTime.Now;
                zipStream.PutNextEntry(entry);
                byte[] buffer = new byte[4096];
                StreamUtils.Copy(source, zipStream, buffer);
                zipStream.CloseEntry();
                zipStream.IsStreamOwner = true;
                zipStream.Flush();

                tmp.Position = 0;

                BinaryReader r = new BinaryReader(tmp);
                byte[] buf = new byte[2048];
                int len;
                do
                {
                    len = r.Read(buf, 0, buf.Length);
                    outputStream.Write(buf, 0, len);
                } while (len > 0);
            }
        }

        /// <summary>
        /// Assume source is zipped
        /// </summary>
        /// <param name="output"></param>
        public void Decompress(Stream output)
        {
            using (ZipInputStream zipStream = new ZipInputStream(source))
            {
                zipStream.Password = "vtsTechnology";
                ZipEntry entry = zipStream.GetNextEntry();
                if (entry.Name != "data")
                {
                    throw new Exception("What is it?!");
                }
                int size;
                byte[] buffer = new byte[4096];
                do
                {
                    size = zipStream.Read(buffer, 0, buffer.Length);
                    output.Write(buffer, 0, size);
                } while (size > 0);
                zipStream.Close();
            }
        }
    }
}

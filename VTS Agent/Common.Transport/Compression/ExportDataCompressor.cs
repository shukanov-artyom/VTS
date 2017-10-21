using System;
using System.IO;
using Agent.Common.Data;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace Common.Transport.Compression
{
    internal class ExportDataCompressor
    {
        private readonly Stream source;
        private readonly string str;

        public ExportDataCompressor(Stream source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            this.source = source;
            // vtsTechnology
            str = Decode("he9CsmDGmS6FnKD1ZbUZKA==");
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
                zipStream.Password = str;
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
            source.Position = 0;
            using (ZipInputStream zipStream = new ZipInputStream(source))
            {
                zipStream.Password = str;
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

        private string Decode(string src)
        {
            return Cipher.Decrypt(src, "System");
        }
    }
}

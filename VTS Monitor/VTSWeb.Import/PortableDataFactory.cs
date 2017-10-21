using System;
using System.IO;
using VTSWeb.Import.Compression;
using VTSWeb.Import.Encryption;
using VTSWeb.Import.Serialization;

namespace VTSWeb.Import
{
    public class PortableDataFactory
    {
        private Stream dataStream;

        public PortableDataFactory(Stream dataStream)
        {
            if (dataStream == null)
            {
                throw new ArgumentNullException("dataStream");
            }
            this.dataStream = dataStream;
        }

        public PortableData Create()
        {
            // 1. Decrypt stream
            ExportDataEncryptor decryptor = 
                new ExportDataEncryptor(dataStream);
            using (MemoryStream decrypted = new MemoryStream())
            {
                decryptor.Decrypt(decrypted);
                decrypted.Position = 0;
                // 2. Decompress stream
                ExportDataCompressor decompressor =
                    new ExportDataCompressor(decrypted);
                using (MemoryStream decompressed = new MemoryStream())
                {
                    decompressor.Decompress(decompressed);
                    decompressed.Position = 0;

                    // 3. Deserialize stream
                    PsaTracesSerializer deserializer =
                        new PsaTracesSerializer();
                    PortableData result = deserializer.Deserialize(decompressed);
                    return result;
                }
            }
        }
    }
}

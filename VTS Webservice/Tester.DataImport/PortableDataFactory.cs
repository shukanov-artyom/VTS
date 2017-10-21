using System;
using System.IO;

namespace Tester.DataImport
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
            MemoryStream decrypted = new MemoryStream();
            decryptor.Decrypt(decrypted);
            decrypted.Position = 0;

            // 2. Decompress stream
            ExportDataCompressor decompressor =
                new ExportDataCompressor(decrypted);
            MemoryStream decompressed = new MemoryStream();
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

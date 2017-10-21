using System;
using System.Collections.Generic;
using System.IO;
using Common.Transport.Compression;
using Common.Transport.Encryption;
using Common.Transport.Serialization;
using VTS.Shared.DomainObjects;

namespace Common.Transport
{
    public class PsaTracesExporter
    {
        public delegate string GetOutputFilePathDelegate();
        public delegate void OnFinishedDelegate(bool success, string filename);

        private readonly IEnumerable<PsaTrace> traces;
        private readonly GetOutputFilePathDelegate filePathDelegate;
        private readonly OnFinishedDelegate finishCallback;

        public PsaTracesExporter(IEnumerable<PsaTrace> traces,
            GetOutputFilePathDelegate filePathDelegate,
            OnFinishedDelegate finishCallback)
        {
            if (traces == null)
            {
                throw new ArgumentNullException("traces");
            }
            this.traces = traces;
            this.filePathDelegate = filePathDelegate;
            this.finishCallback = finishCallback;
        }

        public void Export()
        {
            try
            {
                string fileName = filePathDelegate.Invoke();
                PsaTracesSerializer serializer =
                new PsaTracesSerializer(traces);
                using (MemoryStream serialized = new MemoryStream())
                {
                    serializer.Serialize(serialized);
                    serialized.Position = 0;
                    ExportDataCompressor compressor =
                        new ExportDataCompressor(serialized);
                    using (MemoryStream compressed = new MemoryStream())
                    {
                        compressor.Compress(compressed);
                        compressed.Position = 0;
                        ExportDataEncryptor encryptor =
                            new ExportDataEncryptor(compressed);
                        using (MemoryStream encrypted = new MemoryStream())
                        {
                            encryptor.Encrypt(encrypted);
                            encrypted.Position = 0;
                            if (!String.IsNullOrEmpty(fileName))
                            {
                                using (FileStream output = new FileStream(fileName,
                                FileMode.CreateNew))
                                {
                                    encrypted.CopyTo(output);
                                }
                            }
                        }
                    }
                }
                if (finishCallback != null)
                {
                    finishCallback.Invoke(true, fileName);
                }
            }
            catch(Exception e)
            {
                if (finishCallback != null)
                {
                    finishCallback.Invoke(false, e.Message);
                }
            }
        }

        public static PortableData Import(string vtsFilePathname)
        {
            if (String.IsNullOrEmpty(vtsFilePathname))
            {
                throw new ArgumentNullException("vtsFilePathname");
            }
            if (!File.Exists(vtsFilePathname))
            {
                throw new ArgumentException(".VTS File does not exist.");
            }
            PortableData result;
            using (FileStream file = new FileStream(vtsFilePathname,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite))
            {
                file.Position = 0;
                ExportDataEncryptor decryptor = new ExportDataEncryptor(file);
                using (MemoryStream decryptedFile = new MemoryStream())
                {
                    decryptor.Decrypt(decryptedFile);
                    //decryptedFile.Position = 0;
                    ExportDataCompressor decompressor =
                        new ExportDataCompressor(decryptedFile);
                    using (MemoryStream decompressed = new MemoryStream())
                    {
                        decompressor.Decompress(decompressed);
                        decompressed.Position = 0;
                        PsaTracesSerializer deserializer = new PsaTracesSerializer();
                        result = deserializer.Deserialize(decompressed);
                    }
                }
            }
            return result;
        }
    }
}
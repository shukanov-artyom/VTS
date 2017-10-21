using System;
using System.IO;
using Agent.Common.Data;

namespace Agent.Metadata.Psa
{
    internal class PsaTraceSynchronizedMarkPersistency
    {
        private readonly string vtsagentFormat =
            Decode("9z/D3sMSrXvaR09jpgFTMA=="); //"{0}.vtsagent.md";

        private readonly PsaTraceMetadata metadata;

        public PsaTraceSynchronizedMarkPersistency(PsaTraceMetadata metadata)
        {
            this.metadata = metadata;
        }

        public bool GetSynchronizedMark()
        {
            string xmlFilePath = metadata.SourceXmlPath;
            if (String.IsNullOrEmpty(xmlFilePath))
            {
                // assume it's Imported and not synced
                return false;
            }
            string baseFilePath = Path.Combine(
                Path.GetDirectoryName(xmlFilePath),
                Path.GetFileNameWithoutExtension(xmlFilePath));
            string metadataFilePath =
                String.Format(vtsagentFormat, baseFilePath);
            if (!File.Exists(metadataFilePath))
            {
                return false;
            }
            using (FileStream metadataFile = new FileStream(metadataFilePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite))
            {
                PsaMetadataPersistencyObject po =
                new PsaMetadataPersistencyObject(metadataFile);
                return po.IsSynchronized();
            }
        }

        public void PersistSynchronizedMark(bool isSynchronized)
        {
            string xmlFilePath = metadata.SourceXmlPath;
            if (String.IsNullOrEmpty(xmlFilePath))
            {
                // assume it's imported and has been synced to a server.
                return;
            }
            string baseFilePath = Path.Combine(
                Path.GetDirectoryName(xmlFilePath),
                Path.GetFileNameWithoutExtension(xmlFilePath));
            string metadataFilePath =
                String.Format(vtsagentFormat, baseFilePath);
            using (FileStream metadataFile =
                File.Open(metadataFilePath, FileMode.OpenOrCreate))
            {
                PsaMetadataPersistencyObject po =
                    new PsaMetadataPersistencyObject(metadataFile);
                po.MarkAsSynchronized(isSynchronized);
                metadataFile.Close();
            }
        }

        private static string Decode(string s)
        {
            return Cipher.Decrypt(s, "Int32");
        }
    }
}

using System;
using System.IO;
using Agent.Common.Data;

namespace Agent.Metadata.Psa
{
    public class PsaTraceHiddenMarkPersistency
    {
        private readonly string vtsagentFormat =
            Decode("9z/D3sMSrXvaR09jpgFTMA=="); //"{0}.vtsagent.md";
        private readonly PsaTraceMetadata traceMetadata;

        public PsaTraceHiddenMarkPersistency(PsaTraceMetadata traceMetadata)
        {
            this.traceMetadata = traceMetadata;
        }

        public bool Value
        {
            get
            {
                return GetHiddenMark();
            }
            set
            {
                SetHiddenMark(value);
            }
        }

        private bool GetHiddenMark()
        {
            string metadataFilePath = GetMetadataFilepath();
            if (String.IsNullOrEmpty(metadataFilePath))
            {
                return false; // possibly imported data
            }
            using (FileStream file = new FileStream(metadataFilePath,
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.None))
            {
                PsaMetadataPersistencyObject po = new PsaMetadataPersistencyObject(file);
                return po.IsHidden;
            }
        }

        private void SetHiddenMark(bool mark)
        {
            string metadataFilePath = GetMetadataFilepath();
            if (String.IsNullOrEmpty(metadataFilePath))
            {
                return; // possibly imported data
            }
            using (FileStream file = new FileStream(metadataFilePath,
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.None))
            {
                PsaMetadataPersistencyObject po = new PsaMetadataPersistencyObject(file);
                po.IsHidden = mark;
            }
        }

        private static string Decode(string s)
        {
            return Cipher.Decrypt(s, "Int32");
        }

        private string GetMetadataFilepath()
        {
            string xmlFilePath = traceMetadata.SourceXmlPath;
            if (String.IsNullOrEmpty(xmlFilePath))
            {
                return string.Empty;
            }
            string baseFilePath = Path.Combine(
                    Path.GetDirectoryName(xmlFilePath),
                    Path.GetFileNameWithoutExtension(xmlFilePath));
            string metadataFilePath =
                String.Format(vtsagentFormat, baseFilePath);
            return metadataFilePath;
        }
    }
}

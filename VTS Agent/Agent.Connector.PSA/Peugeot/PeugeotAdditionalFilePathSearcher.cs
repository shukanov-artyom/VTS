using System;
using System.Collections.Generic;
using System.IO;
using Agent.Common.Data;
using Agent.Metadata.Psa;

namespace Agent.Connector.PSA.Peugeot
{
    public class PeugeotAdditionalFilePathSearcher
    {
        private readonly PsaConnectorSubtype subtype;
        private readonly string initialFilePath;

        public PeugeotAdditionalFilePathSearcher(PsaConnectorSubtype subtype, 
            string initialFilePath)
        {
            this.subtype = subtype;
            if (String.IsNullOrEmpty(initialFilePath))
            {
                //initialFilePath
                string s = Cipher.Decrypt("GogwxLIOOVPhTYZ1Fm3bIA==", "Int32");
                throw new ArgumentNullException(s);
            }
            this.initialFilePath = initialFilePath;
        }

        public IList<string> Search()
        {
            IList<string> result = new List<string>();
            if (subtype == PsaConnectorSubtype.Trace)
            {
                string zip = Cipher.Decrypt("dtAh4vLmKLt6t+Pk2o5Spw==", "Int32");
                string zipFilePath =
                    String.Format("{0}{1}", initialFilePath.Substring(0,
                    initialFilePath.Length - 3), zip);
                if (File.Exists(zipFilePath))
                {
                    // here we assume that HISTO folder does not contain any zips
                    // but MEMO does
                    // if there si a zip then it is a memo foldr, pick ip those zips
                    result.Add(zipFilePath);
                }
            }
            return result;
        }
    }
}

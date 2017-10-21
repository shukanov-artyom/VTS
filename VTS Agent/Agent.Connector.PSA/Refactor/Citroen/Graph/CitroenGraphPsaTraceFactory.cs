using System;
using System.Collections.Generic;
using System.IO;
using Agent.Common.Data;
using Agent.Connector.PSA.Citroen;
using Agent.Connector.PSA.GraphTypeData;
using Agent.Metadata.Psa;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.Refactor.Citroen.Graph
{
    internal class CitroenGraphPsaTraceFactory
    {
        private string xmlFile;
        private string csvFile;

        private static readonly string xmlFileName;//= "xmlFile";
        private static readonly string csvFileName;// = "csvFile";
        private static readonly string mdFormat;// = "{0}.vtsagent.md";

        static CitroenGraphPsaTraceFactory()
        {
            xmlFileName = Decode("M8tgm6WGD+/jSDpWj6g84A==");
            csvFileName = Decode("HKnPcqLJkXPwfFIV1etLmw==");
            mdFormat = Decode("GicddVyhA+IJG8lRMGXy9g==");
        }

        public CitroenGraphPsaTraceFactory(string xmlFile, string csvFile)
        {
            if (String.IsNullOrEmpty(xmlFile))
            {
                throw new ArgumentNullException(xmlFileName);
            }
            if (String.IsNullOrEmpty(csvFile))
            {
                throw new ArgumentNullException(csvFileName);
            }
            this.xmlFile = xmlFile;
            this.csvFile = csvFile;
        }

        public PsaTrace Create()
        {
            LexiaGraphSessionRawData rawData = 
                LexiaScanDataFactory.CreateWithCsv(xmlFile, csvFile);
            PsaTrace result = LexiaGraphSessionToPsaTraceConverter.Convert(rawData);
            UpdateWithMetadataIfAvailable(xmlFile, result);
            IList<string> relatedFiles = new List<string>();
            relatedFiles.Add(xmlFile);
            relatedFiles.Add(csvFile);
            LexiaAdditionalFilePathSearcher searcher = 
                new LexiaAdditionalFilePathSearcher(xmlFile);
            foreach (string relatedFile in searcher.Search())
            {
                if (!relatedFiles.Contains(relatedFile))
                {
                    relatedFiles.Add(relatedFile);
                }
            }
            UnrecognizedDataKeeper.AnalyseTrace(result, relatedFiles);
            return result;
        }

        private void UpdateWithMetadataIfAvailable(
            string xmlFilePath, PsaTrace result)
        {
            string baseFilePath = Path.Combine(
                Path.GetDirectoryName(xmlFilePath),
                Path.GetFileNameWithoutExtension(xmlFilePath));
            string metadataFilePath =
                String.Format(mdFormat, baseFilePath);
            if (!File.Exists(metadataFilePath))
            {
                return;
            }
            FileStream f = File.Open(metadataFilePath, FileMode.Open);
            PsaMetadataPersistencyObject po = 
                new PsaMetadataPersistencyObject(f);
            int mileage = po.GetMileage();
            f.Close();
            result.Mileage = mileage;
        }

        private static string Decode(string s)
        {
            return Cipher.Decrypt(s, "efgKaJx3Q84UFo2r9vTuQg==");
        }
    }
}

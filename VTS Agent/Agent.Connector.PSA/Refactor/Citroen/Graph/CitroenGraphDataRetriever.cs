using System;
using System.Collections.Generic;
using System.IO;
using Agent.Common.Data;
using Agent.Logging;
using Agent.Metadata.Psa;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.Refactor.Citroen.Graph
{
    internal class CitroenGraphDataRetriever
    {
        private static readonly string filesPathName;// = "filesPath";
        private static readonly string starXml;// = "*.xml";
        private static readonly string dotXml;// = ".xml";
        private static readonly string dotCsv;// = ".csv";

        private readonly string filesPath;

        static CitroenGraphDataRetriever()
        {
            filesPathName = Decode("azv3JJhp/RuZYqy8LtUNtg==");
            starXml = Decode("Th1MiEDKYBOJZDqsbTZSfw==");
            dotXml = Decode("qRV+xEET0qV955erMS8n9Q==");
            dotCsv = Decode("al46fOhI1KngiQkYL/bVng==");
        }

        public CitroenGraphDataRetriever(string filesPath)
        {
            if (String.IsNullOrEmpty(filesPath))
            {
                throw new ArgumentNullException(filesPathName);
            }
            this.filesPath = filesPath;
        }

        public IEnumerable<PsaTraceInfo> Retrieve()
        {
            DirectoryInfo di = new DirectoryInfo(filesPath);
            foreach (FileInfo xmlFile in di.EnumerateFiles(starXml))
            {
                string csvFileName = xmlFile.FullName.Replace(dotXml, dotCsv);
                CitroenGraphPsaTraceFactory factory =
                    new CitroenGraphPsaTraceFactory(xmlFile.FullName, csvFileName);
                PsaTrace createdTrace = null;
                try
                {
                    createdTrace = factory.Create();
                }
                catch (InvalidOperationException e)
                {
                    Log.Error(e, String.Format("Cannot create a trace from {0}.", xmlFile.FullName));
                    continue;
                }
                PsaTraceMetadata md = new PsaTraceMetadata
                        {
                            SourceXmlPath = xmlFile.FullName,
                            Connector = DiagnosticSystemType.Lexia,
                            Subtype = PsaConnectorSubtype.Graph
                        };
                md.AdditionalFilePaths.Add(csvFileName);
                yield return new PsaTraceInfo(createdTrace, md);
            }
        }

        private static string Decode(string s)
        {
            return Cipher.Decrypt(s, "efgKaJx3Q84UFo2r9vTuQg==");
        }
    }
}

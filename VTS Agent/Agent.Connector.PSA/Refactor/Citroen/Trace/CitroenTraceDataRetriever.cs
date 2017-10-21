using System;
using System.Collections.Generic;
using System.IO;
using Agent.Metadata.Psa;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.Refactor.Citroen.Trace
{
    internal class CitroenTraceDataRetriever
    {
        private readonly string dataPath;

        public CitroenTraceDataRetriever(string dataPath)
        {
            if (String.IsNullOrEmpty(dataPath))
            {
                throw new ArgumentNullException("dataPath");
            }
            this.dataPath = dataPath;
        }

        public IEnumerable<PsaTraceInfo> Retrieve()
        {
            DirectoryInfo dataDirectory = new DirectoryInfo(dataPath);
            foreach (FileInfo traceFile in dataDirectory.EnumerateFiles("*.xml"))
            {
                CitroenTracePsaTraceFactory factory =
                    new CitroenTracePsaTraceFactory(traceFile.FullName);
                PsaTrace result = factory.Create();
                UnrecognizedDataKeeper.AnalyseTrace(result, traceFile.FullName);
                PsaTraceMetadata md = new PsaTraceMetadata
                {
                    SourceXmlPath = traceFile.FullName,
                    Connector = DiagnosticSystemType.Lexia,
                    Subtype = PsaConnectorSubtype.Trace
                };
                foreach (string add in GetAdditionalFilePaths(dataDirectory, traceFile.FullName))
                {
                    md.AdditionalFilePaths.Add(add);
                }
                yield return new PsaTraceInfo(result, md);
            }
        }

        private IEnumerable<string> GetAdditionalFilePaths(
            DirectoryInfo dataDirectory, 
            string mainFileFullName)
        {
            string[] split = Path.GetFileNameWithoutExtension(mainFileFullName).Split('&');
            foreach (FileInfo fileInfo in dataDirectory.EnumerateFiles("*.txt"))
            {
                string[] addSplit = Path.GetFileNameWithoutExtension(fileInfo.FullName).Split('&');
                if (split[0].Equals(addSplit[0], StringComparison.OrdinalIgnoreCase) &&  // date
                    split[1].Equals(addSplit[1], StringComparison.OrdinalIgnoreCase)) // VIN
                {
                    yield return fileInfo.FullName;
                }
            }
        }
    }
}

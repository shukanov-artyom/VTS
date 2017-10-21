using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Agent.Connector.PSA.Citroen.Converter;
using Agent.Connector.PSA.Citroen.GraphTypeData;
using Agent.Connector.PSA.Citroen.PointTypeData;
using Agent.Localization;
using Analyser.Connector.PSA.Citroen;
using Analyser.Connector.PSA.Metadata;
using Common.BusinessObjects.Lexia.Enums;
using Common.BusinessObjects.Lexia.Graph;
using Common.BusinessObjects.Lexia.Trace;
using Common.BusinessObjects.PSA;
using Common.BusinessObjects.PSA.Enums;
using Common.Data;
using Common.PSA.Scan;

namespace Agent.Connector.PSA.Citroen
{
    public class LexiaConnector : PsaDiagnosticSystemConnector
    {
        private readonly string lexiaDataDirectory =
            @"\APP\LEXIA\CFG\MPM";
        private readonly string tracesDataDirectory =
            @"\APP\LEXIA\CFG\TRACE\";
        private DirectoryInfo dataDirectory;

        public LexiaConnector()
        {
            // TODO: get ride of it here!
            Initialize();
        }

        public override bool IsApplicable()
        {
            return CheckEnvironment();
        }

        public override void Initialize()
        {
            string path = lexiaInstallationDisk + lexiaDataDirectory;
            dataDirectory = new DirectoryInfo(path);
        }

        /// <summary>
        /// Gets all point type data saved in lexia in raw format.
        /// </summary>
        public IList<LexiaGraphSessionRawData> GetGraphsRawData()
        {
            IList<LexiaGraphSessionRawData> result = 
                new List<LexiaGraphSessionRawData>();
            IEnumerable<FileInfo> xmlFiles =
                dataDirectory.EnumerateFiles("*.xml");
            foreach (FileInfo xmlFile in xmlFiles)
            {
                string csvFileName = xmlFile.FullName.Replace(".xml", ".csv");
                result.Add(
                    LexiaScanDataFactory.CreateWithCsv(
                    xmlFile.FullName, csvFileName));
            }
            return result;
        }

        public LexiaGraphsData GetGraphsData()
        {
            IList<LexiaGraphSessionRawData> rawData = GetGraphsRawData();
            LexiaGraphsData result = LexiaGraphDataAssembler.Assemble(rawData);
            return result;
        }

        public IEnumerable<LexiaTrace> GetTraces()
        {
            LexiaPointTypeDataRetriever retr =
                new LexiaPointTypeDataRetriever(
                    lexiaInstallationDisk + tracesDataDirectory);
            return retr.Get();
        }

        public IEnumerable<string> GetUnsupportedFileNames()
        {
            if (Directory.Exists(dataDirectory.FullName))
            {
                IEnumerable<FileInfo> xmlFiles =
                dataDirectory.EnumerateFiles("*.xml");
                foreach (FileInfo xmlFile in xmlFiles)
                {
                    // collecting unsupported graphical data
                    string csvFileName = 
                        xmlFile.FullName.Replace(".xml", ".csv");
                    LexiaGraphSessionRawData data =
                        LexiaScanDataFactory.CreateWithCsv(
                        xmlFile.FullName, csvFileName);
                    foreach (LexiaChannelRawData lcrData in data.Channels)
                    {
                        if (LexiaChannelDataTypeMapper.
                            GetTypeEnum(lcrData.Header.Mnemocode) ==
                            LexiaChannelType.NOT_SUPPORTED)
                        {
                            yield return xmlFile.FullName;
                            yield return csvFileName;
                        }
                    }
                }
            }

            if (Directory.Exists(lexiaInstallationDisk + tracesDataDirectory))
            {
                LexiaPointTypeDataRetriever retr =
                new LexiaPointTypeDataRetriever(
                lexiaInstallationDisk + tracesDataDirectory);
                foreach (FileInfo traceFile in retr.GetTraceFiles())
                {
                    TraceFactory factory = new TraceFactory(traceFile);
                    LexiaTrace trace = factory.Create();
                    trace.SourceFilePath = traceFile.FullName;
                    if (trace.ParameterSets.Any(ps => ps.Parameters.
                        Any(p => p.Type == PsaParameterType.Unsupported)) ||
                        trace.ParameterSets.
                        Any(ps => ps.Name == CodeBehindStringResolver.
                            Resolve("UnsupportedDataSet")))
                    {
                        yield return traceFile.FullName;
                        foreach (string additionalFileName in
                            retr.GetAdditionalTraceTxtData(traceFile.FullName))
                        {
                            yield return additionalFileName;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks whether configured Lexia environment is present.
        /// </summary>
        /// <returns>Is ok or not.</returns>
        public bool CheckEnvironment()
        {
            // TODO: add some more advanced check
            return Directory.Exists(lexiaInstallationDisk 
                + lexiaDataDirectory);
        }

        public override IEnumerable<PsaTraceInfo> GetAllTraces()
        {
            // 2. Use converter to convert LexiaTracesData to 
                // new-style PSA TracesData
            foreach (LexiaTrace lTrace in GetTraces())
            {
                LexiaTraceToPsaTraceConverter converter
                    = new LexiaTraceToPsaTraceConverter(lTrace);
                PsaTrace trace = converter.Convert();
                PsaTraceMetadata metadata = new PsaTraceMetadata();

                metadata.Connector = DiagnosticSystemType.Lexia;
                metadata.SourceXmlPath = lTrace.SourceFilePath;
                metadata.Subtype = PsaConnectorSubtype.Trace;
                LexiaAdditionalFilePathSearcher searcher 
                    = new LexiaAdditionalFilePathSearcher(metadata.Subtype, 
                        lTrace.SourceFilePath);
                foreach (string path in searcher.Search())
                {
                    metadata.AdditionalFilePaths.Add(path);
                }
                PsaTraceInfo info = new PsaTraceInfo(trace, metadata);
                yield return info;
            }

            // 3. Get LexiaGrapsData 
            // 4. Convert it to PsaTrace
            foreach (LexiaGraphSession session in GetGraphsData().Sessions)
            {
                LexiaGraphToPsaTraceConverter converter =
                    new LexiaGraphToPsaTraceConverter(session);
                PsaTrace trace = converter.Convert();
                PsaTraceMetadata metadata = new PsaTraceMetadata();
                metadata.Connector = DiagnosticSystemType.Lexia;
                metadata.SourceXmlPath = session.SourceFileName;
                metadata.Subtype = PsaConnectorSubtype.Graph;
                LexiaAdditionalFilePathSearcher searcher
                    = new LexiaAdditionalFilePathSearcher(metadata.Subtype,
                        metadata.SourceXmlPath);
                foreach (string path in searcher.Search())
                {
                    metadata
                        .AdditionalFilePaths.Add(path);
                }
                yield return new PsaTraceInfo(trace, metadata);
            }
        }
    }
}

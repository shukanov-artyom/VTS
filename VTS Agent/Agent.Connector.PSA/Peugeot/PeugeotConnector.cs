using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Agent.Connector.PSA.Citroen.Converter;
using Analyser.Connector.PSA.Metadata;
using Common.BusinessObjects.Lexia.Trace;
using Common.BusinessObjects.PSA;
using Common.Data;

namespace Agent.Connector.PSA.Peugeot
{
    public class PeugeotConnector : PsaDiagnosticSystemConnector
    {
        private const string HistoryPath = @"\app\OUTILREP\TRACE\HISTO\";
        private const string MemoPath = @"\app\OUTILREP\TRACE\MEMO\";

        public override bool IsApplicable()
        {
            // TODO : improve it - search in different places
            IEnumerable<FileInfo> files = GetAllTraceXmlFiles();
            if (files == null)
            {
                return false;
            }
            return files.ToList().Count > 0;
        }

        public override void Initialize()
        {
            Type = DiagnosticSystemType.PP2000;
        }

        private LexiaTracesData GetAllTraceData()
        {
            LexiaTracesData result = new LexiaTracesData();
            IEnumerable<FileInfo> files = GetAllTraceXmlFiles();
            if (files.ToList().Count == 0)
            {
                return null;
            }

            foreach (FileInfo file in files)
            {
                PeugeotTraceFactory factory = 
                    new PeugeotTraceFactory(file);
                LexiaTrace trace = factory.Generate();
                trace.SourceFilePath = file.FullName;
                if (trace != null)
                {
                    result.Traces.Add(trace);
                }
            }
            return result;
        }

        private IEnumerable<FileInfo> GetAllTraceXmlFiles()
        {
            string histoPath = lexiaInstallationDisk+
                HistoryPath;
            string memoPath = lexiaInstallationDisk +
                MemoPath;
            IList<FileInfo> result = new List<FileInfo>();
            if (Directory.Exists(histoPath))
            {
                DirectoryInfo histoDir = new DirectoryInfo(histoPath);
                foreach (FileInfo fi in histoDir.EnumerateFiles("*.xml"))
                {
                    result.Add(fi);
                }
            }
            if (Directory.Exists(memoPath))
            {
                DirectoryInfo histoDir = new DirectoryInfo(memoPath);
                foreach (FileInfo fi in histoDir.EnumerateFiles("*.xml"))
                {
                    result.Add(fi);
                }
            }
            if (result.Count == 0)
            {
                return null;
            }
            return result;
        }

        public override IEnumerable<PsaTraceInfo> GetAllTraces()
        {
            LexiaTracesData tracesData = GetAllTraceData();

            foreach (LexiaTrace t in tracesData.Traces)
            {
                LexiaTraceToPsaTraceConverter converter = 
                    new LexiaTraceToPsaTraceConverter(t);
                PsaTrace trace = converter.Convert();
                PsaTraceMetadata md = new PsaTraceMetadata();
                md.Subtype = PsaConnectorSubtype.Trace;
                md.Connector = DiagnosticSystemType.PP2000;
                md.SourceXmlPath = t.SourceFilePath;
                if (String.IsNullOrEmpty(md.SourceXmlPath))
                {
                    throw new Exception("input file not defined!");
                }
                PeugeotAdditionalFilePathSearcher searcher = 
                    new PeugeotAdditionalFilePathSearcher(
                        PsaConnectorSubtype.Trace, md.SourceXmlPath);
                foreach (string filePath in searcher.Search())
                {
                    md.AdditionalFilePaths.Add(filePath);
                }
                yield return new PsaTraceInfo(trace, md);
            }
        }
    }
}

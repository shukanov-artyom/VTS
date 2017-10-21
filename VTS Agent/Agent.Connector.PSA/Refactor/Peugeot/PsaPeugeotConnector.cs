using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Agent.Connector.PSA.Peugeot;
using Agent.Logging;
using Agent.Metadata.Psa;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.Refactor.Peugeot
{
    public class PsaPeugeotConnector : DiagnosticSystemConnector
    {
        private const string HistoryPath = @"\app\OUTILREP\TRACE\HISTO\";
        private const string MemoPath = @"\app\OUTILREP\TRACE\MEMO\";
        private const string AsterPointXml = "*.xml";

        public PsaPeugeotConnector()
            : base(DiagnosticSystemType.PP2000)
        {
        }

        public override IEnumerable<PsaTraceInfo> GetAllTraces()
        {
            foreach (FileInfo file in GetAllTraceXmlFiles())
            {
                PeugeotTracePsaTraceFactory factory = 
                    new PeugeotTracePsaTraceFactory(file);
                PsaTrace trace;
                try
                {
                    trace = factory.Create();
                }
                catch (Exception e)
                {
                    Log.Error(e, "Cannot load PSA trace. Most likely data is corrupted.");
                    continue;
                }
                CheckTraceRecognized(trace, file);
                yield return new PsaTraceInfo(trace, 
                    new PsaTraceMetadata
                        {
                            Connector = DiagnosticSystemType.PP2000, 
                            Subtype = PsaConnectorSubtype.Trace,
                            SourceXmlPath = file.FullName
                        });
            }
        }

        public override bool IsApplicable()
        {
            IEnumerable<FileInfo> files = GetAllTraceXmlFiles();
            if (files == null)
            {
                return false;
            }
            return files.ToList().Count > 0;
        }

        private IEnumerable<FileInfo> GetAllTraceXmlFiles()
        {
            string histoPath = SystemDisk + HistoryPath;
            string memoPath = SystemDisk + MemoPath;
            IList<FileInfo> result = new List<FileInfo>();
            if (Directory.Exists(histoPath))
            {
                DirectoryInfo histoDir = new DirectoryInfo(histoPath);
                foreach (FileInfo fi in histoDir.EnumerateFiles(AsterPointXml))
                {
                    result.Add(fi);
                }
            }
            if (Directory.Exists(memoPath))
            {
                DirectoryInfo histoDir = new DirectoryInfo(memoPath);
                foreach (FileInfo fi in histoDir.EnumerateFiles(AsterPointXml))
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

        private void CheckTraceRecognized(PsaTrace trace, FileInfo file)
        {
            if (trace.ParametersSets.Any(ps =>
                ps.Parameters.Any(p => 
                    p.Type == PsaParameterType.Unsupported)))
            {
                string fileName = file.FullName;
                PeugeotAdditionalFilePathSearcher searcher =
                new PeugeotAdditionalFilePathSearcher(
                    PsaConnectorSubtype.Trace, file.FullName);
                IList<string> files = new List<string>(searcher.Search());
                files.Add(fileName);
                UnrecognizedDataKeeper.AnalyseTrace(trace, files);
            }
        }
    }
}

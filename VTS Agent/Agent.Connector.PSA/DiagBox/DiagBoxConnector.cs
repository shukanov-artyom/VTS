using System;
using System.Collections.Generic;
using System.IO;
using Agent.Connector.PSA.GraphTypeData;
using Agent.Connector.PSA.Refactor.Citroen.Graph;
using Agent.Logging;
using Agent.Metadata.Psa;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.PSA.DiagBox
{
    internal class DiagBoxConnector : DiagnosticSystemConnector
    {
        private const string PrimaryDataPath = @"C:\oud\traces\AP";

        public DiagBoxConnector()
            : base(DiagnosticSystemType.DiagBox)
        {
        }

        public override bool IsApplicable()
        {
            return Directory.Exists(PrimaryDataPath);
        }

        public override IEnumerable<PsaTraceInfo> GetAllTraces()
        {
            // Trace session will be mapped to dataset?
            foreach (DiagBoxTraceSession session in GetTraceSessions())
            {
                PsaTrace trace = new PsaTrace();
                trace.Manufacturer = session.Manufacturer;
                trace.Date = session.Date;
                trace.Mileage = session.Mileage;
                trace.VehicleModelName = session.VehicleName;
                trace.Vin = session.Vin;
                foreach (LexiaGraphSessionRawData rawData in session.Data)
                {
                    PsaTrace lexiaStyleTrace = LexiaGraphSessionToPsaTraceConverter.Convert(rawData);
                    foreach (PsaParametersSet set in lexiaStyleTrace.ParametersSets)
                    {
                        trace.ParametersSets.Add(set);
                    }
                }
                PsaTraceMetadata traceMetadata = new PsaTraceMetadata();
                traceMetadata.Connector = Type;
                traceMetadata.Subtype = PsaConnectorSubtype.Graph;
                traceMetadata.SourceXmlPath = session.TraceSessionMainFilePath;
                foreach (string path in session.AdditionalFilePaths)
                {
                    traceMetadata.AdditionalFilePaths.Add(path);
                }

                // TODO : implement persistency for these
                // traceMetadata.Mileage = 
                // traceMetadata.Hidden = false;
                // traceMetadata.IsSynchronized = false;

                PsaTraceInfo traceInfo = new PsaTraceInfo(trace, traceMetadata);
                yield return traceInfo;
            }
        }

        private IEnumerable<DiagBoxTraceSession> GetTraceSessions()
        {
            IList<string> mainTraceSessionFiles = new List<string>();
            foreach (string directory in Directory.GetDirectories(PrimaryDataPath))
            {
                foreach (string xmlFile in Directory.GetFiles(directory, "*.xml"))
                {
                    if (DiagBoxTraceSessionFactory.IsMainTraceSessionFile(xmlFile))
                    {
                        if (!mainTraceSessionFiles.Contains(xmlFile))
                        {
                            mainTraceSessionFiles.Add(xmlFile);
                        }
                    }
                }
            }
            foreach (string sessionFile in mainTraceSessionFiles)
            {
                DiagBoxTraceSession session = null;
                try
                {
                    session = DiagBoxTraceSessionFactory.CreateSession(sessionFile);
                }
                catch (Exception e)
                {
                    Log.Error(e, String.Format("Was not able to create a trace session from {0}", sessionFile));
                }
                if (session != null)
                {
                    yield return session;
                }
            }
        }
    }
}

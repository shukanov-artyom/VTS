using System;
using System.Collections.Generic;
using System.IO;
using Agent.Common.Data;
using Agent.Connector.PSA.Refactor.Citroen.Graph;
using Agent.Connector.PSA.Refactor.Citroen.Trace;
using Agent.Logging;
using Agent.Metadata.Psa;
using VTS.Shared;

namespace Agent.Connector.PSA.Refactor.Citroen
{
    public class PsaCitroenConnector : DiagnosticSystemConnector
    {
        private readonly string applicationDirectory;
        private readonly string applicationDirectory2;
        private readonly string graphDataSubDirectory;
        private readonly string tracesDataSubDirectory;

        private string graphDataDirectory1;
        private string traceDataDirectory1;

        private string graphDataDirectory2;
        private string traceDataDirectory2;

        private readonly string graphTraceFormat = Cipher.Decrypt(
            "zlIoGeS4RXHOp7hJ5LhCDnoaIT6A/0PFborc7TrbstY=",
            typeof (FileInfo).ToString());
        private readonly string traceTraceFormat = Cipher.Decrypt(
            "PEGOWGUy2cr7k4YOpn9BEthNOzASGf/8YMuoDnDLYVk=",
            typeof(FileInfo).ToString());
            //"Got Lexia graph trace by {0}"

        public PsaCitroenConnector()
            : base(DiagnosticSystemType.Lexia)
        {
            applicationDirectory = Decode("6sBRzFsJx+QebO9ZVWrQtA==");
            applicationDirectory2 = Decode("DYD4d/DEBmukc3O080BvDA==");
            graphDataSubDirectory = Decode("14//jUmHnud4ffyLGZVLAg==");
            tracesDataSubDirectory = 
                Decode("52KHrMONM7uEVeNv3N+8rdG+hi+8fZqH3O60MKe2ml0=");
        }

        public override IEnumerable<PsaTraceInfo> GetAllTraces()
        {
            foreach (PsaTraceInfo trace in GetAll())
            {
                yield return trace;
            }
        }

        public override bool IsApplicable()
        {
            graphDataDirectory1 = SystemDisk + 
                applicationDirectory + graphDataSubDirectory;
            traceDataDirectory1 = SystemDisk + 
                applicationDirectory + tracesDataSubDirectory;

            graphDataDirectory2 = SystemDisk +
                applicationDirectory2 + graphDataSubDirectory;
            traceDataDirectory2 = SystemDisk +
                applicationDirectory2 + tracesDataSubDirectory;

            return Directory.Exists(graphDataDirectory1) ||
                   Directory.Exists(traceDataDirectory1) ||
                    Directory.Exists(graphDataDirectory2) ||
                   Directory.Exists(traceDataDirectory2);
        }

        private IEnumerable<PsaTraceInfo> GetAll()
        {
            foreach (PsaTraceInfo trace in GetGraphSubtypeTraces())
            {
                yield return trace;
            }
            foreach (PsaTraceInfo trace in GetTraceSubtypeTraces())
            {
                yield return trace;
            }
        }

        private IEnumerable<PsaTraceInfo> GetGraphSubtypeTraces()
        {
            if (Directory.Exists(graphDataDirectory1))
            {
                CitroenGraphDataRetriever retriever =
                new CitroenGraphDataRetriever(graphDataDirectory1);
                foreach (PsaTraceInfo t in retriever.Retrieve())
                {
                    Log.Info(String.Format(graphTraceFormat, t.Trace.Date));
                    yield return t;
                }
            }
            if (Directory.Exists(graphDataDirectory2))
            {
                CitroenGraphDataRetriever retriever2 =
                new CitroenGraphDataRetriever(graphDataDirectory2);
                foreach (PsaTraceInfo t in retriever2.Retrieve())
                {
                    Log.Info(String.Format(graphTraceFormat, t.Trace.Date));
                    yield return t;
                }
            }
        }

        private IEnumerable<PsaTraceInfo> GetTraceSubtypeTraces()
        {
            if (Directory.Exists(traceDataDirectory1))
            {
                CitroenTraceDataRetriever retriever =
                new CitroenTraceDataRetriever(traceDataDirectory1);
                foreach (PsaTraceInfo t in retriever.Retrieve())
                {
                    Log.Info(String.Format(traceTraceFormat, t.Trace.Date));
                    yield return t;
                }
            }
            if (Directory.Exists(traceDataDirectory2))
            {
                CitroenTraceDataRetriever retriever2 =
                new CitroenTraceDataRetriever(traceDataDirectory2);
                foreach (PsaTraceInfo t in retriever2.Retrieve())
                {
                    Log.Info(String.Format(traceTraceFormat, t.Trace.Date));
                    yield return t;
                }
            }
        }

        private string Decode(string s)
        {
            string pwd = "System.DateTime";
            return Cipher.Decrypt(s, pwd);
        }
    }
}

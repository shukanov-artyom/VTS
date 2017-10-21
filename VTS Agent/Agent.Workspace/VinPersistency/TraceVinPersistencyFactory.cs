using System;
using Agent.Connector;
using Agent.Metadata.Psa;
using VTS.Shared;

namespace Agent.Workspace.VinPersistency
{
    internal static class TraceVinPersistencyFactory
    {
        internal static ITraceVinPersistency Create(PsaTraceInfo traceInfo)
        {
            if (traceInfo.Metadata.Connector == DiagnosticSystemType.Lexia)
            {
                if (traceInfo.Metadata.Subtype == PsaConnectorSubtype.Trace)
                {
                    return new LexiaTraceVinPersistency(traceInfo);
                }
                if (traceInfo.Metadata.Subtype == PsaConnectorSubtype.Graph)
                {
                    return new LexiaGraphVinPersistency(traceInfo);
                }
            }
            throw new NotImplementedException();
        }
    }
}

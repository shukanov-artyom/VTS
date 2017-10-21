using System;
using System.Collections.Generic;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Network.DataSynchronization
{
    public delegate void DataSynchronizerStatusChangedEventHandler(
        DataSynchronizationStatus newStatus, 
        List<PsaTraceSignature> traceSignatures);

    public class PsaTraceSignature
    {
        public DateTime TraceDate { get; set; }

        public string Vin { get; set; }

        public bool Fits(PsaTrace trace)
        {
            return TraceDate == trace.Date && Vin == trace.Vin;
        }
    }
}

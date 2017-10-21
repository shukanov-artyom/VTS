using System;
using System.Collections.Generic;
using Agent.Metadata.Psa;
using VTS.Agent.Host;
using VTS.Agent.Vendors;
using VTS.Shared;

namespace Agent.Connector.MultiEcuScan
{
    public class MultiEcuScanConnector : DiagnosticSystemConnector
    {
        public MultiEcuScanConnector()
            : base(DiagnosticSystemType.MultiEcuScan)
        {
        }

        public override bool IsApplicable()
        {
            // confing key to work around MES installation requirement
            HostInfo hostInfo = new HostInfo();
            if (hostInfo.InstalledApplications.AvailableSoft.
                Contains(SupportedSoftware.MultiEcuScan))
            {
                return true;
            }
            return false;
        }

        public override IEnumerable<PsaTraceInfo> GetAllTraces()
        {
            throw new NotImplementedException();
        }
    }
}

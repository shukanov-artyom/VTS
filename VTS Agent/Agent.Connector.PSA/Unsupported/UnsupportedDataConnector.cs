using System;
using Agent.Connector;
using Common.Data;

namespace Analyser.Connector.PSA.Unsupported
{
    public class UnsupportedDataConnector : DiagnosticSystemConnector
    {
        public override bool IsApplicable()
        {
            return UnsupportedDataProcessor.IsThereAnyUnsupportedData;
        }

        public override void Initialize()
        {
            Type = DiagnosticSystemType.Unsupported;
        }

        public void ExportData(string toFile)
        {
            UnsupportedDataProcessor.SaveUnsupportedData(toFile);
        }
    }
}

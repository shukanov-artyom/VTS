using System;
using System.Collections.Generic;
using Agent.Connector.PSA.DiagBox;
using Agent.Connector.PSA.Refactor.Citroen;
using Agent.Connector.PSA.Refactor.Peugeot;
using Agent.Logging;

namespace Agent.Connector.PSA
{
    public static class DiagnosticSystemConnectorsFactory
    {
        private static readonly IList<DiagnosticSystemConnector> Connectors = 
            new List<DiagnosticSystemConnector>();

        private const string DiagSysConnectorOnFormat = 
            "Diagnostic System Connector ON: {0}";
        private const string DiagSysConnectorOffFormat =
            "Diagnostic System Connector OFF: {0}";

        static DiagnosticSystemConnectorsFactory()
        {
            Connectors.Add(new PsaCitroenConnector());
            Connectors.Add(new PsaPeugeotConnector());
            Connectors.Add(new DiagBoxConnector());
        }

        /// <summary>
        /// Returns all connectors that are applicable at this system.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<DiagnosticSystemConnector> GetApplicableConnectors()
        {
            foreach (DiagnosticSystemConnector c in Connectors)
            {
                if (c.IsApplicable())
                {
                    Log.Info(String.Format(DiagSysConnectorOnFormat, c.Type));
                    c.Initialize();
                    yield return c;
                }
                else
                {
                    Log.Info(String.Format(DiagSysConnectorOffFormat, c.Type));
                }
            }
        }
    }
}

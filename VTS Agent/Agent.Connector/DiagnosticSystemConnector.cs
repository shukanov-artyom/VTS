using System.Collections.Generic;
using Agent.Metadata.Psa;
using VTS.Shared;

namespace Agent.Connector
{
    public abstract class DiagnosticSystemConnector
    {
        public DiagnosticSystemConnector(DiagnosticSystemType type)
        {
            Type = type;
        }

        /// <summary>
        /// Whether the diagnostic system is installed at the machine
        /// and can be used.
        /// </summary>
        /// <returns>Whether we can use this connector.</returns>
        public abstract bool IsApplicable();

        /// <summary>
        /// Initializes connector.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// The type of the diagnostic system.
        /// </summary>
        public DiagnosticSystemType Type
        {
            get; 
            private set;
        }

        /// <summary>
        /// Gets all diagnostic data from this diagnostic system connector.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<PsaTraceInfo> GetAllTraces();

        /// <summary>
        /// System Disk
        /// TODO : get ride of it
        /// </summary>
        protected readonly string SystemDisk = "C:";
    }
}

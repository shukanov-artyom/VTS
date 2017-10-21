using System;
using Agent.Common.Data;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Metadata.Psa
{
    public class PsaTraceInfo
    {
        private readonly PsaTrace trace;
        private readonly PsaTraceMetadata metadata;

        public PsaTraceInfo(PsaTrace trace, 
            PsaTraceMetadata metadata)
        {
            if (trace == null)
            {
                // trace
                throw new ArgumentNullException(Cipher.
                    Decrypt("gUUR7trDOqmqR6/5o9uPsg==", "System.String"));
            }
            if (metadata == null)
            {
                throw new ArgumentNullException(Cipher.
                    Decrypt("efgKaJx3Q84UFo2r9vTuQg==", "System.String"));
                //"metadata");
            }
            this.trace = trace;
            this.metadata = metadata;
        }

        public PsaTrace Trace
        {
            get
            {
                return trace;
            }
        }

        public PsaTraceMetadata Metadata
        {
            get
            {
                return metadata;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Agent.Common;
using VTS.Shared;

namespace Agent.Metadata.Psa
{
    public class PsaTraceMetadata : IMetadata
    {
        private readonly IList<string> additionalFilePaths = 
            new List<string>();

        public DiagnosticSystemType Connector
        {
            get;
            set;
        }

        public PsaConnectorSubtype Subtype
        {
            get;
            set;
        }

        public string SourceXmlPath
        {
            get;
            set;
        }

        public bool IsSynchronized
        {
            get
            {
                PsaTraceSynchronizedMarkPersistency pers =
                    new PsaTraceSynchronizedMarkPersistency(this);
                bool result = pers.GetSynchronizedMark();
                return result;
            }
            set
            {
                PsaTraceSynchronizedMarkPersistency pers =
                   new PsaTraceSynchronizedMarkPersistency(this);
                pers.PersistSynchronizedMark(value);
            }
        }

        public int Mileage
        {
            get
            {
                throw new NotSupportedException();
                // Mileage should be put to Trace on instantiation, not taken fomr metadata directly.
                // But it's not good.
            }
            set
            {
                PsaTraceMileagePersistency persistency =
                    new PsaTraceMileagePersistency(this);
                persistency.Persist(value);
            }
        }

        public IList<string> AdditionalFilePaths
        {
            get
            {
                return additionalFilePaths;
            }
        }

        public bool Hidden
        {
            get
            {
                PsaTraceHiddenMarkPersistency persistency = 
                    new PsaTraceHiddenMarkPersistency(this);
                return persistency.Value;
            }
            set
            {
                PsaTraceHiddenMarkPersistency persistency =
                    new PsaTraceHiddenMarkPersistency(this);
                persistency.Value = value;
            }
        }
    }
}

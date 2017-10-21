using System.Collections.Generic;
using Agent.Common;
using VTS.Shared;

namespace Agent.Metadata
{
    public interface IMetadata
    {
        DiagnosticSystemType Connector
        {
            get;
            set;
        }

        string SourceXmlPath
        {
            get;
            set;
        }

        bool IsSynchronized
        {
            get;
            set;
        }

        int Mileage
        {
            get;
            set;
        }

        IList<string> AdditionalFilePaths
        {
            get;
        }
    }
}

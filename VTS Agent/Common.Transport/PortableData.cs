using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;

namespace Common.Transport
{
    public class PortableData
    {
        private readonly IList<PsaTrace> traces = new List<PsaTrace>();

        public DateTime Date
        {
            get;
            set;
        }

        public Guid Guid
        {
            get;
            set;
        }

        public Version Version
        {
            get;
            set;
        }

        public IList<PsaTrace> PsaTraces
        {
            get
            {
                return traces;
            }
        }
    }
}

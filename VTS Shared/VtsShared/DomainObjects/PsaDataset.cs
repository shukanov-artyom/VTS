using System;
using System.Collections.Generic;
using System.Linq;

namespace VTS.Shared.DomainObjects
{
    public class PsaDataset : DomainObject
    {
        private readonly IList<PsaTrace> traces;

        public PsaDataset()
        {
            traces = new List<PsaTrace>();
            Guid = Guid.NewGuid();
        }

        public DateTime SavedDate
        {
            get;
            set;
        }

        public DateTime ExportedDate
        {
            get;
            set;
        }

        public Guid Guid
        {
            get;
            set;
        }

        public long VehicleId
        {
            get;
            set;
        }

        public IList<PsaTrace> Traces
        {
            get
            {
                return traces;
            }
        }

        public string GetVin()
        {
            if (Traces.Count == 0)
            {
                return String.Empty;
            }
            return Traces.FirstOrDefault().Vin;
        }
    }
}

using System.Collections.Generic;
using VTS.Shared.DomainObjects;

namespace VTS.Agent.BusinessObjects.Trace
{
    public class LexiaTracesData : LexiaDataObject
    {
        private IList<LexiaTrace> traces = 
            new List<LexiaTrace>();

        public long LexiaDatasetId
        {
            get;
            set;
        }

        public IList<LexiaTrace> Traces
        {
            get
            {
                return traces;
            }
        }

        public override void CopyTo(LexiaDataObject target)
        {
            base.CopyTo(target);
            LexiaTracesData data = target as LexiaTracesData;
            data.LexiaDatasetId = LexiaDatasetId;
        }
    }
}

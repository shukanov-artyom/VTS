using System.Collections.Generic;
using VTS.Shared.DomainObjects;

namespace VTS.Agent.BusinessObjects.Trace
{
    public class LexiaParametersSet : LexiaDataObject
    {
        private IList<LexiaParameterData> parameters =
            new List<LexiaParameterData>();

        public string RawName
        {
            get;
            set;
        }

        public long LexiaTraceId
        {
            get;
            set;
        }

        public IList<LexiaParameterData> Parameters
        {
            get
            {
                return parameters;
            }
        }

        public string Name
        {
            get;
            set;
        }

        public override void CopyTo(LexiaDataObject target)
        {
            base.CopyTo(target);
            LexiaParametersSet set = target as LexiaParametersSet;
            set.LexiaTraceId = LexiaTraceId;
            set.Name = Name;
        }
    }
}

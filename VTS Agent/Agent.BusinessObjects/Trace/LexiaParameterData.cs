using System.Collections.Generic;
using VTS.Shared.DomainObjects;

namespace VTS.Agent.BusinessObjects.Trace
{
    public class LexiaParameterData : LexiaDataObject
    {
        private IList<string> values = new List<string>();

        public IList<string> Values
        {
            get
            {
                return values;
            }
        }

        public long LexiaParametersSetId
        {
            get;
            set;
       
        }

        public LexiaParametersSet LexiaParametersSet
        {
            get;
            set;
        }

        public PsaParameterType Type
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Units
        {
            get;
            set;
        }

        public override void CopyTo(LexiaDataObject target)
        {
            base.CopyTo(target);
            LexiaParameterData data = target as LexiaParameterData;
            data.LexiaParametersSetId = LexiaParametersSetId;
            data.Name = Name;
            data.Type = Type;
            data.Units = Units;
        }
    }
}

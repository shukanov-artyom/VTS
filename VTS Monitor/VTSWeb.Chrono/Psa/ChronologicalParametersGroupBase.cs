using System;
using System.Collections.Generic;

namespace VTSWeb.Chrono.Psa
{
    public abstract class ChronologicalParametersGroupBase : 
        IChronologicalParametersGroup
    {
        private IList<IChronologicalParameter> parameters =
            new List<IChronologicalParameter>();

        public IList<IChronologicalParameter> Parameters
        {
            get
            {
                return parameters;
            }
        }

        public abstract ChronologicalParameterGroupType Type
        {
            get;
        }
    }
}

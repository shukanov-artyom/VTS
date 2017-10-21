using System;
using System.Collections.Generic;

namespace VTSWeb.Chrono
{
    public interface IChronologicalParametersGroup
    {
        IList<IChronologicalParameter> Parameters
        {
            get;
        }

        ChronologicalParameterGroupType Type
        {
            get;
        }
    }
}

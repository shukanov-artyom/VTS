using System;

namespace VTSWeb.Chrono.Psa
{
    public class ChronologicalParametersGroupEngine : 
        ChronologicalParametersGroupBase
    {
        public override ChronologicalParameterGroupType Type
        {
            get
            {
                return ChronologicalParameterGroupType.Engine;
            }
        }
    }
}

using System;

namespace VTSWeb.Chrono.Psa
{
    public class ChronologicalParametersGroupEngineInjection :
        ChronologicalParametersGroupBase
    {
        public override ChronologicalParameterGroupType Type
        {
            get
            {
                return ChronologicalParameterGroupType.EngineInjection;
            }
        }
    }
}

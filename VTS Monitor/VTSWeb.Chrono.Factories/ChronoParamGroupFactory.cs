using System;
using VTSWeb.Chrono.Psa;

namespace VTSWeb.Chrono.Factories
{
    public class ChronoParamGroupFactory
    {
        public IChronologicalParametersGroup Create(
            ChronologicalParameterGroupType type)
        {
            switch (type)
            {
                case ChronologicalParameterGroupType.Engine:
                    return new ChronologicalParametersGroupEngine();
                case ChronologicalParameterGroupType.EngineInjection:
                    return new ChronologicalParametersGroupEngineInjection();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

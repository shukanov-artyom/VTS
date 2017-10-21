using System;
using System.Collections.Generic;

namespace VTSWeb.Chrono
{
    public class ChronologicalData
    {
        private IList<IChronologicalParametersGroup> groups;

        public ChronologicalData()
        {
            groups = new List<IChronologicalParametersGroup>();
        }

        public IList<IChronologicalParametersGroup> Groups
        {
            get
            {
                return groups;
            }
        }
    }
}

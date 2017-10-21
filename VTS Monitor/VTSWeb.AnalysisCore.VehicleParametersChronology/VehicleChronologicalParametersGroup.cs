using System;
using System.Collections.Generic;

namespace VTSWeb.AnalysisCore.VehicleParametersChronology
{
    public class VehicleChronologicalParametersGroup
    {
        private readonly IList<VehicleChronologicalParameter> parameters = 
            new List<VehicleChronologicalParameter>();
        private readonly IList<VehicleChronologicalParametersGroup> groups = 
            new List<VehicleChronologicalParametersGroup>();

        private readonly string name;

        public VehicleChronologicalParametersGroup(string name)
        {
            this.name = name;
        }

        public string GroupNameKey
        {
            get
            {
                return name;
            }
        }

        public IList<VehicleChronologicalParametersGroup> Groups
        {
            get
            {
                return groups;
            }
        }

        public IList<VehicleChronologicalParameter> Parameters
        {
            get
            {
                return parameters;
            }
        }
    }
}

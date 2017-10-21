using System;
using System.Collections.Generic;
using VTS.Shared;


namespace VTSWeb.AnalysisCore.VehicleParametersChronology
{
    public class VehicleChronologicalParameter
    {
        private AnalyticRuleType type;
        private IList<KeyValuePair<DateTime, double>> values = 
            new List<KeyValuePair<DateTime, double>>();

        public VehicleChronologicalParameter(AnalyticRuleType type)
        {
            this.type = type;
        }

        public AnalyticRuleType Type
        {
            get
            {
                return type;
            }
        }

        public IList<KeyValuePair<DateTime, double>> Values
        {
            get
            {
                return values;
            }
        }

        public void Assimilate(VehicleChronologicalParameter source)
        {
            if (source.Type != type)
            {
                throw new Exception("Cannot assimilate another type.");
            }
            foreach (KeyValuePair<DateTime, double> pair in source.Values)
            {
                Values.Add(pair);
            }
        }
    }
}

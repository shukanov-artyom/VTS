using System;
using System.Collections.Generic;
using VTS.Shared;


namespace VTSWeb.AnalysisCore.VehicleParametersChronology
{
    public class VehicleParametersChronology
    {
        private readonly IList<VehicleChronologicalParametersGroup> groups = 
            new List<VehicleChronologicalParametersGroup>();

        public IList<VehicleChronologicalParametersGroup> Groups
        {
            get
            {
                return groups;
            }
        }

        public void AddValue(AnalyticRuleType type, DateTime date, double value)
        {
            RuleTypeTreePathResolver pathResolver = 
                new RuleTypeTreePathResolver(type);
            string ruleTypeTreePath = pathResolver.GetPath();
            PutDataByPath(ruleTypeTreePath, type, date, value);
        }

        private void PutDataByPath(string path, AnalyticRuleType type, 
            DateTime captureDate, double value)
        {
            ChronologyPathNavigator navigator = 
                new ChronologyPathNavigator(this);
            var parameter = new VehicleChronologicalParameter(type);
            parameter.Values.Add(new KeyValuePair<DateTime, double>(captureDate, value));
            navigator.PlaceDataByPath(path, parameter);
        }
    }
}

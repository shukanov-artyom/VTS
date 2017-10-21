using System;
using System.Collections.Generic;
using Agent.Common.Instance;
using VTS.Agent.BusinessObjects;

namespace Agent.Common.Presentation.Evaluation
{
    public class EvaluationGrid
    {
        private readonly string vin;
        private readonly StatisticsPerVehicleSubCache vehicleStatistics;
        private readonly List<AnalyticRuleSettings> ruleSettings;

        private readonly List<DateTime> revisions = new List<DateTime>();

        public EvaluationGrid(string vin,
            StatisticsPerVehicleSubCache vehicleStatistics,
            List<AnalyticRuleSettings> ruleSettings)
        {
            this.vin = vin;
            this.vehicleStatistics = vehicleStatistics;
            this.ruleSettings = ruleSettings;
            Evaluate();
        }

        public List<DateTime> Revisions
        {
            get
            {
                return revisions;
            }
        }

        private void Evaluate()
        {
            
        }
    }
}

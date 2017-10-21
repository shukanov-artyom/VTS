using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Common.Instance;
using VTS.Agent.BusinessObjects;
using VTS.Agent.BusinessObjects.Enums;
using VTS.Shared;

namespace Agent.Evaluation
{
    public class VehicleEvaluationGrid
    {
        private readonly string vin;
        private readonly StatisticsPerVehicleSubCache vehicleStatistics;
        private readonly List<AnalyticRuleSettings> ruleSettings = 
            new List<AnalyticRuleSettings>();
        private readonly List<AnalyticRuleType> availableRuleTypes = 
            new List<AnalyticRuleType>();
        private readonly List<DateTime> revisionDates = new List<DateTime>();
        private readonly Dictionary<AnalyticRuleType, RuleEvaluationChronology> chronologiesByType = 
            new Dictionary<AnalyticRuleType, RuleEvaluationChronology>();

        public VehicleEvaluationGrid(string vin,
            StatisticsPerVehicleSubCache vehicleStatistics,
            List<AnalyticRuleSettings> ruleSettings)
        {
            this.vin = vin;
            this.vehicleStatistics = vehicleStatistics;
            this.ruleSettings = ruleSettings;
            Evaluate();
        }

        public List<DateTime> AvailableRevisionDates
        {
            get
            {
                return revisionDates;
            }
        }

        public List<AnalyticRuleType> AvailableRuleTypes
        {
            get
            {
                return availableRuleTypes;
            }
        }

        public EvaluationRevision GetRevision(DateTime date)
        {
            if (!revisionDates.Contains(date))
            {
                throw new NotSupportedException("There is no such revision in this grid");
            }
            EvaluationRevision result = new EvaluationRevision();
            foreach (KeyValuePair<AnalyticRuleType, RuleEvaluationChronology> 
                row in chronologiesByType)
            {
                result.AddParameterMark(row.Key, row.Value.GetMarkForRevision(date));
            }
            return result;
        }

        public RuleEvaluationChronology GetRuleEvaluationChronology(AnalyticRuleType ruleType)
        {
            return chronologiesByType[ruleType];
        }

        private void Evaluate()
        {
            foreach (DateTime date in vehicleStatistics.GetDatesOfDataUnits())
            {
                revisionDates.Add(date);
            }
            var v = AnalyticRuleSettingsCache.GetAvailableTypes(vin);
            foreach (AnalyticRuleType ruleType in v)
            {
                chronologiesByType[ruleType] =
                        new RuleEvaluationChronology(
                            ruleSettings.First(rs => rs.RuleType == ruleType),
                            vehicleStatistics.Get(ruleType));
            }
        }
    }
}

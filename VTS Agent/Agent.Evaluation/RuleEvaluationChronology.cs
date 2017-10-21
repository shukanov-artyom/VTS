using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Evaluation
{
    public class RuleEvaluationChronology
    {
        private readonly AnalyticRuleSettings ruleSettings;
        private readonly List<AnalyticStatisticsValue> values;
        private readonly Dictionary<DateTime, EvaluationMark> markChronology =
            new Dictionary<DateTime, EvaluationMark>();

        public RuleEvaluationChronology(AnalyticRuleSettings ruleSettings, 
            List<AnalyticStatisticsValue> values)
        {
            if (values == null)
            {
                throw new ArgumentException("values");
            }
            this.ruleSettings = ruleSettings;
            this.values = values;
            EvaluateValues();
        }

        public AnalyticRuleSettings RuleSettings
        {
            get
            {
                return ruleSettings;
            }
        }

        public Dictionary<DateTime, EvaluationMark> MarkChronology
        {
            get
            {
                return markChronology;
            }
        }

        public EvaluationMark GetMarkForRevision(DateTime revisionDate)
        {
            if (MarkChronology.ContainsKey(revisionDate))
            {
                return MarkChronology[revisionDate];
            }
            DateTime closestDate = GetClosestDateTo(revisionDate);
            return MarkChronology[closestDate];
        }

        private void EvaluateValues()
        {
            foreach (AnalyticStatisticsValue value in values)
            {
                markChronology[value.SourceDataCaptureDateTime] =
                    new EvaluationMark(ruleSettings, value);
            }
        }

        private DateTime GetClosestDateTo(DateTime revisionDate)
        {
            if (MarkChronology.Count == 0)
            {
                throw new NotSupportedException("It's wrong that there is no marks at all.");
            }
            DateTime result = MarkChronology.FirstOrDefault().Key;
            foreach (KeyValuePair<DateTime, EvaluationMark> pair in MarkChronology)
            {
                if (pair.Key > result && pair.Key < revisionDate)
                {
                    result = pair.Key;
                }
            }
            return result;
        }
    }
}

using System;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Evaluation
{
    public class EvaluationMark
    {
        private readonly AnalyticRuleSettings ruleSettings;
        private readonly AnalyticStatisticsValue statisticsValue;

        private double value;

        public EvaluationMark(
            AnalyticRuleSettings ruleSettings,
            AnalyticStatisticsValue statisticsValue)
        {
            this.ruleSettings = ruleSettings;
            this.statisticsValue = statisticsValue;
            CalculateValue();
        }

        public double Value
        {
            get
            {
                return value;
            }
        }

        private void CalculateValue()
        {
            IAnalyticRuleSettingsApplier settingsApplier =
                SettingsApplierFactory.CreateFor(ruleSettings);
            value = settingsApplier.ApplyTo(statisticsValue);
        }
    }
}

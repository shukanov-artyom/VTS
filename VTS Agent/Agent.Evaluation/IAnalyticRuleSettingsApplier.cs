using System;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Evaluation
{
    internal interface IAnalyticRuleSettingsApplier
    {
        double ApplyTo(AnalyticStatisticsValue statisticsValue);
    }
}

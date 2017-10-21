using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;


namespace VTSWeb.AnalysisCore.Models.Settings
{
    public abstract class AnalyticModelSettings
    {
        private readonly IList<AnalyticRuleSettings> rulesSettings = 
            new List<AnalyticRuleSettings>();
        private readonly List<AnalyticRuleType> requiredTypes = 
            new List<AnalyticRuleType>();

        public IList<AnalyticRuleSettings> RulesSettings
        {
            get
            {
                return rulesSettings;
            }
        }

        protected void RegisterRequiredType(AnalyticRuleType type)
        {
            requiredTypes.Add(type);
        }

        protected void CheckAndThrow()
        {
            if (requiredTypes.Any(ruleType => rulesSettings.All(rs => rs.RuleType != ruleType)))
            {
                throw new Exception("Insufficient rule settings!");
            }
        }

        public AnalyticRuleSettings GetOfType(AnalyticRuleType type)
        {
            return RulesSettings.FirstOrDefault(r => r.RuleType == type);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Common.PetrolEngineIgnition;
using VTSWeb.AnalysisCore.Models.Settings;

namespace VTSWeb.AnalysisCore.Models.PetrolEngineIgnition
{
    public class AnalyticModelCylinderCoilChargeTime : AnalyticModel
    {
        private int cylNumber;

        public AnalyticModelCylinderCoilChargeTime(int cylNumber,
            IList<AnalyticRuleSettings> rules)
        {
            this.cylNumber = cylNumber;
            AnalyticRuleSettings rpm1000Settings = GetSettings(cylNumber, CheckpointRpm.Rpm1000, rules);
            AnalyticRuleSettings rpm2000Settings = GetSettings(cylNumber, CheckpointRpm.Rpm2000, rules);
            AnalyticRuleSettings rpm3000Settings = GetSettings(cylNumber, CheckpointRpm.Rpm3000, rules);
            Rules.Add(new AnalyticRuleCylinderCoilChargeTimeForRpm(rpm1000Settings));
            Rules.Add(new AnalyticRuleCylinderCoilChargeTimeForRpm(rpm2000Settings));
            Rules.Add(new AnalyticRuleCylinderCoilChargeTimeForRpm(rpm3000Settings));
        }

        public override string AdditionalInfo
        {
            get
            {
                return cylNumber.ToString(CultureInfo.InvariantCulture);
            }
        }

        private AnalyticRuleSettings GetSettings(
            int n, CheckpointRpm rpm, IList<AnalyticRuleSettings> rules)
        {
            return rules.First(
                    r => CylinderNumberToCoilRuleTypeMapper.Map(r.RuleType) == n &&
                         RpmToCoilRuleTypeMapper.Map(r.RuleType) == rpm);
        }
    }
}

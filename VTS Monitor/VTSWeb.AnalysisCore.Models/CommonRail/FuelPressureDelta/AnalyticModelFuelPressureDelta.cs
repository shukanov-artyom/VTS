using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTSWeb.AnalysisCore.Models.Settings;


namespace VTSWeb.AnalysisCore.Models.CommonRail.FuelPressureDelta
{
    public class AnalyticModelFuelPressureDelta : AnalyticModel
    {
        public AnalyticModelFuelPressureDelta(
            IList<AnalyticRuleSettings> rulesSettings)
        {
            AnalyticRuleSettings for1000 = rulesSettings.First(r =>
                r.RuleType == AnalyticRuleType.FuelPressureDelta1000Rpm);
            AnalyticRuleSettings for2000 = rulesSettings.First(r =>
                r.RuleType == AnalyticRuleType.FuelPressureDelta2000Rpm);
            AnalyticRuleSettings for3000 = rulesSettings.First(r =>
                r.RuleType == AnalyticRuleType.FuelPressureDelta3000Rpm);

            Rules.Add(new AnalyticRuleFuelPressureDeltaForRpm(for1000));
            Rules.Add(new AnalyticRuleFuelPressureDeltaForRpm(for2000));
            Rules.Add(new AnalyticRuleFuelPressureDeltaForRpm(for3000));
        }
    }
}

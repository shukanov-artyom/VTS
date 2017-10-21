using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTSWeb.AnalysisCore.Models.Settings;


namespace VTSWeb.AnalysisCore.Models.CommonRail.FuelPressureRegulatorCurrent
{
    public class AnalyticModelFuelPressureRegulatorCurrent : AnalyticModel
    {
        public AnalyticModelFuelPressureRegulatorCurrent(
            IList<AnalyticRuleSettings> rulesSettings)
        {
            AnalyticRuleSettings for1000 = rulesSettings.First(r =>
                r.RuleType == AnalyticRuleType.FuelPressureRegulatorCurrent1000Rpm);
            AnalyticRuleSettings for2000 = rulesSettings.First(r =>
                r.RuleType == AnalyticRuleType.FuelPressureRegulatorCurrent2000Rpm);
            AnalyticRuleSettings for3000 = rulesSettings.First(r =>
                r.RuleType == AnalyticRuleType.FuelPressureRegulatorCurrent3000Rpm);

            Rules.Add(new AnalyticRuleFuelRegulatorCurrentForRpm(for1000));
            Rules.Add(new AnalyticRuleFuelRegulatorCurrentForRpm(for2000));
            Rules.Add(new AnalyticRuleFuelRegulatorCurrentForRpm(for3000));
        }
    }
}

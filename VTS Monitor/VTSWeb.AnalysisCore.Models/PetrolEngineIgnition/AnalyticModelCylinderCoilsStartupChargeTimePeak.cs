using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTSWeb.AnalysisCore.Models.Settings;


namespace VTSWeb.AnalysisCore.Models.PetrolEngineIgnition
{
    public class AnalyticModelCylinderCoilsStartupChargeTimePeak : AnalyticModel
    {
        public AnalyticModelCylinderCoilsStartupChargeTimePeak(
            IList<AnalyticRuleSettings> rulesSettings)
        {
            Rules.Add(new AnalyticRuleCylinderCoilChargePeakTime(1, 
                rulesSettings.First(s => s.RuleType ==
                    AnalyticRuleType.Cylinder1CoilStartupChargeTimePeak)));
            Rules.Add(new AnalyticRuleCylinderCoilChargePeakTime(2,
                rulesSettings.First(s => s.RuleType ==
                    AnalyticRuleType.Cylinder2CoilStartupChargeTimePeak)));
            Rules.Add(new AnalyticRuleCylinderCoilChargePeakTime(3,
                rulesSettings.First(s => s.RuleType ==
                    AnalyticRuleType.Cylinder3CoilStartupChargeTimePeak)));
            Rules.Add(new AnalyticRuleCylinderCoilChargePeakTime(4,
                rulesSettings.First(s => s.RuleType ==
                    AnalyticRuleType.Cylinder4CoilStartupChargeTimePeak)));
        }
    }
}

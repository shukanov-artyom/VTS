using System;
using System.Collections.Generic;
using VTSWeb.AnalysisCore.Models.Settings;

namespace VTSWeb.AnalysisCore.Models.PetrolEngineIgnition
{
    public class AnalyticModelCylinderCoilsChargeTime : AnalyticModel
    {
        public AnalyticModelCylinderCoilsChargeTime(
            IList<AnalyticRuleSettings> rulesSettings)
        {
            Models.Add(new AnalyticModelCylinderCoilChargeTime(1, rulesSettings));
            Models.Add(new AnalyticModelCylinderCoilChargeTime(2, rulesSettings));
            Models.Add(new AnalyticModelCylinderCoilChargeTime(3, rulesSettings));
            Models.Add(new AnalyticModelCylinderCoilChargeTime(4, rulesSettings));
        }
    }
}

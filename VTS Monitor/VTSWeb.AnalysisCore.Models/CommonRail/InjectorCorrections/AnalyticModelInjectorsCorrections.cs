using System;
using System.Collections.Generic;
using VTSWeb.AnalysisCore.Models.Settings;

namespace VTSWeb.AnalysisCore.Models.CommonRail.InjectorCorrections
{
    public class AnalyticModelInjectorsCorrections : AnalyticModel
    {
        public AnalyticModelInjectorsCorrections(
            IList<AnalyticRuleSettings> rulesSettings)
        {
            Models.Add(new AnalyticModelInjectorCorrections(rulesSettings, 1));
            Models.Add(new AnalyticModelInjectorCorrections(rulesSettings, 2));
            Models.Add(new AnalyticModelInjectorCorrections(rulesSettings, 3));
            Models.Add(new AnalyticModelInjectorCorrections(rulesSettings, 4));
        }
    }
}

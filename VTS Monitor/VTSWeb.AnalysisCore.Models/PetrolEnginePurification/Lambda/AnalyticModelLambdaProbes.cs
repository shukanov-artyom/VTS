using System;
using System.Collections.Generic;
using VTSWeb.AnalysisCore.Models.Settings;

namespace VTSWeb.AnalysisCore.Models.PetrolEnginePurification.Lambda
{
    public class AnalyticModelLambdaProbes : AnalyticModel
    {
        public AnalyticModelLambdaProbes(
            IList<AnalyticRuleSettings> rulesSettings)
        {
            Models.Add(new AnalyticModelUpperLambdaProbe(rulesSettings));
            Models.Add(new AnalyticModelLowerLambdaProbe(rulesSettings));
        }
    }
}

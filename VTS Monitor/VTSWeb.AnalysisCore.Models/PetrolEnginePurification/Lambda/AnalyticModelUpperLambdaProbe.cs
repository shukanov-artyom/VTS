using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTSWeb.AnalysisCore.Models.Settings;


namespace VTSWeb.AnalysisCore.Models.PetrolEnginePurification.Lambda
{
    public class AnalyticModelUpperLambdaProbe : AnalyticModel
    {
        public AnalyticModelUpperLambdaProbe(
            IList<AnalyticRuleSettings> rulesSettings)
        {
            AnalyticRuleSettings rpm1000Settings =
                rulesSettings.FirstOrDefault(rs => 
                    rs.RuleType == AnalyticRuleType.LambdaUpperVoltageAt1000Rpm);
            AnalyticRuleSettings rpm2000Settings =
                rulesSettings.FirstOrDefault(rs =>
                    rs.RuleType == AnalyticRuleType.LambdaUpperVoltageAt2000Rpm);
            AnalyticRuleSettings rpm3000Settings =
                rulesSettings.FirstOrDefault(rs =>
                    rs.RuleType == AnalyticRuleType.LambdaUpperVoltageAt3000Rpm);

            if (rpm1000Settings == null || rpm2000Settings == null || rpm3000Settings == null)
            {
                throw new Exception("Insufficient rules!");
            }

            Rules.Add(new AnalyticRuleUpperLamdaProbeVoltageForRpm(
                rpm1000Settings));
            Rules.Add(new AnalyticRuleUpperLamdaProbeVoltageForRpm(
                rpm2000Settings));
            Rules.Add(new AnalyticRuleUpperLamdaProbeVoltageForRpm(
                rpm3000Settings));
        }
    }
}

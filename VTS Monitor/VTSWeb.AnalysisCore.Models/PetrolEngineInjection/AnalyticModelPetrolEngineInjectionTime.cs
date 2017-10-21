using System;
using VTS.Shared;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Models.Settings;


namespace VTSWeb.AnalysisCore.Models.PetrolEngineInjection
{
    public class AnalyticModelPetrolEngineInjectionTime : AnalyticModel
    {
        public AnalyticModelPetrolEngineInjectionTime(
            AnalyticModelSettings settings)
        {
            Rules.Add(new AnalyticRuleInjectionTimeStartupPeak(settings.
                GetOfType(AnalyticRuleType.InjectionTimeStartupPeak)));
            Rules.Add(new AnalyticRulePetrolEngineInjectionTimeForRpm(
                CheckpointRpm.Rpm1000,
                settings.GetOfType(AnalyticRuleType.InjectionTimeAt1000Rpm)));
            Rules.Add(new AnalyticRulePetrolEngineInjectionTimeForRpm(
                CheckpointRpm.Rpm2000,
                settings.GetOfType(AnalyticRuleType.InjectionTimeAt2000Rpm)));
            Rules.Add(new AnalyticRulePetrolEngineInjectionTimeForRpm(
                CheckpointRpm.Rpm3000,
                settings.GetOfType(AnalyticRuleType.InjectionTimeAt3000Rpm)));
        }
    }
}

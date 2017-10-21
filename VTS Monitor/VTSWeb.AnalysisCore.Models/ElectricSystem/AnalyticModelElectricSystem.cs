using System;
using System.Linq;
using VTS.Shared;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Models.Settings.ElectricSystem;


namespace VTSWeb.AnalysisCore.Models.ElectricSystem
{
    public class AnalyticModelElectricSystem : AnalyticModel
    {
        private AnalyticModelSettingsElectricSystem settings;

        public AnalyticModelElectricSystem(AnalyticModelSettingsElectricSystem settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            this.settings = settings;
            AnalyticRuleSettings setting = settings.RulesSettings.
                FirstOrDefault(r => r.RuleType == AnalyticRuleType.EngineStartUndervoltage);
            Rules.Add(new AnalyticRuleStartupUndervoltage(setting));
        }
    }
}

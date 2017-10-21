using System;
using VTSWeb.AnalysisCore.Models.Settings.PetrolEngineIgnition;

namespace VTSWeb.AnalysisCore.Models.PetrolEngineIgnition
{
    public class AnalyticModelPetrolEngineIgnition : AnalyticModel
    {
        private AnalyticModelSettingsPetrolEngineIgnition settings;

        public AnalyticModelPetrolEngineIgnition(
            AnalyticModelSettingsPetrolEngineIgnition settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            this.settings = settings;
            Models.Add(new AnalyticModelCylinderCoilsChargeTime(settings.RulesSettings));
            Models.Add(new AnalyticModelCylinderCoilsStartupChargeTimePeak(settings.RulesSettings));
        }
    }
}

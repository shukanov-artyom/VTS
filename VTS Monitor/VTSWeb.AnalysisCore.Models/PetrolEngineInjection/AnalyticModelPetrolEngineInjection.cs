using System;
using VTSWeb.AnalysisCore.Models.Settings.PetrolEngineInjection;

namespace VTSWeb.AnalysisCore.Models.PetrolEngineInjection
{
    public class AnalyticModelPetrolEngineInjection : AnalyticModel
    {
        private AnalyticModelSettingsPetrolEngineInjection settings;

        public AnalyticModelPetrolEngineInjection(
            AnalyticModelSettingsPetrolEngineInjection settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            this.settings = settings;
            Models.Add(new AnalyticModelPetrolEngineInjectionTime(settings));
        }
    }
}

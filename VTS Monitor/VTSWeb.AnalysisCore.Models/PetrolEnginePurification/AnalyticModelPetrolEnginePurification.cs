using System;
using VTSWeb.AnalysisCore.Models.PetrolEnginePurification.Lambda;
using VTSWeb.AnalysisCore.Models.Settings.PetrolEnginePurification;

namespace VTSWeb.AnalysisCore.Models.PetrolEnginePurification
{
    public class AnalyticModelPetrolEnginePurification : AnalyticModel
    {
        private AnalyticModelSettingsPetrolEnginePurification settings;

        public AnalyticModelPetrolEnginePurification(
            AnalyticModelSettingsPetrolEnginePurification settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            this.settings = settings;
            Models.Add(new AnalyticModelLambdaProbes(settings.RulesSettings));
        }
    }
}

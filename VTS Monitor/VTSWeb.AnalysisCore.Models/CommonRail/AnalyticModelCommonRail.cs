using System;
using VTSWeb.AnalysisCore.Models.CommonRail.FuelPressureDelta;
using VTSWeb.AnalysisCore.Models.CommonRail.FuelPressureRegulatorCurrent;
using VTSWeb.AnalysisCore.Models.CommonRail.InjectorCorrections;
using VTSWeb.AnalysisCore.Models.Settings.CommonRail;

namespace VTSWeb.AnalysisCore.Models.CommonRail
{
    public class AnalyticModelCommonRail : AnalyticModel
    {
        private AnalyticModelSettingsCommonRail settings;

        public AnalyticModelCommonRail(
            AnalyticModelSettingsCommonRail settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            this.settings = settings;

            Models.Add(
                new AnalyticModelInjectorsCorrections(settings.RulesSettings));
            Models.Add(
                new AnalyticModelFuelPressureDelta(settings.RulesSettings));
            Models.Add(
                new AnalyticModelFuelPressureRegulatorCurrent(settings.RulesSettings));
        }
    }
}

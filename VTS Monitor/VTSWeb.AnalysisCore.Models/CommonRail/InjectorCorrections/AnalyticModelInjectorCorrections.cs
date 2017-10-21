using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Models.Settings;

namespace VTSWeb.AnalysisCore.Models.CommonRail.InjectorCorrections
{
    /// <summary>
    /// Incudes all correction values (1000, 2000, 3000) for one injector
    /// </summary>
    public class AnalyticModelInjectorCorrections : AnalyticModel
    {
        private int injectorNumber;

        public AnalyticModelInjectorCorrections(
            IList<AnalyticRuleSettings> rulesSettings, int injectorNumber)
        {
            if (injectorNumber < 1 || injectorNumber > 4)
            {
                throw new Exception("Incorrect injector number");
            }
            this.injectorNumber = injectorNumber;
            AnalyticRuleSettings settings1000 =
                GetInjectorCorrectionRuleSettings(
                rulesSettings, CheckpointRpm.Rpm1000);
            AnalyticRuleSettings settings2000 = 
                GetInjectorCorrectionRuleSettings(
                rulesSettings, CheckpointRpm.Rpm2000);
            AnalyticRuleSettings settings3000 =
                GetInjectorCorrectionRuleSettings(
                rulesSettings, CheckpointRpm.Rpm3000);

            Rules.Add(new AnalyticRuleInjectorCorrection(settings1000, 
                injectorNumber));
            Rules.Add(new AnalyticRuleInjectorCorrection(settings2000,
                injectorNumber));
            Rules.Add(new AnalyticRuleInjectorCorrection(settings3000,
                injectorNumber));
        }

        public override string AdditionalInfo
        {
            get
            {
                return injectorNumber.ToString(
                    CultureInfo.InvariantCulture);
            }
        }

        private AnalyticRuleSettings GetInjectorCorrectionRuleSettings(
            IList<AnalyticRuleSettings> settings, CheckpointRpm rpm)
        {
            return settings.FirstOrDefault(s =>
                RuleTypeToRpm.Map(s.RuleType) == rpm);
        }
    }
}

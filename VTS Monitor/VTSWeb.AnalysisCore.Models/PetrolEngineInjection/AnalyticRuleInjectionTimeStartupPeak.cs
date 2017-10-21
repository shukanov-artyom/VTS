using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Tools;
using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.AnalysisCore.Models.PetrolEngineInjection
{
    public class AnalyticRuleInjectionTimeStartupPeak : AnalyticRuleBase
    {
        public AnalyticRuleInjectionTimeStartupPeak(
            AnalyticRuleSettings settings)
            : base(settings)
        {
            RegisterRequiredParameter(PsaParameterType.EngineRpm);
            RegisterRequiredParameter(PsaParameterType.InjectionTime);
        }

        protected override void PickFromPsaParametersSet(
            DateTime date, PsaParametersSet set)
        {
            IList<double> rpmData = set.GetParameterOfType(PsaParameterType.EngineRpm).GetDoubles();
            IList<double> timeData = set.GetParameterOfType(PsaParameterType.InjectionTime).GetDoubles();
            EngineStartupDetector detector = new EngineStartupDetector(rpmData);
            if (!detector.EngineStartupDetected())
            {
                return;
            }
            IList<int> indexes = detector.GetEngineStartupPointIndexes();
            double peakTime = 0;
            foreach (int i in indexes)
            {
                double newPeakTime = StartupRegionExtractor.Extract(i, timeData).Max();
                if (newPeakTime > peakTime)
                {
                    peakTime = newPeakTime;
                }
            }
            if (peakTime == 0)
            {
                return;
            }
            SettingsAtomApplier applier = new SettingsAtomApplier(
                Settings.SettingsMolecule.GetPriorityAtom());
            double mark = applier.GetMarkForValue(peakTime);
            MarksHistory[date] = mark;
        }
    }
}

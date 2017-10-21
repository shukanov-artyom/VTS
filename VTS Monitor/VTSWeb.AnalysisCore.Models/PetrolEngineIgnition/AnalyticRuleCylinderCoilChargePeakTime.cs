using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Common.PetrolEngineIgnition;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Tools;

using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.AnalysisCore.Models.PetrolEngineIgnition
{
    public class AnalyticRuleCylinderCoilChargePeakTime : AnalyticRuleBase
    {
        private int cylNum;
        private PsaParameterType requiredParameter;

        public AnalyticRuleCylinderCoilChargePeakTime(
            int cylNum, AnalyticRuleSettings settings)
            : base(settings)
        {
            this.cylNum = cylNum;
            requiredParameter = ModelRelatedPsaParameters.Get(cylNum);
            RegisterRequiredParameter(PsaParameterType.EngineRpm);
            RegisterRequiredParameter(requiredParameter);
        }

        public override string AdditionalInfo
        {
            get
            {
                return cylNum.ToString(CultureInfo.InvariantCulture);
            }
        }

        protected override void PickFromPsaParametersSet(
            DateTime date, PsaParametersSet set)
        {
            IList<double> rpmData = set.GetParameterOfType(PsaParameterType.EngineRpm).GetDoubles();
            IList<double> requiredData = set.GetParameterOfType(requiredParameter).GetDoubles();
            EngineStartupDetector detector = new EngineStartupDetector(rpmData);
            if (!detector.EngineStartupDetected())
            {
                return;
            }
            IList<int> startupIndexes = detector.GetEngineStartupPointIndexes();
            foreach (int startupIndex in startupIndexes)
            {
                double peak = StartupRegionExtractor.Extract(startupIndex, requiredData).Max();
                if (double.IsNaN(peak))
                {
                    continue;
                }
                SettingsAtomApplier applier = new SettingsAtomApplier(
                    Settings.SettingsMolecule.GetPriorityAtom());
                double mark = applier.GetMarkForValue(peak);
                if (!double.IsNaN(mark))
                {
                    MarksHistory[date] = mark;
                }
            }
        }
    }
}

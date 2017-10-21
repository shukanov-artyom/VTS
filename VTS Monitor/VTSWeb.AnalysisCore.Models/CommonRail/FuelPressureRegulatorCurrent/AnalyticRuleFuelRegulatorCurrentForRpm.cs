using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Tools;

namespace VTSWeb.AnalysisCore.Models.CommonRail.FuelPressureRegulatorCurrent
{
    public class AnalyticRuleFuelRegulatorCurrentForRpm : AnalyticRuleBase
    {
        public AnalyticRuleFuelRegulatorCurrentForRpm(
            AnalyticRuleSettings settings)
            : base(settings)
        {
            RegisterRequiredParameter(PsaParameterType.EngineRpm);
            RegisterRequiredParameter(PsaParameterType.FuelRegulatorCurrent);
        }

        public override string AdditionalInfo
        {
            get
            {
                string result = Enum.GetName(typeof(CheckpointRpm), (int)Rpm);
                return result;
            }
        }

        private CheckpointRpm Rpm
        {
            get
            {
                return RuleTypeToRpm.Map(Type);
            }
        }

        protected override void PickFromPsaParametersSet(
            DateTime date, PsaParametersSet set)
        {
            IList<double> rpmData = set.GetParameterOfType(
                PsaParameterType.EngineRpm).GetDoubles();
            IList<double> regulatorCurrentData = set.GetParameterOfType(
                PsaParameterType.FuelRegulatorCurrent).GetDoubles();
            CorrelatedMedianExtractor medianExtractor =
                new CorrelatedMedianExtractor(rpmData, regulatorCurrentData, 5);
            double value = medianExtractor.GetForBaseValue(Convert.ToDouble((int)Rpm));
            if (double.IsNaN(value))
            {
                return;
            }
            SettingsAtomApplier applier = 
                new SettingsAtomApplier(Settings.SettingsMolecule.GetPriorityAtom());
            MarksHistory[date] = applier.GetMarkForValue(value);
        }
    }
}

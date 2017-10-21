using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Tools;
using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.AnalysisCore.Models.CommonRail.FuelPressureDelta
{
    public class AnalyticRuleFuelPressureDeltaForRpm :
        AnalyticRuleBase
    {
        public AnalyticRuleFuelPressureDeltaForRpm(AnalyticRuleSettings settings)
            : base(settings)
        {
            RegisterRequiredParameter(PsaParameterType.EngineRpm);
            RegisterRequiredParameter(PsaParameterType.FuelSystemPressureDelta);
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
            IList<double> rpmData = set.GetParameterOfType(PsaParameterType.EngineRpm).GetDoubles();
            IList<double> pressureDeltaData = set.GetParameterOfType(PsaParameterType.FuelSystemPressureDelta).GetDoubles();
            CorrelatedMedianExtractor medianExtractor = 
                new CorrelatedMedianExtractor(rpmData, pressureDeltaData, 5);
            double value = medianExtractor.GetForBaseValue(Convert.ToDouble((int)Rpm));
            if (double.IsNaN(value))
            {
                return;
            }
            SettingsAtomApplier applier = new SettingsAtomApplier(
                Settings.SettingsMolecule.GetPriorityAtom());
            MarksHistory[date] = applier.GetMarkForValue(value);
        }
    }
}

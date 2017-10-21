using System;
using System.Collections.Generic;
using System.Globalization;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Common.PetrolEngineIgnition;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Tools;

using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.AnalysisCore.Models.PetrolEngineIgnition
{
    public class AnalyticRuleCylinderCoilChargeTimeForRpm : AnalyticRuleBase
    {
        private PsaParameterType requiredParameter;
        private int cylNumber;

        public AnalyticRuleCylinderCoilChargeTimeForRpm(
            AnalyticRuleSettings settings)
            : base(settings)
        {
            cylNumber = CylinderNumberToCoilRuleTypeMapper.Map(settings.RuleType);
            requiredParameter = ModelRelatedPsaParameters.Get(cylNumber);
            RegisterRequiredParameter(PsaParameterType.EngineRpm);
            RegisterRequiredParameter(ModelRelatedPsaParameters.Get(
                CylinderNumberToCoilRuleTypeMapper.Map(settings.RuleType)));
        }

        public override string AdditionalInfo
        {
            get
            {
                string rpm = ((int) Rpm).ToString(CultureInfo.InvariantCulture);
                string key = String.Format("Coil{0}Rpm{1}", cylNumber, rpm);
                return key;
            }
        }

        private CheckpointRpm Rpm
        {
            get
            {
                return RpmToCoilRuleTypeMapper.Map(Type);
            }
        }

        protected override void PickFromPsaParametersSet(
            DateTime date, PsaParametersSet set)
        {
            IList<double> rpmLine = set.GetParameterOfType(PsaParameterType.EngineRpm).GetDoubles();
            IList<double> chargeTimeLine = set.GetParameterOfType(requiredParameter).GetDoubles();
            CorrelatedMedianExtractor exractor = new CorrelatedMedianExtractor(
                rpmLine, chargeTimeLine, 5);
            double value = exractor.GetForBaseValue((int) Rpm);
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

using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Tools;

namespace VTSWeb.AnalysisCore.Models.CommonRail.InjectorCorrections
{
    public class AnalyticRuleInjectorCorrection : AnalyticRuleBase
    {
        private readonly AnalyticRuleSettings settings;
        private readonly int injectorNumber;

        public AnalyticRuleInjectorCorrection(AnalyticRuleSettings settings, 
            int injectorNumber)
            : base(settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            this.settings = settings;
            this.injectorNumber = injectorNumber;
            RegisterRequiredParameter(PsaParameterType.EngineRpm);
            RegisterRequiredParameter(PsaParameterType.Injector1Correction);
            RegisterRequiredParameter(PsaParameterType.Injector2Correction);
            RegisterRequiredParameter(PsaParameterType.Injector3Correction);
            RegisterRequiredParameter(PsaParameterType.Injector4Correction);
        }

        public override string AdditionalInfo
        {
            get
            {
                string result = Enum.GetName(typeof(CheckpointRpm), 
                    (int)RuleTypeToRpm.Map(Type));
                return result;
            }
        }

        protected override void PickFromPsaParametersSet(
            DateTime date, PsaParametersSet source)
        {
            IList<double> baseLine = source.GetParameterOfType(
                PsaParameterType.EngineRpm).GetDoubles();
            IList<double> dependantLine = source.
                GetCertainInjectorCorrections(injectorNumber).GetDoubles();
            CorrelatedMedianExtractor correlatedMedianExtractor = 
                new CorrelatedMedianExtractor(baseLine, dependantLine,
                    BaseValuesDifferencePercentageThreshold);
            CheckpointRpm targetRpm = RuleTypeToRpm.Map(settings.RuleType);
            double resultValue = correlatedMedianExtractor.GetForBaseValue(
                Convert.ToDouble(targetRpm));
            if (double.IsNaN(resultValue))
            {
                return;
            }
            SettingsAtomApplierByAbsoluteValue applier =
                new SettingsAtomApplierByAbsoluteValue(
                    settings.SettingsMolecule.GetPriorityAtom());
            MarksHistory[date] = applier.GetMarkForValue(resultValue);
        }
    }
}

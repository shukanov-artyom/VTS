using System;
using System.Collections.Generic;
using System.Globalization;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Tools;

using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.AnalysisCore.Models.PetrolEngineInjection
{
    public class AnalyticRulePetrolEngineInjectionTimeForRpm 
        : AnalyticRuleBase
    {
        private CheckpointRpm rpm;

        public AnalyticRulePetrolEngineInjectionTimeForRpm(
            CheckpointRpm rpm, AnalyticRuleSettings settings)
            : base(settings)
        {
            this.rpm = rpm;
            RegisterRequiredParameter(PsaParameterType.EngineRpm);
            RegisterRequiredParameter(PsaParameterType.InjectionTime);
        }

        public override string AdditionalInfo
        {
            get
            {
                return ((int)rpm).ToString(
                    CultureInfo.InvariantCulture);
            }
        }

        protected override void PickFromPsaParametersSet(
            DateTime date, PsaParametersSet set)
        {
            IList<double> rpmData = set.GetParameterOfType(
                PsaParameterType.EngineRpm).GetDoubles();
            IList<double> injectionTimeData = set.GetParameterOfType(
                PsaParameterType.InjectionTime).GetDoubles();
            CorrelatedMedianExtractor extractor = 
                new CorrelatedMedianExtractor(rpmData, injectionTimeData, 5);
            double value = extractor.GetForBaseValue((int) rpm);
            if (double.IsNaN(value))
            {
                return;
            }
            ISettingsAtomApplier applier = new SettingsAtomApplier(
                Settings.SettingsMolecule.GetPriorityAtom());
            double mark = applier.GetMarkForValue(value);
            if (double.IsNaN(mark))
            {
                return;
            }
            MarksHistory[date] = mark;
        }
    }
}

using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Common;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Tools;
using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.AnalysisCore.Models.PetrolEnginePurification.Lambda
{
    public abstract class AnalyticRuleLambdaProbeVoltageForRpmBase : AnalyticRuleBase
    {
        public AnalyticRuleLambdaProbeVoltageForRpmBase(
            AnalyticRuleSettings settings)
            : base(settings)
        {

        }

        public override string AdditionalInfo
        {
            get
            {
                return Enum.GetName(typeof(CheckpointRpm),
                    (int)GetTargetRpm());
            }
        }

        protected abstract PsaParameterType GetOxygenSensorParameter();

        protected override void PickFromPsaParametersSet(
            DateTime date, PsaParametersSet set)
        {
            // 1. get our target rpm from settings
            PsaParameterData rpmParameter = 
                set.GetParameterOfType(PsaParameterType.EngineRpm);

            // 2. find data in the dataset
            PsaParameterData oxygenSensorVoltageParameter =
                set.GetParameterOfType(GetOxygenSensorParameter());

            // 3. get base line (rpm)
            IList<double> baseLine = rpmParameter.GetDoubles();

            // 4. get dependant line (probe voltage)
            IList<double> dependantLine = oxygenSensorVoltageParameter.GetDoubles();

            // 5. extract correlated median for base RPM value
            CorrelatedMedianExtractor extractor = 
                new CorrelatedMedianExtractor(baseLine, dependantLine, 
                    BaseValuesDifferencePercentageThreshold);
            CheckpointRpm rpm = GetTargetRpm();
            double corellatedMedian = extractor.GetForBaseValue(Convert.ToDouble(rpm));
            if (double.IsNaN(corellatedMedian))
            {
                return;
            }

            // 6. apply atom settings
            SettingsAtomApplier applier = new SettingsAtomApplier(
                Settings.SettingsMolecule.GetPriorityAtom());
            double mark =
                Math.Round(applier.GetMarkForValue(corellatedMedian), 1);

            // 7. save to marks history
            MarksHistory[date] = mark;
        }

        protected abstract CheckpointRpm GetTargetRpm();
    }
}

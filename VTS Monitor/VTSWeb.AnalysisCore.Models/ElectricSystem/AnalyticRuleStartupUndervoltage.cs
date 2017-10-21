using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Models.Settings;
using VTSWeb.AnalysisCore.Tools;
using VTSWeb.DomainObjects.Psa;

using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.AnalysisCore.Models.ElectricSystem
{
    public class AnalyticRuleStartupUndervoltage : AnalyticRuleBase
    {
        public AnalyticRuleStartupUndervoltage(AnalyticRuleSettings settings)
            : base(settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            RegisterRequiredParameter(PsaParameterType.EngineRpm);
            RegisterRequiredParameter(PsaParameterType.BatteryVoltage);
        }

        protected override void PickFromPsaParametersSet(
            DateTime date, PsaParametersSet set)
        {
            PsaParameterData rpm = set.GetParameterOfType(
                PsaParameterType.EngineRpm);
            PsaParameterData voltage = set.GetParameterOfType(
                PsaParameterType.BatteryVoltage);
            IList<double> startupVoltageValues =
                ExtractStartupVoltageValues(rpm, voltage);
            if (startupVoltageValues.Count == 0)
            {
                // no action - this set does not have engine start info
                return;
            }
            double minVoltageValue = startupVoltageValues.Min();
            SettingsAtomApplier applier = new SettingsAtomApplier(
                Settings.SettingsMolecule.GetPriorityAtom());
            double mark =
                Math.Round(applier.GetMarkForValue(minVoltageValue), 1);
            MarksHistory[date] = mark;
        }

        private IList<double> ExtractStartupVoltageValues(PsaParameterData rpm, 
            PsaParameterData voltage)
        {
            IList<double> result = new List<double>();
            EngineStartupDetector detector =
                new EngineStartupDetector(rpm.GetDoubles());
            if (detector.EngineStartupDetected())
            {
                IList<int> startupIndexes =
                    detector.GetEngineStartupPointIndexes();
                IList<double> voltageValues = voltage.GetDoubles();
                foreach (int index in startupIndexes)
                {
                    result.Add(voltageValues[index]);
                }
            }
            return result;
        }
    }
}

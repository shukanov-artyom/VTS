using System;
using System.Linq;
using VTS.AnalysisCore.Common;
using AnalyticRuleSettingsEntity = VTSWebService.DataAccess.AnalyticRuleSettings;

namespace VTSWebService.AnalysisCore.Aggregation
{
    public class ReliabilitySummarizer
    {
        // we need to update a Reliability data if there is some override
        // overrides are a Special Case for Reliability - need a special Arbitrator here
        public void SummarizeFor(AnalyticRuleSettingsEntity settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            if (!settings.SettingsMolecule.First().OverrideAcceptable &&
                !settings.SettingsMolecule.First().OverrideOptimal)
            {
                // all is up to date
                return;
            }
            if (settings.SettingsMolecule.First().OverrideAcceptable &&
                settings.SettingsMolecule.First().OverrideOptimal)
            {
                settings.Reliability = (int)AnalyticItemSettingsReliability.High;
                return;
            }
            // We have one predefined value, it means that Reliability is High for overriden
            AnalyticItemSettingsReliability finalReliability =
                Upgrade(settings.Reliability);
            settings.Reliability = (int)finalReliability;
        }

        private AnalyticItemSettingsReliability Upgrade(int reliability)
        {
            if (reliability == (int)AnalyticItemSettingsReliability.High)
            {
                return AnalyticItemSettingsReliability.High;
            }
            int reliabilityInt = (int)reliability;
            return (AnalyticItemSettingsReliability)(reliabilityInt++);
        }
    }
}

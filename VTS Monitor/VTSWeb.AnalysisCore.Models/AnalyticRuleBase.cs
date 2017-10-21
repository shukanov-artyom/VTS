using System;
using System.Collections.Generic;
using System.Linq;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Interfaces;
using VTSWeb.AnalysisCore.Models.Settings;

using VTSWeb.DomainObjects.Psa;


namespace VTSWeb.AnalysisCore.Models
{
    public abstract class AnalyticRuleBase : IAnalyticRule
    {
        private AnalyticRuleSettings settings;
        private IList<PsaParameterType> requiredParametersInSet =
            new List<PsaParameterType>();
        private IDictionary<DateTime, double> marksHistory =
            new Dictionary<DateTime, double>();
        protected const int BaseValuesDifferencePercentageThreshold = 5;

        protected AnalyticRuleBase(AnalyticRuleSettings settings)
        {
            this.settings = settings;
        }

        public AnalyticRuleType Type
        {
            get
            {
                return settings.RuleType;
            }
        }

        public AnalyticRuleSettings Settings
        {
            get
            {
                return settings;
            }
        }

        public AnalyticItemSettingsReliability Reliability
        {
            get
            {
                return settings.Reliability;
            }
        }

        public IDictionary<DateTime, double> MarksHistory
        {
            get
            {
                return marksHistory;
            }
        }

        public virtual string AdditionalInfo
        {
            get
            {
                return String.Empty;
            }
        }

        protected void RegisterRequiredParameter(PsaParameterType type)
        {
            requiredParametersInSet.Add(type);
        }

        public void Pick(PsaDataset dataset)
        {
            foreach (PsaTrace trace in dataset.Traces)
            {
                ProcessTrace(trace);
            }
        }

        private void ProcessTrace(PsaTrace trace)
        {
            DateTime traceDate = trace.Date;
            foreach (PsaParametersSet suitableSet in
                trace.ParametersSets.Where(set => SetSuits(set)))
            {
                PickFromPsaParametersSet(traceDate, suitableSet);
            }
        }

        private bool SetSuits(PsaParametersSet set)
        {
            if (requiredParametersInSet.Count == 0)
            {
                throw new Exception("required parameters not initialized");
            }
            foreach (PsaParameterType requiredType in requiredParametersInSet)
            {
                if (!set.Parameters.Any(p => p.Type == requiredType))
                {
                    return false;
                }
            }
            return true;
        }

        protected abstract void PickFromPsaParametersSet(
            DateTime date, PsaParametersSet set);
    }
}

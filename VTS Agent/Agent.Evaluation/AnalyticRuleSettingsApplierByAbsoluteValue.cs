using System;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Evaluation
{
    internal class AnalyticRuleSettingsApplierByAbsoluteValue : IAnalyticRuleSettingsApplier
    {
        private readonly AnalyticRuleSettings settings;
        private readonly SettingsAtom atom;

        public AnalyticRuleSettingsApplierByAbsoluteValue(AnalyticRuleSettings settings)
        {
            this.settings = settings;
            atom = settings.SettingsMolecule.GetPriorityAtom();
        }

        public double ApplyTo(AnalyticStatisticsValue statisticsValue)
        {
            double value = statisticsValue.Value;
            double module = Math.Abs(value);
            if (module > atom.MinOptimal &&
                module < atom.MaxOptimal)
            {
                return 10;
            }
            if (module.Equals(atom.MinOptimal) ||
                module.Equals(atom.MaxOptimal))
            {
                return 9;
            }
            if (module.Equals(atom.MaxAcceptable))
            {
                return 2;
            }
            if (module > atom.MaxAcceptable)
            {
                return 1;
            }
            return GetDifferentialMark(module);
        }

        private double GetDifferentialMark(double module)
        {
            double L = (atom.MaxAcceptable - atom.MinAcceptable) / 6;
            double mark = ((module - atom.MinAcceptable) / L) + 2;
            return mark;
        }
    }
}

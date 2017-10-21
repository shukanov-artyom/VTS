using System;
using VTS.Agent.BusinessObjects;
using VTS.Shared.DomainObjects;

namespace Agent.Evaluation
{
    internal class AnalyticRuleSettingsApplierDefault : IAnalyticRuleSettingsApplier
    {
        private readonly AnalyticRuleSettings settings;
        private readonly SettingsAtom atom;

        public AnalyticRuleSettingsApplierDefault(AnalyticRuleSettings settings)
        {
            this.settings = settings;
            atom = settings.SettingsMolecule.GetPriorityAtom();
        }

        public double ApplyTo(AnalyticStatisticsValue statisticsValue)
        {
            double value = statisticsValue.Value;
            if (value < atom.MaxOptimal &&
                value > atom.MinOptimal)
            {
                return 10;
            }
            if (value == atom.MaxOptimal ||
                value == atom.MinOptimal)
            {
                return 9;
            }
            if (value == atom.MinAcceptable ||
                value == atom.MaxAcceptable)
            {
                return 2;
            }
            if (value > atom.MaxAcceptable ||
                value < atom.MinAcceptable)
            {
                return 1;
            }
            return GetDifferentialMark(value);
        }

        private double GetDifferentialMark(double value)
        {
            if (value > atom.MinAcceptable && value < atom.MinOptimal)
            {
                return GetDifferentalMarkForRange(value,
                    atom.MinAcceptable, atom.MinOptimal);
            }
            if (value > atom.MaxOptimal && value < atom.MaxAcceptable)
            {
                return GetDifferentalMarkForRange(value,
                    atom.MaxOptimal, atom.MaxAcceptable);
            }
            throw new Exception();
        }

        private double GetDifferentalMarkForRange(double value,
            double lowBorder, double highBorder)
        {
            double L = (highBorder - lowBorder) / 6;
            return ((value - lowBorder) / L) + 2;
        }
    }
}

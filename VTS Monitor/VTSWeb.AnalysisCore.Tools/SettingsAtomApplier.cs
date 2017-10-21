using System;
using VTSWeb.AnalysisCore.Models.Settings;

namespace VTSWeb.AnalysisCore.Tools
{
    public class SettingsAtomApplier : ISettingsAtomApplier
    {
        private SettingsAtom atom;

        public SettingsAtomApplier(SettingsAtom atom)
        {
            if (atom == null)
            {
                throw new ArgumentNullException("atom");
            }
            this.atom = atom;
        }

        public double GetMarkForValue(double value)
        {
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
            else
            {
                return GetDifferentialMark(value);
            }
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

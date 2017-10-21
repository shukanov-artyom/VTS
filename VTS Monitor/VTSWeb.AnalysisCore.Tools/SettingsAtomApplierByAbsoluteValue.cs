using System;
using VTSWeb.AnalysisCore.Models.Settings;

namespace VTSWeb.AnalysisCore.Tools
{
    public class SettingsAtomApplierByAbsoluteValue : ISettingsAtomApplier
    {
        private SettingsAtom atom;

        public SettingsAtomApplierByAbsoluteValue(SettingsAtom atom)
        {
            if (atom == null)
            {
                throw new ArgumentNullException("atom");
            }
            this.atom = atom;
        }

        public double GetMarkForValue(double value)
        {
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
            double L = (atom.MaxAcceptable - atom.MinAcceptable)/6;
            double mark = ((module - atom.MinAcceptable)/L) + 2;
            return mark;
        }
    }
}

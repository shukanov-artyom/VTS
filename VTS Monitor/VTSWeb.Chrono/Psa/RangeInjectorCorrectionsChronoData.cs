using System;
using System.Collections.Generic;

namespace VTSWeb.Chrono.Psa
{
    public class RangeInjectorCorrectionsChronoData
    {
        private int injectorNumber;
        private IList<DateTime> dates = new List<DateTime>();
        private IList<double> maxValues = new List<double>();
        private IList<double> minValues = new List<double>();

        public IList<DateTime> Dates
        {
            get
            {
                return dates;
            }
        }

        public IList<double> MaxValues
        {
            get
            {
                return maxValues;
            }
        }

        public IList<double> MinValues
        {
            get
            {
                return minValues;
            }
        }

        public int InjectorNumber
        {
            get
            {
                return injectorNumber;
            }
            set
            {
                injectorNumber = value;
            }
        }
    }
}

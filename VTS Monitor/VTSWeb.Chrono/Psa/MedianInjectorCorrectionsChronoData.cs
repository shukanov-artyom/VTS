using System;
using System.Collections.Generic;

namespace VTSWeb.Chrono.Psa
{
    public class MedianInjectorCorrectionsChronoData
    {
        private int injectorNumber;
        private IList<DateTime> dates = new List<DateTime>();

        private IList<double> values = new List<double>();

        public IList<DateTime> Dates
        {
            get
            {
                return dates;
            }
        }

        public IList<double> Values
        {
            get
            {
                return values;
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

using System;
using System.Collections.Generic;

namespace VTSWeb.Chrono.Psa
{
    public class ChronoParamIdleEngineRpm : IChronologicalParameter
    {
        private IList<DateTime> coldStartRpmDates = new List<DateTime>();
        private IList<DateTime> middleStartRpmDates = new List<DateTime>();
        private IList<DateTime> hotStartRpmDates = new List<DateTime>();

        private IList<double> coldStartRpmValues = new List<double>();
        private IList<double> middleStartRpmValues = new List<double>();
        private IList<double> hotStartRpmValues = new List<double>();

        public ChronologicalParameterType Type
        {
            get
            {
                return ChronologicalParameterType.IdleEngineRpm;
            }
        }

        public IList<DateTime> ColdStartRpmDates
        {
            get
            {
                return coldStartRpmDates;
            }
        }

        public IList<DateTime> MiddleStartRpmDates
        {
            get
            {
                return middleStartRpmDates;
            }
        }

        public IList<DateTime> HotStartRpmDates
        {
            get
            {
                return hotStartRpmDates;
            }
        }

        public IList<double> ColdStartRpmValues
        {
            get
            {
                return coldStartRpmValues;
            }
        }

        public IList<double> MiddleStartRpmValues
        {
            get
            {
                return middleStartRpmValues;
            }
        }

        public IList<double> HotStartRpmValues
        {
            get
            {
                return hotStartRpmValues;
            }
        }
    }
}

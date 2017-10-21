using System;
using System.Collections.Generic;

namespace VTSWeb.Chrono.Psa
{
    public class ChronoParamInjectorCorrections : IChronologicalParameter
    {
        // shold be 4 of each collections - for each injector
        private IList<MedianInjectorCorrectionsChronoData> lowRpmMedianData =
            new List<MedianInjectorCorrectionsChronoData>();
        private IList<MedianInjectorCorrectionsChronoData> mediumRpmMedianData =
            new List<MedianInjectorCorrectionsChronoData>();
        private IList<MedianInjectorCorrectionsChronoData> highRpmMedianData =
            new List<MedianInjectorCorrectionsChronoData>();
        private IList<MedianInjectorCorrectionsChronoData> medianData = 
            new List<MedianInjectorCorrectionsChronoData>();

        private IList<RangeInjectorCorrectionsChronoData> lowRpmRangeData = 
            new List<RangeInjectorCorrectionsChronoData>();
        private IList<RangeInjectorCorrectionsChronoData> mediumRpmRangeData =
            new List<RangeInjectorCorrectionsChronoData>();
        private IList<RangeInjectorCorrectionsChronoData> highRpmRangeData =
            new List<RangeInjectorCorrectionsChronoData>();
        private IList<RangeInjectorCorrectionsChronoData> rangeData =
            new List<RangeInjectorCorrectionsChronoData>();

        public ChronologicalParameterType Type
        {
            get
            {
                return ChronologicalParameterType.InjectorCorrections;
            }
        }

        public IList<MedianInjectorCorrectionsChronoData> LowRpmMedianData
        {
            get
            {
                return lowRpmMedianData;
            }
        }

        public IList<MedianInjectorCorrectionsChronoData> MediumRpmMedianData
        {
            get
            {
                return mediumRpmMedianData;
            }
        }

        public IList<MedianInjectorCorrectionsChronoData> HighRpmMedianData
        {
            get
            {
                return highRpmMedianData;
            }
        }

        public IList<MedianInjectorCorrectionsChronoData> MedianData
        {
            get
            {
                return medianData;
            }
        }

        public IList<RangeInjectorCorrectionsChronoData> LowRpmRangeData
        {
            get
            {
                return lowRpmRangeData;
            }
        }

        public IList<RangeInjectorCorrectionsChronoData> MediumRpmRangeData
        {
            get
            {
                return mediumRpmRangeData;
            }
        }

        public IList<RangeInjectorCorrectionsChronoData> HighRpmRangeData
        {
            get
            {
                return highRpmRangeData;
            }
        }

        public IList<RangeInjectorCorrectionsChronoData> RangeData
        {
            get
            {
                return rangeData;
            }
        }
    }
}

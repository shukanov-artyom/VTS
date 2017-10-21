using System;
using System.Collections.Generic;

namespace VTSWeb.Chrono.Psa
{
    public class ChronoParamIdleRpmFuelPressure : IChronologicalParameter
    {
        private IList<KeyValuePair<DateTime, double>> values = 
            new List<KeyValuePair<DateTime, double>>();
        private IDictionary<DateTime, double> ocrToRpmRatio = 
            new Dictionary<DateTime, double>();

        public IList<KeyValuePair<DateTime, double>> Values
        {
            get
            {
                return values;
            }
        }

        public IDictionary<DateTime, double> OcrToRpmRatio
        {
            get
            {
                return ocrToRpmRatio;
            }
        }

        public ChronologicalParameterType Type
        {
            get
            {
                return ChronologicalParameterType.IdleRpmFuelPressure;
            }
        }
    }
}

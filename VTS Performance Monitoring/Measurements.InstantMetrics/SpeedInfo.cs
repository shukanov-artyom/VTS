using System;

namespace Measurements.InstantMetrics
{
    internal class SpeedInfo
    {
        public SpeedInfo(DateTime time, double value)
        {
            StartTime = time;
            IncrementValue = value;
        }

        public DateTime StartTime
        {
            get;
            private set;
        }

        public double IncrementValue
        {
            get;
            private set;
        }
    }
}

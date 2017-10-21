using System;

namespace Measurements.InstantMetrics
{
    internal class InstantMeterQueueItem
    {
        public string ResourceName
        {
            get;
            set;
        }

        public double IncrementValue
        {
            get;
            set;
        }

        public DateTime Time
        {
            get;
            set;
        }
    }
}

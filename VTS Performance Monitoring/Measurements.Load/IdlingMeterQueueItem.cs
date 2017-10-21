using System;

namespace Measurements.Load
{
    internal class IdlingMeterQueueItem<T>
    {
        public string ResourceName
        {
            get;
            set;
        }

        public IdlingMeterOperation Operation
        {
            get;
            set;
        }

        public T DifferenceValue
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

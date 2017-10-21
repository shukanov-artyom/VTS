using System;

namespace Measurements.Load
{
    internal class UsageItem<T>
    {
        public UsageItem(DateTime time, T value)
        {
            StartTime = time;
            Value = value;
        }

        public DateTime StartTime
        {
            get;
            private set;
        }

        public T Value
        {
            get;
            private set;
        }
    }
}

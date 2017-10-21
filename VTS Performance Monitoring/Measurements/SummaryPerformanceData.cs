using System;

namespace Measurements
{
    internal class SummaryPerformanceData
    {
        public TimeSpan SummarySpan
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string ParentName
        {
            get;
            set;
        }
    }
}

using System;

namespace Measurements
{
    internal class PerformanceQueueItem
    {
        public string Name
        {
            get; set;
        }

        public string ParentName
        {
            get; 
            set;
        }

        public DateTime Time 
        {
            get;
            set;
        }

        public QueueActionType Action
        {
            get; 
            set;
        }

        public int ThreadId
        {
            get;
            set;
        }
    }
}

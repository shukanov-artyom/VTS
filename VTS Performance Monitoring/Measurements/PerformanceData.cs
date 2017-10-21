using System;

namespace Measurements
{
    internal class PerformanceData
    {
        private readonly string name;
        private readonly string parentName;

        private DateTime startTime;
        private DateTime finishTime;
        private bool closed;
        private TimeSpan accumulatedTimeSpan;

        public PerformanceData(string name, string parentName)
        {
            this.name = name;
            this.parentName = parentName;
            startTime = DateTime.Now;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string ParentName
        {
            get
            {
                return parentName;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            private set
            {
                startTime = value;
            }
        }

        public DateTime FinishTime
        {
            get
            {
                return finishTime;
            }
        }

        public TimeSpan AccumulatedSpan
        {
            get
            {
                return accumulatedTimeSpan;
            }
        }

        private TimeSpan Span
        {
            get
            {
                TimeSpan sp = finishTime - startTime;
                return sp;
            }
        }

        public bool Closed
        {
            get
            {
                return closed;
            }
        }

        public void Close()
        {
            finishTime = DateTime.Now;
            accumulatedTimeSpan = accumulatedTimeSpan.Add(Span);
            closed = true;
        }

        public void Reopen()
        {
            StartTime = DateTime.Now;
            closed = false;
        }
    }
}

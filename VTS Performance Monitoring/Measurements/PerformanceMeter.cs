using System;

namespace Measurements
{
    public class PerformanceMeter : IDisposable
    {
        private readonly string name;
        private readonly string parentName;
        private readonly int threadId;

        public PerformanceMeter(string name, string parentName)
        {
            if (String.IsNullOrEmpty(parentName))
            {
                PerformanceMap.StartActivity(name);
            }
            else
            {
                PerformanceMap.StartSubActivity(name, parentName);
            }
            this.name = name;
            this.parentName = parentName;
        }

        public PerformanceMeter(string name, PerformanceMeter parent)
            : this(name, parent.Name)
        {
        }

        private string Name
        {
            get
            {
                return name;
            }
        }

        public void Dispose()
        {
            if (String.IsNullOrEmpty(parentName))
            {
                PerformanceMap.StopActivity(name);
            }
            else
            {
                PerformanceMap.StopSubActivity(name);
            }
        }
    }
}

using System;

namespace Performance
{
    public class PerformanceMeter : IDisposable
    {
        private readonly string name;

        public PerformanceMeter(string name, string parentName)
        {
            PerformanceDataAccumulator.Start(name, parentName);
            this.name = name;
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
            PerformanceDataAccumulator.Finish(name);
        }
    }
}

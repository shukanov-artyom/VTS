using System;

namespace Measurements.Load
{
    public class ResourceMeter : IDisposable
    {
        private readonly string name;

        public ResourceMeter(string name)
        {
            this.name = name;
            ResourceMap.IncrementResourceUsage(name);
        }

        public void Dispose()
        {
            ResourceMap.DecrementResourceUsage(name);
        }
    }
}

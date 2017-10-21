using System;
using System.Collections.Generic;
using System.Linq;

namespace Measurements
{
    internal class ExecutionMap
    {
        private readonly IDictionary<string, TimingData> map = 
            new Dictionary<string, TimingData>();

        private TimingData root;

        public IDictionary<string, TimingData> Map
        {
            get
            {
                return map;
            }
        }

        public void Start(string name, string parentCounterName, DateTime startTime)
        {
            if (Map.ContainsKey(name))
            {
                TimingData ctr = Map[name];
                if (!ctr.Closed)
                {
                    throw new NotSupportedException("Cannot reopen counter which is not yet closed.");
                }
                ctr.Reopen(startTime);
            }
            else
            {
                Map[name] = new TimingData(name, parentCounterName, startTime);
            }
        }

        public void Finish(string counterName, DateTime finishTime)
        {
            foreach (TimingData child in GetChildren(counterName))
            {
                if (!child.Closed)
                {
                    throw new NotSupportedException(String.Format("Cannot close parent {0} when child {1} is not closed.", counterName, child));
                }
            }
            Map[counterName].Close(finishTime);
        }

        private IEnumerable<TimingData> GetChildren(string parent)
        {
            return Map.Where(c => c.Value.ParentName.Equals(parent, StringComparison.Ordinal)).Select(c => c.Value);
        }
    }
}

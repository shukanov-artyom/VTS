using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Measurements.Load
{
    public static class ResourceMap
    {
        private static object sync = new object();
        private static int maxItemsPerResource = 100;
        private static AutoResetEvent pass = new AutoResetEvent(false);
        private static Queue<IdlingMeterQueueItem<int>> queue = 
            new Queue<IdlingMeterQueueItem<int>>();

        private static Thread worker;
        private static bool shutdown = false;
        private static Dictionary<string, List<UsageItem<int>>> usage = 
            new Dictionary<string, List<UsageItem<int>>>();
        private static readonly IDictionary<string, int> currentUsages = 
            new Dictionary<string, int>(); 

        static ResourceMap()
        {
            worker = new Thread(Process);
            worker.IsBackground = true;
            worker.Start();
        }

        public static IEnumerable<string> GetAllCounters()
        {
            return usage.Keys;
        }

        public static void IncrementResourceUsage(string resourceName)
        {
            IdlingMeterQueueItem<int> item = new IdlingMeterQueueItem<int>
                                             {
                                                 ResourceName = resourceName,
                                                 DifferenceValue = 1,
                                                 Operation = IdlingMeterOperation.Increase,
                                                 Time = DateTime.Now
                                             };
            lock (sync)
            {
                queue.Enqueue(item);
                pass.Set();
            }
        }

        public static void DecrementResourceUsage(string resourceName)
        {
            IdlingMeterQueueItem<int> item = new IdlingMeterQueueItem<int>
                {
                    ResourceName = resourceName,
                    DifferenceValue = 1,
                    Operation = IdlingMeterOperation.Decrease,
                    Time = DateTime.Now
                };
            lock (sync)
            {
                queue.Enqueue(item);
                pass.Set();
            }
        }

        private static void IncrementResourceUsagePrivate(string resourceName, int val, DateTime time)
        {
            if (!usage.ContainsKey(resourceName))
            {
                usage[resourceName] = new List<UsageItem<int>>();
            }
            if (!currentUsages.ContainsKey(resourceName))
            {
                currentUsages[resourceName] = 0;
            }
            currentUsages[resourceName] += val;
            usage[resourceName].Add(new UsageItem<int>(time, currentUsages[resourceName]));
            if (usage[resourceName].Count > maxItemsPerResource)
            {
                usage[resourceName].RemoveAt(0);
            }
        }

        private static void DecrementResourceUsagePrivate(string resourceName, int val, DateTime time)
        {
            if (!usage.ContainsKey(resourceName))
            {
                throw new NotSupportedException("Cannot decrement a resource usage before it has been incremented.");
            }
            if (!currentUsages.ContainsKey(resourceName))
            {
                throw new NotSupportedException("Cannot decrement a resource usage before it has been incremented.");
            }
            if (currentUsages[resourceName] < 0)
            {
                throw new NotSupportedException("Counter cannot be less than null.");
            }
            currentUsages[resourceName] -= val;
            usage[resourceName].Add(new UsageItem<int>(time, currentUsages[resourceName]));
        }

        private static void Process()
        {
            while (true)
            {
                if (shutdown)
                {
                    return;
                }
                IdlingMeterQueueItem<int> item = null;
                lock (sync)
                {
                    if (queue.Count > 0)
                    {
                        item = queue.Dequeue();
                    }
                }
                if (item != null)
                {
                    DispatchItem(item);
                }
                else
                {
                    pass.WaitOne();
                }
            }
        }

        private static void DispatchItem(IdlingMeterQueueItem<int> item)
        {
            switch (item.Operation)
            {
                case IdlingMeterOperation.Increase:
                    IncrementResourceUsagePrivate(item.ResourceName, item.DifferenceValue, item.Time);
                    break;
                case IdlingMeterOperation.Decrease:
                    DecrementResourceUsagePrivate(item.ResourceName, item.DifferenceValue, item.Time);
                    break;
                default:
                    throw new NotSupportedException("Incorrect queue operation.");
            }
        }

        public static double GetCurrentIdlePercentage(string resourceName)
        {
            if (!usage.ContainsKey(resourceName))
            {
                throw new NotSupportedException("Cannot get statistics for not initialized resource.");
            }
            List<UsageItem<int>> usag = usage[resourceName];
            lock (usag)
            {
                if (usag.Count == 0)
                {
                    return 0; // assume it's not idling yet as it's not working yet
                }
                if (DateTime.Now - usag.Last().StartTime > TimeSpan.FromSeconds(10))
                {
                    return double.NaN;
                }
                TimeSpan totalObservationSpan = usag[usag.Count - 1].StartTime - usag[0].StartTime;
                TimeSpan idleSpan = ExtractIdleSpan(resourceName);
                return idleSpan.Ticks * 100 / totalObservationSpan.Ticks;
            }
        }

        public static double GetOverloadPercentage(string resName, int moreThan)
        {
            List<UsageItem<int>> usag = usage[resName];
            lock (usag)
            {
                if (usag.Count == 0)
                {
                    return 0;
                }
                if (DateTime.Now - usag.Last().StartTime > TimeSpan.FromSeconds(10))
                {
                    return double.NaN;
                }
                TimeSpan totalObservationSpan = usag[usag.Count - 1].StartTime - usag[0].StartTime;
                TimeSpan overloadSpan = GetOverloadSpan(resName, moreThan);
                return overloadSpan.Ticks * 100 / totalObservationSpan.Ticks;
            }
        }

        public static double GetOverloadRate(string resourceName)
        {
            List<UsageItem<int>> usag;
            lock (usage)
            {
                usag = usage[resourceName];
            }
            lock (usag)
            {
                if (usag.Count == 0)
                {
                    return 0;
                }
                if (DateTime.Now - usag.Last().StartTime > TimeSpan.FromSeconds(10))
                {
                    return double.NaN;
                }
                TimeSpan totalObservationSpan = usag[usag.Count - 1].StartTime - usag[0].StartTime;
                double pr = 0;
                for (int i = 0; i < usag.Count - 1; i++)
                {
                    int itemUsage = usag[i].Value;
                    TimeSpan itemTimeSpan = usag[i + 1].StartTime - usag[i].StartTime;
                    pr += itemUsage * itemTimeSpan.TotalMilliseconds;
                }
                return Math.Round(pr/totalObservationSpan.TotalMilliseconds, 3);
            }
        }

        private static TimeSpan GetOverloadSpan(string resourceName, int moreThan)
        {
            TimeSpan result = TimeSpan.Zero;
            List<UsageItem<int>> usag;
            lock (usage)
            {
                usag = usage[resourceName];
            }
            for (int i = 0; i < usag.Count - 1; i++)
            {
                if (usag[i].Value > moreThan)
                {
                    result = result.Add(usag[i + 1].StartTime - usag[i].StartTime);
                }
            }
            return result;
        }

        private static TimeSpan ExtractIdleSpan(string resourceName)
        {
            TimeSpan result = TimeSpan.Zero;
            List<UsageItem<int>> usag;
            lock (usage)
            {
                usag = usage[resourceName];
            }
            lock (usag)
            {
                for (int i = 0; i < usag.Count - 1; i++)
                {
                    if (usag[i].Value == 0) // idling
                    {
                        result = result.Add(usag[i + 1].StartTime - usag[i].StartTime);
                    }
                }
                return result;
            }
        }
    }
}

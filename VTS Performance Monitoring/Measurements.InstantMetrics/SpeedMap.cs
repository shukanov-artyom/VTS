using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Measurements.InstantMetrics
{
    public static class SpeedMap
    {
        private static object sync = new object();
        private static AutoResetEvent pass = new AutoResetEvent(false);
        private static Queue<InstantMeterQueueItem> queue = 
            new Queue<InstantMeterQueueItem>();

        private static int maxHistoryNumber = 10;

        private static Thread worker;
        private static bool shutdown = false;
        private static IDictionary<string, List<SpeedInfo>> usage = 
            new Dictionary<string, List<SpeedInfo>>();

        static SpeedMap()
        {
            worker = new Thread(Process);
            worker.IsBackground = true;
            worker.Start();
        }

        public static IEnumerable<string> GetAllCounters()
        {
            return usage.Keys;
        }

        public static void IncrementCounter(string resourceName, double incrementValue)
        {
            InstantMeterQueueItem item = new InstantMeterQueueItem
                {
                    IncrementValue = incrementValue,
                    ResourceName = resourceName,
                    Time = DateTime.Now
                };
            lock (sync)
            {
                queue.Enqueue(item);
                pass.Set();
            }
        }

        private static void IncrementCounterPrivate(string resourceName, double val, DateTime time)
        {
            lock (sync)
            {
                if (!usage.ContainsKey(resourceName))
                {
                    usage[resourceName] = new List<SpeedInfo>();
                }
                List<SpeedInfo> usg = usage[resourceName];
                usg.Add(new SpeedInfo(time, val));
                if (usg.Count > maxHistoryNumber)
                {
                    usg.RemoveAt(0);
                }
            }
        }

        private static void Process()
        {
            while (true)
            {
                if (shutdown)
                {
                    return;
                }
                InstantMeterQueueItem item = null;
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

        private static void DispatchItem(InstantMeterQueueItem item)
        {
            IncrementCounterPrivate(item.ResourceName, item.IncrementValue, item.Time);
        }

        public static double GetCurrentSpeed(string resourceName)
        {
            List<SpeedInfo> subusage;
            lock (usage)
            {
                if (!usage.ContainsKey(resourceName))
                {
                    throw new NotSupportedException("Cannot get data for resource which has not been monitored.");
                }
                subusage = usage[resourceName];
            }
            lock (subusage)
            {
                if (DateTime.Now - subusage.Last().StartTime > TimeSpan.FromSeconds(10))
                {
                    return double.NaN; //expired
                }
                TimeSpan totalSpan = subusage[subusage.Count - 1].StartTime - subusage[0].StartTime;
                return Math.Round(subusage.Sum(s => s.IncrementValue) / totalSpan.TotalSeconds / 1024 /1024 , 1); //to MB/s
            }
        }
    }
}

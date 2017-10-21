using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace Measurements
{
    public static class PerformanceMap
    {
        private static readonly EventWaitHandle waitHandle = new AutoResetEvent(false);
        private static readonly Thread worker;
        private static Queue<PerformanceQueueItem> queue = new Queue<PerformanceQueueItem>();
        private static bool shutdown = false;

        private static readonly object sync = new object();

        private static readonly HashSet<string> activities = new HashSet<string>(); 

        private static readonly IDictionary<int, IDictionary<string, Activity>> map = 
            new Dictionary<int, IDictionary<string, Activity>>();

        private static readonly IDictionary<string, string> subActivityToActivityMap = 
            new Dictionary<string, string>();

        static PerformanceMap()
        {
            worker = new Thread(Process);
            worker.IsBackground = true;
            worker.Start();
        }

        public static void StartActivity(string activityName)
        {
            PerformanceQueueItem item = new PerformanceQueueItem
                {
                    Time = DateTime.Now,
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    Name = activityName,
                    ParentName = String.Empty,
                    Action = QueueActionType.StartActivity
                };
            lock (sync)
            {
                queue.Enqueue(item);
                waitHandle.Set();
            }
        }

        public static void StopActivity(string activityName)
        {
            PerformanceQueueItem item = new PerformanceQueueItem
                {
                    Time = DateTime.Now,
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    Name = activityName,
                    ParentName = String.Empty,
                    Action = QueueActionType.StopActivity
                };
            lock (sync)
            {
                queue.Enqueue(item);
                waitHandle.Set();
            }
        }

        public static void StartSubActivity(string subActivityName, string parentName)
        {
            PerformanceQueueItem item = new PerformanceQueueItem
                {
                    Time = DateTime.Now,
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    Name = subActivityName,
                    ParentName = parentName,
                    Action = QueueActionType.StartSubActivity
                };
            lock (sync)
            {
                queue.Enqueue(item);
                waitHandle.Set();
            }
        }

        public static void StopSubActivity(string subActivityName)
        {
            PerformanceQueueItem item = new PerformanceQueueItem
                {
                    Time = DateTime.Now,
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    Name = subActivityName,
                    ParentName = String.Empty,
                    Action = QueueActionType.StopSubActivity
                };
            lock (sync)
            {
                queue.Enqueue(item);
                waitHandle.Set();
            }
        }

        public static void Flush(XDocument doc)
        {
            lock (sync)
            {
                foreach (string activity in activities)
                {
                    IList<Activity> items = GetAllActivityItems(activity);
                    XElement itemElement = SummarizeHierarchy(items);
                    doc.Root.Add(itemElement);
                }
            }
        }

        private static XElement SummarizeHierarchy(IList<Activity> items)
        {
            SummaryActivity summary = SummaryActivity.Summarize(items);
            return SummaryActivity.ExportToXml(summary);
        }

        private static IList<Activity> GetAllActivityItems(string activity)
        {
            IList<Activity> result = new List<Activity>();
            foreach (KeyValuePair<int, IDictionary<string, Activity>> pair in map)
            {
                if (pair.Value.ContainsKey(activity))
                {
                    result.Add(pair.Value[activity]);
                }
            }
            return result;
        }

        private static void StartActivityPrivate(int threadId, string activityName, DateTime startTime)
        {
            if (map.ContainsKey(threadId))
            {
                if (map[threadId].ContainsKey(activityName))
                {
                    // this thread has already performed this activity
                    Activity act = map[threadId][activityName];
                    if (!act.Complete)
                    {
                        throw new NotSupportedException(String.Format("Cannot restart counter for activity {0} thread {1} as it's not stopped yet",
                            activityName, threadId));
                    }
                    act.Restart(startTime);
                }
                else
                {
                    // this thread performs this activity for the first time
                    if (!activities.Contains(activityName))
                    {
                        activities.Add(activityName);
                    }
                    var newActivity = new Activity(activityName, startTime);
                    map[threadId][activityName] = newActivity;
                }
            }
            else
            {
                // this thread is monitored for the first time
                var threadMap = new Dictionary<string, Activity>();
                threadMap[activityName] = new Activity(activityName, startTime);
                if (!activities.Contains(activityName))
                {
                    if (!activities.Contains(activityName))
                    {
                        activities.Add(activityName);
                    }
                }
                map[threadId] = threadMap;
            }
        }

        private static void StopActivityPrivate(int threadId, string activityName, DateTime finishTime)
        {
            if (!map.ContainsKey(threadId))
            {
                throw new NotSupportedException(String.Format("Cannot stop activity for thread {0} as this thread is not registered in performance map.", threadId));
            }
            if (!map[threadId].ContainsKey(activityName))
            {
                throw new NotSupportedException(String.Format("Cannot stop activity {0} for thread {1} as this activity is not present in performance map for this thread.", activityName, threadId));
            }
            map[threadId][activityName].Finish(finishTime);
        }

        private static void StartSubActivityPrivate(string subActivityName, string parentName, int threadId, DateTime startTime)
        {
            if (!map.ContainsKey(threadId))
            {
                throw new NotSupportedException(String.Format("Cannot register sub-activity «{0}» for thread «{1}» as thread «{1}» is not registered in performance map.", subActivityName, threadId));
            }
            if (!subActivityToActivityMap.ContainsKey(subActivityName))
            {
                if (activities.Any(a => a.Equals(parentName, StringComparison.Ordinal))) // if parent is activity, not sub-activity
                {
                    subActivityToActivityMap[subActivityName] = parentName;
                }
                else if (!subActivityToActivityMap.ContainsKey(parentName))
                {
                    throw new NotSupportedException(String.Format("Cannot register «{0}» sub-activity as its parent «{1}» is not registered yet.", subActivityName, parentName));
                }
                else
                {
                    subActivityToActivityMap[subActivityName] = subActivityToActivityMap[parentName];
                }
            }
            string activityName = subActivityToActivityMap[subActivityName];
            if (!map[threadId].ContainsKey(activityName))
            {
                throw new NotSupportedException(String.Format("When adding a sub-activity «{0}» for activity «{1}» activity «{1}» must be already registered in performance map.", subActivityName, activityName));
            }
            map[threadId][activityName].StartSub(subActivityName, parentName, startTime);
        }

        private static void StopSubActivityPrivate(string subActivityName, int threadId, DateTime finishTime)
        {
            if (!map.ContainsKey(threadId))
            {
                throw new NotSupportedException(String.Format("Cannot stop sub-activity «{0}» for thread «{1}» as this thread is not registered in performance map", subActivityName, threadId));
            }
            if (!subActivityToActivityMap.ContainsKey(subActivityName))
            {
                throw new NotSupportedException(String.Format("Sub-activity «{0}» cannot be found in sub-activity map. Looks like it has not been previously registered.", subActivityName));
            }
            string activityName = subActivityToActivityMap[subActivityName];
            map[threadId][activityName].FinishSub(subActivityName, finishTime);
        }

        private static void Process()
        {
            while (true)
            {
                if (shutdown)
                {
                    return;
                }
                PerformanceQueueItem item = null;
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
                    waitHandle.WaitOne();
                }
            }
        }

        private static void DispatchItem(PerformanceQueueItem item)
        {
            switch (item.Action)
            {
                case QueueActionType.StartActivity:
                    StartActivityPrivate(item.ThreadId, item.Name, item.Time);
                    break;
                case QueueActionType.StopActivity:
                    StopActivityPrivate(item.ThreadId, item.Name, item.Time);
                    break;
                case QueueActionType.StartSubActivity:
                    StartSubActivityPrivate(item.Name, item.ParentName, item.ThreadId, item.Time);
                    break;
                case QueueActionType.StopSubActivity:
                    StopSubActivityPrivate(item.Name, item.ThreadId, item.Time);
                    break;
                default:
                    throw new NotSupportedException("Queue action is not supported.");
            }
        }
    }
}

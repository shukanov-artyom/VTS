using System;
using System.Threading;
using Measurements;
using Measurements.InstantMetrics;
using Measurements.Load;
using Measurements.Output;

namespace LoadPrototype
{
    class Program
    {
        private const string resourceName = "resource";
        private static Thread th;

        static void Main(string[] args)
        {
            th = new Thread(Do);
            th.IsBackground = true;
            th.Start();
            Do();
            th.Join();
            double idling = ResourceMap.GetCurrentIdlePercentage(resourceName);
            double overload = ResourceMap.GetOverloadPercentage(resourceName, 2);
            double overloadRate = ResourceMap.GetOverloadRate(resourceName);
            double speed = SpeedMap.GetCurrentSpeed(resourceName);
        }

        private static void Do()
        {
            MetricsConsoleWriter.Start();
            for (int i = 0; i < 400; i++)
            {
                using (PerformanceMeter root = new PerformanceMeter("root", String.Empty))
                {
                    SpeedMap.IncrementCounter(resourceName, i);
                    Process(i, root);
                    using (new PerformanceMeter("Sleep", root))
                    {
                        Thread.Sleep(5);
                    }
                    if (i%3 == 0)
                    {
                        using (new PerformanceMeter("Process 2", root))
                        {
                            Process2();
                        }
                    }
                }
            }
        }

        private static void Process(int i, PerformanceMeter parent)
        {
            using (PerformanceMeter pmeter = new PerformanceMeter("Process", parent))
            using (ResourceMeter meter = new ResourceMeter(resourceName))
            {
                Thread.Sleep(20);
            }
        }

        private static void Process2()
        {
            Thread.Sleep(14);
        }
    }
}

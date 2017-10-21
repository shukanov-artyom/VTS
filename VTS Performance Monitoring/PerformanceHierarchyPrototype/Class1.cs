using System;
using System.Threading;
using Measurements;

namespace PerformanceHierarchyPrototype
{
    class Class1
    {
        public void Method1()
        {
            using (new PerformanceMeter("initial wait M1", "Call Method 1 in loop"))
            {
                Thread.Sleep(120);
            }
            using (new PerformanceMeter("Method21 loop", "Call Method 1 in loop"))
            {
                for (int i = 0; i < 30; i++)
                {
                    Class2 c2 = new Class2();
                    c2.Method21();
                }
            }
        }

        public void Method2()
        {
            using (new PerformanceMeter("Initial wait M2", "Call Method 2 in loop"))
            {
                Thread.Sleep(100);
            }
            using (new PerformanceMeter("Method22 loop", "Call Method 2 in loop"))
            {
                for (int i = 0; i < 30; i++)
                {
                    Class2 c2 = new Class2();
                    c2.Method22();
                }
            }
        }
    }
}

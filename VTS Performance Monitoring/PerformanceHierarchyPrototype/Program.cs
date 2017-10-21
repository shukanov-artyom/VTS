using System;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using Measurements;

namespace PerformanceHierarchyPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(ActivityMethod).Start();
            new Thread(ActivityMethod).Start();
            new Thread(ActivityMethod).Start();
            ActivityMethod();

            XDocument doc = new XDocument();
            doc.Add(new XElement("root"));
            PerformanceMap.Flush(doc);
            using (FileStream s = new FileStream(@"C:\tmp\out.xml", FileMode.CreateNew))
            {
                doc.Save(s);
            }
        }

        private static void ActivityMethod()
        {
            using (var rootPm = new PerformanceMeter("Root activity", String.Empty))
            {
                Class1 c11;
                using (new PerformanceMeter("create instance", rootPm))
                {
                    c11 = new Class1();
                }
                using (new PerformanceMeter("Call Method 1 in loop", rootPm))
                {
                    for (int i = 0; i < 30; i++)
                    {
                        c11.Method1();
                    }
                }
                using (new PerformanceMeter("Call Method 2 in loop", rootPm))
                {
                    for (int i = 0; i < 60; i++)
                    {
                        c11.Method2();
                    }
                }
                Thread.Sleep(200);
            }
        }
    }
}

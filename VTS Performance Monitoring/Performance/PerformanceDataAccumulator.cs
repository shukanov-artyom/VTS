using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Performance
{
    internal static class PerformanceDataAccumulator
    {
        private static readonly IDictionary<string, PerformanceData> Map = 
            new Dictionary<string, PerformanceData>();
        private static PerformanceData root;

        public static void Start(string name, string parentCounterName)
        {
            if (String.IsNullOrEmpty(parentCounterName)) // it's root
            {
                lock (Map)
                {
                    if (Map.Count > 0)
                    {
                        throw new NotSupportedException("Root element already set.");
                    }
                    root = new PerformanceData(name, parentCounterName);
                    Map[name] = root;
                }
            }
            else
            {
                if (Map.ContainsKey(name))
                {
                    PerformanceData ctr = Map[name];
                    if (!ctr.Closed)
                    {
                        throw new NotSupportedException("Cannot reopen counter which is not yet closed.");
                    }
                    ctr.Reopen();
                }
                else
                {
                    Map[name] = new PerformanceData(name, parentCounterName);
                }
            }
        }

        public static void Finish(string counterName)
        {
            Map[counterName].Close();
            if (counterName.Equals(root.Name, StringComparison.OrdinalIgnoreCase))
            {
                ExportAccumulatedData();
            }
        }

        private static void ExportAccumulatedData()
        {
            using (FileStream file = new FileStream(@"C:\tmp\out.xml", FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None))
            {
                XDocument doc = new XDocument();
                XElement rootElement = new XElement("root");
                rootElement.Add(GenerateElementForPerformanceCounter(root, null));
                doc.Add(rootElement);
                doc.Save(file);
            }
        }

        private static XElement GenerateElementForPerformanceCounter(
            PerformanceData counter,
            PerformanceData parent)
        {
            XElement result = new XElement("counter");
            result.Add(new XAttribute("name", counter.Name));
            double percentage;
            if (parent == null)
            {
                percentage = 100;
            }
            else
            {
                percentage = GetPercentage(parent.AccumulatedSpan, counter.AccumulatedSpan);
            }
            result.Add(new XAttribute("Percentage", percentage));
            result.Add(new XAttribute("TotalTime", counter.AccumulatedSpan));
            foreach (PerformanceData child in GetChildren(counter))
            {
                result.Add(GenerateElementForPerformanceCounter(child, counter));
            }
            return result;
        }

        private static IEnumerable<PerformanceData> GetChildren(PerformanceData parent)
        {
            return Map.Where(v => 
                    v.Value.ParentName.Equals(parent.Name, StringComparison.OrdinalIgnoreCase))
                    .Select(v => v.Value);
        }

        private static double GetPercentage(TimeSpan full, TimeSpan partial)
        {
            double notRounded = (((double)partial.Ticks) / ((double)full.Ticks)) *100;
            return Math.Round(notRounded, 2);
        }
    }
}

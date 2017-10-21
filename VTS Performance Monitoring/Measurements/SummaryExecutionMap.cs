using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Measurements
{
    internal class SummaryExecutionMap
    {
        private readonly IDictionary<string, SummaryPerformanceData> sumMap =
            new Dictionary<string, SummaryPerformanceData>();

        public IDictionary<string, SummaryPerformanceData> SumMap
        {
            get
            {
                return sumMap;
            }
        }

        public static SummaryExecutionMap Summarize(IEnumerable<ExecutionMap> maps)
        {
            SummaryExecutionMap result = new SummaryExecutionMap();
            HashSet<string> perfDatas = new HashSet<string>();
            foreach (ExecutionMap map in maps)
            {
                foreach (KeyValuePair<string, TimingData> pair in map.Map)
                {
                    if (!perfDatas.Contains(pair.Key))
                    {
                        perfDatas.Add(pair.Key);
                    }
                }
            }
            foreach (string perfData in perfDatas)
            {
                TimeSpan summaryTime = new TimeSpan();
                string parentName = String.Empty;
                foreach (ExecutionMap executionMap in maps)
                {
                    if (executionMap.Map.ContainsKey(perfData))
                    {
                        TimingData pData = executionMap.Map[perfData];
                        summaryTime += pData.AccumulatedSpan;
                        parentName = pData.ParentName;
                    }
                }
                result.SumMap[perfData] = new SummaryPerformanceData()
                    {
                        SummarySpan = summaryTime,
                        Name = perfData,
                        ParentName = parentName
                    };
            }
            return result;
        }

        public XElement ExportToXml()
        {
            SummaryPerformanceData root = SumMap.First(m => String.IsNullOrEmpty(m.Value.ParentName)).Value;
            XElement element = GenerateElementForPerformanceCounter(root, null);
            return element;
        }

        private XElement GenerateElementForPerformanceCounter(
            SummaryPerformanceData counter,
            SummaryPerformanceData parent)
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
                percentage = GetPercentage(parent.SummarySpan, counter.SummarySpan);
            }
            result.Add(new XAttribute("Percentage", percentage));
            result.Add(new XAttribute("TotalTime", counter.SummarySpan));
            foreach (SummaryPerformanceData child in GetChildren(counter))
            {
                result.Add(GenerateElementForPerformanceCounter(child, counter));
            }
            return result;
        }

        private IEnumerable<SummaryPerformanceData> GetChildren(SummaryPerformanceData parent)
        {
            return SumMap.Where(v =>
                    v.Value.ParentName.Equals(parent.Name, StringComparison.OrdinalIgnoreCase))
                    .Select(v => v.Value);
        }

        private double GetPercentage(TimeSpan full, TimeSpan partial)
        {
            double notRounded = (((double)partial.Ticks) / ((double)full.Ticks)) * 100;
            return Math.Round(notRounded, 2);
        }
    }
}

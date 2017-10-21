using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Measurements
{
    internal class SummaryActivity 
    {
        private SummaryExecutionMap summaryExecutionMap;

        public SummaryActivity(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        public SummaryExecutionMap SummaryMap
        {
            get
            {
                return summaryExecutionMap;
            }
            set
            {
                summaryExecutionMap = value;
            }
        }

        public static SummaryActivity Summarize(IEnumerable<Activity> activities)
        {
            string name = String.Empty;
            IList<ExecutionMap> maps = new List<ExecutionMap>();
            foreach (Activity activity in activities)
            {
                if (String.IsNullOrEmpty(name))
                {
                    name = activity.Name;
                }
                else
                {
                    if (!name.Equals(activity.Name, StringComparison.Ordinal))
                    {
                        throw new NotSupportedException("All activities should have identical names at this point.");
                    }
                }
                foreach (ExecutionMap executionMap in activity.Maps)
                {
                    maps.Add(executionMap);
                }
            }
            return new SummaryActivity(name)
                   {
                       SummaryMap = SummarizeExecutionMaps(maps)
                   };
        }

        private static SummaryExecutionMap SummarizeExecutionMaps(IEnumerable<ExecutionMap> maps)
        {
            return SummaryExecutionMap.Summarize(maps);
        }

        public static XElement ExportToXml(SummaryActivity summaryActivity)
        {
            return summaryActivity.SummaryMap.ExportToXml();
        }
    }
}

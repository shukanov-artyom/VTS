using System;
using System.Collections.Generic;
using System.Linq;

namespace VTSWeb.AnalysisCore.Statistics
{
    public class AnalyticStatistics
    {
        private readonly IList<AnalyticStatisticsItem> items = 
            new List<AnalyticStatisticsItem>();

        public IList<AnalyticStatisticsItem> Items
        {
            get
            {
                return items;
            }
        }

        public void Assimilate(IList<AnalyticStatisticsItem> newcomers)
        {
            foreach (AnalyticStatisticsItem newcomer in newcomers)
            {
                AnalyticStatisticsItem oldie = 
                    Items.FirstOrDefault(i => i.SameAs(newcomer));
                if (oldie == null)
                {
                    Items.Add(newcomer);
                }
                else
                {
                    oldie.Assimilate(newcomer);
                }
            }
        }
    }
}

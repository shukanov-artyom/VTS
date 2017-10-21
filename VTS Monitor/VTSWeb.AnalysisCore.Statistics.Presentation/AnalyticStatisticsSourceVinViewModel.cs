using System;
using System.Collections.Generic;
using System.Globalization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.AnalysisCore.Statistics.Presentation
{
    public class AnalyticStatisticsSourceVinViewModel : ViewModelBase
    {
        private string vin;
        private List<long> datasetIds = new List<long>();

        public AnalyticStatisticsSourceVinViewModel(
            string vin, long datasetId)
        {
            this.vin = vin;
            datasetIds.Add(datasetId);
        }

        public string Vin
        {
            get
            {
                return vin;
            }
        }

        public string ValuesCount
        {
            get
            {
                return datasetIds.Count.ToString(
                    CultureInfo.InvariantCulture);
            }
        }

        public string DatasetIds
        {
            get
            {
                string result = String.Empty;
                foreach (long id in datasetIds)
                {
                    if (String.IsNullOrEmpty(result))
                    {
                        result = id.ToString(
                            CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        result = String.Format("{0},{1}", result, id);
                    }
                }
                return result;
            }
        }

        public void AddId(long id)
        {
            datasetIds.Add(id);
        }
    }
}

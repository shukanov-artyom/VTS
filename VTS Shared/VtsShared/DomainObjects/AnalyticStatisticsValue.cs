using System;

namespace VTS.Shared.DomainObjects
{
    public class AnalyticStatisticsValue : DomainObject
    {
        private readonly double value;
        private readonly string vin;
        private readonly long sourcePsaParametersSetId;
        private readonly DateTime sourceDataCaptureDateTime;

        public AnalyticStatisticsValue(
            double value,
            string vin,
            long sourcePsaParametersSetId,
            DateTime sourceDataCaptureDateTime)
        {
            if (double.IsNaN(value))
            {
                throw new ArgumentNullException("value");
            }
            if (String.IsNullOrEmpty(vin))
            {
                throw new ArgumentNullException("vin");
            }
            if (sourcePsaParametersSetId == 0)
            {
                throw new ArgumentException("sourcePsaParametersSetId");
            }
            this.value = value;
            this.vin = vin;
            this.sourcePsaParametersSetId = sourcePsaParametersSetId;
            this.sourceDataCaptureDateTime = sourceDataCaptureDateTime;
        }

        public long AnalyticStatisticsItemId
        {
            get;
            set;
        }

        public DateTime SourceDataCaptureDateTime
        {
            get
            {
                return sourceDataCaptureDateTime;
            }
        }

        public double Value
        {
            get
            {
                return value;
            }
        }

        public string SourceVin
        {
            get
            {
                return vin;
            }
        }

        public long SourcePsaParametersSetId
        {
            get
            {
                return sourcePsaParametersSetId;
            }
        }
    }
}

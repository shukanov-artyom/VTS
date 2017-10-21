using System;
using VTS.Shared.DomainObjects;


namespace VTSWebService.AnalysisCore.Statistics
{
    public interface IFitter
    {
        /// <summary>
        /// decides whether this set has all required data to Get()
        /// </summary>
        bool Fits(PsaParametersSet set);

        /// <summary>
        /// Generates AnalyticStatisticsItem
        /// </summary>
        AnalyticStatisticsItem Get(
            PsaParametersSet set, DateTime sourceDataCaptureDateTime);
    }
}

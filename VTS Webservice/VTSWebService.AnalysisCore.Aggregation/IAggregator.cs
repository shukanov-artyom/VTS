using System;
using VTS.AnalysisCore.Common;

namespace VTSWebService.AnalysisCore.Aggregation
{
    public interface IAggregator
    {
        AnalyticRuleSettings Aggregate();
    }
}

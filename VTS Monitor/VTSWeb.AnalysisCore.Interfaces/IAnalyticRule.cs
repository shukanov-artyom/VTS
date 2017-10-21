using System;
using VTS.Shared;


namespace VTSWeb.AnalysisCore.Interfaces
{
    public interface IAnalyticRule : IAnalyticItem
    {
        AnalyticRuleType Type
        {
            get;
        }
    }
}

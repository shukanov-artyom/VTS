using System;
using System.Collections.Generic;

namespace VTSWeb.AnalysisCore.Interfaces
{
    public interface IAnalyticModel : IAnalyticItem
    {
        IList<IAnalyticModel> Models
        {
            get;
        }

        IList<IAnalyticRule> Rules
        {
            get;
        }
    }
}

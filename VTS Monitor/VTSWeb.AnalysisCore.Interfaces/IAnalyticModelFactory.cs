using System;

namespace VTSWeb.AnalysisCore.Interfaces
{
    public interface IAnalyticModelFactory
    {
        bool Finished
        {
            get;
        }

        IAnalyticModel Result
        {
            get;
        }

        bool HasError
        {
            get;
        }

        Exception Error
        {
            get;
        }

        void CreateAsync();
    }
}

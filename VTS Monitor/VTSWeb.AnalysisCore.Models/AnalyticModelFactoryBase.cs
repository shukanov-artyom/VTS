using System;
using VTSWeb.AnalysisCore.Interfaces;

namespace VTSWeb.AnalysisCore.Models
{
    public abstract class AnalyticModelFactoryBase : IAnalyticModelFactory
    {
        public delegate void
            AnalyticModelFactoryFinishedCallback(IAnalyticModelFactory myself);

        protected AnalyticModelFactoryBase(
            AnalyticModelFactoryFinishedCallback callback)
        {
            Callback = callback;
        }

        protected AnalyticModelFactoryFinishedCallback Callback 
        {
            get;
            private set;
        }

        public bool Finished
        {
            get;
            protected set;
        }

        public IAnalyticModel Result
        {
            get; 
            protected set;
        }

        public bool HasError 
        {
            get; 
            protected set;
        }

        public Exception Error
        {
            get;
            protected set;
        }

        public abstract void CreateAsync();
    }
}

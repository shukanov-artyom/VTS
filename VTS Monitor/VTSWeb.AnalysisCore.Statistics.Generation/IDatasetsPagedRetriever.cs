using System;
using VTSWeb.AnalysisCore.Statistics.Generation.PagedRetrievers;

namespace VTSWeb.AnalysisCore.Statistics.Generation
{
    public interface IDatasetsPagedRetriever
    {
        void GetNextPage();

        event RetrieverGotPageEventHandler GotNextPage;

        event PagedRetrieverErrorEventHandler Error;

        int PercentComplete
        {
            get;
        }
    }
}

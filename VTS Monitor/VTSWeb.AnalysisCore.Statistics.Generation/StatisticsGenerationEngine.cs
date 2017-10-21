using System;
using System.Collections.Generic;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Statistics.Generation.PagedRetrievers;
using VTSWeb.Common;
using VTSWeb.DomainObjects.Psa;

namespace VTSWeb.AnalysisCore.Statistics.Generation
{
    public class StatisticsGenerationEngine
    {
        public delegate void StatisticsGenerationUpdate(int percentComplete);

        private readonly ErrorCallbackDelegate errorCallback;
        private readonly SuccessCallbackDelegate completeCallback;
        private readonly StatisticsGenerationUpdate updateCallback;
        private readonly IDatasetsPagedRetriever datasetsPagedRetriever;
        private readonly AnalyticStatistics result = new AnalyticStatistics();

        private StatisticsGenerationDatasetCollectionProcessor processor;

        public StatisticsGenerationEngine(
            StatisticsGenerationUpdate updateCallback,
            SuccessCallbackDelegate completeCallback,
            ErrorCallbackDelegate errorCallback,
            IDatasetsPagedRetriever datasetsPagedRetriever)
        {
            this.errorCallback = errorCallback;
            this.completeCallback = completeCallback;
            this.updateCallback = updateCallback;
            this.datasetsPagedRetriever = datasetsPagedRetriever;
            this.datasetsPagedRetriever.Error += OnPagedRetrieverError;
            this.datasetsPagedRetriever.GotNextPage += OnRetrieverGotNextPage;
        }

        public AnalyticStatistics Result
        {
            get
            {
                return result;
            }
        }

        private void OnPagedRetrieverError(
            object sender, PagedRetrieverErrorEventArgs e)
        {
            errorCallback.Invoke(e.Error, e.Msg);
        }

        private void OnRetrieverGotNextPage(object sender, RetrieverGotPageEventArgs e)
        {
            if (e.Page.Count == 0) // no more
            {
                completeCallback.Invoke();
                return;
            }
            PickFromDatasets(e.Page);
        }

        public void StartGeneration()
        {
            datasetsPagedRetriever.GetNextPage();
        }

        private void PickFromDatasets(IList<PsaDataset> datasets)
        {
            processor = new StatisticsGenerationDatasetCollectionProcessor(datasets, 
                    ProcessorComplete, ErrorCallback);
            processor.Process();
        }

        private void ProcessorComplete()
        {
            result.Assimilate(processor.Result);
            updateCallback.Invoke(datasetsPagedRetriever.PercentComplete);
            datasetsPagedRetriever.GetNextPage();
        }

        private void ErrorCallback(Exception e, string msg)
        {
            errorCallback.Invoke(e, msg);
        }
    }
}

using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.AnalysisCore.Statistics.Generation.PagedRetrievers
{
    public class AllDatasetsPagedRetriever : DatasetsPagedRetrieverBase
    {
        public AllDatasetsPagedRetriever()
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.GetDatasetsCountCompleted += GotDatasetsCount;
            service.GetDatasetsCountAsync();
        }

        private void GotDatasetsCount(object sender, GetDatasetsCountCompletedEventArgs ea)
        {
            if (ea.Error != null)
            {
                ErrorCallback(ea.Error, ea.Error.Message);
            }
            else
            {
                GotCount(ea.Result);
            }
        }

        public override void GetNextPage()
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.GetNextPageCompleted += ServiceOnGetNextPageCompleted;
            service.GetNextPageAsync(LastId, PageSize);
        }

        private void ServiceOnGetNextPageCompleted(object sender, GetNextPageCompletedEventArgs ea)
        {
            if (ea.Error != null)
            {
                ErrorCallback(ea.Error, ea.Error.Message);
            }
            else
            {
                List<PsaDataset> result = new List<PsaDataset>();
                foreach (PsaDatasetDto datasetDto in ea.Result)
                {
                    result.Add(PsaDatasetAssembler.FromDtoToDomainObject(datasetDto));
                }
                PageRetrievedCallback(result);
            }
        }
    }
}

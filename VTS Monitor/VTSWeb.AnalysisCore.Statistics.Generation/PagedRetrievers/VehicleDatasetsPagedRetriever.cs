using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.AnalysisCore.Statistics.Generation.PagedRetrievers
{
    public class VehicleDatasetsPagedRetriever : DatasetsPagedRetrieverBase
    {
        private long vehicleId;

        public VehicleDatasetsPagedRetriever(long vehicleId)
        {
            this.vehicleId = vehicleId;
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.GetDatasetsCountForVehicleCompleted += OnGotCount;
        }

        private void OnGotCount(object sender, GetDatasetsCountForVehicleCompletedEventArgs ea)
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
            service.GetNextPageForVehicleCompleted += ServiceOnGetNextPageForVehicleCompleted;
            service.GetNextPageForVehicleAsync(LastId, PageSize, vehicleId);
        }

        private void ServiceOnGetNextPageForVehicleCompleted(object sender, 
            GetNextPageForVehicleCompletedEventArgs ea)
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

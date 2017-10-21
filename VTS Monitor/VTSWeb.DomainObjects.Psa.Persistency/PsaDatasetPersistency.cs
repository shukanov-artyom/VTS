using System;
using System.Collections.Generic;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.DomainObjects.Psa.Persistency
{
    public class PsaDatasetPersistency
    {
        public delegate void DatasetsRetrievedDelegate(IList<PsaDataset> result);

        private DatasetsRetrievedDelegate callbackSuccess;
        private ErrorCallbackDelegate callbackError;
        private readonly VtsWebServiceClient service = new VtsWebServiceClient();

        public PsaDatasetPersistency(DatasetsRetrievedDelegate callbackSuccess,
            ErrorCallbackDelegate callbackError)
        {
            this.callbackSuccess = callbackSuccess;
            this.callbackError = callbackError;
        }

        ~PsaDatasetPersistency()
        {
            service.CloseAsync();
        }

        public void GetAllForVehicle(string vin)
        {
            service.GetDatasetsForVehicleCompleted += GotDatasets;
            service.GetDatasetsForVehicleAsync(vin);
        }

        private void GotDatasets(object s, GetDatasetsForVehicleCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                callbackError.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                List<PsaDataset> result = new List<PsaDataset>();
                foreach (PsaDatasetDto dto in e.Result)
                {
                    result.Add(PsaDatasetAssembler.FromDtoToDomainObject(dto));
                }
                callbackSuccess.Invoke(result);
            }
        }
    }
}

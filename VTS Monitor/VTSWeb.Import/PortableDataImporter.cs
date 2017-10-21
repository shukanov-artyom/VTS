using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.Common;
using VTSWeb.DomainObjects.Psa.Extensions;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.Import
{
    public class PortableDataImporter
    {
        public delegate void UnsupportedVinsFoundEventHandler(List<string> unsupportedVins);
        public event UnsupportedVinsFoundEventHandler UnsupportedVinsFound;

        private readonly SuccessCallbackDelegate successCallback;
        private readonly ErrorCallbackDelegate errorCallback;
        private PortableData data;
        private List<string> supportedVins;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PortableDataImporter(SuccessCallbackDelegate successCallback,
            ErrorCallbackDelegate errorCallback)
        {
            this.errorCallback = errorCallback;
            this.successCallback = successCallback;
        }

        /// <summary>
        /// Checks data for consistency, excludes duplicates and persists to database.
        /// Ignores the fact that we are importing data for someboy else's vehicle. Imports anyway. I decided so.
        /// </summary>
        public void CheckAndImport(PortableData data)
        {
            this.data = data;
            CheckForUnsupportedVehicles();
        }

        /// <summary>
        /// Step1 - check for unsupported vehicles.
        /// </summary>
        private void CheckForUnsupportedVehicles()
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.CheckVinsReturnUnsupportedCompleted += OnVinCheckComplete;
            ObservableCollection<string> arg = new ObservableCollection<string>(
                data.PsaTraces.GetVins().ToList());
            service.CheckVinsReturnUnsupportedAsync(arg);
        }

        /// <summary>
        /// Step 1 completed.
        /// </summary>
        private void OnVinCheckComplete(object s, 
            CheckVinsReturnUnsupportedCompletedEventArgs e)
        {
            VtsWebServiceClient cl = s as VtsWebServiceClient;
            cl.CheckVinsReturnUnsupportedCompleted -= OnVinCheckComplete;
            if (e.Error != null)
            {
                OnError(e.Error, e.Error.Message);
            }
            else
            {
                if (UnsupportedVinsFound != null && e.Result.Count > 0)
                {
                    UnsupportedVinsFound.Invoke(e.Result.ToList());
                }
                supportedVins = ExtractSupportedVins(data.PsaTraces.GetVins().ToList(), e.Result.ToList());
                CheckSupportedForUnregisteredVehiclesAndRegisterIfAny(supportedVins);
            }
            cl.CloseAsync();
        }

        private List<string> ExtractSupportedVins(List<string> allVins, List<string> unsupportedVins)
        {
            List<string> result = new List<string>(allVins);
            foreach (string vin in unsupportedVins)
            {
                result.Remove(vin);
            }
            return result;
        }

        private void CheckSupportedForUnregisteredVehiclesAndRegisterIfAny(
            List<string> supportedVins)
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.CheckVinsReturnUnregisteredCompleted +=
                OnUnregisteredCheckComplete;
            service.CheckVinsReturnUnregisteredAsync(
                new ObservableCollection<string>(supportedVins));
        }

        private void OnUnregisteredCheckComplete(object s,
            CheckVinsReturnUnregisteredCompletedEventArgs e)
        {
            VtsWebServiceClient cl = s as VtsWebServiceClient;
            cl.CheckVinsReturnUnregisteredCompleted -=
                OnUnregisteredCheckComplete;
            if (e.Error != null)
            {
                OnError(e.Error, e.Error.Message);
            }
            else
            {
                VtsWebServiceClient service = new VtsWebServiceClient();
                service.RegisterVehiclesCompleted += OnVehiclesRegistered;
                service.RegisterVehiclesAsync(e.Result);
            }
            cl.CloseAsync();
        }

        private void OnVehiclesRegistered(object s, AsyncCompletedEventArgs e)
        {
            VtsWebServiceClient cl = s as VtsWebServiceClient;
            cl.RegisterVehiclesCompleted -= OnVehiclesRegistered;
            if (e.Error != null)
            {
                OnError(e.Error, e.Error.Message);
            }
            else
            {
                List<PsaDataset> datasets = FormDatasets(data);
                List<PsaDatasetDto> dtos = new List<PsaDatasetDto>();
                foreach (PsaDataset obj in datasets)
                {
                    PsaDatasetDto dto = PsaDatasetAssembler.FromDomainObjectToDto(obj);
                    dtos.Add(dto);
                }
                UploadDataToService(dtos);
            }
            cl.CloseAsync();
        }

        private void UploadDataToService(List<PsaDatasetDto> dtos)
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.UploadDatasetsCompleted += ServiceOnUploadDatasetsCompleted;
            service.UploadDatasetsAsync(new ObservableCollection<PsaDatasetDto>(dtos));
        }

        private void ServiceOnUploadDatasetsCompleted(object s, 
            AsyncCompletedEventArgs e)
        {
            VtsWebServiceClient cl = s as VtsWebServiceClient;
            cl.UploadDatasetsCompleted -= OnDatasetsUploaded;
            VtsWebServiceClient service = new VtsWebServiceClient();
            if (e.Error != null)
            {
                OnError(e.Error, e.Error.Message);
            }
            else
            {
                service.AssociateVehiclesWithUserCompleted += OnVehiclesAssociated;
                service.AssociateVehiclesWithUserAsync(
                    new ObservableCollection<string>(supportedVins),
                    LoggedUserContext.LoggedUser.Login,
                    LoggedUserContext.LoggedUser.PasswordHash);
            }
            cl.CloseAsync();
        }

        private void OnDatasetsUploaded(object s, AsyncCompletedEventArgs e)
        {
            VtsWebServiceClient cl = s as VtsWebServiceClient;
            cl.UploadDatasetsCompleted -= OnDatasetsUploaded;
            VtsWebServiceClient service = new VtsWebServiceClient();
            if (e.Error != null)
            {
                OnError(e.Error, e.Error.Message);
            }
            else
            {
                service.AssociateVehiclesWithUserCompleted += OnVehiclesAssociated;
                service.AssociateVehiclesWithUserAsync(
                    new ObservableCollection<string>(supportedVins), 
                    LoggedUserContext.LoggedUser.Login,
                    LoggedUserContext.LoggedUser.PasswordHash);
            }
            cl.CloseAsync();
        }

        private void OnVehiclesAssociated(object s, AsyncCompletedEventArgs e)
        {
            VtsWebServiceClient cl = s as VtsWebServiceClient;
            cl.AssociateVehiclesWithUserCompleted -= OnVehiclesAssociated;
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                successCallback.Invoke();
            }
            cl.CloseAsync();
        }

        private List<PsaDataset> FormDatasets(PortableData data)
        {
            IDictionary<string, List<PsaTrace>> sortedTraces = 
                new Dictionary<string, List<PsaTrace>>();
            foreach (PsaTrace psaTrace in data.PsaTraces)
            {
                string vin = psaTrace.Vin;
                if (sortedTraces.ContainsKey(vin))
                {
                    sortedTraces[vin].Add(psaTrace);
                }
                else
                {
                    sortedTraces[vin] = new List<PsaTrace>() {psaTrace};
                }
            }
            List<PsaDataset> result = new List<PsaDataset>();
            foreach (KeyValuePair<string, List<PsaTrace>> pair in sortedTraces)
            {
                PsaDataset dataset = new PsaDataset();
                dataset.ExportedDate = data.Date;
                dataset.Guid = data.Guid;
                dataset.VehicleId = 0; // Service will fill it 
                dataset.SavedDate = DateTime.Now;
                foreach (PsaTrace trace in pair.Value)
                {
                    dataset.Traces.Add(trace);
                }
                result.Add(dataset);
            }
            return result.Where(r => supportedVins.Contains(r.GetVin())).ToList();
        }

        private void OnError(Exception e, string msg)
        {
            errorCallback.Invoke(e, msg);
        }
    }
}

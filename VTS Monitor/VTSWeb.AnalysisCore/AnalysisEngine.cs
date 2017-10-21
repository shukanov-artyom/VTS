using System;
using System.Collections.Generic;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Interfaces;
using VTSWeb.AnalysisCore.Models;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.DomainObjects.Psa.Persistency;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.AnalysisCore
{
    public class AnalysisEngine
    {
        private readonly Vehicle vehicle;
        private readonly ErrorCallbackDelegate errorCallback;
        private readonly EngineFinishedCallback finishedCallback;
        private IVehicleAnalyticModel vehicleAnalyticModel;

        public delegate void EngineFinishedCallback(
            IVehicleAnalyticModel result);

        public AnalysisEngine(Vehicle vehicle,
            EngineFinishedCallback finishedCallback,
            ErrorCallbackDelegate errorCallback)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException("vehicle");
            }
            this.vehicle = vehicle;
            this.finishedCallback = finishedCallback;
            this.errorCallback = errorCallback;
        }

        /// <summary>
        /// Analysis core entry point.
        /// Starts async data processing.
        /// </summary>
        public void Start()
        {
            VtsWebServiceClient client = new VtsWebServiceClient();
            client.GetVehicleInformationByVinCompleted += VehicleInformationRetrieved;
            client.GetVehicleInformationByVinAsync(vehicle.Vin);
        }

        private void VehicleInformationRetrieved(object s, 
            GetVehicleInformationByVinCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                errorCallback.Invoke(e.Error, e.Error.Message);
            }
            else
            {
                VehicleInformation info = VehicleInformationAssembler.FromDtoToDomainObject(e.Result);
                VehicleAnalyticModelFactory factory =
                    new VehicleAnalyticModelFactory(vehicle, info,
                    AnalyticModelCreatedCallback, ErrorCallback);
                factory.CreateAsync();
            }
        }

        private void AnalyticModelCreatedCallback(IVehicleAnalyticModel result)
        {
            vehicleAnalyticModel = result;
            PsaDatasetPersistency retriever =
                new PsaDatasetPersistency(DatasetsRetrieved, ErrorCallback);
            retriever.GetAllForVehicle(vehicle.Vin);
        }

        private void DatasetsRetrieved(IList<PsaDataset> datasets)
        {
            foreach (PsaDataset dataset in datasets)
            {
                vehicleAnalyticModel.Pick(dataset);
            }
            finishedCallback.Invoke(vehicleAnalyticModel);
        }

        private void ErrorCallback(Exception e, string msg)
        {
            errorCallback.Invoke(e, msg);
        }
    }
}

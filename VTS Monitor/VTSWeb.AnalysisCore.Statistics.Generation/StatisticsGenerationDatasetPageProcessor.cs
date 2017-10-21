using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.AnalysisCore.Recognition;
using VTSWeb.Common;
using VTSWeb.DomainObjects.Psa;
using VTSWeb.VTSWebService.Assemblers;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.AnalysisCore.Statistics.Generation
{
    internal class StatisticsGenerationDatasetCollectionProcessor
    {
        private IList<PsaDataset> source;
        private SuccessCallbackDelegate successCallback;
        private ErrorCallbackDelegate errorCallback;
        private IList<AnalyticStatisticsItem> result = 
            new List<AnalyticStatisticsItem>();

        public StatisticsGenerationDatasetCollectionProcessor(
            IList<PsaDataset> source, 
            SuccessCallbackDelegate successCallback,
            ErrorCallbackDelegate errorCallback)
        {
            this.errorCallback = errorCallback;
            this.successCallback = successCallback;
            this.source = source;
        }

        public IList<AnalyticStatisticsItem> Result
        {
            get
            {
                return result;
            }
        }

        public void Process()
        {
            List<long> vehicleIds = new List<long>();
            foreach (PsaDataset dataset in source)
            {
                if (!vehicleIds.Contains(dataset.VehicleId))
                {
                    vehicleIds.Add(dataset.VehicleId);
                }
            }
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.GetVehiclesInformationCompleted += ServiceOnGetVehiclesInformationCompleted;
            service.GetVehiclesInformationAsync(new ObservableCollection<long>(vehicleIds));
        }

        private void ServiceOnGetVehiclesInformationCompleted(object sender, 
            GetVehiclesInformationCompletedEventArgs ea)
        {
            if (ea.Error != null)
            {
                OnError(ea.Error, ea.Error.Message);
                return;
            }
            List<VehicleInformation> rlt = new List<VehicleInformation>();
            foreach (VehicleInformationDto dto in ea.Result)
            {
                rlt.Add(VehicleInformationAssembler.FromDtoToDomainObject(dto));
            }
            VehicleInformationSetRetrieved(rlt);
        }

        private void VehicleInformationSetRetrieved(
            IList<VehicleInformation> infos)
        {
            foreach (PsaDataset dataset in source)
            {
                ProcessDataset(dataset, infos.FirstOrDefault(c => c.Vin == dataset.GetVin()));
            }
            successCallback.Invoke();
        }

        private void OnError(Exception e, string msg)
        {
            errorCallback.Invoke(e, msg);
        }

        private void ProcessDataset(PsaDataset dataset, 
            VehicleInformation info)
        {
            foreach (PsaTrace trace in dataset.Traces)
            {
                ProcessTrace(trace, info);
            }
        }

        private void ProcessTrace(PsaTrace trace, 
            VehicleInformation info)
        {
            StatisticsGenerationConveyor conveyor = 
                new StatisticsGenerationConveyor(trace, info);
            foreach (AnalyticStatisticsItem item in conveyor.RollAlong())
            {
                result.Add(item);
            }
        }
    }
}

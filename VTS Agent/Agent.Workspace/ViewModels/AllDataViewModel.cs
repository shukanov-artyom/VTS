using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Agent.Common.Instance;
using Agent.Common.Presentation.Error;
using Agent.Common.Presentation.Vehicles;
using Agent.Network.Monitor;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.ViewModels
{
    public class AllDataViewModel : VehicleSelectionViewModel, IDisposable
    {
        private readonly ObservableCollection<PsaDatasetViewModel> datasets = 
             new ObservableCollection<PsaDatasetViewModel>();
        private readonly BackgroundWorker datasetsRetriever = new BackgroundWorker();

        public AllDataViewModel()
            : base()
        {
            datasetsRetriever.WorkerSupportsCancellation = true;
            datasetsRetriever.DoWork += RetrieveDatasetsAsync;
            datasetsRetriever.RunWorkerCompleted += OnDatasetsRetrieved;
            LoggedUserContext.UserLoggedOn += OnUserLoggedOn;
            LoggedUserContext.UserLoggedOff += OnUserLoggedOff;
        }

        public ObservableCollection<PsaDatasetViewModel> Datasets
        {
            get
            {
                return datasets;
            }
        }

        public void UpdateDatasetsForVehicle(VehicleViewModel vm)
        {
            if (vm == null)
            {
                Datasets.Clear();
                return;
            }
            if (datasetsRetriever.IsBusy)
            {
                datasetsRetriever.CancelAsync();
                return;
            }
            datasetsRetriever.RunWorkerAsync(vm.Vin);
        }

        private void RetrieveDatasetsAsync(object sender, DoWorkEventArgs ea)
        {
            List<PsaDataset> result = new List<PsaDataset>();
            try
            {
                VtsWebServiceClient service = new VtsWebServiceClient();
                List<long> datasetIds = service.GetDatasetIdsForVehicle(
                    ea.Argument as string,
                    LoggedUserContext.LoggedUser.Login,
                    LoggedUserContext.LoggedUser.PasswordHash).ToList();
                foreach (long datasetId in datasetIds)
                {
                    PsaDatasetDto dto = service.GetDatasetById(datasetId,
                        LoggedUserContext.LoggedUser.Login,
                    LoggedUserContext.LoggedUser.PasswordHash);
                    PsaDataset dataset = PsaDatasetAssembler.FromDtoToDomainObject(dto);
                    result.Add(dataset);
                }
                ea.Result = result;
            }
            catch (Exception e)
            {
                ea.Result = e;
            }
        }

        protected override void OnVehicleSelected()
        {
            UpdateDatasetsForVehicle(SelectedVehicle);
        }

        private void OnDatasetsRetrieved(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is Exception)
            {
                Dispatcher.CurrentDispatcher.Invoke(new ParameterizedThreadStart(ShowErrorWindow), e.Result);
                return;
            }
            List<PsaDataset> result = e.Result as List<PsaDataset>;
            Dispatcher.CurrentDispatcher.Invoke(new ThreadStart(ClearDatasets));
            foreach (PsaDataset dataset in result)
            {
                Dispatcher.CurrentDispatcher.Invoke(new ParameterizedThreadStart(AddDataset), dataset);
            }
            StopWaiting();
        }

        private void AddDataset(object datasetObject)
        {
            PsaDataset dataset = datasetObject as PsaDataset;
            Datasets.Add(new PsaDatasetViewModel(dataset));
        }

        private void ClearDatasets()
        {
            Datasets.Clear();
        }

        private void ShowErrorWindow(object e)
        {
            ErrorWindow w = new ErrorWindow(e as Exception, "Cannot get data.");
            w.Owner = MainWindowKeeper.MainWindowInstance as Window;
            w.ShowDialog();
        }

        private void OnUserLoggedOn(object sender, EventArgs eventArgs)
        {
            GetVehicles();
        }

        private void OnUserLoggedOff(object sender, EventArgs eventArgs)
        {
            Datasets.Clear();
            Vehicles.Clear();
        }

        public void Dispose()
        {
            datasetsRetriever.Dispose();
        }
    }
}

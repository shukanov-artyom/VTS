using System;
using System.Collections.ObjectModel;
using Agent.Common.Instance;
using Agent.Common.Presentation.Vehicles;
using Agent.Logging;
using Agent.Network.Monitor;
using Agent.Network.Monitor.VtsWebService;

namespace Agent.Workspace.ViewModels
{
    public abstract class VehicleSelectionViewModel : ServiceRequestingViewModel
    {
        private static readonly ObservableCollection<VehicleViewModel> vehicles =
            new ObservableCollection<VehicleViewModel>();
        private static VehicleViewModel selectedVehicle;
        private bool isWaitingMode = false;

        static VehicleSelectionViewModel()
        {
            GetVehicles();
        }

        public bool IsWaitingMode
        {
            get
            {
                return isWaitingMode;
            }
            private set
            {
                isWaitingMode = value;
                OnPropertyChanged("IsWaitingMode");
            }
        }

        public ObservableCollection<VehicleViewModel> Vehicles
        {
            get
            {
                return vehicles;
            }
        }

        public VehicleViewModel SelectedVehicle
        {
            get
            {
                return selectedVehicle;
            }
            set
            {
                IsWaitingMode = true;
                selectedVehicle = value;
                OnPropertyChanged("SelectedVehicle");
                OnVehicleSelected();
            }
        }

        protected abstract void OnVehicleSelected();

        protected void StopWaiting()
        {
            IsWaitingMode = false;
        }

        protected static void GetVehicles()
        {
            if (LoggedUserContext.LoggedUser == null)
            {
                return;
            }
            vehicles.Clear();
            VtsWebServiceClient client = new VtsWebServiceClient();
            try
            {
                foreach (VehicleDto dto in client.GetVehiclesForUser(
                    LoggedUserContext.LoggedUser.Login,
                    LoggedUserContext.LoggedUser.PasswordHash))
                {
                    vehicles.Add(new VehicleViewModel(
                        VehicleAssembler.FromDtoToDomainObject(dto)));
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Could not retrieve vehicles.");
            }
        }
    }
}

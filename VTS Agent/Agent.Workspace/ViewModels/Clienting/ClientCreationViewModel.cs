using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Agent.Common.Instance;
using Agent.Common.Presentation;
using Agent.Common.Presentation.Error;
using Agent.Common.Presentation.Vehicles;
using Agent.Network.Monitor.VtsWebService;

namespace Agent.Workspace.ViewModels.Clienting
{
    public class ClientCreationViewModel : ViewModelBase
    {
        private readonly ObservableCollection<VehicleViewModel> vehicles;
        private readonly ICommand clientCreationCommand;
        private readonly Window window;

        private VehicleViewModel selectedVehicle;
        private string email;

        private bool isWaitingMode;

        public ClientCreationViewModel(
            ObservableCollection<VehicleViewModel> vehicles,
            Window window)
        {
            this.vehicles = vehicles;
            clientCreationCommand = new DelegateCommand(
                CreateClient, CanCreateClient);
            this.window = window;
        }

        public ICommand ClientCreationCommand
        {
            get
            {
                return clientCreationCommand;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("Email");
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
                selectedVehicle = value;
                OnPropertyChanged("SelectedVehicle");
            }
        }

        public bool IsWaitingMode
        {
            get
            {
                return isWaitingMode;
            }
            set
            {
                isWaitingMode = value;
                OnPropertyChanged("IsWaitingMode");
            }
        }

        private bool CanCreateClient()
        {
            if (String.IsNullOrEmpty(Email) || !email.Contains("@"))
            {
                return false;
            }
            return true;
        }

        private void CreateClient()
        {
            try
            {
                IsWaitingMode = true;
                var service = Infrastructure.Container.GetInstance<IVtsWebService>();
                service.ProvideAccessToVehicleForClientUsingEmail(
                    SelectedVehicle.Model.Id, 
                    Email, 
                    LoggedUserContext.LoggedUser.Login,
                    LoggedUserContext.LoggedUser.PasswordHash);
                window.Close();
            }
            catch (Exception e)
            {
                string msg = "Can not create client.";
                Logging.Log.Error(e, msg);
                ErrorWindow w = new ErrorWindow(e, msg);
                w.ShowDialog();
            }
        }
    }
}

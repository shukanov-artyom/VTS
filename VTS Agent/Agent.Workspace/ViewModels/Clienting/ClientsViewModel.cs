using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Agent.Common.Instance;
using Agent.Common.Presentation;
using Agent.Common.Presentation.Error;
using Agent.Common.Presentation.Vehicles;
using Agent.Logging;
using Agent.Network.Monitor;
using Agent.Network.Monitor.VtsWebService;
using Agent.Workspace.Views.Clienting;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.ViewModels.Clienting
{
    public class ClientsViewModel : ViewModelBase
    {
        private readonly ObservableCollection<UserViewModel> clients =
            new ObservableCollection<UserViewModel>();
        private readonly ObservableCollection<VehicleViewModel> vehicles = 
            new ObservableCollection<VehicleViewModel>(); 
        private readonly ObservableCollection<VehicleViewModel> availableVehiclesForClient = 
            new ObservableCollection<VehicleViewModel>();

        private UserViewModel selectedClient;
        private VehicleViewModel selectedAvailableVehicle;
        private bool canProvideAccess;
        private bool canBlock;

        private readonly ICommand createClientCommand;
        private readonly ICommand grantAccessCommand;
        private readonly ICommand blockCommand;

        public ClientsViewModel()
        {
            if (!ConnectionToServiceAvailable())
            {
                return;
            }
            GetClients();
            createClientCommand = new DelegateCommand(CreateClient);
            grantAccessCommand = new DelegateCommand(GrantAccessToVehicle);
            blockCommand = new DelegateCommand(BlockAccess);
            GetVehicles();
        }

        public ObservableCollection<VehicleViewModel> Vehicles
        {
            get
            {
                return vehicles;
            }
        }

        public UserViewModel SelectedClient
        {
            get
            {
                return selectedClient;
            }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
                CanProvideAccess = true;
                CanBlock = false;
                GetAvailableVehicles();
            }
        }

        public VehicleViewModel SelectedAvailableVehicle
        {
            get
            {
                return selectedAvailableVehicle;
            }
            set
            {
                selectedAvailableVehicle = value;
                CanBlock = true;
                OnPropertyChanged("SelectedAvailableVehicle");
            }
        }

        public bool CanProvideAccess
        {
            get
            {
                return canProvideAccess;
            }
            set
            {
                canProvideAccess = value;
                OnPropertyChanged("CanProvideAccess");
            }
        }

        public bool CanBlock
        {
            get
            {
                return canBlock;
            }
            set
            {
                canBlock = value;
                OnPropertyChanged("CanBlock");
            }
        }

        public ObservableCollection<UserViewModel> Clients
        {
            get
            {
                return clients;
            }
        }

        public ICommand CreateClientCommand
        {
            get
            {
                return createClientCommand;
            }
        }

        public ICommand GrantAccessCommand
        {
            get
            {
                return grantAccessCommand;
            }
        }

        public ICommand BlockCommand
        {
            get
            {
                return blockCommand;
            }
        }

        public ObservableCollection<VehicleViewModel> AvailableVehiclesForClient
        {
            get
            {
                return availableVehiclesForClient;
            }
        }

        private void CreateClient()
        {
            ClientCreationWindow window = new ClientCreationWindow();
            window.Owner = MainWindowKeeper.MainWindowInstance as Window;
            window.DataContext = new ClientCreationViewModel(vehicles, window);
            bool? result = window.ShowDialog();
            if (result != null)
            {
                GetClients();
            }
        }

        private void GetAvailableVehicles()
        {
            availableVehiclesForClient.Clear();
            if (selectedClient == null)
            {
                return;
            }
            try
            {
                var service = Infrastructure.Container.GetInstance<IVtsWebService>();
                foreach (VehicleDto vehicleDto in service.GetVehiclesForUser(
                    selectedClient.Model.Login,
                    selectedClient.Model.PasswordHash))
                {
                    availableVehiclesForClient.Add(new VehicleViewModel(
                        VehicleAssembler.FromDtoToDomainObject(vehicleDto)));
                }
            }
            catch (Exception e)
            {
                const string msg = "Could not retrieve available vehicles.";
                Log.Error(e, msg);
                ErrorWindow w = new ErrorWindow(e, msg);
                w.Owner = MainWindowKeeper.MainWindowInstance as Window;
                w.ShowDialog();
            }
        }

        private void GetClients()
        {
            try
            {
                Clients.Clear();
                var service = Infrastructure.Container.GetInstance<IVtsWebService>();
                if (LoggedUserContext.LoggedUser == null)
                {
                    return;
                }
                foreach (UserDto userDto in service.GetClientsForPartner(
                    LoggedUserContext.LoggedUser.Login,
                    LoggedUserContext.LoggedUser.PasswordHash))
                {
                    User user = UserAssembler.FromDtoToDomainObject(userDto);
                    clients.Add(new UserViewModel(user));
                }
            }
            catch (Exception e)
            {
                const string msg = "Could not retrieve clients";
                Log.Error(e, msg);
                ErrorWindow w = new ErrorWindow(e, msg);
                w.Owner = MainWindowKeeper.MainWindowInstance as Window;
                w.ShowDialog();
            }
        }

        private void GetVehicles()
        {
            try
            {
                Vehicles.Clear();
                var service = Infrastructure.Container.GetInstance<IVtsWebService>();
                if (LoggedUserContext.LoggedUser == null)
                {
                    return;
                }
                foreach (VehicleDto vehicleDto in service.GetVehiclesForUser(
                    LoggedUserContext.LoggedUser.Login,
                    LoggedUserContext.LoggedUser.PasswordHash))
                {
                    vehicles.Add(new VehicleViewModel(
                        VehicleAssembler.FromDtoToDomainObject(vehicleDto)));
                }
            }
            catch (Exception e)
            {
                const string message = "Can not get vehicles for current user.";
                Log.Error(e, message);
                ErrorWindow w = new ErrorWindow(e, message);
                w.ShowDialog();
            }
        }

        private void GrantAccessToVehicle()
        {
            VehicleSelectionWindow win = new VehicleSelectionWindow(
                Vehicles);
            win.Owner = MainWindowKeeper.MainWindowInstance as Window;
            bool? result = win.ShowDialog();
            if (result == true)
            {
                VehicleViewModel vm = win.SelectedVehicle;
                try
                {
                    var service = Infrastructure.Container.GetInstance<IVtsWebService>();
                    service.ProvideAccessToVehicleForClientUsingEmail(
                        vm.Model.Id,
                        selectedClient.Email,
                        LoggedUserContext.LoggedUser.Login, 
                        LoggedUserContext.LoggedUser.PasswordHash);
                    GetAvailableVehicles();
                }
                catch (Exception e)
                {
                    const string message = "Can not provide access for vehicle for current user.";
                    Log.Error(e, message);
                    ErrorWindow w = new ErrorWindow(e, message);
                    w.ShowDialog();
                }
            }
        }

        private void BlockAccess()
        {
            if (selectedAvailableVehicle == null)
            {
                throw new Exception();
            }
            try
            {
                var service = Infrastructure.Container.GetInstance<IVtsWebService>();
                service.RevokeAccessToVehicleForClientUsingEmail(
                    selectedAvailableVehicle.Model.Id,
                    selectedClient.Email,
                    LoggedUserContext.LoggedUser.Login,
                    LoggedUserContext.LoggedUser.PasswordHash);
                GetAvailableVehicles();
            }
            catch (Exception e)
            {
                const string message = "Can not revoke access for vehicle for current user.";
                Log.Error(e, message);
                ErrorWindow w = new ErrorWindow(e, message);
                w.ShowDialog();
            }
        }

        private bool ConnectionToServiceAvailable()
        {
            var service = Infrastructure.Container.GetInstance<IVtsWebService>();
            try
            {
                return service.CheckConnection() == "ok";
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

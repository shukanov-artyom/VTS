using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Agent.Common.Presentation;
using Agent.Common.Presentation.Error;
using Agent.Logging;
using Agent.Network.Monitor;
using Agent.Network.Monitor.VtsWebService;
using Agent.Workspace.Views;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.ViewModels.Maintenance
{
    public class MaintenanceLogViewModel : VehicleSelectionViewModel
    {
        private readonly ObservableCollection<VehicleEventViewModel> attentionEvents =
            new ObservableCollection<VehicleEventViewModel>();
        private readonly ObservableCollection<VehicleEventViewModel> allEvents =
            new ObservableCollection<VehicleEventViewModel>();

        private readonly ICommand createNewCommand;
        private readonly ICommand editCommand;
        private readonly ICommand deleteCommand;

        private VehicleEventViewModel selectedEvent;

        public MaintenanceLogViewModel()
        {
            createNewCommand = new DelegateCommand(CreateNew);
            editCommand = new DelegateCommand(Edit, () => SelectedEvent != null);
            deleteCommand = new DelegateCommand(DeleteSelected, () => SelectedEvent != null);
        }

        public ICommand CreateNewCommand
        {
            get
            {
                return createNewCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return editCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand;
            }
        }

        public ObservableCollection<VehicleEventViewModel> AttentionItems
        {
            get
            {
                return attentionEvents;
            }
        }

        public ObservableCollection<VehicleEventViewModel> AllItems
        {
            get
            {
                return allEvents;
            }
        }

        public VehicleEventViewModel SelectedEvent
        {
            get
            {
                return selectedEvent;
            }
            set
            {
                selectedEvent = value;
                OnPropertyChanged("SelectedEvent");
            }
        }

        protected override void OnVehicleSelected()
        {
            AttentionItems.Clear();
            AllItems.Clear();
            var svc = Infrastructure.Container.GetInstance<IVtsWebService>();
            try
            {
                List<VehicleEventViewModel> vms = svc.GetVehicleEvents(SelectedVehicle.Vin).
                    Select(VehicleEventAssembler.FromDtoToDomainObject).
                    Select(v => new VehicleEventViewModel(v, SelectedVehicle.Model)).
                    ToList();
                foreach (VehicleEventViewModel viewModel in vms.OrderByDescending(v =>v.Date))
                {
                    AllItems.Add(viewModel);
                    if (RequiresAttention(viewModel, vms))
                    {
                        AttentionItems.Add(viewModel);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorWindow w = new ErrorWindow(e, "Cannot get vehicle events");
                w.ShowDialog();
                Log.Error(e, "Cannot get vehicle events");
            }
            finally
            {
                StopWaiting();
            }
        }

        private bool RequiresAttention(VehicleEventViewModel vm, List<VehicleEventViewModel> all)
        {
            if (vm.RequiresAttention())
            {
                IEnumerable<VehicleEventViewModel> sameType = 
                    from item in all
                    where item.Model.Type == vm.Model.Type
                    select item;
                return !sameType.Any(model => model.Date > vm.Date || model.Mileage > vm.Mileage);
            }
            return false;
        }

        private void CreateNew()
        {
            var w = new VehicleEventWindow();
            //VehicleEventViewModel newVm; 
            w.DataContext = new VehicleEventViewModel(
                new VehicleEvent(), 
                SelectedVehicle.Model);
            w.ShowDialog();
        }

        private void DeleteSelected()
        {
            throw new NotImplementedException();
        }

        private void Edit()
        {
            throw new NotImplementedException();
        }
    }
}

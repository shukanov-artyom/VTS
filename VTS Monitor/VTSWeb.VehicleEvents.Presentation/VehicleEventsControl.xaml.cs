using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VTS.Shared.DomainObjects;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.Presentation.Common.Vehicles;
using VTSWeb.VehicleEvents.Persistency;

namespace VTSWeb.VehicleEvents.Presentation
{
    public partial class VehicleEventsControl : NavigatablePage
    {
        private ObservableCollection<VehicleEventViewModel> allEvents = 
            new ObservableCollection<VehicleEventViewModel>();
        private ObservableCollection<VehicleEventViewModel>
            eventsRequireAttention =
            new ObservableCollection<VehicleEventViewModel>();

        private VehicleViewModel selectedVehicle;
        private VehicleEventViewModel selectedEvent;

        public VehicleEventsControl()
        {
            InitializeComponent();
            InitializeHeader("VehicleEvents");
            controlVehicleSelection.VehicleSelected += OnSelectedVehicleChanged;
            dataGridAllEvents.ItemsSource = allEvents;
            dataGridRequiresAttention.ItemsSource = eventsRequireAttention;
            ApplicationSizeKeeper.AppResized += OnAppResize;
            OnAppResize(this, EventArgs.Empty);
        }

        private void OnSelectedVehicleChanged(
            object sender, SelectionChangedEventArgs e)
        {
            buttonEdit.IsEnabled = false;
            buttonRemove.IsEnabled = false;
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            progressBarCircular.Visibility = Visibility.Visible;
            selectedVehicle = e.AddedItems[0] as VehicleViewModel;
            if (selectedVehicle != null)
            {
                LoadEvents();
            }
        }

        private void LoadEvents()
        {
            if (selectedVehicle == null)
            {
                return;
            }
            VehicleEventsPersistency persistency = 
                new VehicleEventsPersistency(OnAllLoaded, OnError);
            persistency.GetAllForVehicle(selectedVehicle.Model);
        }

        private void OnAllLoaded(List<VehicleEvent> events)
        {
            allEvents.Clear();
            eventsRequireAttention.Clear();
            foreach (VehicleEvent vehicleEvent in events)
            {
                VehicleEventViewModel eventViewModel =
                    new VehicleEventViewModel(
                        vehicleEvent, selectedVehicle.Model);
                allEvents.Add(eventViewModel);
                CheckAndAddIfRequiresAttention(eventViewModel, 2000);
            }
            progressBarCircular.Visibility = Visibility.Collapsed;
            buttonCreateNew.IsEnabled = true;
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow wnd = new ErrorWindow(e, msg);
            wnd.Closed += DialogWindowStatus.OnDialogClosed;
            wnd.Show();
        }

        private void CreateNewClicked(object sender, RoutedEventArgs e)
        {
            VehicleEvent ve = new VehicleEvent();
            VehicleEventViewModel vm = new VehicleEventViewModel(ve, 
                selectedVehicle.Model);
            VehicleEventWindow w = new VehicleEventWindow(vm);
            w.Closed += DialogWindowStatus.OnDialogClosed;
            w.Show();
        }

        private void EditClicked(object sender, RoutedEventArgs e)
        {
            VehicleEventViewModel vm = new VehicleEventViewModel(
                selectedEvent.Model, selectedVehicle.Model);
            VehicleEventWindow w = new VehicleEventWindow(vm);
            w.Closed += DialogWindowStatus.OnDialogClosed;
            w.Show();
        }

        private void RefreshClicked(object sender, RoutedEventArgs e)
        {
            LoadEvents();
        }

        private void RemoveClicked(object sender, RoutedEventArgs e)
        {
            VehicleEventsPersistency persistency =
                new VehicleEventsPersistency(OnDeleted, OnError);
            persistency.Delete(selectedEvent.Model);
        }

        private void OnDeleted()
        {
            selectedEvent = null;
            buttonEdit.IsEnabled = false;
            buttonRemove.IsEnabled = false;
            LoadEvents();
        }

        private void OnAllItemsGridSelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                selectedEvent = null;
                buttonEdit.IsEnabled = false;
                buttonRemove.IsEnabled = false;
                return;
            }
            selectedEvent = e.AddedItems[0] as VehicleEventViewModel;
            buttonEdit.IsEnabled = true;
            buttonRemove.IsEnabled = true;
        }

        private void CheckAndAddIfRequiresAttention(
            VehicleEventViewModel eventViewModel, int threshold)
        {
            if (RequiresAttention(eventViewModel, threshold))
            {
                eventsRequireAttention.Add(eventViewModel);
            }
        }

        private bool RequiresAttention(VehicleEventViewModel eventViewModel, 
            int threshold)
        {
            if (!eventViewModel.UseRecurring)
            {
                return false;
            }
            int mileageToReplace =
                eventViewModel.Mileage + eventViewModel.MileageUntilChange;
            int currentVehicleMileage = eventViewModel.CurrentVehicleMileage;
            TimeSpan leftUntilNextChange = eventViewModel.Date + TimeSpan.
                FromDays(eventViewModel.NextReplacementPeriod) - DateTime.Now;
            double daysLeftUntilNextChange = leftUntilNextChange.TotalDays;

            if (mileageToReplace - threshold <= currentVehicleMileage ||
                daysLeftUntilNextChange < 30)
            {
                if (!allEvents.Any(e => e.Model.Type == eventViewModel.Model.Type &&
                    e.Date > eventViewModel.Date))
                {
                    return true;
                }
            }
            return false;
        }

        private void OnAppResize(object sender, EventArgs e)
        {
            dataGridAllEvents.Height =
                (ApplicationSizeKeeper.Height - 300) / 2;
            dataGridRequiresAttention.Height = 
                (ApplicationSizeKeeper.Height - 300) / 2;
        }
    }
}

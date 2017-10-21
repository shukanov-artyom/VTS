using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using VTS.Shared.DomainObjects;

using VTSWeb.Localization;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.VehicleEvents.Presentation
{
    public class VehicleEventViewModel : ViewModelBase
    {
        private VehicleEvent model;
        private Vehicle vehicle;
        private int mileageUntilChange;
        private bool checkingInProgress;
        private ObservableCollection<VehicleEventTypeViewModel> availableTypes = 
            new ObservableCollection<VehicleEventTypeViewModel>();
        private VehicleEventTypeViewModel selectedType;
        private string errorMessage;
        private bool useRecurring;

        public VehicleEventViewModel(VehicleEvent model, Vehicle vehicle)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.vehicle = vehicle;
            this.model = model;
            model.VehicleId = vehicle.Id;
            // Init avail types
            foreach (VehicleEventType type in
                Enum.GetValues(typeof(VehicleEventType)))
            {
                availableTypes.Add(new VehicleEventTypeViewModel(type));
            }
            selectedType = availableTypes.
                FirstOrDefault(t => t.Model == model.Type);
            if (model.RedMileage != null)
            {
                UseRecurring = true;
                MileageUntilChange = model.RedMileage.Value - model.Mileage;
            }
        }

        public ObservableCollection<VehicleEventTypeViewModel> AvailableTypes
        {
            get
            {
                return availableTypes;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return vehicle;
            }
        }

        public int NextReplacementPeriod
        {
            get
            {
                return model.NextReplacementPeriod;
            }
            set
            {
                model.NextReplacementPeriod = value;
                OnPropertyChanged("NextReplacementPeriod");
            }
        }

        public int CurrentVehicleMileage
        {
            get
            {
                return vehicle.Mileage;
            }
            set
            {
                vehicle.Mileage = value;
                SaveNewVehicleMileage(value);
            }
        }

        private void SaveNewVehicleMileage(int newMileage)
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            service.UpdateVehicleMileageCompleted += OnMileageUpdated;
            service.UpdateVehicleMileageAsync(vehicle.Vin, newMileage);
        }

        private void OnMileageUpdated(object s, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(e.Error, e.Error.Message);
            }
            else
            {
                OnPropertyChanged("CurrentVehicleMileage");
            }
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow w = new ErrorWindow(e, msg);
            w.Closed += DialogWindowStatus.OnDialogClosed;
            w.Show();
        }

        public VehicleEventTypeViewModel SelectedType
        {
            get
            {
                return selectedType;
            }
            set
            {
                selectedType = value;
                model.Type = selectedType.Model;
                OnPropertyChanged("SelectedType");
            }
        }

        public DateTime Date
        {
            get
            {
                return model.Date;
            }
            set
            {
                model.Date = value;
                OnPropertyChanged("Date");
            }
        }

        public int Mileage
        {
            get
            {
                return model.Mileage;
            }
            set
            {
                model.Mileage = value;
                OnPropertyChanged("Mileage");
            }
        }

        public int MileageLeftUntilChange
        {
            get
            {
                if (model.RedMileage == null)
                {
                    throw new Exception();
                }
                return model.RedMileage.Value - vehicle.Mileage;
            }
        }

        public int MileageUntilChange
        {
            get
            {
                return mileageUntilChange;
            }
            set
            {
                mileageUntilChange = value;
                RedMileage = Mileage + mileageUntilChange;
                OnPropertyChanged("MileageUntilChange");
            }
        }

        public int? RedMileage
        {
            get
            {
                return model.RedMileage;
            }
            set
            {
                model.RedMileage = value;
                OnPropertyChanged("RedMileage");
            }
        }

        public int? YellowMileage
        {
            get
            {
                return model.RedMileage;
            }
            set
            {
                model.RedMileage = value;
                OnPropertyChanged("YellowMileage");
            }
        }

        public string Comment
        {
            get
            {
                return model.Comment;
            }
            set
            {
                model.Comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public bool CheckingInProgress
        {
            get
            {
                return checkingInProgress;
            }
            set
            {
                checkingInProgress = value;
                OnPropertyChanged("CheckingInProgress");
            }
        }

        public bool UseRecurring
        {
            get
            {
                return useRecurring;
            }
            set
            {
                useRecurring = value;
                OnPropertyChanged("UseRecurring");
            }
        }

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public VehicleEvent Model
        {
            get
            {
                return model;
            }
        }

        public bool CheckDataConsistency()
        {
            if (selectedType == null)
            {
                ErrorMessage = CodeBehindStringResolver.Resolve(
                    "VehicleEventTypeNotSelectedErrorMessage");
                CheckingInProgress = false;
                return false;
            }
            if (Mileage == 0)
            {
                ErrorMessage = CodeBehindStringResolver.Resolve(
                    "VehicleEventMileageNotSetErrorMessage");
                CheckingInProgress = false;
                return false;
            }
            if (Mileage > vehicle.Mileage)
            {
                ErrorMessage = CodeBehindStringResolver.
                    Resolve("MileageShouldNoBeMoreThanCurrentErrorMessage");
                CheckingInProgress = false;
                return false;
            }
            if (UseRecurring && Mileage + MileageUntilChange < vehicle.Mileage)
            {
                ErrorMessage = CodeBehindStringResolver.Resolve(
                    "PlannedNextReplacementAppearsInThePastErrorMessage");
                CheckingInProgress = false;
                return false;
            }
            return true;
        }
    }
}

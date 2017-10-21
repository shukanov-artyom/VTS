using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Agent.Common.Instance;
using Agent.Common.Presentation;
using Agent.Common.Presentation.Error;
using Agent.Localization;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.ViewModels.Maintenance
{
    public class VehicleEventViewModel : ViewModelBase
    {
        private readonly VehicleEvent model;
        private readonly Vehicle vehicle;
        private int mileageUntilChange;
        private bool checkingInProgress;
        private readonly ObservableCollection<VehicleEventTypeViewModel> availableTypes =
            new ObservableCollection<VehicleEventTypeViewModel>();
        private VehicleEventTypeViewModel selectedType;
        private string errorMessage;
        private bool useRecurring;
        private readonly ICommand showDescriptionCommand;

        public VehicleEventViewModel(VehicleEvent model, Vehicle vehicle)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.vehicle = vehicle;
            this.model = model;
            model.VehicleId = vehicle.Id;
            foreach (VehicleEventType type in Enum.GetValues(typeof(VehicleEventType)))
            {
                availableTypes.Add(new VehicleEventTypeViewModel(type));
            }
            selectedType = availableTypes.
                FirstOrDefault(t => t.Model == model.Type);
            if (model.RedMileage != null)
            {
                MileageUntilChange = model.RedMileage.Value - model.Mileage;
            }
            showDescriptionCommand = new DelegateCommand(ShowDescription);
        }

        public ICommand ShowDescriptionCommand
        {
            get
            {
                return showDescriptionCommand;
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

        // TODO: rename it to just type
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

        public string DateFormattedD
        {
            get
            {
                return Date.ToString("D", TranslationManager.CurrentCulture);
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
                    return -1;
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

        public string DisplaySummary
        {
            get
            {
                return FormatEventDisplayInfo();
            }
        }

        public VehicleEvent Model
        {
            get
            {
                return model;
            }
        }

        private void SaveNewVehicleMileage(int newMileage)
        {
            VtsWebServiceClient service = new VtsWebServiceClient();
            try
            {
                service.UpdateVehicleMileage(vehicle.Vin, newMileage);
                OnPropertyChanged("CurrentVehicleMileage");
            }
            catch (Exception e)
            {
                OnError(e, e.Message);
            }
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow w = new ErrorWindow(e, msg);
            w.Show();
        }


        public bool RequiresAttention()
        {
            const int threshold = 2000;
            int mileageToReplace = Mileage + MileageUntilChange;
            int currentVehicleMileage = CurrentVehicleMileage;
            TimeSpan leftUntilNextChange = Date + TimeSpan.
                FromDays(NextReplacementPeriod) - DateTime.Now;
            double daysLeftUntilNextChange = leftUntilNextChange.TotalDays;

            if (mileageToReplace - threshold <= currentVehicleMileage ||
                daysLeftUntilNextChange < 30)
            {
                return true;
            }
            return false;
        }

        private void ShowDescription()
        {
            MessageBox.Show(MainWindowKeeper.MainWindowInstance as Window, 
                Comment, 
                "Comment", 
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private string FormatEventDisplayInfo()
        {
            const string format = "{0} {1} {2} {3}";
            if (MileageLeftUntilChange < 0)
            {
                return String.Format(format,
                    SelectedType.DisplayName, 
                    CodeBehindStringResolver.Resolve("ExpiredText"),
                    MileageLeftUntilChange *-1,
                    CodeBehindStringResolver.Resolve("UnitsKm"));
            }
            return String.Format(format,
                    SelectedType.DisplayName,
                    CodeBehindStringResolver.Resolve("InPeriodText"),
                    MileageLeftUntilChange,
                    CodeBehindStringResolver.Resolve("UnitsKm"));
        }

        protected override void ChangeLanguage()
        {
            base.ChangeLanguage();
            OnPropertyChanged("DateFormattedD");
            OnPropertyChanged("DisplaySummary");
            OnPropertyChanged("Comment");
        }
    }
}

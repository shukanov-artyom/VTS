using System;
using System.ComponentModel;
using VTS.Shared.DomainObjects;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.VTSWebService.VTSWebService;

namespace VTSWeb.Presentation.Common.Vehicles
{
    public class VehicleViewModel : ViewModelBase
    {
        private readonly Vehicle model;
        private int newMileage;

        public VehicleViewModel(Vehicle model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
        }

        public string ManufacturerAndModel
        {
            get
            {
                return String.Format("{0} {1}", 
                    model.Manufacturer, model.Model);
            }
        }

        public DateTime RegisteredDate
        {
            get
            {
                return model.RegisteredDate;
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
                if (value < model.Mileage)
                {
                    return;
                }
                newMileage = value;
                VtsWebServiceClient service = new VtsWebServiceClient();
                service.UpdateVehicleMileageCompleted += ServiceOnUpdateVehicleMileageCompleted;
                service.UpdateVehicleMileageAsync(model.Vin, value);
            }
        }

        public string Vin
        {
            get
            {
                return model.Vin;
            }
        }

        public Vehicle Model
        {
            get
            {
                return model;
            }
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow w = new ErrorWindow(e, msg);
            w.Closed += DialogWindowStatus.OnDialogClosed;
            w.Show();
        }

        private void ServiceOnUpdateVehicleMileageCompleted(object sender, 
            AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                OnError(e.Error, e.Error.Message);
            }
            else
            {
                model.Mileage = newMileage;
            }
            OnPropertyChanged("Mileage");
        }
    }
}

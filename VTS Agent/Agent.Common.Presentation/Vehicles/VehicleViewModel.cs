using System;
using Agent.Logging;
using Agent.Network.Monitor.VtsWebService;
using VTS.Shared.DomainObjects;

namespace Agent.Common.Presentation.Vehicles
{
    public class VehicleViewModel : ViewModelBase
    {
        private readonly Vehicle model;

        public VehicleViewModel(Vehicle model)
        {
            this.model = model;
        }

        public string Vin
        {
            get
            {
                return model.Vin;
            }
        }

        public string ModelName
        {
            get
            {
                return model.Model;
            }
        }

        public string ManufacturerName
        {
            get
            {
                return model.Manufacturer.ToString();
            }
        }

        public string Summary
        {
            get
            {
                return String.Format("{0} {1} {2}",
                    ManufacturerName, ModelName, Vin);
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
                if (model.Mileage != value)
                {
                    model.Mileage = value;
                    OnPropertyChanged("Mileage");
                    UpdateMileage();
                }
            }
        }

        public Vehicle Model
        {
            get
            {
                return model;
            }
        }

        private void UpdateMileage()
        {
            try
            {
                IVtsWebService client = Infrastructure.Container.GetInstance<IVtsWebService>();
                client.UpdateVehicleMileage(Vin, Mileage);
            }
            catch (Exception e)
            {
                Log.Error(e, "Cannot update vehicle's mileage.");
            }
        }
    }
}

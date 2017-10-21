using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa.Extensions;
using VTSWeb.Presentation.Psa;

namespace VTSWeb.Presentation.Common.Workspace.Psa
{
    public class VehicleImportedDataViewModel : ViewModelBase
    {
        private ObservableCollection<PsaTraceViewModel> traces =
            new ObservableCollection<PsaTraceViewModel>();

        private IEnumerable<PsaTrace> model;
        private string vin;
        private Manufacturer manufacturer;
        private string vehicleModelName;
        private int mileage;
        private bool isSelected;

        public VehicleImportedDataViewModel(IEnumerable<PsaTrace> model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            foreach (PsaTrace trace in model)
            {
                Traces.Add(new PsaTraceViewModel(trace));
            }
            if (!model.AreAllVinsEqual())
            {
                throw new ArgumentException("Vins are not equal here!");
            }
            mileage = model.GetMaxMileage();
            if (Traces.Count > 0)
            {
                vin = Traces[0].Vin;
                manufacturer = Traces[0].Manufacturer;
                vehicleModelName = Traces[0].VehicleModelName;
            }
        }

        public ObservableCollection<PsaTraceViewModel> Traces
        {
            get
            {
                return traces;
            }
        }

        public string Vin
        {
            get
            {
                return vin;
            }
        }

        public Manufacturer Manufacturer
        {
            get
            {
                return manufacturer;
            }
        }

        public string VehicleModelName
        {
            get
            {
                return vehicleModelName;
            }
        }

        public string ManufacturerAndModelName
        {
            get
            {
                return String.Format("{0} {1}",
                    Manufacturer.ToString(),
                    VehicleModelName);
            }
        }

        public int Mileage
        {
            get
            {
                return mileage;
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
    }
}

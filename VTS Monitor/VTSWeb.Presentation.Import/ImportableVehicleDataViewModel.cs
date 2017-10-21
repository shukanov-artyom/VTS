using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa.Extensions;

namespace VTSWeb.Presentation.Import
{
    public class ImportableVehicleViewModel : ImportableViewModel
    {
        private ObservableCollection<ImportablePsaTraceViewModel> traces =
            new ObservableCollection<ImportablePsaTraceViewModel>();

        private IEnumerable<PsaTrace> model;
        private string vin;
        private Manufacturer manufacturer;
        private string vehicleModelName;
        private int mileage;
        private bool isSelected;

        public ImportableVehicleViewModel(IEnumerable<PsaTrace> model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            foreach (PsaTrace trace in model)
            {
                ImportablePsaTraceViewModel traceViewModel =
                    new ImportablePsaTraceViewModel(trace);
                RegisterExportableChild(traceViewModel);
                Traces.Add(traceViewModel);
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

        public ObservableCollection<ImportablePsaTraceViewModel> Traces
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

        public override bool CanBeSelectedForExport
        {
            get
            {
                return true;
            }
        }
    }
}

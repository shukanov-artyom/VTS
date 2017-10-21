using System;
using System.Collections.ObjectModel;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace VTSWeb.Presentation.Import
{
    public class ImportablePsaTraceViewModel : ImportableViewModel
    {
        private PsaTrace model;

        private ObservableCollection<
            ImportablePsaParametersSetViewModel> parametersSets;

        public ImportablePsaTraceViewModel(PsaTrace model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            parametersSets = new 
                ObservableCollection<ImportablePsaParametersSetViewModel>();
            foreach (PsaParametersSet v in model.ParametersSets)
            {
                ImportablePsaParametersSetViewModel b = 
                    new ImportablePsaParametersSetViewModel(v);
                RegisterExportableChild(b);
                parametersSets.Add(b);
            }
        }

        public override bool CanBeSelectedForExport
        {
            get
            {
                return true;
            }
        }

        public long PsaDatasetId
        {
            get
            {
                return model.PsaDatasetId;
            }
        }

        public DateTime Date
        {
            get
            {
                return model.Date;
            }
        }

        public string DateString
        {
            get
            {
                return model.Date.ToLongDateString();
            }
        }

        public string Application
        {
            get
            {
                return model.Application;
            }
        }

        public string Format
        {
            get
            {
                return model.Format;
            }
        }

        public string Phone
        {
            get
            {
                return model.Phone;
            }
        }

        public string PhoneChannel
        {
            get
            {
                return model.PhoneChannel;
            }
        }

        public string Vin
        {
            get
            {
                return model.Vin;
            }
        }

        public string Address
        {
            get
            {
                return model.Address;
            }
        }

        public string ToolSerialNumber
        {
            get
            {
                return model.ToolSerialNumber;
            }
        }

        public string ToolName
        {
            get
            {
                return model.ToolSerialNumber;
            }
        }

        public string SavesetId
        {
            get
            {
                return model.SavesetId;
            }
        }

        public string VehicleModelName
        {
            get
            {
                return model.VehicleModelName;
            }
        }

        public int Mileage
        {
            get;
            set;
        }

        public Manufacturer Manufacturer
        {
            get
            {
                return model.Manufacturer;
            }
        }

        public ObservableCollection<
            ImportablePsaParametersSetViewModel> ParametersSets
        {
            get
            {
                return parametersSets;
            }
        }

        public PsaTrace Model
        {
            get
            {
                return model;
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("DateString");
            base.ChangeLanguage();
        }
    }
}

using System;
using System.Collections.ObjectModel;
using VTS.Shared;
using VTS.Shared.DomainObjects;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Psa
{
    public class PsaTraceViewModel : ViewModelBase
    {
        private PsaTrace model;
        private ObservableCollection<PsaParametersSetViewModel> 
            parametersSets = 
            new ObservableCollection<PsaParametersSetViewModel>();

        public PsaTraceViewModel(PsaTrace model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            foreach (PsaParametersSet set in model.ParametersSets)
            {
                parametersSets.Add(new PsaParametersSetViewModel(set));
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
                string result = String.Format("{0} {1}",
                    CodeBehindStringResolver.Resolve("CapturedWord"),
                    model.Date.ToLongDateString());
                return result;
            }
        }

        public string Vin
        {
            get
            {
                return model.Vin;
            }
        }

        public string VehicleModelName
        {
            get
            {
                return model.VehicleModelName;
            }
        }

        public Manufacturer Manufacturer
        {
            get
            {
                return model.Manufacturer;
            }
        }

        public int Mileage
        {
            get
            {
                return model.Mileage;
            }
        }

        public ObservableCollection<PsaParametersSetViewModel> ParametersSets
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

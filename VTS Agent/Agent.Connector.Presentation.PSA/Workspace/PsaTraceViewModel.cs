using System;
using System.Collections.Generic;
using System.Linq;
using Agent.Common.Presentation.Lexia;
using Agent.Metadata.Psa;
using Agent.Network.DataSynchronization;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Connector.Presentation.PSA.Workspace
{
    public class ExportablePsaTraceViewModel : ExportableViewModel
    {
        private readonly PsaTraceInfo model;
        private readonly IList<ExportablePsaParametersSetViewModel> parametersSets = 
            new List<ExportablePsaParametersSetViewModel>();

        private readonly DataSynchronizationStatusViewModel dataSynchronizationStatus = 
            new DataSynchronizationStatusViewModel();

        private bool isSelected;
        private bool containsUnrecognizedData;

        public ExportablePsaTraceViewModel(PsaTraceInfo model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;

            foreach (PsaParametersSet set in model.Trace.ParametersSets)
            {
                ExportablePsaParametersSetViewModel vm =
                    new ExportablePsaParametersSetViewModel(set);
                RegisterExportableChild(vm);
                parametersSets.Add(vm);
            }
            containsUnrecognizedData = IsThereUnrecognizedData(model.Trace);
            if (containsUnrecognizedData)
            {
                SynchronizationStatus.Update(DataSynchronizationStatus.DataUnsupported);
            }
            else
            {
                SynchronizationStatus.Update(DataSynchronizationStatus.InProgress);
            }
        }

        public DataSynchronizationStatusViewModel SynchronizationStatus
        {
            get
            {
                return dataSynchronizationStatus;
            }
        }

        public DateTime Date
        {
            get
            {
                return model.Trace.Date;
            }
            set
            {
                model.Trace.Date = value;
                OnPropertyChanged("Date");
            }
        }

        public string DateString
        {
            get
            {
                return Date.ToLongDateString();
            }
        }

        public string Vin
        {
            get
            {
                return model.Trace.Vin;
            }
            set
            {
                model.Trace.Vin = value;
                OnPropertyChanged("Vin");
            }
        }

        public string VehicleModelName
        {
            get
            {
                return model.Trace.VehicleModelName;
            }
            set
            {
                model.Trace.VehicleModelName = value;
                OnPropertyChanged("VehicleModelName");
            }
        }

        public int Mileage
        {
            get
            {
                return model.Trace.Mileage;
            }
            set
            {
                model.Trace.Mileage = value;
                model.Metadata.Mileage = value;
                OnPropertyChanged("Mileage");
            }
        }

        public override bool CanBeSelectedForExport
        {
            get
            {
                return base.CanBeSelectedForExport && ParametersSets.Any();
            }
        }

        public bool ContainsUnrecognizedData
        {
            get
            {
                return containsUnrecognizedData;
            }
        }

        public Manufacturer Manufacturer
        {
            get
            {
                return model.Trace.Manufacturer;
            }
        }

        public IList<ExportablePsaParametersSetViewModel> ParametersSets
        {
            get
            {
                return parametersSets;
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

        public PsaTraceInfo ModelTraceInfo
        {
            get
            {
                return model;
            }
        }

        public PsaTrace Model
        {
            get
            {
                return model.Trace;
            }
        }

        private bool IsThereUnrecognizedData(PsaTrace trace)
        {
            foreach (PsaParametersSet parametersSet in trace.ParametersSets)
            {
                if (parametersSet.Type == PsaParametersSetType.Unsupported)
                {
                    return true;
                }
                if (parametersSet.Parameters.Any(p => 
                    p.Type == PsaParameterType.Unsupported))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("DateString");
            base.ChangeLanguage();
        }
    }
}

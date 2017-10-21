using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Agent.Common.Instance;
using Agent.Common.Presentation.Lexia;
using Agent.Connector;
using Agent.Connector.Presentation.PSA.Workspace;
using Agent.Localization;
using Agent.Logging;
using Agent.Metadata.Psa;
using Agent.Workspace.VinPersistency;
using VTS.Shared;

namespace Agent.Workspace.ViewModels
{
    /// <summary>
    /// Does not have model, is used for traces grouping in the UI
    /// </summary>
    public class ExportableVehicleViewModel : ExportableViewModel
    {
        private readonly ObservableCollection<ExportablePsaTraceViewModel> traces = 
            new ObservableCollection<ExportablePsaTraceViewModel>();

        private string vin;
        private Manufacturer manufacturer;
        private string modelName;
        private int mileage;
        private bool isSelected;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportableVehicleViewModel"/> .
        /// </summary>
        public ExportableVehicleViewModel(
            string vin, 
            Manufacturer manufacturer,
            int mileage,
            string modelName)
        {
            this.vin = vin;
            this.manufacturer = manufacturer;
            this.mileage = mileage;
            this.modelName = modelName;
        }

        public ObservableCollection<ExportablePsaTraceViewModel> Traces
        {
            get
            {
                return traces;
            }
        }

        public void Add(ExportablePsaTraceViewModel trace)
        {
            Traces.Add(trace);
            SortTracesByDate();
            RegisterExportableChild(trace);
        }

        public string Vin
        {
            get
            {
                return vin;
            }
            set
            {
                if (!VinChecker.IsValid(value))
                {
                    MessageBox.Show(MainWindowKeeper.MainWindowInstance as Window,
                        CodeBehindStringResolver.Resolve("IncorrectVinMessage"),
                        CodeBehindStringResolver.Resolve("IncorrectVinMessageBoxCaption"),
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }
                vin = value;
                bool success = true;
                foreach (ExportablePsaTraceViewModel trace in traces)
                {
                    PsaTraceInfo traceInfo = trace.ModelTraceInfo;
                    ITraceVinPersistency pers;
                    try
                    {
                        pers = TraceVinPersistencyFactory.Create(traceInfo);
                    }
                    catch (NotImplementedException)
                    {
                        MessageBox.Show(MainWindowKeeper.MainWindowInstance as Window,
                            "Function not implemented",
                            "Not implemented");
                        return;
                    }
                    try
                    {
                        pers.PersistNewVin(vin);
                    }
                    catch (Exception e)
                    {
                        success = false;
                        Log.Error(e, "Cannot persist VIN code in trace.");
                    }
                    if (!success)
                    {
                        throw new NotImplementedException("Show a messagebos with an error.");
                    }
                    // use vin persistency factory based on: 
                    // - trace.ModelTraceInfo.Metadata.Connector and 
                    // - trace.ModelTraceInfo.Metadata.Subtype
                    // rollback changes in case of exception
                }
                OnPropertyChanged("Vin");
            }
        }

        public bool ChangeVinAllowed
        {
            get
            {
                return !IsVinKnownAndValid(vin);
            }
        }

        public Manufacturer Manufacturer
        {
            get
            {
                return manufacturer;
            }
            set
            {
                manufacturer = value;
                OnPropertyChanged("Manufacturer");
            }
        }

        public string ManufacturerAndModelName
        {
            get
            {
                return String.Format("{0} {1}", Manufacturer, ModelName);
            }
        }

        public string ModelName
        {
            get
            {
                return modelName;
            }
            set
            {
                modelName = value;
                OnPropertyChanged("ModelName");
            }
        }

        public int Mileage
        {
            get
            {
                return mileage;
            }
            set
            {
                mileage = value;
                OnPropertyChanged("Mileage");
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
                return Traces.Any(t => t.CanBeSelectedForExport);
            }
        }

        private void SortTracesByDate()
        {
            List<ExportablePsaTraceViewModel> trcs
                = traces.OrderByDescending(t => t.Date).ToList();
            traces.Clear();
            foreach (ExportablePsaTraceViewModel vm in trcs)
            {
                traces.Add(vm);
            }
        }

        private bool IsVinKnownAndValid(string vinToCheck)
        {
            if (String.IsNullOrWhiteSpace(vinToCheck) || 
                vinToCheck.Length != 17)
            {
                return false;
            }
            try
            {
                VinChecker.GetManufacturer(vinToCheck);
            }
            catch (NotSupportedException)
            {
                return false;
            }
            return true;
        }
    }
}

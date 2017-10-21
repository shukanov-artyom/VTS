using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Agent.Common.Data;
using Agent.Common.Instance;
using Agent.Common.Presentation;
using Agent.Common.Presentation.Error;
using Agent.Common.Presentation.Unrecognized;
using Agent.Connector;
using Agent.Connector.Presentation.PSA.Workspace;
using Agent.Localization;
using Agent.Logging;
using Agent.Metadata.Psa;
using Agent.Network.DataSynchronization;
using Agent.Network.DataSynchronization.Psa;
using Agent.Network.Monitor.VtsWebService;
using Agent.Workspace.Filtering;
using Agent.Workspace.ViewModels.Clienting;
using Agent.Workspace.ViewModels.Maintenance;
using Common.Transport;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.ViewModels
{
    public class PsaDataViewModel : ServiceRequestingViewModel, IDisposable
    {
        private const string CipherPwd = "DateTime";

        private readonly Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        private readonly ObservableCollection<ExportablePsaTraceViewModel> traces =
            new ObservableCollection<ExportablePsaTraceViewModel>();
        private readonly ObservableCollection<ExportableVehicleViewModel> byVehicles =
            new ObservableCollection<ExportableVehicleViewModel>();
        private readonly AllDataViewModel allData = new AllDataViewModel();
        private readonly ChronologyDataViewModel chronologyData = new ChronologyDataViewModel();
        private readonly EvaluationDataViewModel evaluationData = new EvaluationDataViewModel();
        private readonly ClientsViewModel clientsData = new ClientsViewModel();
        private readonly PsaDataSynchronizer synchronizer;

        protected delegate void AddToByVehiclesDelegate(ExportableVehicleViewModel v);
        protected delegate void AddToExistingByVehicleDelegate(ExportablePsaTraceViewModel trc);

        private MaintenanceLogViewModel maintenanceData;

        private bool isWaitingMode;
        private bool isSynchronizingData;

        private ICommand exportCommand;
        private ICommand selectAllCommand;
        private ICommand deselectAllCommand;
        private ICommand exportUnrecognizedCommand;
        private ICommand importCommand;

        public PsaDataViewModel(params DiagnosticSystemConnector[] connectors)
        {
            InitializeCommands();
            LoadConnectorsData(connectors);
            synchronizer = new PsaDataSynchronizer();
            synchronizer.StatusUpdated += OnSynchronizerStatusUpdated;
        }

        public ICommand ExportCommand
        {
            get
            {
                return exportCommand;
            }
        }

        public ICommand SelectAllCommand
        {
            get
            {
                return selectAllCommand;
            }
        }

        public ICommand DeselectAllCommand
        {
            get
            {
                return deselectAllCommand;
            }
        }

        public ICommand ExportUnrecognizedCommand
        {
            get
            {
                return exportUnrecognizedCommand;
            }
        }

        public ICommand ImportCommand
        {
            get
            {
                return importCommand;
            }
        }

        public ObservableCollection<ExportableVehicleViewModel> ByVehicles
        {
            get
            {
                return byVehicles;
            }
        }

        public AllDataViewModel AllData
        {
            get
            {
                return allData;
            }
        }

        public ChronologyDataViewModel ChronologyData
        {
            get
            {
                return chronologyData;
            }
        }

        public EvaluationDataViewModel EvaluationData
        {
            get
            {
                return evaluationData;
            }
        }

        public MaintenanceLogViewModel MaintenanceData
        {
            get
            {
                if (maintenanceData == null)
                {
                    return maintenanceData = new MaintenanceLogViewModel();
                }
                return maintenanceData;
            }
        }

        public ClientsViewModel ClientsData
        {
            get
            {
                return clientsData;
            }
        }

        public bool IsWaitingMode
        {
            get
            {
                return isWaitingMode;
            }
            set
            {
                isWaitingMode = value;
                // "IsWaitingMode"
                OnPropertyChanged("IsWaitingMode");
            }
        }

        public bool IsSynchronizingData
        {
            get
            {
                return isSynchronizingData;
            }
            set
            {
                OnPropertyChanged("IsSynchronizingData");
                isSynchronizingData = value;
            }
        }

        private void InitializeCommands()
        {
            exportCommand = new DelegateCommand(Export, CanExport);
            selectAllCommand = new DelegateCommand(SelectAll, CanSelectAll);
            deselectAllCommand = new DelegateCommand(DeselectAll, CanDeselectAll);
            exportUnrecognizedCommand =
                new DelegateCommand(ExportUnrecognized, CanExportUnrecognized);
            importCommand = new DelegateCommand(Import);
        }

        private void LoadConnectorsData(DiagnosticSystemConnector[] connectors)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = false;
            bw.WorkerReportsProgress = false;
            bw.DoWork += BackgroundJob;
            bw.RunWorkerCompleted += BackgroundJobFinished;

            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync(connectors);
            }
        }

        private void BackgroundJob(object sender, DoWorkEventArgs e)
        {
            IsWaitingMode = true;
            DiagnosticSystemConnector[] connectors = 
                e.Argument as DiagnosticSystemConnector[];
            foreach (DiagnosticSystemConnector cnctr in connectors)
            {
                try
                {
                    HashSet<string> vins = new HashSet<string>();
                    foreach (PsaTraceInfo info in cnctr.GetAllTraces())
                    {
                        var viewModel = new ExportablePsaTraceViewModel(info);
                        traces.Add(viewModel);
                        if (vins.All(vin => vin != viewModel.Vin))
                        {
                            ExportableVehicleViewModel veh = new ExportableVehicleViewModel(
                                    viewModel.Vin,
                                    viewModel.Manufacturer,
                                    viewModel.Mileage,
                                    viewModel.VehicleModelName);
                            veh.Add(viewModel);
                            vins.Add(viewModel.Vin);
                            dispatcher.
                                BeginInvoke(DispatcherPriority.Normal,
                                new AddToByVehiclesDelegate(AddTraceToByVehicles), veh);
                        }
                        else
                        {
                            dispatcher.
                                BeginInvoke(DispatcherPriority.Normal,
                                new AddToExistingByVehicleDelegate(AddTraceToExistingByVehicle), viewModel);
                        }
                    }
                    e.Result = connectors;
                }
                catch (Exception exc)
                {
                    Log.Error(exc, exc.Message);
                }
            }
        }

        private void AddTraceToExistingByVehicle(ExportablePsaTraceViewModel vm)
        {
            byVehicles.First(v => v.Vin == vm.Vin).Add(vm);
        }

        private void AddTraceToByVehicles(ExportableVehicleViewModel veh)
        {
            byVehicles.Add(veh);
        }

        private void BackgroundJobFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            IsWaitingMode = false;
            IsSynchronizingData = true;
            List<PsaTraceInfo> traceInfos = traces.Select(t => t.ModelTraceInfo).ToList();
            try
            {
                synchronizer.StartSynchronizationAsync(traceInfos);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }

        private void Export()
        {
            PsaPreExportDataValidator validator =
                new PsaPreExportDataValidator(ByVehicles);
            if (validator.Validate())
            {
                PsaPreExportDataFilter filter =
                new PsaPreExportDataFilter(ByVehicles);
                PsaTracesExporter exporter =
                    new PsaTracesExporter(filter.Filter(),
                        SelectOutputFile, OnExported);
                exporter.Export();
            }
        }

        private string SelectOutputFile()
        {
            Microsoft.Win32.SaveFileDialog dlg =
                new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = CodeBehindStringResolver.
                Resolve(
                Cipher.Decrypt("Ynbz6JPcC3pNwhk9VU+fegAwfFwG8FxFGBDVQ3pcRWA=",
                "5c3a7443-d013-4e0b-a61d-4ceede81e581"));
            // ExportedDataExtension
            dlg.Filter = CodeBehindStringResolver.
                Resolve(
                Cipher.Decrypt("6r+CAEGiPIzlOssxNsl1XqOUOxPJ3+1T0vXvsvJdzD8=",
                "5c3a7443-d013-4e0b-a61d-4ceede81e581"));
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                return dlg.FileName;
            }
            return null;
        }

        private void OnSynchronizerStatusUpdated(DataSynchronizationStatus status,
            List<PsaTraceSignature> traceSignatures)
        {
            foreach (ExportableVehicleViewModel vehicleViewModel in byVehicles)
            {
                foreach (ExportablePsaTraceViewModel traceViewModel in vehicleViewModel.Traces)
                {
                    foreach (PsaTraceSignature signature in traceSignatures)
                    {
                        if (signature.Fits(traceViewModel.Model))
                        {
                            traceViewModel.SynchronizationStatus.Update(status);
                        }
                    }
                }
            }
        }

        private void OnExported(bool success, string pathOrErrorMessage)
        {
            if (success)
            {
                DataExportedInformationalWindow window = new DataExportedInformationalWindow();
                window.Owner = (Window)MainWindowKeeper.MainWindowInstance;
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show((Window)MainWindowKeeper.MainWindowInstance,
                    String.Format("{0}:\r\n{1}",
                    //DataExportFailedMessage
                    CodeBehindStringResolver.Resolve(Cipher.Decrypt("CqMEEckaxjfkk/gksV0oRWZxBgfn8ILeo5/aNOUcmxI=",
                        "5c3a7443-d013-4e0b-a61d-4ceede81e581")), 
                    pathOrErrorMessage),
                    //DataExportText
                    CodeBehindStringResolver.Resolve(Cipher.Decrypt("l3GDl+UGFCDfn2Knn5k1zg==",
                        "5c3a7443-d013-4e0b-a61d-4ceede81e581")),
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void ExportUnrecognized()
        {
            UnrecognizedDataExportTypeSelectionWindow w = 
                new UnrecognizedDataExportTypeSelectionWindow();
            w.Owner = MainWindowKeeper.MainWindowInstance as Window;
            bool? result = w.ShowDialog();
            if (result.HasValue && result.Value)
            {
                if (w.IsManually)
                {
                    string file = SelectFile();
                    if (String.IsNullOrEmpty(file))
                    {
                        return;
                    }
                    UnrecognizedDataKeeper.ExportUnrecognizedData(file);
                    UnrecognizedDataExportedWindow window =
                        new UnrecognizedDataExportedWindow();
                    window.Owner = MainWindowKeeper.MainWindowInstance as Window;
                    window.ShowDialog();
                }
                else if (w.IsAutomatically)
                {
                    Stream dataStream = null;
                    MemoryStream str = null;
                    try
                    {
                        dataStream = UnrecognizedDataKeeper.GetDataStream();
                        str = new MemoryStream();
                        dataStream.Position = 0;
                        dataStream.CopyTo(str);
                        str.Position = 0;
                    }
                    catch (Exception)
                    {
                        if (dataStream != null)
                        {
                            dataStream.Close();
                        }
                        str.Close();
                        throw;
                    }
                    try
                    {
                        VtsWebServiceClient client = new VtsWebServiceClient();
                        client.ReportUnrecognizedData(str);
                        if (!(MainWindowKeeper.MainWindowInstance is Window))
                        {
                            throw new ApplicationException("Main window should have already been here!");
                        }
                        MessageBox.Show(MainWindowKeeper.MainWindowInstance as Window,
                                    CodeBehindStringResolver.Resolve(
                                    "UnrecognizedDataReportedSuccessfullyMessage"),
                        "Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    }
                    catch (Exception e)
                    {
                        ErrorWindow wnd = new ErrorWindow(e, e.Message);
                        wnd.Owner = MainWindowKeeper.MainWindowInstance as Window;
                        wnd.ShowDialog();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        private bool CanExportUnrecognized()
        {
            if (ByVehicles.Any(
                    v =>
                    v.Traces.Any(
                        t =>
                        t.ParametersSets.Any(ps =>
                            ps.Parameters.Any(p =>
                                p.Model.Type ==
                                PsaParameterType.Unsupported)))))
            {
                return true;
            }
            if (ByVehicles.Any(v =>
                v.Traces.Any(t => t.ParametersSets.Any(ps => 
                    ps.Model.Type == PsaParametersSetType.Unsupported))))
            {
                return true;
            }
            return false;
        }

        private bool CanExport()
        {
            return HaveSelectedData();
        }

        private void SelectAll()
        {
            foreach (ExportableVehicleViewModel vehicle in ByVehicles)
            {
                vehicle.IsSelectedForExport = true;
            }
        }

        private bool CanSelectAll()
        {
            return true;
        }

        private void DeselectAll()
        {
            foreach (ExportableVehicleViewModel vehicle in ByVehicles)
            {
                vehicle.IsSelectedForExport = false;
            }
        }

        private bool CanDeselectAll()
        {
            return HaveSelectedData();
        }

        private bool HaveSelectedData()
        {
            return ByVehicles.Any(v => v.IsSelectedForExport);
        }

        private string SelectFile()
        {
            Microsoft.Win32.SaveFileDialog dlg =
                new Microsoft.Win32.SaveFileDialog();
            DateTime dt = DateTime.Now;
            string ext = Cipher.Decrypt("bTbP/Mhm7Iqnec4TfSWxXA==", CipherPwd);
            dlg.FileName =
                String.Format("{0}_{1}_{2}", dt.Day,
                dt.Month, dt.Year);
            dlg.AddExtension = true;
            dlg.DefaultExt = ext; // Default file extension
            string filterCiphered = CodeBehindStringResolver.
                Resolve(Cipher.Decrypt("khCQHwVb07U/yyMAMJZycg==", CipherPwd));
            dlg.Filter = Cipher.Decrypt(filterCiphered, CipherPwd);

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                return dlg.FileName;
            }
            return String.Empty;
        }

        /// <summary>
        /// Used to import PortableData from .vts file.
        /// </summary>
        private void Import()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
                {
                    AddExtension = true,
                    DefaultExt = ".vts"
                };
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string filePathName = dlg.FileName;
                try
                {
                    PortableData portableData = PsaTracesExporter.Import(filePathName);
                    CheckTracesForAlreadyDisplayedAndDisplayNew(portableData);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Canot import data.");
                    MessageBox.Show(MainWindowKeeper.MainWindowInstance as Window, 
                        "Cannot parse data file.", 
                        "Import error",
                        MessageBoxButton.OK, 
                        MessageBoxImage.Error, 
                        MessageBoxResult.No, 
                        MessageBoxOptions.None);
                }
            }
        }

        private void CheckTracesForAlreadyDisplayedAndDisplayNew(PortableData data)
        {
            // Select those traces that are not yet displayed.
            IList<PsaTrace> tracesToDisplay =
                data.PsaTraces.Where(t => !IsAlreadyDisplayed(t)).ToList();
            // Display traces that are ready to be displayed (possibly creating Vehicles for them).
            foreach (PsaTrace trace in tracesToDisplay)
            {
                if (!ByVehicles.Any(v => v.Vin.Equals(trace.Vin, StringComparison.OrdinalIgnoreCase)))
                {
                    // add vehicle, add trace to vehicle
                    ExportableVehicleViewModel newVehicle = new ExportableVehicleViewModel(
                        trace.Vin,
                        trace.Manufacturer,
                        trace.Mileage,
                        trace.VehicleModelName);
                    ExportablePsaTraceViewModel traceViewModel =
                        new ExportablePsaTraceViewModel(new PsaTraceInfo(trace, new PsaTraceMetadata()));
                    newVehicle.Traces.Add(traceViewModel);
                    traces.Add(traceViewModel);
                    ByVehicles.Add(newVehicle);
                }
                else
                {
                    // add trace to vehicle
                    ExportablePsaTraceViewModel traceViewModel =
                        new ExportablePsaTraceViewModel(new PsaTraceInfo(trace, new PsaTraceMetadata()));
                    ByVehicles.First(v => v.Vin.Equals(trace.Vin, 
                        StringComparison.OrdinalIgnoreCase)).
                        Traces.Add(traceViewModel);
                    traces.Add(traceViewModel);
                }
            }
            if (tracesToDisplay.Count != 0)
            {
                synchronizer.StartSynchronizationAsync(traces.Select(t => t.ModelTraceInfo).ToList());
            }
        }

        public bool IsAlreadyDisplayed(PsaTrace trace)
        {
            foreach (ExportableVehicleViewModel vehicle in ByVehicles)
            {
                foreach (ExportablePsaTraceViewModel displayedTrace in vehicle.Traces)
                {
                    if (displayedTrace.Model.Equals(trace))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Dispose()
        {
            allData.Dispose();
            synchronizer.Dispose();
        }
    }
}

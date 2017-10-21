using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa.Extensions;
using VTSWeb.Import;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Common.ErrorReporting;
using VTSWeb.Presentation.Import;

namespace VTSWeb.Presentation.Workspace.Partner.DataUpload
{
    public class DataUploadViewModel : ViewModelBase
    {
        private const string CurrentSelectedFileStubTextName =
            "CurrentSelectedFileStubTextName";
        private string currentSelectedFile = String.Empty;
        private bool isWaitingMode;
        private PortableData importedData;

        private DelegateCommand selectFileCommand;
        private ObservableCollection<ImportableVehicleViewModel> perVeh = 
            new ObservableCollection<ImportableVehicleViewModel>();

        public DataUploadViewModel()
        {
            selectFileCommand = new DelegateCommand(SelectFile, CanSelectFile);
            isWaitingMode = false;
        }

        public ICommand UploadFileCommand
        {
            get
            {
                return selectFileCommand;
            }
        }

        public ObservableCollection<ImportableVehicleViewModel> ByVehicle
        {
            get
            {
                return perVeh;
            }
        }

        public string CurrentSelectedFile
        {
            get
            {
                if (String.IsNullOrEmpty(currentSelectedFile))
                {
                    return CodeBehindStringResolver.Resolve(
                        CurrentSelectedFileStubTextName);
                }
                return currentSelectedFile;
            }
            set 
            { 
                currentSelectedFile = value;
                OnPropertyChanged("CurrentSelectedFile");
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
                OnPropertyChanged("IsWaitingMode");
            }
        }

        public PortableData ImportedData
        {
            get
            {
                return importedData;
            }
        }

        private void SelectFile(object arg)
        {
            string fileMask = "(.vts)|*.vts";
            string diagDataString =  
                CodeBehindStringResolver.Resolve("DiagnosticData");
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = String.Format("{0} {1}", 
                diagDataString, fileMask);
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                FileStream fileStream = dlg.File.OpenRead();
                CurrentSelectedFile = dlg.File.Name;
                perVeh.Clear();
                IsWaitingMode = true;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += ProcessUploadedFile;
                worker.RunWorkerCompleted += OnFileProcessingComplete;
                worker.RunWorkerAsync(fileStream);
            }
        }

        private bool CanSelectFile(object arg)
        {
            return !IsWaitingMode;
        }

        private void ProcessUploadedFile(object sender,
            DoWorkEventArgs doWorkEventArgs)
        {
            FileStream stream = doWorkEventArgs.Argument as FileStream;
            try
            {
                PortableDataFactory factory = new PortableDataFactory(stream);
                importedData = factory.Create();
                PortableDataImporter importer = new PortableDataImporter(
                    OnSuccessfullyImported, OnError);
                importer.UnsupportedVinsFound += OnUnsupportedVinsFound;
                importer.CheckAndImport(importedData);
            }
            catch (Exception)
            {
                stream.Close();
                SmartDispatcher.BeginInvoke(NotifyAboutFailure);
            }
        }

        private void OnUnsupportedVinsFound(List<string> unsupportedvins)
        {
            SmartDispatcher.BeginInvoke(NotifyAboutUnsupportedVins);
        }

        private void OnSuccessfullyImported()
        {
            DisplayObtainedData(importedData);
        }

        private void OnError(Exception e, string msg)
        {
            ErrorWindow w = new ErrorWindow(e, msg);
            w.Show();
        }

        private void OnFileProcessingComplete(object sender, 
            RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            //IsWaitingMode = false;
        }

        private void NotifyAboutFailure()
        {
            CurrentSelectedFile =
                CodeBehindStringResolver.Resolve("UnableToOpenFile");
        }

        private void NotifyAboutUnsupportedVins()
        {
            //ImportMessage = "Not all imported vehicles are supported.";
            // TODO 
        }

        private void DisplayObtainedData(PortableData data)
        {
            SmartDispatcher.BeginInvoke(() => UpdateVisualData(data));
        }

        private void UpdateVisualData(PortableData data)
        {
            IsWaitingMode = false;
            perVeh.Clear();
            foreach (ImportableVehicleViewModel vm in
                ExtractImportableVehicles(data.PsaTraces))
            {
                perVeh.Add(vm);
            }
        }

        private IEnumerable<ImportableVehicleViewModel>
            ExtractImportableVehicles(IEnumerable<PsaTrace> traces)
        {
            IList<string> vins = traces.GetVins();

            foreach (string vin in vins)
            {
                ImportableVehicleViewModel ivm =
                    new ImportableVehicleViewModel(traces.Where(
                        t => t.Vin == vin));
                yield return ivm;
            }
        }
    }
}

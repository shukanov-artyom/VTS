using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using Agent.Common.Presentation;
using Agent.Workspace.ViewModels.Evaluation;

namespace Agent.Workspace.ViewModels
{
    public class EvaluationDataViewModel : VehicleSelectionViewModel, IDisposable
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private EvaluationRootFolderViewModel rootFolder;

        public EvaluationDataViewModel()
            : base()
        {
            worker.DoWork += AssembleEvaluationDataAsync;
            worker.WorkerReportsProgress = false;
            worker.WorkerSupportsCancellation = false;
            worker.RunWorkerCompleted += OnEvaluationDataReady;
        }

        public ObservableCollection<ViewModelBase> RootFolder
        {
            get
            {
                if (rootFolder == null)
                {
                    return null;
                }
                return rootFolder.Children;
            }
        }

        protected override void OnVehicleSelected()
        {
            worker.RunWorkerAsync();
        }

        private void OnEvaluationDataReady(object sender, RunWorkerCompletedEventArgs e)
        {
            EvaluationRootFolderViewModel root =
                e.Result as EvaluationRootFolderViewModel;
            rootFolder = root;
            Dispatcher.CurrentDispatcher.Invoke(new ThreadStart(() => OnPropertyChanged("RootFolder")));
            Dispatcher.CurrentDispatcher.Invoke(new ThreadStart(StopWaiting));
        }

        private void AssembleEvaluationDataAsync(object sender, DoWorkEventArgs e)
        {
            EvaluationRootFolderViewModel result = 
                new EvaluationRootFolderViewModel(SelectedVehicle);
            e.Result = result;
        }

        public void Dispose()
        {
            worker.Dispose();
        }
    }
}

using System;
using System.Collections.ObjectModel;
using Agent.Common.Presentation;
using Agent.Localization;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.ViewModels
{
    public class PsaDatasetViewModel : ViewModelBase
    {
        private readonly PsaDataset model;
        private readonly ObservableCollection<PsaTraceViewModel> traces = 
            new ObservableCollection<PsaTraceViewModel>();

        public PsaDatasetViewModel(PsaDataset model)
        {
            this.model = model;
            foreach (PsaTrace trace in model.Traces)
            {
                traces.Add(new PsaTraceViewModel(trace));
            }
        }

        public string Summary
        {
            get
            {
                string format = CodeBehindStringResolver.Resolve("DatasetSummaryFormat");
                return String.Format(format, model.SavedDate.ToLongDateString());
            }
        }

        public ObservableCollection<PsaTraceViewModel> Traces
        {
            get
            {
                return traces;
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("Summary");
            base.ChangeLanguage();
        }
    }
}

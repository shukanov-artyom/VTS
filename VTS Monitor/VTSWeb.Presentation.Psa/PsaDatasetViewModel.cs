using System;
using System.Collections.ObjectModel;
using System.Threading;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Psa
{
    public class PsaDatasetViewModel : ViewModelBase
    {
        private PsaDataset model;
        private ObservableCollection<PsaTraceViewModel> traces;

        public PsaDatasetViewModel(PsaDataset model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            traces = new ObservableCollection<PsaTraceViewModel>();
            foreach (PsaTrace trace in model.Traces)
            {
                traces.Add(new PsaTraceViewModel(trace));
            }
        }

        public DateTime ExportedDate
        {
            get
            {
                return model.ExportedDate;
            }
        }

        public string ExportedDateString
        {
            get
            {
                string result = String.Format("{0} {1}", 
                    CodeBehindStringResolver.Resolve("UploadedWord"),
                    model.ExportedDate.ToLongDateString());
                return result;
            }
        }

        public ObservableCollection<PsaTraceViewModel> Traces
        {
            get
            {
                return traces;
            }
        }

        public PsaDataset Model
        {
            get
            {
                return model;
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("ExportedDateString");
            base.ChangeLanguage();
        }
    }
}

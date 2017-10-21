using System;
using System.Collections.ObjectModel;
using Agent.Common.Presentation;
using Agent.Localization;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.ViewModels
{
    public class PsaTraceViewModel : ViewModelBase
    {
        private readonly PsaTrace model;
        private ObservableCollection<PsaParametersSetViewModel> parameterSets = 
            new ObservableCollection<PsaParametersSetViewModel>();

        public PsaTraceViewModel(PsaTrace model)
        {
            this.model = model;
            foreach (PsaParametersSet parametersSet in model.ParametersSets)
            {
                parameterSets.Add(new PsaParametersSetViewModel(parametersSet));
            }
        }

        public string Summary
        {
            get
            {
                string format = CodeBehindStringResolver.Resolve("TraceSummaryFormat");
                return String.Format(format, model.Date.ToLongDateString());
            }
        }

        public ObservableCollection<PsaParametersSetViewModel> ParameterSets
        {
            get
            {
                return parameterSets;
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("Summary");
            base.ChangeLanguage();
        }
    }
}

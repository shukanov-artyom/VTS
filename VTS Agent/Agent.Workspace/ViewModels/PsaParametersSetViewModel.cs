using System;
using System.Collections.ObjectModel;
using Agent.Common.Presentation;
using Agent.Common.Presentation.Data;
using Agent.Localization;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.ViewModels
{
    public class PsaParametersSetViewModel : ViewModelBase, IPsaParametersSetViewModel
    {
        private readonly PsaParametersSet model;
        private readonly ObservableCollection<PsaParameterDataViewModel> parameters =
            new ObservableCollection<PsaParameterDataViewModel>();

        public PsaParametersSetViewModel(PsaParametersSet model)
        {
            this.model = model;
            foreach (PsaParameterData parameter in model.Parameters)
            {
                parameters.Add(new PsaParameterDataViewModel(parameter));
            }
        }

        public string Summary
        {
            get
            {
                if (!String.IsNullOrEmpty(model.EcuLabel) && !String.IsNullOrEmpty(model.EcuName))
                {
                    return String.Format("{0} ({1})", model.EcuName, model.EcuLabel);
                }
                return CodeBehindStringResolver.Resolve(model.Type.ToString());
            }
        }

        public ObservableCollection<PsaParameterDataViewModel> Parameters
        {
            get
            {
                return parameters;
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("Summary");
            base.ChangeLanguage();
        }
    }
}

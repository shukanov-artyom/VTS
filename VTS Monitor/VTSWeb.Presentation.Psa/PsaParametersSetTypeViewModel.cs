using System;
using VTS.Shared.DomainObjects;
using VTSWeb.Localization;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.Psa
{
    public class PsaParametersSetTypeViewModel : ViewModelBase
    {
        private PsaParametersSetType model;

        public PsaParametersSetTypeViewModel(PsaParametersSetType model)
        {
            this.model = model;
        }

        public string Name
        {
            get
            {
                return CodeBehindStringResolver.Resolve(model.ToString());
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("Name");
            base.ChangeLanguage();
        } 
    }
}

using System;
using VTS.Shared;

using VTSWeb.Localization;

namespace VTSWeb.Presentation.Common
{
    public class UnitsViewModel : ViewModelBase
    {
        private Unit model;

        public UnitsViewModel(Unit model)
        {
            this.model = model;
        }

        public string MeasureUnits
        {
            get
            {
                string key = String.Format("Units{0}", model);
                return CodeBehindStringResolver.Resolve(key);
            }
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("MeasureUnits");
            base.ChangeLanguage();
        } 
    }
}

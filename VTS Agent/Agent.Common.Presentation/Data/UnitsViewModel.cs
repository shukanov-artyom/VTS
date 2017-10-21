using System;
using Agent.Localization;
using VTS.Shared;

namespace Agent.Common.Presentation.Data
{
    public class UnitsViewModel : ViewModelBase
    {
        private readonly Unit model;

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

        public override string ToString()
        {
            return MeasureUnits;
        }

        protected override void ChangeLanguage()
        {
            OnPropertyChanged("MeasureUnits");
            base.ChangeLanguage();
        }
    }
}

using System;
using VTS.Shared.DomainObjects;
using VTSWeb.Presentation.Common;

namespace VTSWeb.Presentation.DomainObjects
{
    public class DomainObjectViewModel : ViewModelBase
    {
        private DomainObject model;

        public DomainObjectViewModel(DomainObject model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
        }

        public long Id
        {
            get
            {
                return model.Id;
            }
        }
    }
}

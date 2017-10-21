using System;
using VTSWeb.DomainObjects;

namespace VTSWeb.Presentation.DomainObjects
{
    public class PartnerViewModel : UserViewModel
    {
        private Partner model;

        public PartnerViewModel(Partner model)
            : base(model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
        }

        public string OrgName
        {
            get
            {
                return model.OrgName;
            }
            set
            {
                model.OrgName = value;
                OnPropertyChanged("OrgName");
            }
        }

        public string Address
        {
            get
            {
                return model.Address;
            }
            set
            {
                model.Address = value;
                OnPropertyChanged("Address");
            }
        }

        public string ContactPerson
        {
            get
            {
                return String.Format("{0} {1}", model.Name, model.Surname);
            }
        }

        public Partner Model
        {
            get
            {
                return model;
            }
        }
    }
}

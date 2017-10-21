using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;
using VTSWeb.Presentation.Common;
using VTSWeb.Presentation.Psa.Interfaces;

namespace VTSWeb.Presentation.Psa
{
    public class PsaParametersSetViewModel : ViewModelBase, 
        IPsaParametersSetViewModel
    {
        private PsaParametersSet model;
        private PsaParametersSetTypeViewModel type;
        private ObservableCollection<PsaParameterDataViewModel> parameters =
            new ObservableCollection<PsaParameterDataViewModel>();

        public PsaParametersSetViewModel(PsaParametersSet model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            this.model = model;
            type = new PsaParametersSetTypeViewModel(model.Type);
            foreach (PsaParameterData param in model.Parameters)
            {
                Parameters.Add(new PsaParameterDataViewModel(param));
            }
        }

        public PsaParametersSetTypeViewModel Type
        {
            get
            {
                return type;
            }
        }

        public ObservableCollection<PsaParameterDataViewModel> Parameters
        {
            get
            {
                return parameters;
            }
        }

        public ObservableCollection<IPsaParameterDataViewModel> Params
        {
            get
            {
                return new ObservableCollection<
                    IPsaParameterDataViewModel>(parameters);
            }
        }

        public PsaParametersSet Model
        {
            get
            {
                return model;
            }
        }
    }
}

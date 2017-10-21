using System;
using System.Collections.ObjectModel;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;

namespace VTSWeb.Presentation.Psa.Interfaces
{
    public interface IPsaParametersSetViewModel
    {
        PsaParametersSetTypeViewModel Type
        {
            get;
        }

        ObservableCollection<IPsaParameterDataViewModel> Params
        {
            get;
        }

        PsaParametersSet Model
        {
            get;
        }
    }
}

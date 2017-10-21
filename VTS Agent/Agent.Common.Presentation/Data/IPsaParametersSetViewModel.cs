using System;
using System.Collections.ObjectModel;

namespace Agent.Common.Presentation.Data
{
    public interface IPsaParametersSetViewModel
    {
        ObservableCollection<PsaParameterDataViewModel> Parameters { get; }
    }
}

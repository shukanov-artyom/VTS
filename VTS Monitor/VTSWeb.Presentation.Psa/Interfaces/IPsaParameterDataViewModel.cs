using System;
using System.Collections.Generic;
using VTS.Shared.DomainObjects;
using VTSWeb.DomainObjects.Psa;

namespace VTSWeb.Presentation.Psa.Interfaces
{
    public interface IPsaParameterDataViewModel
    {
        PsaParameterTypeViewModel Type { get; }

        PsaParameterData Model { get; }

        bool HasTimestamps { get; }

        IList<double> Values { get; }
    }
}

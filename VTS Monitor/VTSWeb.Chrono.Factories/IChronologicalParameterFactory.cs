using System;
using VTS.Shared.DomainObjects;

namespace VTSWeb.Chrono.Factories
{
    public interface IChronologicalParameterFactory
    {
        /// <summary>
        /// Determines whether this factory can produce any result from the provided data.
        /// </summary>
        /// <returns></returns>
        bool CanGenerateFrom(DomainObject source);

        /// <summary>
        /// Creates the data according to existing raw data.
        /// </summary>
        /// <returns></returns>
        void PickUpFrom(DomainObject source);

        /// <summary>
        /// Get resulting parameter data after all processing has been completed
        /// </summary>
        /// <returns>Data assembled from all PickUpFrom() calls</returns>
        IChronologicalParameter GetResult();
    }
}

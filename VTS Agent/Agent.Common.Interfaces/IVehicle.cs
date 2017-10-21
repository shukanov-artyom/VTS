using System;
using VTS.Shared;

namespace Common.Interfaces
{
    public interface IVehicle
    {
        /// <summary>
        /// Vehicle Vin code.
        /// </summary>
        string Vin
        {
            get;
            set;
        }

        /// <summary>
        /// Vehicle Manufacturer.
        /// </summary>
        Manufacturer Manufacturer
        {
            get;
            set;
        }

        /// <summary>
        /// Vehicle model.
        /// </summary>
        string Model
        {
            get;
            set;
        }

        int Mileage
        {
            get; 
            set;
        }
    }
}

using System;
using VTS.Shared.DomainObjects;

namespace VTSWeb.Presentation.Common.Vehicles
{
    public class VehicleViewModelFactory
    {
        public VehicleViewModel Create(Vehicle veh)
        {
            if (veh is Vehicle)
            {
                return new VehicleViewModel(veh);
            }
            throw new NotImplementedException();
        }
    }
}
